using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AngelScript;

public unsafe class ScriptFunction : IDisposable {
	public asScriptFunction* Handle;
	public static implicit operator asScriptFunction*(ScriptFunction c) => c.Handle;

	internal ScriptFunction(asScriptFunction* type) {
		Handle = type;
	}
	
	public static ScriptFunction FromPtr(asScriptFunction* ctx, bool useUserdata = true, bool createUserdata = false) {
		if (!useUserdata)
			return new ScriptFunction(ctx);
		var userData = ScriptFunction_GetUserData(ctx, 2000);
		if (userData is null) {
			if (!createUserdata)
				throw new NullReferenceException("Provided pointer have not been instantiated in managed realm");
			var scriptContext = new ScriptFunction(ctx);
			var handle = GCHandle.Alloc(scriptContext, GCHandleType.Normal);
			ScriptFunction_SetUserData(ctx, (void*)GCHandle.ToIntPtr(handle), 2000);
			return scriptContext;
		}
		var handle1 = GCHandle.FromIntPtr((IntPtr)userData);
		if (handle1.Target is not ScriptFunction ctx2)
			throw new ArgumentException("A userdata 2000 is occupied by something different than ScriptFunction instance");
		return ctx2;
	}
	
	public ScriptEngine Engine => ScriptEngine.FromPtr(ScriptFunction_GetEngine(this));

	#region Memory management
	/// <summary>
	/// Increases the reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>Call this method when storing an additional reference to the object</remarks>
	internal int AddRef() => ScriptFunction_AddRef(this);
	/// <summary>
	/// Decrease reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>Call this method when you will no longer use the references that you own</remarks>
	internal int Release() => ScriptFunction_Release(this);

	public void Dispose() {
		GC.SuppressFinalize(this);
		Release();
	}
	#endregion
	#region Miscellaneous
	/// <summary>
	/// The id of the function. 
	/// </summary>
	/// <remarks>
	///	The id is always positive and larger than 0 for actual functions, and 0 for delegates. 
	/// </remarks>
	public int Id => ScriptFunction_GetId(this);
	/// <summary>
	/// The type of the function 
	/// </summary>
	/// <returns>The type of the function</returns>
	public FunctionType GetFuncType() => (FunctionType)ScriptFunction_GetFuncType(this);
	internal byte* GetModuleNameRaw() => (byte*)ScriptFunction_GetModuleName(this);
	/// <summary>
	/// The name of the module where the function was implemented. 
	/// </summary>
	/// <returns>The name of the module where the function was implemented</returns>
	public string? GetModuleName()
		=> Util.ConvertPtrToString(GetModuleNameRaw());
	/// <summary>
	/// The module where the function is declared. 
	/// </summary>
	public asScriptModule* Module => ScriptFunction_GetModule(this);

