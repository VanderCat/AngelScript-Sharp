using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

public unsafe class ScriptContext {
    public asScriptContext* Handle;
    public static implicit operator asScriptContext*(ScriptContext c) => c.Handle;

    internal ScriptContext(asScriptContext* context) {
        Handle = context;
    }

    public static ScriptContext FromPtr(asScriptContext* ctx, bool useUserdata = true, bool createUserdata = false) {
	    if (!useUserdata)
		    return new ScriptContext(ctx);
	    var userData = ScriptContext_GetUserData(ctx, 2000);
	    if (userData is null) {
		    if (!createUserdata)
			    throw new NullReferenceException("Provided pointer have not been instantiated in managed realm");
		    var scriptContext = new ScriptContext(ctx);
		    var handle = GCHandle.Alloc(scriptContext, GCHandleType.Normal);
		    ScriptContext_SetUserData(ctx, (void*)GCHandle.ToIntPtr(handle), 2000);
		    return scriptContext;
	    }
	    var handle1 = GCHandle.FromIntPtr((IntPtr)userData);
        if (handle1.Target is not ScriptContext ctx2)
            throw new ArgumentException("A userdata 2000 is occupied by something different than ScriptContext instance");
        return ctx2;
    }
    
    #region Memory management
    public int AddRef() => ScriptContext_AddRef(this);
    public int Release() => ScriptContext_Release(this);
    #endregion

	#region Miscellaneous
	public ScriptEngine Engine => ScriptEngine.FromPtr(ScriptContext_GetEngine(this));
	#endregion
	#region Execution
	public int Prepare(asScriptFunction* func) => ScriptContext_Prepare(this, func);
	public int Unprepare() => ScriptContext_Unprepare(this);
	public int Execute() => ScriptContext_Execute(this);
	public int Abort() => ScriptContext_Abort(this);
	public int Suspend() => ScriptContext_Suspend(this);
	public asEContextState GetState() => ScriptContext_GetState(this);
	public int PushState() => ScriptContext_PushState(this);
	public int PopState() => ScriptContext_PopState(this);
	public bool IsNested(asUINT* nestCount = null) => ScriptContext_IsNested(this, nestCount);
	#endregion
	#region Object pointer for calling class methods
	public int SetObject(void* obj) => ScriptContext_SetObject(this, obj);
	#endregion
	#region Arguments
	public int SetArgByte(asUINT arg, asBYTE value) => ScriptContext_SetArgByte(this, arg, value);
	public int SetArgWord(asUINT arg, asWORD value) => ScriptContext_SetArgWord(this, arg, value);
	public int SetArgDWord(asUINT arg, asDWORD value) => ScriptContext_SetArgDWord(this, arg, value);
	public int SetArgQWord(asUINT arg, asQWORD value) => ScriptContext_SetArgQWord(this, arg, value);
	public int SetArgFloat(asUINT arg, float value) => ScriptContext_SetArgFloat(this, arg, value);
	public int SetArgDouble(asUINT arg, double value) => ScriptContext_SetArgDouble(this, arg, value);
	public int SetArgAddress(asUINT arg, void* addr) => ScriptContext_SetArgAddress(this, arg, addr);
	public int SetArgObject(asUINT arg, void* obj) => ScriptContext_SetArgObject(this, arg, obj);
	public int SetArgVarType(asUINT arg, void* ptr, int typeId) => ScriptContext_SetArgVarType(this, arg, ptr, typeId);
	public void* GetAddressOfArg(asUINT arg) => ScriptContext_GetAddressOfArg(this, arg);
	#endregion
	#region Return value
	public asBYTE GetReturnByte() => ScriptContext_GetReturnByte(this);
	public asWORD GetReturnWord() => ScriptContext_GetReturnWord(this);
	public asDWORD GetReturnDWord() => ScriptContext_GetReturnDWord(this);
	public asQWORD GetReturnQWord() => ScriptContext_GetReturnQWord(this);
	public float GetReturnFloat() => ScriptContext_GetReturnFloat(this);
	public double GetReturnDouble() => ScriptContext_GetReturnDouble(this);
	public void* GetReturnAddress() => ScriptContext_GetReturnAddress(this);
	public void* GetReturnObject() => ScriptContext_GetReturnObject(this);
	public void* GetAddressOfReturnValue() => ScriptContext_GetAddressOfReturnValue(this);
	#endregion
	#region Exception handling
	internal int SetException(byte* info, bool allowCatch = true) => ScriptContext_SetException(this, (sbyte*)info, allowCatch);
	
