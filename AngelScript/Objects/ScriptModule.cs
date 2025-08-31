using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

public unsafe class ScriptModule {
	public asScriptModule* Handle;
	public static implicit operator asScriptModule*(ScriptModule c) => c.Handle;

	internal ScriptModule(asScriptModule* module) {
		Handle = module;
	}
	
	public static ScriptModule FromPtr(asScriptModule* ctx, bool useUserdata = true, bool createUserdata = false) {
		if (!useUserdata)
			return new ScriptModule(ctx);
		var userData = ScriptModule_GetUserData(ctx, 2000);
		if (userData is null) {
			if (!createUserdata)
				throw new NullReferenceException("Provided pointer have not been instantiated in managed realm");
			var scriptContext = new ScriptModule(ctx);
			var handle = GCHandle.Alloc(scriptContext, GCHandleType.Normal);
			ScriptModule_SetUserData(ctx, (void*)GCHandle.ToIntPtr(handle), 2000);
			return scriptContext;
		}
		var handle1 = GCHandle.FromIntPtr((IntPtr)userData);
		if (handle1.Target is not ScriptModule ctx2)
			throw new ArgumentException("A userdata 2000 is occupied by something different than ScriptModule instance");
		return ctx2;
	}

	public ScriptEngine Engine => ScriptEngine.FromPtr(ScriptModule_GetEngine(this));
	internal void SetName(byte* name) => ScriptModule_SetName(this, (sbyte*)name);
	internal byte* GetName() => (byte*)ScriptModule_GetName(this);

	public string? Name {
		get {
			var ptr = GetName();
			return Util.ConvertPtrToString(ptr);
		}
		set {
			if (value is null) {
				SetName(null);
				return;
			}
			fixed (byte* name = Encoding.UTF8.GetBytes(value + '\0'))
				SetName(name);
		}
	}
	public void Discard() => ScriptModule_Discard(this);

	#region Compilation
	internal int AddScriptSection(byte* name, byte* code, nuint codeLength = 0, int lineOffset = 0) 
		=> ScriptModule_AddScriptSection(this, (sbyte*)name, (sbyte*)code, codeLength, lineOffset);
	
	/// <summary>
	/// Add a script section for the next build
	/// </summary>
	/// <param name="name">The name of the script section</param>
	/// <param name="code">The script code</param>
	/// <param name="lineOffset">An offset that will be added to compiler message line numbers</param>
	/// <remarks>
	///	This adds a script section to the module. The script section isn't processed with this call.
	/// Only when Build is called will the script be parsed and compiled into executable byte code.
	/// <br/><br/>
	/// Error messages from the compiler will refer to the name of the script section and the position within it.
	/// Normally each section is the content of a source file, so it is recommended to name the script sections as the name of the source file.
	/// <br/><br/>
	/// The code added is copied by the engine, so there is no need to keep the original buffer after the call.
	/// Note that this can be changed by setting the engine property asEP_COPY_SCRIPT_SECTIONS with <see cref="ScriptEngine.SetEngineProperty"/>.
	/// This however won't work with high level C# bindings this library provides.
	/// </remarks>
	/// <exception cref="ArgumentException">The <i>code</i> argument is null</exception>
	/// <exception cref="NotSupportedException">Compiler support is disabled in the engine</exception>
	/// <exception cref="OutOfMemoryException">The necessary memory to hold the script code couldn't be allocated</exception>
	public void AddScriptSection(string name, byte[] code, int lineOffset = 0) {
		fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
		fixed (byte* codePtr = code) {
			var result = (RetCode)AddScriptSection(namePtr, codePtr, (nuint)code.Length, lineOffset);
			if (result < 0)
				switch (result) {
					case RetCode.InvalidArg: throw new ArgumentException("The code argument is null", nameof(code));
					case RetCode.NotSupported: throw new NotSupportedException("Compiler support is disabled in the engine");
					case RetCode.OutOfMemory: throw new OutOfMemoryException("The necessary memory to hold the script code couldn't be allocated");
					default: throw result.GetException();
				}
		}
	}
	/// <inheritdoc cref="AddScriptSection(string,byte[],int)"/>
	public void AddScriptSection(string name, string code, int lineOffset = 0) 
		=> AddScriptSection(name, Encoding.UTF8.GetBytes(code), lineOffset);