	/// <summary>
	/// Returns the name of the config group in which the function was registered. 
	/// </summary>
	/// <returns>The name of the config group in which the function was registered. </returns>
	/// <exception cref="NotImplementedException">Not implemented (Missing Bindings)</exception>
	[DoesNotReturn]
	public string GetScriptSectionName() => throw new NotImplementedException();
	/// <summary>
	/// Returns the name of the config group in which the function was registered. 
	/// </summary>
	/// <returns>The name of the config group in which the function was registered.</returns>
	public string? GetConfigGroup() => Util.ConvertPtrToString((byte*)ScriptFunction_GetConfigGroup(this));
	/// <summary>
	/// Returns the access mask of the function.
	/// </summary>
	/// <returns>The access mask of the function.</returns>
	public asDWORD GetAccessMask() => ScriptFunction_GetAccessMask(this);
	/// <summary>
	/// Returns the auxiliary object registered with the function. 
	/// </summary>
	/// <returns>The auxiliary object registered with the function.</returns>
	public IntPtr GetAuxiliary() => (IntPtr)ScriptFunction_GetAuxiliary(this);
	#endregion
	#region Function signature
	/// <summary>
	/// Returns the object type for class or interface method. 
	/// </summary>
	/// <returns>A pointer to the object type interface if this is a method.</returns>
	/// <remarks>This does not increase the reference count of the returned object type. </remarks>
	public asTypeInfo* GetObjectType() => ScriptFunction_GetObjectType(this);
	/// <summary>
	/// Returns the name of the object for class or interface methods. 
	/// </summary>
	/// <returns>A string with the name of the object type if this a method.</returns>
	public string? GetObjectName() => Util.ConvertPtrToString((byte*)ScriptFunction_GetObjectName(this));
	/// <summary>
	/// Returns the name of the function or method. 
	/// </summary>
	/// <returns>A string with the name of the function.</returns>
	public string? GetName() => Util.ConvertPtrToString((byte*)ScriptFunction_GetName(this));
	/// <summary>
	/// Returns the namespace of the function. 
	/// </summary>
	/// <returns>The namespace of the function, or null if not defined.</returns>
	public string? GetNamespace() => Util.ConvertPtrToString((byte*)ScriptFunction_GetNamespace(this));
	internal byte* GetDeclarationRaw(bool includeObjectName = true, bool includeNamespace = false, bool includeParamNames = false) 
		=> (byte*)ScriptFunction_GetDeclaration(this);
	/// <summary>
	/// Returns the function declaration. 
	/// </summary>
	/// <param name="includeObjectName">Whether the object name should be prepended to the function name</param>
	/// <param name="includeNamespace">Whether the namespace should be prepended to the function name and types</param>
	/// <param name="includeParamNames">Whether parameter names should be added to the declaration</param>
	/// <returns>A string with the function declaration.</returns>
	/// <remarks>
	///	<p>
	/// The parameter names are not stored for virtual methods. If you want to know the name of parameters to class
	/// methods, be sure to get the actual implementation rather than the virtual method.
	///	</p>
	/// <p>
	/// The namespace will always be included for types that are declared in a different namespace than the function
	/// itself. 
	///	</p>
	/// </remarks>
	public string? GetDeclaration(bool includeObjectName = true, bool includeNamespace = false, bool includeParamNames = false) 
		=> Util.ConvertPtrToString(GetDeclarationRaw(includeObjectName, includeNamespace, includeParamNames));
	/// <summary>
	/// Returns true if the class method is read-only. 
	/// </summary>
	public bool IsReadOnly => ScriptFunction_IsReadOnly(this);
	/// <summary>
	/// Returns true if the class method is private. 
	/// </summary>
	public bool IsPrivate => ScriptFunction_IsPrivate(this);
	/// <summary>
	/// Returns true if the class method is protected. 
	/// </summary>
	public bool IsProtected => ScriptFunction_IsProtected(this);
	/// <summary>
	/// Returns true if the method is final. 
	/// </summary>
	public bool IsFinal => ScriptFunction_IsFinal(this);
	/// <summary>
	/// Returns true if the method is meant to override a method in the base class.
	/// </summary>
	public bool IsOverride => ScriptFunction_IsOverride(this);
	/// <summary>
	/// Returns true if the function is shared. 
	/// </summary>
	public bool IsShared => ScriptFunction_IsShared(this);
	/// <summary>
	/// Returns true if the function is declared as 'explicit'.
	/// </summary>
	public bool IsExplicit => ScriptFunction_IsExplicit(this);
	/// <summary>
	/// Returns true if the function is declared as 'property'.
	/// </summary>
	public bool IsProperty => ScriptFunction_IsProperty(this);
	/// <summary>
	/// Returns true if the function has variadic arguments.
	/// </summary>
	public bool IsVariadic => ScriptFunction_IsVariadic(this);
	/// <summary>
	/// Returns the number of parameters for this function. 
	/// </summary>
	public asUINT ParamCount => ScriptFunction_GetParamCount(this);
	
	internal RetCode GetParam(asUINT index, int* typeId, TypeModifiers* flags = null, byte** name = null, byte** defaultArg = null) 
		=> (RetCode)ScriptFunction_GetParam(this, index, typeId, (asDWORD*)flags, (sbyte**)name, (sbyte**)defaultArg);
	
	/// <summary>
	/// Returns the type id of the specified parameter
	/// </summary>
	/// <param name="index">The zero based parameter index.</param>
	/// <param name="flags">A combination of <see cref="TypeModifiers"/></param>
	/// <param name="name">The name of the parameter (or null if not defined).</param>
	/// <param name="defaultArg">The default argument expression (or null if not defined).</param>
	/// <returns>The TypeId of the parameter.</returns>
	/// <exception cref="ArgumentException">The index is out of bounds</exception>
	/// <remarks>
	///	The parameter names are not stored for virtual methods.
	/// If you want to know the name of parameters to class methods,
	/// be sure to get the actual implementation rather than the virtual method
	/// </remarks>
	public int GetParam(asUINT index, out TypeModifiers flags, out string? name, out string? defaultArg) {
		var typeId = 0;
		flags = TypeModifiers.None;
		name = null;
		defaultArg = null;
		byte* namePtr = null;
		byte* defaultArgPtr = null;
		var result = GetParam(index, &typeId, (TypeModifiers*)Unsafe.AsPointer(ref flags), &namePtr, &defaultArgPtr);
		if (result < 0)
			switch (result) {
				case RetCode.InvalidArg: throw new ArgumentException("The index is out of bounds");
				default: throw result.GetException();
			}
		name = Util.ConvertPtrToString(namePtr);
		defaultArg = Util.ConvertPtrToString(defaultArgPtr);
		return typeId;
	}

