namespace AngelScript;

public unsafe class TypeInfo {
	public asTypeInfo* Handle;
	public static implicit operator asTypeInfo*(TypeInfo c) => c.Handle;

	internal TypeInfo(asTypeInfo* type) {
		Handle = type;
	}
	
    #region Miscellaneous
    public asScriptEngine* GetEngine() => TypeInfo_GetEngine(this);
    public byte* GetConfigGroup() => (byte*)TypeInfo_GetConfigGroup(this);
    public asDWORD GetAccessMask() => TypeInfo_GetAccessMask(this);
    public asScriptModule* GetModule() => TypeInfo_GetModule(this);
    #endregion
	#region Memory management
	public int AddRef() => TypeInfo_AddRef(this);
	public int Release() => TypeInfo_Release(this);
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
	public void* SetUserData(void* data, asPWORD type = 0) => TypeInfo_SetUserData(this, data, type);
	public void* GetUserData(asPWORD type = 0) => TypeInfo_GetUserData(this, type);
	#endregion
}