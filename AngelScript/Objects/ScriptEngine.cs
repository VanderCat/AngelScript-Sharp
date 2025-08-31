using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

/// <summary>
/// The engine interface
/// </summary>
///	<remarks>
/// The engine is the central object. It is where the application registers the application interface that the
/// scripts should be able to use, and it is where the application can request modules to build scripts and
/// contexts to execute them.
/// <br/><br/>
/// The engine instance is created with a call to .
/// <br/><br/>
/// It is allowed to have multiple instances of script engines, but there is rarely a need for it. Even
/// if the application needs to expose different interfaces to different types of scripts this can usually
/// be accomplished through the use of configuration groups and access profiles.
/// </remarks>
public unsafe class ScriptEngine : IDisposable {
	public asScriptEngine* Handle;
	public static implicit operator asScriptEngine*(ScriptEngine e) => e.Handle;

	internal ScriptEngine(asScriptEngine* engine) {
		Handle = engine;
	}

	public static ScriptEngine FromPtr(asScriptEngine* engine, bool useUserdata = true, bool createUserdata = false) {
		if (!useUserdata)
			return new ScriptEngine(engine);
		var userData = ScriptEngine_GetUserData(engine, 2000);
		if (userData is null) {
			if (!createUserdata)
				throw new NullReferenceException("Provided pointer have not been instantiated in managed realm");
			var scriptEngine = new ScriptEngine(engine);
			var handle = GCHandle.Alloc(scriptEngine, GCHandleType.Normal);
			ScriptEngine_SetUserData(engine, (void*)GCHandle.ToIntPtr(handle), 2000);
			scriptEngine.SetEngineUserDataPtrCleanupCallback(&OnEngineUserDataCleanup, 2000);
			scriptEngine.SetModuleUserDataPtrCleanupCallback(&OnModuleUserDataCleanup, 2000);
			scriptEngine.SetContextUserDataPtrCleanupCallback(&OnContextUserDataCleanup, 2000);
			scriptEngine.SetFunctionUserDataPtrCleanupCallback(&OnFunctionUserDataCleanup, 2000);
			scriptEngine.SetTypeInfoUserDataPtrCleanupCallback(&OnTypeInfoUserDataCleanup, 2000);
			scriptEngine.SetScriptObjectUserDataPtrCleanupCallback(&OnObjectUserDataCleanup, 2000);
			return scriptEngine;
		}
		var handle1 = GCHandle.FromIntPtr((IntPtr)userData);
		if (handle1.Target is not ScriptEngine ctx2)
			throw new ArgumentException("A userdata 2000 is occupied by something different than ScriptEngine instance");
		return ctx2;
	}

	#region Memory Management
	/// <summary>
	/// Increase reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>
	///	Call this method when storing an additional reference to the object. Remember that
	/// the first reference that is received from asCreateScriptEngine is already accounted for.
	/// </remarks>
	internal int AddRef() => ScriptEngine_AddRef(this);
	
	/// <summary>
	/// Decrease reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>
	///	Call this method when you will no longer use the references that you own.
	/// <br/><br/>
	/// If you know that the engine is supposed to be shut down, then it is recommended to call the <see cref="ShutDownAndRelease"/> method instead. 
	/// </remarks>
	internal int Release() => ScriptEngine_Release(this);

	/// <summary>
	/// Shuts down the engine then decrease the reference counter
	/// </summary>
	/// <returns>The number of references to this object</returns>
	/// <remarks>
	///	Call this method when you know it is time to shut down the engine. This will automatically discard all
	/// the script modules and run a complete garbage collection cycle.
	/// <br/><br/>
	/// Calling this method rather than the ordinary Release method will avoid potential memory leaks if for example there
	/// are objects in the modules or garbage collector that indirectly holds a reference to the engine. 
	/// </remarks>
	public int ShutDownAndDispose() {
		Dispose(true);
		return ScriptEngine_ShutDownAndRelease(this);
	}

	public void Dispose() {
		Dispose(false);
	}

	private void Dispose(bool shutdown) {
		GC.SuppressFinalize(this);
		if (shutdown) return;
		Release();
	}
		
	#endregion

	#region Engine properties
	/// <summary>
	/// Dynamically change some engine properties
	/// </summary>
	/// <param name="property">One of the <see cref="asEEngineProp"/> values</param>
	/// <param name="value">The new value of the property</param>
	/// <remarks>With this method you can change the way the script engine works in some regards</remarks>
	/// <exception cref="ArgumentException">Invalid property</exception>
	public void SetEngineProperty(asEEngineProp property, asPWORD value) {
		var ret = (RetCode)ScriptEngine_SetEngineProperty(this, property, value);
		if (ret == RetCode.InvalidArg)
			throw new ArgumentException(ret.GetDescription(), nameof(value));
		if (ret != RetCode.Success)
			throw ret.GetException();
	}
	/// <summary>
	/// Retrieve current engine property settings
	/// </summary>
	/// <param name="property">One of the <see cref="asEEngineProp"/> values</param>
	/// <returns>The value of the property, or 0 if it is an invalid property</returns>
	/// <remarks>Calling this method lets you determine the current value of the engine properties</remarks>
	public asPWORD GetEngineProperty(asEEngineProp property) => ScriptEngine_GetEngineProperty(this, property);
	#endregion