	/// <summary>
	/// Returns the type id of the return type. 
	/// </summary>
	/// <param name="flags"></param>
	/// <returns>The type id of the return type.</returns>
	public int GetReturnTypeId(out TypeModifiers flags) {
		flags = 0;
		return ScriptFunction_GetReturnTypeId(this, (uint*)Unsafe.AsPointer(ref flags));
	}
	#endregion
	#region Template functions
	/// <summary>
	/// The number of template sub types. 
	/// </summary>
	public asUINT SubTypeCount => ScriptFunction_GetSubTypeCount(this);
	/// <summary>
	/// Returns the type id of a template sub type. 
	/// </summary>
	/// <param name="subTypeIndex"></param>
	/// <returns></returns>
	public int GetSubTypeId(asUINT subTypeIndex = 0) => ScriptFunction_GetSubTypeId(this, subTypeIndex);
	/// <summary>
	/// Returns the type info for a template sub type. 
	/// </summary>
	/// <param name="subTypeIndex"></param>
	/// <returns></returns>
	public asTypeInfo* GetSubType(asUINT subTypeIndex = 0) => ScriptFunction_GetSubType(this, subTypeIndex);
	#endregion
	#region Type id for function pointers
	/// <summary>
	/// Type id representing a function pointer for this function. 
	/// </summary>
	public int TypeId => ScriptFunction_GetTypeId(this);
	/// <summary>
	/// Checks if the given type id can represent this function. 
	/// </summary>
	/// <param name="typeId"></param>
	/// <returns></returns>
	public bool IsCompatibleWithTypeId(int typeId) => ScriptFunction_IsCompatibleWithTypeId(this, typeId);
	#endregion
	#region Delegates
	/// <summary>
	/// Returns the object for the delegate. 
	/// </summary>
	/// <returns>A pointer to the delegated object</returns>
	public void* GetDelegateObject() => ScriptFunction_GetDelegateObject(this);
	/// <summary>
	/// Returns the type of the delegated object. 
	/// </summary>
	/// <returns>A pointer to the object type of the delegated object.</returns>
	public TypeInfo GetDelegateObjectType() => TypeInfo.FromPtr(ScriptFunction_GetDelegateObjectType(this));
	/// <summary>
	/// Returns the function for the delegate. 
	/// </summary>
	/// <returns>A pointer to the delegated function </returns>
	public ScriptFunction GetDelegateFunction() => FromPtr(ScriptFunction_GetDelegateFunction(this));
	#endregion
	#region Debug information
	/// <summary>
	/// Returns the number of local variables in the function. 
	/// </summary>
	public asUINT VarCount => ScriptFunction_GetVarCount(this);
	/// <summary>
	/// Returns information about a local variable. 
	/// </summary>
	/// <param name="index"></param>
	/// <param name="name"></param>
	/// <param name="typeId"></param>
	/// <returns></returns>
	public int GetVar(asUINT index, byte** name, int* typeId = null) => ScriptFunction_GetVar(this, index, (sbyte**)name, typeId);
	/// <summary>
	/// Returns the declaration of a local variable. 
	/// </summary>
	/// <param name="index"></param>
	/// <param name="includeNamespace"></param>
	/// <returns></returns>
	public byte* GetVarDecl(asUINT index, bool includeNamespace = false) => (byte*)ScriptFunction_GetVarDecl(this, index, includeNamespace);
	/// <summary>
	/// Returns the next line number with code. 
	/// </summary>
	/// <param name="line"></param>
	/// <returns></returns>
	public int FindNextLineWithCode(int line) => ScriptFunction_FindNextLineWithCode(this, line);
	/// <summary>
	/// Returns the location in the script where the function was declared. 
	/// </summary>
	/// <param name="scriptSection"></param>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <returns></returns>
	public int GetDeclaredAt(byte** scriptSection, int* row, int* col) => ScriptFunction_GetDeclaredAt(this, (sbyte**)scriptSection, row, col);
	public int GetLineEntryCount() => ScriptFunction_GetLineEntryCount(this);
	public int GetLineEntry(asUINT index, int* row, int* col, byte** sectionName, asDWORD** byteCode) 
		=> ScriptFunction_GetLineEntry(this, index, row, col, (sbyte**)sectionName, byteCode);
	#endregion
	#region For JIT compilation
	/// <summary>
	/// Returns the byte code buffer and length. 
	/// </summary>
	/// <param name="length">The length of the byte code buffer in DWORDs </param>
	/// <returns>A pointer to the byte code buffer, or 0 if this is not a script function.</returns>
	/// <remarks>
	/// This function is used by the asIJITCompiler to obtain the byte code buffer for building the native machine code
	/// representation.
	/// </remarks>
	public asDWORD* GetByteCode(asUINT* length = null) => ScriptFunction_GetByteCode(this, length);
	/// <summary>
	/// Link the script function with a JIT compiled function.
	/// </summary>
	/// <param name="jitFunc"></param>
	/// <returns></returns>
	public int SetJITFunction(IntPtr jitFunc) => ScriptFunction_SetJITFunction(this, jitFunc);
	/// <summary>
	/// Returns the linked JIT compiled function. 
	/// </summary>
	/// <returns>A pointer to the JIT function, or 0 if there is none.</returns>
	public IntPtr GetJITFunction() => ScriptFunction_GetJITFunction(this);
	#endregion
	#region User data
	/// <summary>
	/// Register the memory address of some user data. 
	/// </summary>
	/// <param name="userData"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public IntPtr SetUserDataPtr(IntPtr userData, asPWORD type = 0) => (IntPtr)ScriptFunction_SetUserData(this, (void*)userData, type);
	/// <summary>
	/// Returns the address of the previously registered user data. 
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public IntPtr GetUserDataPtr(asPWORD type = 0) => (IntPtr)ScriptFunction_GetUserData(this, type);
	
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