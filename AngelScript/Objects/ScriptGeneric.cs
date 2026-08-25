using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

    /// <summary>
    /// The script engine. 
    /// </summary>
    public ScriptEngine Engine {
        get {
            if (_engine is not null)
                return _engine;
            var ptr = ScriptGeneric_GetEngine(this);
            return _engine = ScriptEngine.FromPtr(ptr);
        }
    }
    
    /// <summary>
    /// The function that is being called. 
    /// </summary>
    public ScriptFunction Function {
        get {
            if (_function is not null)
                return _function;
            var ptr = ScriptGeneric_GetFunction(this);
            return _function = ScriptFunction.FromPtr(ptr, true, true);
        }
    }
    /// <summary>
    /// The auxiliary registered with the function. 
    /// </summary>
    public IntPtr Auxiliary => (IntPtr)ScriptGeneric_GetAuxiliary(this);

    #region Object
    /// <summary>
    /// The object pointer if this is a class method, or null if it not. 
    /// </summary>
    public IntPtr ObjectPtr => (IntPtr)ScriptGeneric_GetObject(this);
    /// <summary>
    /// The type id of the object if this is a class method. 
    /// </summary>
    public int ObjectTypeId => ScriptGeneric_GetObjectTypeId(this);

    public object? Object {
        get {
            var ptr = ObjectPtr;
            if (ptr == 0) return null;
            var handle = GCHandle.FromIntPtr(ptr);
            return handle.Target;
        }
    }
    #endregion
    #region Arguments
    /// <summary>
    /// The number of arguments. 
    /// </summary>
    public int ArgCount => ScriptGeneric_GetArgCount(this);
    /// <summary>
    /// Returns the type id of the argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <param name="flags">A combination of modifiers</param>
    /// <returns>The type id of the argument. </returns>
    public int GetArgTypeId(asUINT arg, TypeModifiers* flags = null) => ScriptGeneric_GetArgTypeId(this, arg, (asDWORD*)flags);

    /// <inheritdoc cref="GetArgTypeId(asUINT, TypeModifiers*)"/>
    public int GetArgTypeId(asUINT arg, out TypeModifiers flags) {
        TypeModifiers f;
        var t = GetArgTypeId(arg, &f);
        flags = f;
        return t;
    }
    /// <summary>
    /// Returns the value of an 8-bit argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The 1 byte argument value. </returns>
    public asBYTE GetArgByte(asUINT arg) => ScriptGeneric_GetArgByte(this, arg);
    /// <summary>
    /// Returns the value of a 16-bit argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The 2 byte argument value. </returns>
    public asWORD GetArgWord(asUINT arg) => ScriptGeneric_GetArgWord(this, arg);
    /// <summary>
    /// Returns the value of a 32-bit integer argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The 4 byte argument value. </returns>
    public asDWORD GetArgDWord(asUINT arg) => ScriptGeneric_GetArgDWord(this, arg);
    /// <summary>
    /// Returns the value of a 64-bit integer argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The 8 byte argument value.</returns>
    public asQWORD GetArgQWord(asUINT arg) => ScriptGeneric_GetArgQWord(this, arg);
    /// <summary>
    /// Returns the value of a float argument.
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The float argument value.</returns>
    public float GetArgFloat(asUINT arg) => ScriptGeneric_GetArgFloat(this, arg);
    /// <summary>
    /// Returns the value of a double argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The double argument value.</returns>
    public double GetArgDouble(asUINT arg) => ScriptGeneric_GetArgDouble(this, arg);
    /// <summary>
    /// Returns the address held in a reference or handle argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The address argument value, which can be a reference or and object handle.</returns>
    /// <remarks>
    /// Don't release the pointer if this is an object or object handle, the ScriptGeneric object will do that for you.
    /// </remarks>
    public void* GetArgAddress(asUINT arg) => ScriptGeneric_GetArgAddress(this, arg);
    /// <summary>
    /// Returns a pointer to the object in a object argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>A pointer to the object argument, which can be an object value or object handle.</returns>
    /// <remarks>
    /// Don't release the pointer if this is an object handle, the ScriptGeneric object will do that for you. 
    /// </remarks>
    public void* GetArgObject(asUINT arg) => ScriptGeneric_GetArgObject(this, arg);
    /// <summary>
    /// Returns a pointer to the argument value. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>A pointer to the argument value.</returns>
    /// <remarks>
    /// <p>
    /// This method returns a pointer to the argument, so the application can read it.
    /// </p>
    /// <p>
    /// This method is generic, i.e. it works for all argument types; primitive, handles, objects, by value, or by
    /// reference. For this reason it is very convenient to be used in generated code, such as templates or macros. 
    /// </p>
    /// </remarks>
    public void* GetAddressOfArg(asUINT arg) => ScriptGeneric_GetAddressOfArg(this, arg);
    
    public int GetArgTypeId(int arg, asDWORD* flags = null) => ScriptGeneric_GetArgTypeId(this, (uint)arg, flags);
    public asBYTE GetArgByte(int arg) => GetArgByte((uint)arg);
    public asWORD GetArgWord(int arg) => GetArgWord((uint)arg);
    public asDWORD GetArgDWord(int arg) => GetArgDWord((uint)arg);
    public asQWORD GetArgQWord(int arg) => GetArgQWord((uint)arg);
    public float GetArgFloat(int arg) => GetArgFloat((uint)arg);
    public double GetArgDouble(int arg) => GetArgDouble((uint)arg);
    /// <summary>
    /// Returns the address held in a reference or handle argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>The address argument value, which can be a reference or and object handle.</returns>
    /// <remarks>
    /// Don't release the pointer if this is an object or object handle, the ScriptGeneric object will do that for you. 
    /// </remarks>
    public void* GetArgAddress(int arg) => GetArgAddress((uint)arg);
    /// <summary>
    /// Returns a pointer to the object in a object argument. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>A pointer to the object argument, which can be an object value or object handle.</returns>
    /// <remarks>
    /// Don't release the pointer if this is an object handle, the asScriptGeneric object will do that for you. 
    /// </remarks>
    public void* GetArgObject(int arg) => GetArgObject((uint)arg);
    /// <summary>
    /// Returns a pointer to the argument value. 
    /// </summary>
    /// <param name="arg">The argument index.</param>
    /// <returns>A pointer to the argument value.</returns>
    /// <remarks>
    /// <p>This method returns a pointer to the argument, so the application can read it.</p>
    /// <p>
    /// This method is generic, i.e. it works for all argument types; primitive, handles, objects, by value, or by
    /// reference. For this reason it is very convenient to be used in generated code, such as templates or macros.
    /// </p>
    /// </remarks>
    public void* GetAddressOfArg(int arg) => GetAddressOfArg((uint)arg);
    
    public T GetArg<T>(uint arg) {
        return (T)GetArg(typeof(T), arg);
    }

    public ref T GetArgRef<T>(uint arg) where T : unmanaged {
        return ref Unsafe.AsRef<T>(GetAddressOfArg(arg));
    }

    public object GetArg(Type T, uint arg) {
        var id = GetArgTypeId(arg, out var flags);
        switch (Type.GetTypeCode(T)) {
            case TypeCode.UInt64:
            case TypeCode.Int64:
                return GetArgQWord(arg);
            case TypeCode.UInt32:
            case TypeCode.Int32:
                return GetArgDWord(arg);
            case TypeCode.Char:
            case TypeCode.UInt16:
            case TypeCode.Int16:
                return GetArgWord(arg);
            case TypeCode.Byte:
            case TypeCode.SByte:
                return GetArgByte(arg);
            case TypeCode.Decimal:
                throw new NotImplementedException();
            case TypeCode.Double:
                return GetArgDouble(arg);
            case TypeCode.Empty:
                throw new NotImplementedException();
            case TypeCode.Object:
                if (T == typeof(nint))
                    return (IntPtr)GetArgAddress(arg);
                if (T.IsPointer)
                    return (IntPtr)GetArgAddress(arg);
                throw new NotImplementedException(T.ToString());
            case TypeCode.DBNull:
                throw new NotImplementedException();
            case TypeCode.Boolean:
                return GetArgByte(arg) > 1;
            case TypeCode.Single:
                return GetArgFloat(arg);
            case TypeCode.DateTime:
                throw new NotImplementedException();
            case TypeCode.String:
                var stringPtr = (IntPtr*)GetArgObject(arg);
                var stringHandle = GCHandle.FromIntPtr(*stringPtr);
                Console.WriteLine(stringHandle.Target);
                return stringHandle.Target;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    #endregion
    #region Return value
    /// <summary>
    /// Gets the type id of the return value
    /// </summary>
    /// <param name="flags">A combination of TypeModifiers.</param>
    /// <returns>The type id of the return value.</returns>
    public int GetReturnTypeId(TypeModifiers* flags) => ScriptGeneric_GetReturnTypeId(this, (asDWORD*)flags);

    /// <inheritdoc cref="GetReturnTypeId(TypeModifiers*)"/>
    public int GetReturnTypeId(out TypeModifiers flags) {
        flags = 0;
        return GetReturnTypeId((TypeModifiers*)Unsafe.AsPointer(ref flags));
    }

    /// <summary>
    /// Sets the 8-bit return value. 
    /// </summary>
    /// <param name="val">The return value.</param>
    /// <exception cref="ArgumentException">The return type is not an 8-bit value.</exception>
    /// <exception cref="Exception"></exception>
    public void SetReturnByte(asBYTE val) {
        var ret = (RetCode)ScriptGeneric_SetReturnByte(this, val);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not an 8-bit value.");
            default: throw ret.GetException();
        }
    }
    /// <summary>
    /// Sets the 16-bit return value.
    /// </summary>
    /// <param name="val">The return value.</param>
    /// <exception cref="ArgumentException">The return type is not an 16-bit value.</exception>
    /// <exception cref="Exception"></exception>
    public void SetReturnWord(asWORD val) {
        var ret = (RetCode)ScriptGeneric_SetReturnWord(this, val);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not an 16-bit value.");
            default: throw ret.GetException();
        }
    }
    /// <summary>
    /// Sets the 32-bit integer return value.
    /// </summary>
    /// <param name="val">The return value.</param>
    /// <exception cref="ArgumentException">The return type is not a 32-bit value.</exception>
    /// <exception cref="Exception"></exception>
    public void SetReturnDWord(asDWORD val) {
        var ret = (RetCode)ScriptGeneric_SetReturnDWord(this, val);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not a 32-bit value.");
            default: throw ret.GetException();
        }
    }
    
    /// <summary>
    /// Sets the 64-bit integer return value. 
    /// </summary>
    /// <param name="val">The return value.</param>
    /// <exception cref="ArgumentException">The return type is not a 64-bit value.</exception>
    /// <exception cref="Exception"></exception>
    public void SetReturnQWord(asQWORD val) {
        var ret = (RetCode)ScriptGeneric_SetReturnQWord(this, val);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not a 64-bit value.");
            default: throw ret.GetException();
        }
    }
    
    /// <summary>
    /// Sets the float return value. 
    /// </summary>
    /// <param name="val">The return value.</param>
    /// <exception cref="ArgumentException">The return type is not a 32-bit value.</exception>
    /// <exception cref="Exception"></exception>
    public void SetReturnFloat(float val) {
        var ret = (RetCode)ScriptGeneric_SetReturnFloat(this, val);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not a 32-bit value.");
            default: throw ret.GetException();
        }
    }
    /// <summary>
    /// Sets the double return value. 
    /// </summary>
    /// <param name="val">The return value.</param>
    /// <exception cref="ArgumentException">The return type is not a 64-bit value.</exception>
    /// <exception cref="Exception"></exception>
    public void SetReturnDouble(double val) {
        var ret = (RetCode)ScriptGeneric_SetReturnDouble(this, val);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not a 64-bit value.");
            default: throw ret.GetException();
        }
    }

    /// <summary>
    /// Sets the address return value when the return is a reference or handle. 
    /// </summary>
    /// <param name="addr">The return value, which is an address. </param>
    /// <exception cref="ArgumentException">The return type is not a reference or handle.</exception>
    public void SetReturnAddress(void* addr) {
        var ret = (RetCode)ScriptGeneric_SetReturnAddress(this, addr);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not a reference or handle.");
            default: throw ret.GetException();
        }
    }

    /// <summary>
    /// Sets the object return value. 
    /// </summary>
    /// <param name="obj">A pointer to the object return value.</param>
    /// <exception cref="ArgumentException">The return type is not an object value or handle.</exception>
    /// <remarks>
    /// If the function returns an object, the library will automatically do what is necessary based on how the object
    /// was declared, i.e. if the function was registered to return a handle then the library will call the addref
    /// behaviour. If it was registered to return an object by value, then the library will make a copy of the object. 
    /// </remarks>
    public void SetReturnObject(void* obj) {
        var ret = (RetCode)ScriptGeneric_SetReturnObject(this, obj);
        switch (ret) {
            case RetCode.Success: break;
            case RetCode.InvalidType: throw new ArgumentException("The return type is not an object value or handle.");
            default: throw ret.GetException();
        }
    }

    public void SetReturn<T>(T val) {
        SetReturn(typeof(T), val);
    }

    public void SetReturn(Type t, object val) {
        switch (Type.GetTypeCode(t)) {
            case TypeCode.UInt64:
            case TypeCode.Int64:
                SetReturnQWord((asQWORD)val);
                break;
            case TypeCode.UInt32:
            case TypeCode.Int32:
                SetReturnDWord((asDWORD)val);
                break;
            case TypeCode.Char:
            case TypeCode.UInt16:
            case TypeCode.Int16:
                SetReturnWord((asWORD)val);
                break;
            case TypeCode.Byte:
            case TypeCode.SByte:
                SetReturnByte((asBYTE)val);
                break;
            case TypeCode.Decimal:
                throw new NotImplementedException();
                break;
            case TypeCode.Double:
                SetReturnDouble((double)val);
                break;
            case TypeCode.Empty:
                throw new NotImplementedException();
                break;
            case TypeCode.Object:
                if (t == typeof(void))
                    return;
                throw new NotImplementedException(t.ToString());
                break;
            case TypeCode.DBNull:
                throw new NotImplementedException();
            case TypeCode.Boolean:
                SetReturnByte((byte)((bool)val ? 1 : 0));
                break;
            case TypeCode.Single:
                SetReturnFloat((float)val);
                break;
            case TypeCode.DateTime:
                throw new NotImplementedException();
                break;
            case TypeCode.String:
                throw new NotImplementedException();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    /// <summary>
    /// Gets the address to the memory where the return value should be placed. 
    /// </summary>
    /// <returns>A pointer to the memory where the return values is to be placed.</returns>
    /// <remarks>
    /// <p>
    /// The memory is not initialized, so if you're going to return a complex type by value you shouldn't use the
    /// assignment operator to initialize it. Instead use the placement new operator to call the type's copy constructor
    /// to perform the initialization
    /// </p>
    /// <p>
    /// The placement new operator works for primitive types too, so this method is ideal for writing automatically
    /// generated functions that works the same way for all types. 
    /// </p>
    /// </remarks>
    public void* GetAddressOfReturnLocation() => ScriptGeneric_GetAddressOfReturnLocation(this);
    #endregion

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void InvokeDelegate(asScriptGeneric* generic) {
        try {
            var gen = FromPtr(generic);
            var handle = GCHandle.FromIntPtr(gen.Auxiliary);
            if (handle.Target is MethodInfo info) {
                var param = info.GetParameters();
                uint i = 0;
                var func = gen.Function;
                var funcParamCount = func.ParamCount;
                var methodParams = info.GetParameters();
                if (funcParamCount != methodParams.Length)
                    throw new TargetInvocationException(new Exception("Parameter count mismatch"));
                var args = new object?[funcParamCount];
                foreach (var parameter in param) {
                    args[i] = gen.GetArg(parameter.ParameterType, i++);
                }

                info.Invoke(gen.Object, args.ToArray());
            }
        }
        catch (Exception e) {
            AngelScript.ActiveContext?.SetException(e.ToString());
        }
    }
}