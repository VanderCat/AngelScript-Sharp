using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AngelScript;

public unsafe class ScriptFunction {
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
	public int AddRef() => ScriptFunction_AddRef(this);
	public int Release() => ScriptFunction_Release(this);
	#endregion
	#region Miscellaneous
	public int Id => ScriptFunction_GetId(this);
	public asEFuncType GetFuncType() => ScriptFunction_GetFuncType(this);
	internal byte* GetModuleNameRaw() => (byte*)ScriptFunction_GetModuleName(this);
	public string? GetModuleName()
		=> Util.ConvertPtrToString(GetModuleNameRaw());
	public asScriptModule* GetModule() => ScriptFunction_GetModule(this);
	public byte* GetConfigGroup() => (byte*)ScriptFunction_GetConfigGroup(this);
	public asDWORD GetAccessMask() => ScriptFunction_GetAccessMask(this);
	public void* GetAuxiliary() => ScriptFunction_GetAuxiliary(this);
	#endregion
	#region Function signature
	public asTypeInfo* GetObjectType() => ScriptFunction_GetObjectType(this);
	public byte* GetObjectName() => (byte*)ScriptFunction_GetObjectName(this);
	public byte* GetName() => (byte*)ScriptFunction_GetName(this);
	public byte* GetNamespace() => (byte*)ScriptFunction_GetNamespace(this);
	internal byte* GetDeclarationRaw(bool includeObjectName = true, bool includeNamespace = false, bool includeParamNames = false) 
		=> (byte*)ScriptFunction_GetDeclaration(this);
	public string? GetDeclaration(bool includeObjectName = true, bool includeNamespace = false, bool includeParamNames = false) 
		=> Util.ConvertPtrToString(GetDeclarationRaw(includeObjectName, includeNamespace, includeParamNames));
	public bool IsReadOnly => ScriptFunction_IsReadOnly(this);
	public bool IsPrivate => ScriptFunction_IsPrivate(this);
	public bool IsProtected => ScriptFunction_IsProtected(this);
	public bool IsFinal => ScriptFunction_IsFinal(this);
	public bool IsOverride => ScriptFunction_IsOverride(this);
	public bool IsShared => ScriptFunction_IsShared(this);
	public bool IsExplicit => ScriptFunction_IsExplicit(this);
	public bool IsProperty => ScriptFunction_IsProperty(this);
	public bool IsVariadic => ScriptFunction_IsVariadic(this);
	public asUINT ParamCount => ScriptFunction_GetParamCount(this);
	
	internal RetCode GetParam(asUINT index, int* typeId, TypeModifiers* flags = null, byte** name = null, byte** defaultArg = null) 
		=> (RetCode)ScriptFunction_GetParam(this, index, typeId, (asDWORD*)flags, (sbyte**)name, (sbyte**)defaultArg);
	
	/// <summary>
	/// Returns the type id of the specified parameter
	/// </summary>
	/// <param name="index"></param>
	/// <param name="typeId"></param>
	/// <param name="flags">A combination of asETypeModifiers</param>
	/// <param name="name">The name of the parameter (or null if not defined).</param>
	/// <param name="defaultArg">The default argument expression (or null if not defined).</param>
	/// <exception cref="ArgumentException">The index is out of bounds</exception>
	/// <remarks>
	///	The parameter names are not stored for virtual methods.
	/// If you want to know the name of parameters to class methods,
	/// be sure to get the actual implementation rather than the virtual method
	/// </remarks>
	public void GetParam(asUINT index, out int typeId, out TypeModifiers flags, out string? name, out string? defaultArg) {
		typeId = 0;
		flags = TypeModifiers.None;
		name = null;
		defaultArg = null;
		byte* namePtr = null;
		byte* defaultArgPtr = null;
		var result = GetParam(index, (int*)Unsafe.AsPointer(ref typeId), (TypeModifiers*)Unsafe.AsPointer(ref flags), &namePtr, &defaultArgPtr);
		if (result < 0)
			switch (result) {
				case RetCode.InvalidArg: throw new ArgumentException("The index is out of bounds");
				default: throw result.GetException();
			}
		name = Util.ConvertPtrToString(namePtr);
		name = Util.ConvertPtrToString(defaultArgPtr);
	}
	public int GetReturnTypeId(asDWORD* flags = null) 
		=> ScriptFunction_GetReturnTypeId(this, flags);
	#endregion
	#region Template functions
	public asUINT SubTypeCount => ScriptFunction_GetSubTypeCount(this);
	public int GetSubTypeId(asUINT subTypeIndex = 0) => ScriptFunction_GetSubTypeId(this, subTypeIndex);
	public asTypeInfo* GetSubType(asUINT subTypeIndex = 0) => ScriptFunction_GetSubType(this, subTypeIndex);
	#endregion
	#region Type id for function pointers
	public int GetTypeId() => ScriptFunction_GetTypeId(this);
	public bool IsCompatibleWithTypeId(int typeId) => ScriptFunction_IsCompatibleWithTypeId(this, typeId);
	#endregion
	#region Delegates
	public void* GetDelegateObject() => ScriptFunction_GetDelegateObject(this);
	public asTypeInfo* GetDelegateObjectType() => ScriptFunction_GetDelegateObjectType(this);
	public asScriptFunction* GetDelegateFunction() => ScriptFunction_GetDelegateFunction(this);
	#endregion
	#region Debug information
	public asUINT VarCount => ScriptFunction_GetVarCount(this);
	public int GetVar(asUINT index, byte** name, int* typeId = null) => ScriptFunction_GetVar(this, index, (sbyte**)name, typeId);
	public byte* GetVarDecl(asUINT index, bool includeNamespace = false) => (byte*)ScriptFunction_GetVarDecl(this, index, includeNamespace);
	public int FindNextLineWithCode(int line) => ScriptFunction_FindNextLineWithCode(this, line);
	public int GetDeclaredAt(byte** scriptSection, int* row, int* col) => ScriptFunction_GetDeclaredAt(this, (sbyte**)scriptSection, row, col);
	public int GetLineEntryCount() => ScriptFunction_GetLineEntryCount(this);
	public int GetLineEntry(asUINT index, int* row, int* col, byte** sectionName, asDWORD** byteCode) 
		=> ScriptFunction_GetLineEntry(this, index, row, col, (sbyte**)sectionName, byteCode);
	#endregion
	#region For JIT compilation
	public asDWORD* GetByteCode(asUINT* length = null) => ScriptFunction_GetByteCode(this, length);
	public int SetJITFunction(IntPtr jitFunc) => ScriptFunction_SetJITFunction(this, jitFunc);
	public IntPtr GetJITFunction() => ScriptFunction_GetJITFunction(this);
	#endregion
	#region User data
	public IntPtr SetUserDataPtr(IntPtr userData, asPWORD type = 0) => (IntPtr)ScriptFunction_SetUserData(this, (void*)userData, type);
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