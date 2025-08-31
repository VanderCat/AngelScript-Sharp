using System.Runtime.InteropServices;

namespace AngelScript;

public unsafe class TypeInfo : IDisposable {
	public asTypeInfo* Handle;
	public static implicit operator asTypeInfo*(TypeInfo c) => c.Handle;

	internal TypeInfo(asTypeInfo* type) {
		Handle = type;
	}
	
	public static TypeInfo FromPtr(asTypeInfo* ctx, bool useUserdata = true, bool createUserdata = false) {
		if (!useUserdata)
			return new TypeInfo(ctx);
		var userData = TypeInfo_GetUserData(ctx, 2000);
		if (userData is null) {
			if (!createUserdata)
				throw new NullReferenceException("Provided pointer have not been instantiated in managed realm");
			var scriptContext = new TypeInfo(ctx);
			var handle = GCHandle.Alloc(scriptContext, GCHandleType.Normal);
			TypeInfo_SetUserData(ctx, (void*)GCHandle.ToIntPtr(handle), 2000);
			return scriptContext;
		}
		var handle1 = GCHandle.FromIntPtr((IntPtr)userData);
		if (handle1.Target is not TypeInfo ctx2)
			throw new ArgumentException("A userdata 2000 is occupied by something different than TypeInfo instance");
		return ctx2;
	}
	
    #region Miscellaneous
    public ScriptEngine Engine => ScriptEngine.FromPtr(TypeInfo_GetEngine(this));
    public byte* GetConfigGroup() => (byte*)TypeInfo_GetConfigGroup(this);
    public asDWORD GetAccessMask() => TypeInfo_GetAccessMask(this);
    public asScriptModule* GetModule() => TypeInfo_GetModule(this);
    #endregion
	#region Memory management
	/// <summary>
	/// Increases the reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>Call this method when storing an additional reference to the object</remarks>
	internal int AddRef() => TypeInfo_AddRef(this);
	
	/// <summary>
	/// Decrease reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>Call this method when you will no longer use the references that you own</remarks>
	internal int Release() => TypeInfo_Release(this);

	internal int ReferenceCount {
		get {
			var num = AddRef();
			var num2 = Release();
			return num2;
		}
	}

	public void Dispose() {
		Release();
	}
	#endregion
	#region Type info
	public byte* GetName() => (byte*)TypeInfo_GetName(this);
	public byte* GetNamespace() => (byte*)TypeInfo_GetNamespace(this);
	public asTypeInfo* GetBaseType() => TypeInfo_GetBaseType(this);
	public bool DerivesFrom(asTypeInfo* objType) => TypeInfo_DerivesFrom(this, objType);
	public asQWORD GetFlags() => TypeInfo_GetFlags(this);
	public asUINT GetSize() => TypeInfo_GetSize(this);
	public int GetTypeId() => TypeInfo_GetTypeId(this);
	public int GetSubTypeId(asUINT subTypeIndex = 0) => TypeInfo_GetSubTypeId(this, subTypeIndex);
	public asTypeInfo* GetSubType(asUINT subTypeIndex = 0) => TypeInfo_GetSubType(this, subTypeIndex);
	public asUINT SubTypeCount => TypeInfo_GetSubTypeCount(this);
	#endregion
	#region Interfaces
	public asUINT InterfaceCount => TypeInfo_GetInterfaceCount(this);
	public asTypeInfo* GetInterface(asUINT index) => TypeInfo_GetInterface(this, index);
	public bool Implements(asTypeInfo* objType) => TypeInfo_Implements(this, objType);
	#endregion
	#region Factories
	public asUINT FactoryCount => TypeInfo_GetFactoryCount(this);
	public asScriptFunction* GetFactoryByIndex(asUINT index) => TypeInfo_GetFactoryByIndex(this, index);
	public asScriptFunction* GetFactoryByDecl(byte* decl) => TypeInfo_GetFactoryByDecl(this, (sbyte*)decl);
	#endregion
	#region Methods
	public asUINT MethodCount => TypeInfo_GetMethodCount(this);
	public asScriptFunction* GetMethodByIndex(asUINT index, bool getVirtual = true) => TypeInfo_GetMethodByIndex(this, index, getVirtual);
	public asScriptFunction* GetMethodByName(byte* name, bool getVirtual = true) => TypeInfo_GetMethodByName(this, (sbyte*)name, getVirtual);
	public asScriptFunction* GetMethodByDecl(byte* decl, bool getVirtual = true) => TypeInfo_GetMethodByDecl(this, (sbyte*)decl, getVirtual);
	#endregion
	#region Properties
	public asUINT PropertyCount => TypeInfo_GetPropertyCount(this);
	public int GetProperty(asUINT index, byte** name, int* typeId = null, bool* isPrivate = null, bool* isProtected = null, int* offset = null, bool* isReference = null, asDWORD* accessMask = null, int* compositeOffset = null, bool* isCompositeIndirect = null, bool* isConst = null) =>
		TypeInfo_GetProperty(this, index, (sbyte**)name, typeId, isPrivate,isProtected, offset, isReference, accessMask, compositeOffset, isCompositeIndirect, isConst);
	public byte* GetPropertyDeclaration(asUINT index, bool includeNamespace = false) => (byte*)TypeInfo_GetPropertyDeclaration(this, index, includeNamespace);
	#endregion
	#region Behaviours
	public asUINT GetBehaviourCount() => TypeInfo_GetBehaviourCount(this);
	public asScriptFunction* GetBehaviourByIndex(asUINT index, asEBehaviours* outBehaviour) => TypeInfo_GetBehaviourByIndex(this, index, outBehaviour);
	#endregion
	#region Child types
	public asUINT ChildFuncdefCount => TypeInfo_GetChildFuncdefCount(this);
	public asTypeInfo* GetChildFuncdef(asUINT index) => TypeInfo_GetChildFuncdef(this, index);
	public asTypeInfo* GetParentType() => TypeInfo_GetParentType(this);
	#endregion
	#region Enums
	public asUINT EnumValueCount => TypeInfo_GetEnumValueCount(this);
	public byte* GetEnumValueByIndex(asUINT index, int* outValue) => (byte*)TypeInfo_GetEnumValueByIndex(this, index, outValue);
	#endregion
	#region Typedef
	public int GetTypedefTypeId() => TypeInfo_GetTypedefTypeId(this);
	#endregion
	#region Funcdef
	public asScriptFunction* GetFuncdefSignature() => TypeInfo_GetFuncdefSignature(this);
	#endregion
	#region User data
	public IntPtr SetUserDataPtr(IntPtr data, asPWORD type = 0) => (IntPtr)TypeInfo_SetUserData(this, (void*)data, type);
	public IntPtr GetUserDataPtr(asPWORD type = 0) => (IntPtr)TypeInfo_GetUserData(this, type);
	
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