	/// <summary>
	/// Sets an exception, which aborts the execution
	/// </summary>
	/// <param name="info"></param>
	/// <param name="allowCatch"></param>
	/// <exception cref="Exception">The context isn't currently calling an application registered function</exception>
	/// <remarks>
	///	This method sets a script exception in the context.
	/// This will only work if the context is currently calling a system function, thus this method can only be used for system functions.
	/// <br/><br/>
	/// Note that if your system function sets an exception, it should not return any object references because the engine will not release the returned reference. 
	/// </remarks>
	public void SetException(string info, bool allowCatch = true) {
		fixed (byte* infoPtr = Encoding.UTF8.GetBytes(info + '\0')) {
			var result = (RetCode)SetException(infoPtr, allowCatch);
			if (result < 0) {
				switch (result) {
					case RetCode.Error: throw new Exception("The context isn't currently calling an application registered function"); //TODO: Exception
					default: throw result.GetException();
				}
			}
		}
	}
	internal int GetExceptionLineNumber(int* column = null, byte** sectionName = null) 
		=> ScriptContext_GetExceptionLineNumber(this, column, (sbyte**)sectionName);

	public ref struct ExceptionInfo {
		public int Line;
		public int Column;
		public string? SectionName;
	}
	public ExceptionInfo GetExceptionInfo() {
		var info = new ExceptionInfo();
		byte* sectionNamePtr;
		info.Line = GetExceptionLineNumber(&info.Column, &sectionNamePtr);
		info.SectionName = Util.ConvertPtrToString(sectionNamePtr);
		return info;
	}
	internal asScriptFunction* GetExceptionFunctionRaw() => ScriptContext_GetExceptionFunction(this);
	public ScriptFunction? GetExceptionFunction() {
		var ptr = GetExceptionFunctionRaw();
		if (ptr is null)
			return null;
		return ScriptFunction.FromPtr(ptr, true, true);
	}
	internal byte* GetExceptionStringRaw() 
		=> (byte*)ScriptContext_GetExceptionString(this);
	public string? GetExceptionString()
		=> Util.ConvertPtrToString(GetExceptionStringRaw());
	public bool WillExceptionBeCaught() => ScriptContext_WillExceptionBeCaught(this);
	public int SetExceptionCallback(asSFuncPtr* callback, void* obj, int callConv) => ScriptContext_SetExceptionCallback(this, callback, obj, callConv);
	public void ClearExceptionCallback() => ScriptContext_ClearExceptionCallback(this);
	#endregion
	#region Debugging
	public int SetLineCallback(asSFuncPtr* callback, void* obj, int callConv) => ScriptContext_SetLineCallback(this, callback, obj, callConv);
	public void ClearLineCallback() => ScriptContext_ClearLineCallback(this);
	public asUINT GetCallstackSize() => ScriptContext_GetCallstackSize(this);
	public asScriptFunction* GetFunction(asUINT stackLevel = 0) => ScriptContext_GetFunction(this, stackLevel);
	public int GetLineNumber(asUINT stackLevel = 0, int* column = null, byte** sectionName = null) => 
		ScriptContext_GetLineNumber(this, stackLevel, column, (sbyte**)sectionName);
	public int GetVarCount(asUINT stackLevel = 0) => ScriptContext_GetVarCount(this, stackLevel);
	public int GetVar(asUINT varIndex, asUINT stackLevel, byte** name, int* typeId = null, asETypeModifiers* typeModifiers = null, bool* isVarOnHeap = null, int* stackOffset = null) 
		=> ScriptContext_GetVar(this, varIndex, stackLevel, (sbyte**)name, typeId, typeModifiers, isVarOnHeap, stackOffset);
	public byte* GetVarDeclaration(asUINT varIndex, asUINT stackLevel = 0, bool includeNamespace = false) 
		=> (byte*)ScriptContext_GetVarDeclaration(this, varIndex, stackLevel, includeNamespace);
	public void* GetAddressOfVar(asUINT varIndex, asUINT stackLevel = 0, bool dontDereference = false, bool returnAddressOfUnitializedObjects = false) 
		=> ScriptContext_GetAddressOfVar(this, varIndex, stackLevel, dontDereference, returnAddressOfUnitializedObjects);
	public bool IsVarInScope(asUINT varIndex, asUINT stackLevel = 0) => ScriptContext_IsVarInScope(this, varIndex, stackLevel);
	public int GetThisTypeId(asUINT stackLevel = 0) => ScriptContext_GetThisTypeId(this, stackLevel);
	public void* GetThisPointer(asUINT stackLevel = 0) => ScriptContext_GetThisPointer(this, stackLevel);
	public asScriptFunction* GetSystemFunction() => ScriptContext_GetSystemFunction(this);
	#endregion
	#region User data
	public IntPtr SetUserDataPtr(IntPtr data, asPWORD type = 0) => (IntPtr)ScriptContext_SetUserData(this, (void*)data, type);
	public IntPtr GetUserDataPtr(asPWORD type = 0) => (IntPtr)ScriptContext_GetUserData(this, type);
	
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
	#region Serialization
	public int StartDeserialization() => ScriptContext_StartDeserialization(this);
	public int FinishDeserialization() => ScriptContext_FinishDeserialization(this);
	public int PushFunction(asScriptFunction* func, void* @object) => ScriptContext_PushFunction(this, func, @object);
	public int GetStateRegisters(asUINT stackLevel, asScriptFunction** callingSystemFunction, asScriptFunction** initialFunction, asDWORD* origStackPointer, asDWORD* argumentsSize, asQWORD* valueRegister, void** objectRegister,
		asTypeInfo** objectTypeRegister) => ScriptContext_GetStateRegisters(this, stackLevel, callingSystemFunction, initialFunction, origStackPointer, argumentsSize, valueRegister, objectRegister, objectTypeRegister);
	public int GetCallStateRegisters(asUINT stackLevel, asDWORD* stackFramePointer, asScriptFunction** currentFunction, asDWORD* programPointer, asDWORD* stackPointer, asDWORD* stackIndex) 
		=> ScriptContext_GetCallStateRegisters(this, stackLevel, stackFramePointer, currentFunction, programPointer, stackPointer, stackIndex);
	public int SetStateRegisters(asUINT stackLevel, asScriptFunction* callingSystemFunction, asScriptFunction* initialFunction, asDWORD origStackPointer, asDWORD argumentsSize, asQWORD valueRegister, void* objectRegister, asTypeInfo* objectTypeRegister) =>
		ScriptContext_SetStateRegisters(this, stackLevel, callingSystemFunction, initialFunction, origStackPointer, argumentsSize, valueRegister, objectRegister, objectTypeRegister);
	public int SetCallStateRegisters(asUINT stackLevel, asDWORD stackFramePointer, asScriptFunction* currentFunction, asDWORD programPointer, asDWORD stackPointer, asDWORD stackIndex) 
		=> ScriptContext_SetCallStateRegisters(this, stackLevel, stackFramePointer, currentFunction, programPointer, stackPointer, stackIndex);
	public int GetArgsOnStackCount(asUINT stackLevel) 
		=> ScriptContext_GetArgsOnStackCount(this, stackLevel);
	public int GetArgOnStack(asUINT stackLevel, asUINT arg, int* typeId, asUINT* flags, void** address) 
		=> ScriptContext_GetArgOnStack(this, stackLevel, arg, typeId, flags, address);
	#endregion
}