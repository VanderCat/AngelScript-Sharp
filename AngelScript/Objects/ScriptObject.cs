namespace AngelScript;

public unsafe class ScriptObject {
    public asScriptObject* Handle;
    public static implicit operator asScriptObject*(ScriptObject c) => c.Handle;

    internal ScriptObject(asScriptObject* @object) {
        Handle = @object;
    }
    
    #region Memory management
    public int AddRef() => ScriptObject_AddRef(this);
    public int Release() => ScriptObject_Release(this);
    public asLockableSharedBool* GetWeakRefFlag() => ScriptObject_GetWeakRefFlag(this);
    #endregion
    #region Type info
    public int GetTypeId() => ScriptObject_GetTypeId(this);
    public asTypeInfo* GetObjectType() => ScriptObject_GetObjectType(this);
    #endregion
    #region Class properties
    public asUINT GetPropertyCount() => ScriptObject_GetPropertyCount(this);
    public int GetPropertyTypeId(asUINT prop) => ScriptObject_GetPropertyTypeId(this, prop);
    public byte* GetPropertyName(asUINT prop)=> (byte*)ScriptObject_GetPropertyName(this, prop);
    public void* GetAddressOfProperty(asUINT prop) => ScriptObject_GetAddressOfProperty(this, prop);
    #endregion
    #region Miscellaneous
    public asScriptEngine* GetEngine() => ScriptObject_GetEngine(this);
    public int CopyFrom(asScriptObject* other) => ScriptObject_CopyFrom(this, other);
    #endregion
    #region User data
    public void* SetUserData(void* data, asPWORD type = 0) => ScriptObject_SetUserData(this, data, type);
    public void* GetUserData(asPWORD type = 0) => ScriptObject_GetUserData(this, type);
    #endregion
}