	/// <summary>
	/// Build the previously added script sections
	/// </summary>
	/// <exception cref="InvalidConfigurationException">The engine configuration is invalid</exception>
	/// <exception cref="BuildFailedException">The script failed to build</exception>
	/// <exception cref="BuildInProgressException">Another thread is currently building</exception>
	/// <exception cref="GlobalVariableInitializationException">
	/// It was not possible to initialize at least one of the global variables.
	/// It is probable that one of the global variables during the initialization is trying
	/// to access another global variable before it has been initialized.
	/// </exception>
	/// <exception cref="NotSupportedException">Compiler support is disabled in the engine</exception>
	/// <exception cref="Exception">The code in the module is still being used and cannot be removed</exception>
	/// <remarks>
	///	Builds the script based on the previously added sections, registered types and functions.
	/// After the build is complete the script sections are removed to free memory.
	/// <br/><br/>
	/// Before starting the build the <see cref="Build"/> method removes any previously compiled script content,
	/// including the dynamically added content from <see cref="CompileFunction"/> and <see cref="CompileGlobalVar"/>.
	/// If the script module needs to be rebuilt all of the script sections needs to be added again.
	/// <br/><br/>
	/// Compiler messages are sent to the message callback function set with <see cref="ScriptEngine.SetMessageCallback"/>.
	/// If there are no errors or warnings, no messages will be sent to the callback function.
	/// <br/><br/>
	/// Any global variables found in the script will be initialized by the compiler if the engine property asEP_INIT_GLOBAL_VARS_AFTER_BUILD is set.
	/// </remarks>
	public void Build() {
		var result = (RetCode)ScriptModule_Build(this);
		if (result < 0) switch (result) {
			case RetCode.InvalidConfiguration: throw new InvalidConfigurationException("The engine configuration is invalid");
			case RetCode.Error: throw new BuildFailedException("The script failed to build");
			case RetCode.BuildInProgress: throw new BuildInProgressException("Another thread is currently building");
			case RetCode.InitGlobalVarsFailed: throw new GlobalVariableInitializationException("It was not possible to initialize at least one of the global variables");
			case RetCode.NotSupported: throw new NotSupportedException("Compiler support is disabled in the engine");
			case RetCode.ModuleIsInUse: throw new Exception("The code in the module is still being used and and cannot be removed."); //TODO: Exception
		}
	}
	public int CompileFunction(byte* sectionName, byte* code, int lineOffset, asDWORD compileFlags, asScriptFunction** outFunc) 
		=> ScriptModule_CompileFunction(this, (sbyte*)sectionName, (sbyte*)code, lineOffset, compileFlags, outFunc);
	public int CompileGlobalVar(byte* sectionName, byte* code, int lineOffset) 
		=> ScriptModule_CompileGlobalVar(this, (sbyte*)sectionName, (sbyte*)code, lineOffset);
	public asDWORD SetAccessMask(asDWORD accessMask) 
		=> ScriptModule_SetAccessMask(this, accessMask);
	public int SetDefaultNamespace(byte* nameSpace) 
		=> ScriptModule_SetDefaultNamespace(this, (sbyte*)nameSpace);
	public byte* GetDefaultNamespace() => (byte*)ScriptModule_GetDefaultNamespace(this);
	#endregion

	#region Functions
	public asUINT GetFunctionCount() => ScriptModule_GetFunctionCount(this);
	public asScriptFunction* GetFunctionByIndex(asUINT index) => ScriptModule_GetFunctionByIndex(this, index);
	internal asScriptFunction* GetFunctionByDeclRaw(byte* decl) => ScriptModule_GetFunctionByDecl(this, (sbyte*)decl);
	public ScriptFunction? GetFunctionByDecl(string decl) {
		fixed (byte* declPtr = Encoding.UTF8.GetBytes(decl + '\0')) {
			var ptr = GetFunctionByDeclRaw(declPtr);
			if (ptr is null)
				return null;
			return ScriptFunction.FromPtr(ptr, true, true);
		}
	}
	public asScriptFunction* GetFunctionByName(byte* name) => ScriptModule_GetFunctionByName(this, (sbyte*)name);
	public int RemoveFunction(asScriptFunction* func) => ScriptModule_RemoveFunction(this, func);
	#endregion