	#region Compiler messages
	/// <summary>
	/// Sets a message callback that will receive compiler messages
	/// </summary>
	/// <param name="callback">A function or class method pointer</param>
	/// <param name="obj">The object for methods, or an optional parameter for functions</param>
	/// <param name="callConv">The calling convention</param>
	/// <remarks>
	/// This method sets the callback routine that will receive compiler messages.
	/// <br/><br/>
	/// It is recommended to register the message callback routine right after creating the engine,
	/// as some of the registration functions can provide useful information to better explain errors. 
	/// </remarks>
	/// <exception cref="ArgumentException">One of the arguments is incorrect, e.g. obj is null for a class method</exception>
	/// <exception cref="NotSupportedException">The arguments are not supported, e.g. <see cref="asECallConvTypes.asCALL_GENERIC"/></exception>
	public void SetMessageCallback(asSFuncPtr* callback, void* obj, asECallConvTypes callConv) {
		var ret = (RetCode)ScriptEngine_SetMessageCallback(this, callback, obj, (asDWORD)callConv);
		switch (ret) {
			case RetCode.Success: break;
			case RetCode.InvalidArg: throw new ArgumentException("One of the arguments is incorrect, e.g. obj is null for a class method");
			case RetCode.NotSupported: throw new NotSupportedException("The arguments are not supported, e.g. asCALL_GENERIC");
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Gets the current message callback
	/// </summary>
	/// <param name="callback">Will be set with the function or method pointer</param>
	/// <param name="obj">Will be set with the object pointer</param>
	/// <param name="callConv">Will be set with the calling convention</param>
	/// <remarks>
	/// The current message callback can be retrieved so that another callback can be temporarily set and then the original one restored.
	/// </remarks>
	/// <exception cref="MissingMethodException">No message callback has been registered</exception>
	public void GetMessageCallback(asSFuncPtr* callback, void** obj, asECallConvTypes* callConv) {
		var ret = (RetCode)ScriptEngine_GetMessageCallback(this, callback, obj, (asDWORD*)callConv);
		switch (ret) {
			case RetCode.Success: break;
			case RetCode.NoFunction: throw new MissingMethodException("No message callback has been registered");
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Clears the registered message callback routine
	/// </summary>
	/// <returns>A negative value on error</returns>
	/// <remarks>Call this method to remove the message callback</remarks>
	public void ClearMessageCallback() {
		var ret = (RetCode)ScriptEngine_ClearMessageCallback(this);
		switch (ret) {
			case RetCode.Success: break;
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Writes a message to the message callback
	/// </summary>
	/// <param name="section">The name of the script section</param>
	/// <param name="row">The row number</param>
	/// <param name="col">The column number</param>
	/// <param name="type">The message type</param>
	/// <param name="message">The message text</param>
	/// <remarks>
	///	This method can be used by the application to write messages to the same message callback that the script compiler uses.
	/// This is useful for example if a preprocessor is used. 
	/// </remarks>
	/// <exception cref="ArgumentException">The section or message is null</exception>
	public void WriteMessage(byte* section, int row, int col, asEMsgType type, byte* message) {
		if (section is null)
			throw new ArgumentException("The section is null", nameof(section));
		if (section is null)
			throw new ArgumentException("The message is null", nameof(message));
		var ret = (RetCode)ScriptEngine_WriteMessage(this, (sbyte*)section, row, col, type, (sbyte*)message);
		switch (ret) {
			case RetCode.Success: break;
			default: throw ret.GetException();
		}
	}
	#endregion

	#region JIT Compiler
	/// <summary>
	/// Sets the JIT compiler
	/// </summary>
	/// <param name="compiler">A pointer to the JIT compiler</param>
	/// <remarks>
	///	This method is used to set the JIT compiler.
	/// The engine will automatically invoke the JIT compiler when it is set after compiling scripts or loading pre-compiled byte code.
	/// </remarks>
	public void SetJITCompiler(asJITCompilerAbstract* compiler) {
		var ret = (RetCode)ScriptEngine_SetJITCompiler(this, compiler);
		switch (ret) {
			case RetCode.Success: break;
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Returns the JIT compiler
	/// </summary>
	/// <returns>A pointer to the JIT compiler </returns>
	public asJITCompilerAbstract* GetJITCompiler() => ScriptEngine_GetJITCompiler(this);
	#endregion

	#region Global functions
	/// <summary>
	/// Registers a global function
	/// </summary>
	/// <param name="declaration">The declaration of the global function in script syntax</param>
	/// <param name="funcPointer">The function pointer</param>
	/// <param name="callConv">The calling convention for the function</param>
	/// <param name="auxiliary">A helper object for use with some calling conventions</param>
	/// <returns>the function id</returns>
	/// <remarks>
	///	This method registers system functions that the scripts may use to communicate with the host application.
	/// <br/><br/>
	/// The auxiliary pointer can optionally be used with <see cref="asECallConvTypes.asCALL_GENERIC"/>.
	/// For the calling convention <see cref="asECallConvTypes.asCALL_THISCALL"/> the auxiliary is required.
	/// </remarks>
	public int RegisterGlobalFunction(byte* declaration, asSFuncPtr* funcPointer, asECallConvTypes callConv, void* auxiliary = null) {
		var result = ScriptEngine_RegisterGlobalFunction(this, (sbyte*)declaration, funcPointer, (asDWORD)callConv, auxiliary);
		if (result >= 0)
			return result;
		var ret = (RetCode)result;
		switch (ret) {
			case RetCode.NotSupported: throw new NotSupportedException("The calling convention is not supported");
			case RetCode.WrongCallingConv: throw new ArgumentException("The function's calling convention doesn't match callConv", nameof(callConv));
			case RetCode.InvalidDeclaration: throw new ArgumentException("The function declaration is invalid", nameof(declaration));
			case RetCode.NameTaken: throw new NameTakenException("The function name is already used elsewhere");
			case RetCode.AlreadyRegistered: throw new AlreadyRegisteredException("The function has already been registered with the same parameter list");
			case RetCode.InvalidArg: throw new ArgumentException("The auxiliary pointer wasn't set according to calling convention", nameof(auxiliary));
			default: throw ret.GetException();
		}
	}
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void NativeDelegateInvoke(asScriptGeneric* asScriptGeneric) {
		try {
			DelegateInvoke(new ScriptGeneric(asScriptGeneric));
		}
		catch (Exception e) {
			AngelScript.ActiveContext?.SetException(e.ToString());
		}
	}

	private static void DelegateInvoke(ScriptGeneric generic) {
		if (generic.Auxiliary == 0)
			throw new Exception("No auxiliary present");
		var handle = GCHandle.FromIntPtr(generic.Auxiliary);
		if (handle.Target is not MethodInfo info)
			throw new InvalidCastException("The auxilary is not a MethodInfo");
		var func = generic.Function;
		var funcParamCount = func.ParamCount;
		var methodParams = info.GetParameters();
		if (funcParamCount != methodParams.Length)
			throw new TargetInvocationException(new Exception("Parameter count mismatch"));
		var args = new object?[funcParamCount];
		for (var i = 0; i < args.Length; i++) {
			var paramInfo = methodParams[i];
			if (!paramInfo.ParameterType.IsPrimitive)
				throw new NotImplementedException("Non primitive type are not supported");
			switch (Type.GetTypeCode(paramInfo.ParameterType)) {
				case TypeCode.Byte: args[i] = generic.GetArgByte(i); break;
				case TypeCode.UInt16: args[i] = generic.GetArgWord(i); break;
				case TypeCode.UInt32: args[i] = generic.GetArgDWord(i); break;
				case TypeCode.UInt64: args[i] = generic.GetArgQWord(i); break;
				case TypeCode.Single: args[i] = generic.GetArgFloat(i); break;
				case TypeCode.Double: args[i] = generic.GetArgDouble(i); break;
				default: throw new ArgumentException("Unsupported argument type "+paramInfo.ParameterType, paramInfo.Name);
			}
		}
		info.Invoke(null, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args, null);
	}
	public int RegisterGlobalFunction(string declaration, Delegate @delegate) {
		var ptr = asSFuncPtr.asFunctionPtr((IntPtr)(delegate*unmanaged[Cdecl]<asScriptGeneric*, void>)&NativeDelegateInvoke);
		//FIXME: memory leak, GCHandle is never released
		fixed (byte* dec = Encoding.UTF8.GetBytes(declaration + '\0'))
			return RegisterGlobalFunction(dec, &ptr, asECallConvTypes.asCALL_GENERIC, (void*)GCHandle.ToIntPtr(GCHandle.Alloc(@delegate.Method)));
	}

	/// <summary>
	/// The number of registered functions
	/// </summary>
	public asUINT GlobalFunctionCount => ScriptEngine_GetGlobalFunctionCount(this);
	/// <summary>
	/// Returns the registered function
	/// </summary>
	/// <param name="index">The index of the registered global function</param>
	/// <returns>The function object, or null on error</returns>
	public asScriptFunction* GetGlobalFunctionByIndex(asUINT index) => ScriptEngine_GetGlobalFunctionByIndex(this, index);
	/// <summary>
	/// Returns the registered function
	/// </summary>
	/// <param name="declaration">The signature of the function</param>
	/// <returns>The function object, or null on error</returns>
	public asScriptFunction* GetGlobalFunctionByDecl(byte* declaration) => ScriptEngine_GetGlobalFunctionByDecl(this, (sbyte*)declaration);
	#endregion

	#region Global properties
	/// <summary>
	/// Registers a global property
	/// </summary>
	/// <param name="declaration">The declaration of the global property in script syntax</param>
	/// <param name="pointer">The address of the property that will be used to access the property value</param>
	/// <returns>The index of the property</returns>
	/// <remarks>
	///	Use this method to register a global property that the scripts will be able to access as global variables.
	/// The property may optionally be registered as const, if the scripts shouldn't be allowed to modify it.
	/// <br/><br/>
	/// When registering the property, the application must pass the address to the actual value.
	/// The application must also make sure that this address remains valid throughout the life time of this registration,
	/// i.e. until the engine is released or the dynamic configuration group is removed.
	/// <br/><br/>
	/// Upon success the function returns the index of the registered property
	/// that can be used to lookup the info with GetGlobalPropertyByIndex.
	/// Note that this index may not stay valid after a dynamic config group has been removed, which would reorganize the internal structure. 
	/// </remarks>
	public int RegisterGlobalProperty(byte* declaration, void* pointer) {
		var result = ScriptEngine_RegisterGlobalProperty(this, (sbyte*)declaration, pointer);
		if (result >= 0) return result;
		var ret = (RetCode)result;
		switch (ret) {
			case RetCode.InvalidDeclaration: throw new ArgumentException("The declaration has invalid syntax", nameof(declaration));
			case RetCode.InvalidType: throw new ArgumentException("The declaration is a reference", nameof(declaration));
			case RetCode.InvalidArg: throw new ArgumentException("The pointer is null", nameof(pointer));
			case RetCode.NameTaken: throw new NameTakenException("The name is already taken");
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Returns the number of registered global properties
	/// </summary>
	/// <returns>The number of registered global properties</returns>
	public asUINT GetGlobalPropertyCount() => ScriptEngine_GetGlobalPropertyCount(this);
	/// <summary>
	/// Returns the detail on the registered global property
	/// </summary>
	/// <param name="index">The index of the global variable</param>
	/// <param name="name">Receives the name of the property</param>
	/// <param name="nameSpace">Receives the namespace of the property</param>
	/// <param name="typeId">Receives the typeId of the property</param>
	/// <param name="isConst">Receives the constness indicator of the property</param>
	/// <param name="configGroup">Receives the config group in which the property was registered</param>
	/// <param name="pointer">Receives the pointer of the property</param>
	/// <param name="accessMask">Receives the access mask of the property</param>
	public void GetGlobalPropertyByIndex(asUINT index, byte** name, byte** nameSpace = null, int* typeId = null, bool* isConst = null, byte** configGroup = null, void** pointer = null, asDWORD* accessMask = null) {
		var ret = (RetCode)ScriptEngine_GetGlobalPropertyByIndex(this, index, (sbyte**)name, (sbyte**)nameSpace, typeId, isConst, (sbyte**)configGroup, pointer, accessMask);
		switch (ret) {
			case RetCode.Success: break;
			default: throw ret.GetException();
		}
	}

	/// <summary>
	/// Returns the index of the property
	/// </summary>
	/// <param name="name">The name of the property</param>
	/// <returns>The index of the matching property</returns>
	/// <remarks>
	///	The search for global properties will be performed in the default namespace as given by <see cref="SetDefaultNamespace"/>
	/// unless the name is prefixed with a scope, using the scoping operator ::.
	/// If the scope starts with :: it will be used as the absolute scope, otherwise it will be relative to the default namespace. 
	/// </remarks>
	public int GetGlobalPropertyIndexByName(byte* name) {
		var result = ScriptEngine_GetGlobalPropertyIndexByName(this, (sbyte*)name);
		if (result >= 0) return result;
		var ret = (RetCode)result;
		switch (ret) {
			case RetCode.NoGlobalVar: throw new MissingFieldException("No matching property was found");
			case RetCode.InvalidArg: throw new ArgumentException("The name and scope for search cannot be determined", nameof(name));
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Returns the index of the property
	/// </summary>
	/// <param name="decl">The declaration of the property to search for</param>
	/// <returns>The index of the matching property</returns>
	public int GetGlobalPropertyIndexByDecl(byte* decl) {
		var result = ScriptEngine_GetGlobalPropertyIndexByDecl(this, (sbyte*)decl);
		if (result >= 0) return result;
		var ret = (RetCode)result;
		switch (ret) {
			case RetCode.NoGlobalVar: throw new MissingFieldException("No matching property was found");
			case RetCode.InvalidArg: throw new ArgumentException("The given declaration is invalid", nameof(decl));
			default: throw ret.GetException();
		}
	}
	#endregion

	#region Object types
	/// <summary>
	/// Registers a new object type
	/// </summary>
	/// <param name="obj">The name of the type</param>
	/// <param name="byteSize">The size of the type in bytes. Only necessary for value types</param>
	/// <param name="flags">One or more of the <see cref="asEObjTypeFlags"/></param>
	/// <returns>The type id</returns>
	/// <remarks>
	///	Use this method to register new types that should be available to the scripts.
	/// Reference types, which have their memory managed by the application, should be registered with asOBJ_REF.
	/// Value types, which have their memory managed by the engine, should be registered with asOBJ_VALUE.
	/// </remarks>
	public int RegisterObjectType(byte* obj, int byteSize, asEObjTypeFlags flags) {
		var result = ScriptEngine_RegisterObjectType(this, (sbyte*)obj, byteSize, (UIntPtr)flags);
		if (result >= 0) return result;
		var ret = (RetCode)result;
		switch (ret) {
			case RetCode.InvalidArg: throw new ArgumentException("The flags are invalid", nameof(flags));
			case RetCode.InvalidName: throw new ArgumentException("The name is invalid", nameof(obj));
			case RetCode.AlreadyRegistered: throw new AlreadyRegisteredException("Another type of the same name already exists");
			case RetCode.NameTaken: throw new NameTakenException("The name conflicts with other symbol names");
			case RetCode.LowerArrayDimensionNotRegistered: 
				throw new LowerArrayDimensionNotRegisteredException("When registering an array type the array element must be a primitive or a registered type");
			case RetCode.InvalidType: throw new ArgumentException("The array type was not properly formed");
			case RetCode.NotSupported: throw new NotSupportedException("The array type is not supported, or already in use preventing it from being overloaded");
			default: throw ret.GetException();
		}
	}
	/// <summary>
	/// Registers a property for the object type
	/// </summary>
	/// <param name="obj">The name of the type</param>
	/// <param name="declaration">The property declaration in script syntax</param>
	/// <param name="byteOffset">The offset into the memory block where this property is found</param>
	/// <param name="compositeOffset">The offset to the composite object</param>
	/// <param name="isCompositeIndirect">Set to false if the composite object is inline, and true if it is refered to by pointer</param>
	/// <returns>The index of the property on success, or a negative value on error</returns>
	/// <remarks>
	///	Use this method to register a member property of a class.
	/// The property must be local to the object, i.e. not a global variable or a static member.
	/// The easiest way to get the offset of the property is to use the asOFFSET macro.
	/// <code>
	/// struct MyType {float prop;};
	/// r = engine->RegisterObjectProperty("MyType", "float prop", asOFFSET(MyType, prop)));
	/// </code>
	/// In case the property to be registered is part of a composite member, t
	/// hen the compositeOffset should be used to give the offset to the composite member,
	/// and byteOffset should be the offset to the property in that composite member.
	/// If the composite member is inline then set isCompositeIndirect as false,
	/// else set it to true for proper indirection.
	/// <br/><br/>
	/// The method returns the index of the property upon success.
	/// This can be used to look up the property in the object type with asITypeInfo::GetProperty. 
	/// </remarks>
	public int RegisterObjectProperty(byte* obj, byte* declaration, int byteOffset, int compositeOffset = 0, bool isCompositeIndirect = false) => 
		ScriptEngine_RegisterObjectProperty(this, (sbyte*)obj, (sbyte*)declaration, byteOffset, compositeOffset, isCompositeIndirect);
	/// <summary>
	/// Registers a method for the object type
	/// </summary>
	/// <param name="obj">The name of the type</param>
	/// <param name="declaration">The declaration of the method in script syntax</param>
	/// <param name="funcPointer">The method or function pointer</param>
	/// <param name="callConv">The calling convention for the method or function</param>
	/// <param name="auxiliary">A helper object for use with some calling conventions</param>
	/// <param name="compositeOffset">The offset to the composite object</param>
	/// <param name="isCompositeIndirect">Set to false if the composite object is inline, and true if it is refered to by pointer</param>
	/// <returns>A negative value on error, or the function id if successful</returns>
	/// <remarks>
	///	Use this method to register a member method for the type.
	/// The method that is registered may be an actual class method,
	/// or a global function that takes the object pointer as either the first or last parameter.
	/// Or it may be a global function implemented with the generic calling convention.
	/// <br/><br/>
	/// The auxiliary pointer can optionally be used with <see cref="asECallConvTypes.asCALL_GENERIC"/>.
	/// For the calling conventions <see cref="asECallConvTypes.asCALL_THISCALL"/> and <see cref="asECallConvTypes.asCALL_THISCALL_OBJLAST"/> the auxiliary is required.
	/// <br/><br/>
	/// In case the method to be registered is part of a composite member,
	/// then the compositeOffset should be used to give the offset to the composite member,
	/// and the method pointer should be method of the composite member.
	/// If the composite member is inline then set isCompositeIndirect as false,
	/// else set it to true for proper indirection.
	/// </remarks>
	public int            RegisterObjectMethod(byte* obj, byte* declaration, asSFuncPtr* funcPointer, asECallConvTypes callConv, void* auxiliary = null, int compositeOffset = 0, bool isCompositeIndirect = false) => 
		ScriptEngine_RegisterObjectMethod(this, (sbyte*)obj, (sbyte*)declaration, funcPointer, (asDWORD)callConv, auxiliary, compositeOffset, isCompositeIndirect);
	/// <summary>
	/// Registers a behaviour for the object type
	/// </summary>
	/// <param name="obj">The name of the type</param>
	/// <param name="behaviour">One of the object behaviours from asEBehaviours</param>
	/// <param name="declaration">The declaration of the method in script syntax</param>
	/// <param name="funcPointer">The method or function pointer</param>
	/// <param name="callConv">The calling convention for the method or function</param>
	/// <param name="auxiliary">A helper object for use with some calling conventions</param>
	/// <param name="compositeOffset">The offset to the composite object</param>
	/// <param name="isCompositeIndirect">Set to false if the composite object is inline, and true if it is refered to by pointer</param>
	/// <returns>A negative value on error, or the function id is successful</returns>
	/// <remarks>
	///	Use this method to register behaviour functions that will be called by the virtual machine to perform certain operations,
	/// such as memory management, math operations, comparisons, etc.
	/// <br/><br/>
	/// The declaration must form a valid function signature, but the give function name will not be used or stored in the application
	/// so there is no need to provide a meaningful function name.
	/// <br/><br/>
	/// The auxiliary pointer can optionally be used with <see cref="asECallConvTypes.asCALL_GENERIC"/>.
	/// For the calling conventions <see cref="asECallConvTypes.asCALL_THISCALL_ASGLOBAL"/>,
	/// <see cref="asECallConvTypes.asCALL_THISCALL_OBJFIRST"/> and <see cref="asECallConvTypes.asCALL_THISCALL_OBJLAST"/> the auxiliary is required.
	/// <br/><br/>
	/// In case the method to be registered is part of a composite member, then the compositeOffset should be used to give the offset
	/// to the composite member, and the method pointer should be method of the composite member. If the composite member is inline
	/// then set isCompositeIndirect as false, else set it to true for proper indirection.
	/// </remarks>
	public int            RegisterObjectBehaviour(byte* obj, asEBehaviours behaviour, byte* declaration, asSFuncPtr* funcPointer, asDWORD callConv, void *auxiliary = null, int compositeOffset = 0, bool isCompositeIndirect = false) => 
		ScriptEngine_RegisterObjectBehaviour(this, (sbyte*)obj, behaviour, (sbyte*)declaration, funcPointer, callConv, auxiliary, compositeOffset, isCompositeIndirect);
	/// <summary>
	/// Registers a script interface
	/// </summary>
	/// <param name="name">The name of the interface</param>
	/// <returns>The type id of the interface on success, else a negative value on error</returns>
	/// <remarks>
	///	This registers an interface that script classes can implement.
	/// By doing this the application can register functions and methods that receives an asIScriptObject
	/// and still be sure that the class implements certain methods needed by the application.
	/// </remarks>
	public int            RegisterInterface(byte* name) => ScriptEngine_RegisterInterface(this, (sbyte*)name);
	/// <summary>
	/// Registers a script interface method
	/// </summary>
	/// <param name="intf">The name of the interface</param>
	/// <param name="declaration">The method declaration</param>
	/// <returns>A negative value on error</returns>
	/// <remarks>
	///	This registers a method that the class that implements the script interface must have
	/// </remarks>
	public int            RegisterInterfaceMethod(byte* intf, byte* declaration) => ScriptEngine_RegisterInterfaceMethod(this, (sbyte*)intf, (sbyte*)declaration);
	/// <summary>
	/// Returns the number of registered object types
	/// </summary>
	/// <returns>The number of object types registered by the application</returns>
	public asUINT         GetObjectTypeCount() => ScriptEngine_GetObjectTypeCount(this);
	/// <summary>
	/// Returns the object type interface by index
	/// </summary>
	/// <param name="index">The index of the type</param>
	/// <returns>The registered object type interface for the type, or null if not found</returns>
	public asTypeInfo   *GetObjectTypeByIndex(asUINT index) => ScriptEngine_GetObjectTypeByIndex(this, index);
	#endregion

	#region String factory
	/// <summary>
	/// Registers the string factory
	/// </summary>
	/// <param name="datatype">The datatype that the string factory returns</param>
	/// <param name="factory">The pointer to the factory object</param>
	/// <returns>A negative value on error, or the function id if successful</returns>
	/// <remarks>
	///	Use this function to register a string factory that will be called during compilation to create instances of a string constant.
	/// The string factory will also be used while saving bytecode in order to get the raw string data for serialization.
	/// <br/><br/>
	/// The data type that represents the string type should be informed without reference or handle token, as the script
	/// engine will assume a const reference anyway.
	/// </remarks>
	public int RegisterStringFactory(byte* datatype, IStringFactory factory) {
		var iface = StringFactory_Create(new asSStringFactory {
			getStringConstantFunc = (IntPtr)(delegate*unmanaged[Cdecl]<byte*, uint, void*, void*>)&IStringFactory.StringFactoryGetStringConstant,
			releaseStringConstantFunc = (IntPtr)(delegate*unmanaged[Cdecl]<void*, void*, asERetCodes>)&IStringFactory.StringFactoryReleaseStringConstant,
			getRawStringDataFunc = (IntPtr)(delegate*unmanaged[Cdecl]<void*, sbyte*, uint*, void*, asERetCodes>)&IStringFactory.StringFactoryGetRawStringData,
			destroyFunc = (IntPtr)(delegate*unmanaged[Cdecl]<void*, asERetCodes>)&IStringFactory.StringFactoryDestroy,
			userdata = (void*)GCHandle.ToIntPtr(GCHandle.Alloc(factory, GCHandleType.Normal))
		});
		//FIXME: leak
		return ScriptEngine_RegisterStringFactory(this, (sbyte*)datatype, iface);
	}
		
	//public int GetStringFactory(asDWORD* typeModifiers = null, asStringFactory** factory = 0) => ScriptEngine_(this);
	#endregion

	#region Default array type
	/// <summary>
	/// Registers the type that should be used as the default array
	/// </summary>
	/// <param name="type">The name of the template type, e.g. "array&lt;T&gt;"</param>
	/// <returns>A negative value on error</returns>
	public int RegisterDefaultArrayType(byte* type) => ScriptEngine_RegisterDefaultArrayType(this, (sbyte*)type);
	/// <summary>
	/// Returns the type id of the registered type
	/// </summary>
	/// <returns>The type id, or a negative value on error</returns>
	public int GetDefaultArrayTypeId() => ScriptEngine_GetDefaultArrayTypeId(this);
	#endregion

	#region Enums
	public int          RegisterEnum(byte* type) => ScriptEngine_RegisterEnum(this, (sbyte*)type);
	public int          RegisterEnumValue(byte* type, byte* name, int value) => ScriptEngine_RegisterEnumValue(this, (sbyte*)type, (sbyte*)name, value);
	public asUINT       GetEnumCount() => ScriptEngine_GetEnumCount(this);
	public asTypeInfo *GetEnumByIndex(asUINT index) => ScriptEngine_GetEnumByIndex(this, index);
	#endregion

	#region Funcdefs
	public int          RegisterFuncdef(byte* decl) => ScriptEngine_RegisterFuncdef(this, (sbyte*)decl);
	public asUINT       GetFuncdefCount() => ScriptEngine_GetFuncdefCount(this);
	public asTypeInfo *GetFuncdefByIndex(asUINT index) => ScriptEngine_GetFuncdefByIndex(this, index);
	#endregion

	#region Typedefs
	public int          RegisterTypedef(byte* type, byte* decl) => ScriptEngine_RegisterTypedef(this, (sbyte*)type, (sbyte*)decl);
	public asUINT       GetTypedefCount() => ScriptEngine_GetTypedefCount(this);
	public asTypeInfo *GetTypedefByIndex(asUINT index) => ScriptEngine_GetTypedefByIndex(this, index);
	#endregion

	#region Configuration groups
	public int         BeginConfigGroup(byte* groupName) => ScriptEngine_BeginConfigGroup(this, (sbyte*)groupName);
	public int         EndConfigGroup() => ScriptEngine_EndConfigGroup(this);
	public int         RemoveConfigGroup(byte* groupName) => ScriptEngine_RemoveConfigGroup(this, (sbyte*)groupName);
	public asDWORD     SetDefaultAccessMask(asDWORD defaultMask) => ScriptEngine_SetDefaultAccessMask(this, defaultMask);
	public int         SetDefaultNamespace(byte* nameSpace) => ScriptEngine_SetDefaultNamespace(this, (sbyte*)nameSpace);
	public byte* GetDefaultNamespace() => (byte*)ScriptEngine_GetDefaultNamespace(this);
	#endregion

	#region Script modules
	internal asScriptModule* GetModule(byte* module, asEGMFlags flag = asEGMFlags.asGM_ONLY_IF_EXISTS) => ScriptEngine_GetModule(this, (sbyte*)module, flag);
	public ScriptModule? GetModule() {
		var ptr = GetModule(null);
		if (ptr is null)
			return null;
		return ScriptModule.FromPtr(ptr, true, true);
	}

	public ScriptModule GetOrCreateModule() {
		var ptr = GetModule(null, asEGMFlags.asGM_CREATE_IF_NOT_EXISTS);
		return ScriptModule.FromPtr(ptr, true, true);
	}
	
	public ScriptModule CreateModule() {
		var ptr = GetModule(null, asEGMFlags.asGM_ALWAYS_CREATE);
		return ScriptModule.FromPtr(ptr, true, true);
	}
	public ScriptModule? GetModule(string module) {
		fixed (char* modPtr = module) {
			var ptr = GetModule((byte*)modPtr);
			if (ptr is null)
				return null;
			return ScriptModule.FromPtr(ptr, true, true);
		}
	}

	public ScriptModule GetOrCreateModule(string module) {
		fixed (char* modPtr = module) {
			var ptr = GetModule((byte*)modPtr, asEGMFlags.asGM_CREATE_IF_NOT_EXISTS);
			return ScriptModule.FromPtr(ptr, true, true);
		}
	}
	
	public ScriptModule CreateModule(string module) {
		fixed (char* modPtr = module) {
			var ptr = GetModule((byte*)modPtr, asEGMFlags.asGM_ALWAYS_CREATE);
			return ScriptModule.FromPtr(ptr, true, true);
		}
	}
	
	public int DiscardModule(byte* module) => ScriptEngine_DiscardModule(this, (sbyte*)module);
	public asUINT GetModuleCount() => ScriptEngine_GetModuleCount(this);
	public asScriptModule* GetModuleByIndex(asUINT index) => ScriptEngine_GetModuleByIndex(this, index);
	#endregion

	#region Script functions
	public int                GetLastFunctionId() => ScriptEngine_GetLastFunctionId(this);
	public asScriptFunction *GetFunctionById(int funcId) => ScriptEngine_GetFunctionById(this, funcId);
	#endregion

	#region Type identification
	public int GetTypeIdByDecl(byte* decl) => ScriptEngine_GetTypeIdByDecl(this, (sbyte*)decl);
	public byte* GetTypeDeclaration(int typeId, bool includeNamespace = false) => (byte*)ScriptEngine_GetTypeDeclaration(this, typeId, includeNamespace);
	public int GetSizeOfPrimitiveType(int typeId) => ScriptEngine_GetSizeOfPrimitiveType(this, typeId);
	public asTypeInfo* GetTypeInfoById(int typeId) => ScriptEngine_GetTypeInfoById(this, typeId);
	public asTypeInfo* GetTypeInfoByName(byte* name) => ScriptEngine_GetTypeInfoByName(this, (sbyte*)name);
	public asTypeInfo* GetTypeInfoByDecl(byte* decl) => ScriptEngine_GetTypeInfoByDecl(this, (sbyte*)decl);
	#endregion

	#region Script execution
	internal asScriptContext* CreateContextRaw() => ScriptEngine_CreateContext(this);
	public ScriptContext CreateContext() {
		var result = CreateContextRaw();
		return ScriptContext.FromPtr(result, true, true);
	}
	public void* CreateScriptObject(asTypeInfo* type) => ScriptEngine_CreateScriptObject(this, type);
	public void* CreateScriptObjectCopy(void *obj, asTypeInfo* type) => ScriptEngine_CreateScriptObjectCopy(this, obj, type);
	public void* CreateUninitializedScriptObject(asTypeInfo* type) => ScriptEngine_CreateUninitializedScriptObject(this, type);
	public asScriptFunction* CreateDelegate(asScriptFunction* func, void* obj) => ScriptEngine_CreateDelegate(this, func, obj);
	public int AssignScriptObject(void* dstObj, void* srcObj, asTypeInfo* type) => ScriptEngine_AssignScriptObject(this, dstObj, srcObj, type);
	public void ReleaseScriptObject(void* obj, asTypeInfo* type) => ScriptEngine_ReleaseScriptObject(this, obj, type);
	public void AddRefScriptObject(void* obj, asTypeInfo* type) => ScriptEngine_AddRefScriptObject(this, obj, type);
	public int RefCastObject(void* obj, asTypeInfo* fromType, asTypeInfo* toType, void** newPtr, bool useOnlyImplicitCast = false) => 
		ScriptEngine_RefCastObject(this, obj, fromType, toType, newPtr, useOnlyImplicitCast);
	public asLockableSharedBool* GetWeakRefFlagOfScriptObject(void* obj, asTypeInfo* type) => ScriptEngine_GetWeakRefFlagOfScriptObject(this, obj, type);
	#endregion

	#region Context pooling
	public asScriptContext* RequestContext() => ScriptEngine_RequestContext(this);
	public void ReturnContext(asScriptContext *ctx) => ScriptEngine_ReturnContext(this, ctx);
	public int SetContextCallbacks(IntPtr requestCtx, IntPtr returnCtx, void* param = null) => 
		ScriptEngine_SetContextCallbacks(this, requestCtx, returnCtx, param);
	#endregion

	#region String interpretation
	public asETokenClass ParseToken(byte* @string, UIntPtr stringLength = 0, asUINT* tokenLength = null) => 
		ScriptEngine_ParseToken(this, (sbyte*)@string, stringLength, tokenLength);
	#endregion

	#region Garbage collection
	public int  GarbageCollect(asEGCFlags flags = asEGCFlags.asGC_FULL_CYCLE, asUINT numIterations = 1) => 
		ScriptEngine_GarbageCollect(this, (asDWORD)flags, numIterations);
	public void GetGCStatistics(asUINT *currentSize, asUINT *totalDestroyed = null, asUINT *totalDetected = null, asUINT *newObjects = null, asUINT *totalNewDestroyed = null) => 
		ScriptEngine_GetGCStatistics(this, currentSize, totalDestroyed, totalDetected, newObjects, totalNewDestroyed);
	public int  NotifyGarbageCollectorOfNewObject(void *obj, asTypeInfo *type) => 
		ScriptEngine_NotifyGarbageCollectorOfNewObject(this, obj, type);
	public int  GetObjectInGC(asUINT idx, asUINT *seqNbr = null, void **obj = null, asTypeInfo **type = null) => 
		ScriptEngine_GetObjectInGC(this, idx, seqNbr, obj, type);
	public void GCEnumCallback(void *reference) => ScriptEngine_GCEnumCallback(this, reference);
	public void ForwardGCEnumReferences(void* @ref, asTypeInfo* type) => ScriptEngine_ForwardGCEnumReferences(this, @ref, type);
	public void ForwardGCReleaseReferences(void* @ref, asTypeInfo* type) => ScriptEngine_ForwardGCEnumReferences(this, @ref, type);
	public void SetCircularRefDetectedCallback(IntPtr callback, void *param = null) => ScriptEngine_SetCircularRefDetectedCallback(this, callback, param);
	#endregion

	#region User data
	/// <summary>
	/// Register the memory address of some user data
	/// </summary>
	/// <param name="data">A pointer to the user data</param>
	/// <param name="type">An identifier specifying the user data to set</param>
	/// <returns>The previous pointer stored in the engine</returns>
	/// <remarks>
	///	This method allows the application to associate a value, e.g. a pointer, with the engine instance.
	/// <br/><br/>
	/// The type values 1000 through 1999 are reserved for use by the official add-ons.
	/// <br/><br/>
	/// The type value 2000 are reserved for use by the bindings.
	/// <br/><br/>
	/// Optionally, an event can be subscribed to clean up the user data when the engine is destroyed. 
	/// </remarks>
	public IntPtr SetUserDataPtr(IntPtr data, asPWORD type = 0) => (IntPtr)ScriptEngine_SetUserData(this, (void*)data, type);
	/// <summary>
	/// Returns the address of the previously registered user data
	/// </summary>
	/// <param name="type">An identifier specifying the user data to get</param>
	/// <returns>The pointer to the user data</returns>
	public IntPtr GetUserDataPtr(asPWORD type = 0) => (IntPtr)ScriptEngine_GetUserData(this, type);
	
	public delegate void CleanEngine(ScriptEngine engine);
	public delegate void CleanModule(ScriptModule module); 
	public delegate void CleanContext(ScriptContext context); 
	public delegate void CleanFunction(ScriptFunction function); 
	public delegate void CleanTypeInfo(TypeInfo typeInfo);
	public delegate void CleanObject(ScriptObject @object);
	
	#region UserData Native Callbacks
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnEngineUserDataCleanup(asScriptEngine* enginePtr) {
		ScriptEngine engine;
		try {
			engine = ScriptEngine.FromPtr(enginePtr);
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while getting the ScriptEngine object. The data will NOT be cleaned up.\n{0}", e);
			return;
		}
		try {
			var handle = GCHandle.FromIntPtr(engine.GetUserDataPtr(2000));
			handle.Free();
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while invoking the ScriptModule clean up event. The data may NOT be cleaned up.\n{0}", e);
			return;
		}
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnModuleUserDataCleanup(asScriptModule* ptr) {
		ScriptModule obj;
		ScriptEngine engine;
		try {
			obj = ScriptModule.FromPtr(ptr);
			engine = obj.Engine;
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while getting the ScriptModule object. The data will NOT be cleaned up.\n{0}", e);
			return;
		}
		try {
			var handle = GCHandle.FromIntPtr(obj.GetUserDataPtr(2000));
			handle.Free();
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while invoking the ScriptModule clean up event. The data may NOT be cleaned up.\n{0}", e);
		}
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnFunctionUserDataCleanup(asScriptFunction* ptr) {
		ScriptFunction obj;
		ScriptEngine engine;
		try {
			obj = ScriptFunction.FromPtr(ptr);
			engine = obj.Engine;
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while getting the ScriptFunction object. The data will NOT be cleaned up.\n{0}", e);
			return;
		}
		try {
			var handle = GCHandle.FromIntPtr(obj.GetUserDataPtr(2000));
			handle.Free();
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while invoking the ScriptFunction clean up event. The data may NOT be cleaned up.\n{0}", e);
		}
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnContextUserDataCleanup(asScriptContext* ptr) {
		ScriptContext obj;
		ScriptEngine engine;
		try {
			obj = ScriptContext.FromPtr(ptr);
			engine = obj.Engine;
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while getting the ScriptContext object. The data will NOT be cleaned up.\n{0}", e);
			return;
		}
		try {
			var handle = GCHandle.FromIntPtr(obj.GetUserDataPtr(2000));
			handle.Free();
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while invoking the ScriptContext clean up event. The data may NOT be cleaned up.\n{0}", e);
		}
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnTypeInfoUserDataCleanup(asTypeInfo* ptr) {
		TypeInfo obj;
		ScriptEngine engine;
		try {
			obj = TypeInfo.FromPtr(ptr);
			engine = obj.Engine;
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while getting the TypeInfo object. The data will NOT be cleaned up.\n{0}", e);
			return;
		}
		try {
			var handle = GCHandle.FromIntPtr(obj.GetUserDataPtr(2000));
			handle.Free();
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while invoking the TypeInfo clean up event. The data may NOT be cleaned up.\n{0}", e);
		}
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnObjectUserDataCleanup(asScriptObject* ptr) {
		ScriptObject obj;
		ScriptEngine engine;
		try {
			obj = ScriptObject.FromPtr(ptr);
			engine = obj.Engine;
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while getting the ScriptEngine object. The data will NOT be cleaned up.\n{0}", e);
			return;
		}
		try {
			var handle = GCHandle.FromIntPtr(obj.GetUserDataPtr(2000));
			handle.Free();
		}
		catch (Exception e) {
			Console.Error.WriteLine("An exception occured while invoking the clean up event. The data may NOT be cleaned up.\n{0}", e);
		}
	}
	#endregion
	
	/// <summary>
	/// Set the function that should be called when the engine is destroyed
	/// </summary>
	/// <param name="callback">A pointer to the function</param>
	/// <param name="type">An identifier specifying which user data the callback is to be used with</param>
	/// <remarks>
	/// The function given with this call will be invoked when the engine is destroyed if any user data has been registered with the engine.
	/// <br/><br/>
	/// The function is called from within the engine destructor, so the callback should not be used for anything but cleaning up the user data itself. 
	/// </remarks>
	public void SetEngineUserDataPtrCleanupCallback(delegate*unmanaged[Cdecl]<asScriptEngine*,void> callback, asPWORD type = 0) => 
		ScriptEngine_SetEngineUserDataCleanupCallback(this, (IntPtr)callback, type);
	/// <summary>
	/// Set the function that should be called when the module is destroyed
	/// </summary>
	/// <param name="callback">A pointer to the function</param>
	/// <param name="type">An identifier specifying which user data the callback is to be used with</param>
	/// <remarks>
	///	The function given with this call will be invoked when the module is destroyed if any user data has been registered with the module.
	/// <br/><br/>
	/// The function is called from within the module destructor, so the callback should not be used for anything but cleaning up the user data itself. 
	/// </remarks>
	public void  SetModuleUserDataPtrCleanupCallback(delegate*unmanaged[Cdecl]<asScriptModule*,void> callback, asPWORD type = 0) => 
		ScriptEngine_SetModuleUserDataCleanupCallback(this, (IntPtr)callback, type);
	/// <summary>
	/// Set the function that should be called when a context is destroyed
	/// </summary>
	/// <param name="callback"></param>
	/// <param name="type"></param>
	public void  SetContextUserDataPtrCleanupCallback(delegate*unmanaged[Cdecl]<asScriptContext*,void> callback, asPWORD type = 0) => 
		ScriptEngine_SetContextUserDataCleanupCallback(this, (IntPtr)callback, type);
	/// <summary>
	/// Set the function that should be called when a function is destroyed
	/// </summary>
	/// <param name="callback"></param>
	/// <param name="type"></param>
	public void  SetFunctionUserDataPtrCleanupCallback(delegate*unmanaged[Cdecl]<asScriptFunction*,void> callback, asPWORD type = 0) => 
		ScriptEngine_SetFunctionUserDataCleanupCallback(this, (IntPtr)callback, type);
	/// <summary>
	/// Set the function that should be called when a type info is destroyed
	/// </summary>
	/// <param name="callback"></param>
	/// <param name="type"></param>
	public void  SetTypeInfoUserDataPtrCleanupCallback(delegate*unmanaged[Cdecl]<asTypeInfo*,void> callback, asPWORD type = 0) => 
		ScriptEngine_SetTypeInfoUserDataCleanupCallback(this, (IntPtr)callback, type);
	/// <summary>
	/// Set the function that should be called when a script object is destroyed
	/// </summary>
	/// <param name="callback"></param>
	/// <param name="type"></param>
	public void  SetScriptObjectUserDataPtrCleanupCallback(delegate*unmanaged[Cdecl]<asScriptObject*,void> callback, asPWORD type = 0) => 
		ScriptEngine_SetScriptObjectUserDataCleanupCallback(this, (IntPtr)callback, type);

	//TODO: figure out a better way to store those stuff, since it will be accessible only in managed realm and it can be lost on managed to native to managed transition
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

	#region Exception handling
	/// <summary>
	/// Register the exception translation callback
	/// </summary>
	/// <param name="callback">The callback function/method that should be called upon an exception</param>
	/// <param name="param">A user defined parameter, or the object pointer on which the callback is called</param>
	/// <param name="callConv">The calling convention of the callback function/method</param>
	/// <returns>A negative value on error</returns>
	public int SetTranslateAppExceptionCallback(asSFuncPtr* callback, void *param, int callConv) => 
		ScriptEngine_SetTranslateAppExceptionCallback(this, callback, param, callConv);
	#endregion
}