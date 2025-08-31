namespace AngelScript;

/// <summary>
/// The interface for the generic calling convention
/// </summary>
public unsafe class ScriptGeneric {
    public asScriptGeneric* Handle;
    public static implicit operator asScriptGeneric*(ScriptGeneric c) => c.Handle;

    internal ScriptGeneric(asScriptGeneric* generic) {
        Handle = generic;
    }
    
    public static ScriptGeneric FromPtr(asScriptGeneric* ctx) => new(ctx);

    private ScriptEngine? _engine;
    private ScriptFunction? _function;

    public ScriptEngine Engine {
        get {
            if (_engine is not null)
                return _engine;
            var ptr = ScriptGeneric_GetEngine(this);
            return _engine = ScriptEngine.FromPtr(ptr);
        }
    }
    public ScriptFunction Function {
        get {
            if (_function is not null)
                return _function;
            var ptr = ScriptGeneric_GetFunction(this);
            return _function = ScriptFunction.FromPtr(ptr, true, true);
        }
    }
    public IntPtr Auxiliary => (IntPtr)ScriptGeneric_GetAuxiliary(this);

    #region Object
    public void* GetObject() => ScriptGeneric_GetObject(this);
    public int GetObjectTypeId() => ScriptGeneric_GetObjectTypeId(this);
    #endregion
    #region Arguments
    public int GetArgCount() => ScriptGeneric_GetArgCount(this);
    public int GetArgTypeId(asUINT arg, asDWORD* flags = null) => ScriptGeneric_GetArgTypeId(this, arg, flags);
    public asBYTE GetArgByte(asUINT arg) => ScriptGeneric_GetArgByte(this, arg);
    public asWORD GetArgWord(asUINT arg) => ScriptGeneric_GetArgWord(this, arg);
    public asDWORD GetArgDWord(asUINT arg) => ScriptGeneric_GetArgDWord(this, arg);
    public asQWORD GetArgQWord(asUINT arg) => ScriptGeneric_GetArgQWord(this, arg);
    public float GetArgFloat(asUINT arg) => ScriptGeneric_GetArgFloat(this, arg);
    public double GetArgDouble(asUINT arg) => ScriptGeneric_GetArgDouble(this, arg);
    public void* GetArgAddress(asUINT arg) => ScriptGeneric_GetArgAddress(this, arg);
    public void* GetArgObject(asUINT arg) => ScriptGeneric_GetArgObject(this, arg);
    public void* GetAddressOfArg(asUINT arg) => ScriptGeneric_GetAddressOfArg(this, arg);
    
    public int GetArgTypeId(int arg, asDWORD* flags = null) => ScriptGeneric_GetArgTypeId(this, (uint)arg, flags);
    public asBYTE GetArgByte(int arg) => GetArgByte((uint)arg);
    public asWORD GetArgWord(int arg) => GetArgWord((uint)arg);
    public asDWORD GetArgDWord(int arg) => GetArgDWord((uint)arg);
    public asQWORD GetArgQWord(int arg) => GetArgQWord((uint)arg);
    public float GetArgFloat(int arg) => GetArgFloat((uint)arg);
    public double GetArgDouble(int arg) => GetArgDouble((uint)arg);
    public void* GetArgAddress(int arg) => GetArgAddress((uint)arg);
    public void* GetArgObject(int arg) => GetArgObject((uint)arg);
    public void* GetAddressOfArg(int arg) => GetAddressOfArg((uint)arg);
    #endregion
    #region Return value
    public int GetReturnTypeId(asDWORD* flags = null) => ScriptGeneric_GetReturnTypeId(this, flags);
    public int SetReturnByte(asBYTE val) => ScriptGeneric_SetReturnByte(this, val);
    public int SetReturnWord(asWORD val) => ScriptGeneric_SetReturnWord(this, val);
    public int SetReturnDWord(asDWORD val) => ScriptGeneric_SetReturnDWord(this, val);
    public int SetReturnQWord(asQWORD val) => ScriptGeneric_SetReturnQWord(this, val);
    public int SetReturnFloat(float val) => ScriptGeneric_SetReturnFloat(this, val);
    public int SetReturnDouble(double val) => ScriptGeneric_SetReturnDouble(this, val);
    public int SetReturnAddress(void* addr) => ScriptGeneric_SetReturnAddress(this, addr);
    public int SetReturnObject(void* obj) => ScriptGeneric_SetReturnObject(this, obj);
    public void* GetAddressOfReturnLocation() => ScriptGeneric_GetAddressOfReturnLocation(this);
    #endregion
}