	#region Global variables
	public int ResetGlobalVars(asScriptContext* ctx = null) => ScriptModule_ResetGlobalVars(this, ctx);
	public asUINT GetGlobalVarCount() => ScriptModule_GetGlobalVarCount(this);
	public int GetGlobalVarIndexByName(byte* name) => ScriptModule_GetGlobalVarIndexByName(this, (sbyte*)name);
	public int GetGlobalVarIndexByDecl(byte* decl) => ScriptModule_GetGlobalVarIndexByDecl(this, (sbyte*)decl);
	public byte* GetGlobalVarDeclaration(asUINT index, bool includeNamespace = false) => (byte*)ScriptModule_GetGlobalVarDeclaration(this, index, includeNamespace);
	public int GetGlobalVar(asUINT index, byte** name, byte** nameSpace = null, int* typeId = null, bool* isConst = null) 
		=> ScriptModule_GetGlobalVar(this, index, (sbyte**)name, (sbyte**)nameSpace, typeId, isConst);
	public void* GetAddressOfGlobalVar(asUINT index) => ScriptModule_GetAddressOfGlobalVar(this, index);
	public int RemoveGlobalVar(asUINT index) => ScriptModule_RemoveGlobalVar(this, index);
	#endregion

	#region Type identification
	public asUINT GetObjectTypeCount() => ScriptModule_GetObjectTypeCount(this);
	public asTypeInfo* GetObjectTypeByIndex(asUINT index) => ScriptModule_GetObjectTypeByIndex(this, index);
	public int GetTypeIdByDecl(byte* decl) => ScriptModule_GetTypeIdByDecl(this, (sbyte*)decl);
	public asTypeInfo* GetTypeInfoByName(byte* name) => ScriptModule_GetTypeInfoByName(this, (sbyte*)name);
	public asTypeInfo* GetTypeInfoByDecl(byte* decl) => ScriptModule_GetTypeInfoByDecl(this, (sbyte*)decl);
	#endregion

	#region Enums
	public asUINT GetEnumCount() => ScriptModule_GetEnumCount(this);
	public asTypeInfo* GetEnumByIndex(asUINT index) => ScriptModule_GetEnumByIndex(this, index);
	#endregion

	#region Typedefs
	public asUINT GetTypedefCount() => ScriptModule_GetTypedefCount(this);
	public asTypeInfo* GetTypedefByIndex(asUINT index) => ScriptModule_GetTypedefByIndex(this, index);
	#endregion

	#region Dynamic binding between modules
	public asUINT ImportedFunctionCount => ScriptModule_GetImportedFunctionCount(this);
	public int GetImportedFunctionIndexByDecl(byte* decl) => ScriptModule_GetImportedFunctionIndexByDecl(this, (sbyte*)decl);
	public byte* GetImportedFunctionDeclaration(asUINT importIndex) => (byte*)ScriptModule_GetImportedFunctionDeclaration(this, importIndex);
	public byte* GetImportedFunctionSourceModule(asUINT importIndex) => (byte*)ScriptModule_GetImportedFunctionSourceModule(this, importIndex);
	public int BindImportedFunction(asUINT importIndex, asScriptFunction* func) => ScriptModule_BindImportedFunction(this, importIndex, func);
	public int UnbindImportedFunction(asUINT importIndex) => ScriptModule_UnbindImportedFunction(this, importIndex);
	public int BindAllImportedFunctions() => ScriptModule_BindAllImportedFunctions(this);
	public int UnbindAllImportedFunctions() => ScriptModule_UnbindAllImportedFunctions(this);
	#endregion

	#region Byte code saving and loading
	public int SaveByteCode(asBinaryStream* @out, bool stripDebugInfo = false) => ScriptModule_SaveByteCode(this, @out, stripDebugInfo);
	public int LoadByteCode(asBinaryStream* @in, bool* wasDebugInfoStripped = null) => ScriptModule_LoadByteCode(this, @in, wasDebugInfoStripped);
	#endregion
	
	#region User data
	public IntPtr SetUserDataPtr(IntPtr data, asPWORD type = 0) => (IntPtr)ScriptModule_SetUserData(this, (void*)data, type);
	public IntPtr GetUserDataPtr(asPWORD type = 0) => (IntPtr)ScriptModule_GetUserData(this, type);
	
	private Dictionary<int, object> _managedUserdata = new();

	public void SetUserData(object? obj, int type = 0) {
		if (obj is null) {
			_managedUserdata.Remove(type);
			return;
		}
		_managedUserdata.Add(type, obj);
	}

	public object? GetUserData(int type = 0) {
		_managedUserdata.TryGetValue(type, out var obj);
		return obj;
	}

	public T? GetUserData<T>(int type = 0) => (T?)GetUserData(type);
	#endregion
}