using System.Runtime.InteropServices;

namespace AngelScript;

public unsafe class ScriptObject {
    public asScriptObject* Handle;
    public static implicit operator asScriptObject*(ScriptObject c) => c.Handle;

    internal ScriptObject(asScriptObject* @object) {
        Handle = @object;
    }
    
    public static ScriptObject FromPtr(asScriptObject* ctx, bool useUserdata = true, bool createUserdata = false) {
        if (!useUserdata)
            return new ScriptObject(ctx);
        var userData = ScriptObject_GetUserData(ctx, 2000);
        if (userData is null) {
            if (!createUserdata)
                throw new NullReferenceException("Provided pointer have not been instantiated in managed realm");
            var scriptContext = new ScriptObject(ctx);
            var handle = GCHandle.Alloc(scriptContext, GCHandleType.Normal);
            ScriptObject_SetUserData(ctx, (void*)GCHandle.ToIntPtr(handle), 2000);
            return scriptContext;
        }
        var handle1 = GCHandle.FromIntPtr((IntPtr)userData);
        if (handle1.Target is not ScriptObject ctx2)
            throw new ArgumentException("A userdata 2000 is occupied by something different than ScriptObject instance");
        return ctx2;
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
    public ScriptEngine Engine => ScriptEngine.FromPtr(ScriptObject_GetEngine(this));
    public int CopyFrom(asScriptObject* other) => ScriptObject_CopyFrom(this, other);
    #endregion
    #region User data
    public IntPtr SetUserDataPtr(IntPtr data, asPWORD type = 0) => (IntPtr)ScriptObject_SetUserData(this, (void*)data, type);
    public IntPtr GetUserDataPtr(asPWORD type = 0) => (IntPtr)ScriptObject_GetUserData(this, type);
    
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