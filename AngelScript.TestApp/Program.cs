using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AngelScript;
using AngelScript.Interop;

internal unsafe class Program {
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void MessageCallback(asSMessageInfo* msg, void *param) {
        var type = "ERR ";
        if( msg->type == asEMsgType.asMSGTYPE_WARNING ) 
            type = "WARN";
        else if( msg->type == asEMsgType.asMSGTYPE_INFORMATION ) 
            type = "INFO";
        var section = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(msg->section));
        var message = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(msg->message));
        Console.WriteLine($"{section} ({msg->row}, {msg->col}) {type} : {message}");
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static asERetCodes StringFactoryReleaseStringConstant(void* str, void* userdata) {
        if (str is null)
            return asERetCodes.asERROR;

        var handle = GCHandle.FromIntPtr((IntPtr)str);
        handle.Free();
        return asERetCodes.asSUCCESS;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void* StringFactoryGetStringConstant(byte* data, uint length, void* userdata) {
        Console.Write($"0x{(IntPtr)data:x} {length} 0x{(IntPtr)userdata:x} ");
        var str = Encoding.UTF8.GetString(data, (int)length);
        var handle = GCHandle.Alloc(str, GCHandleType.Pinned);
        var ptr = GCHandle.ToIntPtr(handle);
        Console.WriteLine($" 0x{ptr:x}");
        return (void*)ptr; //0x7f078baa15f1
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static asERetCodes StringFactoryGetRawStringData(void* str, sbyte* data, uint* length, void* userdata) {
        if (str is null)
            return asERetCodes.asERROR;

        var strHandle = GCHandle.FromIntPtr((IntPtr)str);
        var strObj = strHandle.Target;
        if (strObj is null || strObj is not string sharpStr)
            return asERetCodes.asERROR;
        if (length is not null) 
            *length = (uint)sharpStr.Length;

        if (data is not null) {
            var bytes = Encoding.UTF8.GetBytes(sharpStr);
            fixed(byte* ptr = bytes)
                Unsafe.CopyBlock(data, ptr, *length);
        }

        return asERetCodes.asSUCCESS;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static asERetCodes StringFactoryDestroy(void* userdata) {
        return asERetCodes.asSUCCESS;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static IntPtr ConstructString() {
        var str = "";
        var handle = GCHandle.Alloc(str, GCHandleType.Normal);
        return GCHandle.ToIntPtr(handle);
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static IntPtr* AssignString(IntPtr* other, IntPtr* self) {
        *other = *self;
        return other;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static void DestructString(IntPtr* thisPointer) {
        var handle = GCHandle.FromIntPtr(*thisPointer);
        handle.Free();
    }

    private static void RegisterStdString(ScriptEngine engine) {
        var strName = "string"u8;
        var flags = asEObjTypeFlags.asOBJ_REF | asEObjTypeFlags.asOBJ_NOCOUNT;
    fixed (byte* ptr = strName) {
            Debug.Assert(As.ScriptEngine_RegisterObjectType(engine, (sbyte*)ptr, Unsafe.SizeOf<string>(), (uint)flags) >= 0);
            var factory = As.StringFactory_Create(new asSStringFactory {
                destroyFunc = (IntPtr)(delegate*unmanaged[Cdecl]<void*, asERetCodes>)&StringFactoryDestroy,
                getRawStringDataFunc = (IntPtr)(delegate*unmanaged[Cdecl]<void*, sbyte*, uint*, void*, asERetCodes>)&StringFactoryGetRawStringData,
                releaseStringConstantFunc = (IntPtr)(delegate*unmanaged[Cdecl]<void*, void*, asERetCodes>)&StringFactoryReleaseStringConstant,
                getStringConstantFunc = (IntPtr)(delegate*unmanaged[Cdecl]<byte*, uint, void*, void*>)&StringFactoryGetStringConstant
            });
            Debug.Assert(As.ScriptEngine_RegisterStringFactory(engine, (sbyte*)ptr, factory) >= 0);
            fixed (byte* decl = "string@ f()"u8) {
                var constructPtr = asSFuncPtr.asFunctionPtr((IntPtr)(delegate* unmanaged[Cdecl] <IntPtr>)&ConstructString);
                //var destructPtr = asSFuncPtr.asFunctionPtr((IntPtr)(delegate* unmanaged[Cdecl] <IntPtr*, void>)&DestructString);
                Debug.Assert(As.ScriptEngine_RegisterObjectBehaviour(engine, (sbyte*)ptr, asEBehaviours.asBEHAVE_FACTORY, (sbyte*)decl, &constructPtr, (uint)asECallConvTypes.asCALL_CDECL, null, 0, false)>=0);
            }
            fixed (byte* decl = "string& opAssign(string& in)"u8) {
                var assignPtr = asSFuncPtr.asFunctionPtr((IntPtr)(delegate* unmanaged[Cdecl] <IntPtr*,IntPtr*,IntPtr*>)&AssignString);
                Debug.Assert(As.ScriptEngine_RegisterObjectMethod(engine, (sbyte*)ptr, (sbyte*)decl, &assignPtr, (uint)asECallConvTypes.asCALL_CDECL_OBJLAST, null, 0, false)>=0);
            }
            //As.ScriptEngine_RegisterObjectMethod(engine, ptr, "string &opAssign(const string &in)", asMETHODPR(string, operator =, (const string&), string&), asCALL_THISCALL);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void PrintString(void* ptr) {
        var handle = GCHandle.FromIntPtr((IntPtr)ptr);
        if (handle.Target is string str)
            Console.WriteLine(str);
    }
    private static void PrintString2(void* ptr) {
        var handle = GCHandle.FromIntPtr((IntPtr)ptr);
        if (handle.Target is string str)
            Console.WriteLine(str);
    }
    
    private static void FloatPrinter(float meow) {
        //throw new Exception("Test error");
        Console.WriteLine($"The application requested to print {meow}");
    }
    
    private static void ConfigureEngine(ScriptEngine engine) {
        // Register the script string type
        // Look at the implementation for this function for more information  
        // on how to register a custom string type, and other object types.
        //RegisterStdString(engine);
        var printFuncPtr = asSFuncPtr.asFunctionPtr((IntPtr)(delegate* unmanaged[Cdecl] <void*, void>)&PrintString);
        //engine.RegisterGlobalFunction("void Print(string@)", PrintString2);
        engine.RegisterGlobalFunction("void FloatPrinter(float meow)", FloatPrinter);

        // It is possible to register the functions, properties, and types in 
        // configuration groups as well. When compiling the scripts it then
        // be defined which configuration groups should be available for that
        // script. If necessary a configuration group can also be removed from
        // the engine, so that the engine configuration could be changed 
        // without having to recompile all the scripts.
    }
    
    public static void CompileScript(ScriptEngine engine) {
        
		// We will load the script from a file on the disk.
        var script = File.ReadAllBytes("script.as");

		// Add the script sections that will be compiled into executable code.
		// If we want to combine more than one file into the same script, then 
		// we can call AddScriptSection() several times for the same module and
		// the script engine will treat them all as if they were one. The script
		// section name, will allow us to localize any errors in the script code.
        ScriptModule mod;
        mod = engine.CreateModule();
        mod.AddScriptSection("script", script);
		
		// Compile the script. If there are any compiler messages they will
		// be written to the message stream that we set right after creating the 
		// script engine. If there are no errors, and no warnings, nothing will
		// be written to the stream.
        mod.Build();

		// The engine doesn't keep a copy of the script sections after Build() has
		// returned. So if the script needs to be recompiled, then all the script
		// sections must be added again.

		// If we want to have several scripts executing at different times but 
		// that have no direct relation with each other, then we can compile them
		// into separate script modules. Each module use their own namespace and 
		// scope, so function names, and global variables will not conflict with
		// each other.
	}
    
    public static void Main(string[] args) {
        var engine = AngelScript.AngelScript.CreateScriptEngine(23900);
        var ptr = asSFuncPtr.asFunctionPtr((IntPtr)(delegate* unmanaged[Cdecl] <asSMessageInfo*, void*, void>)&MessageCallback);
        engine.SetMessageCallback(&ptr, null, (uint)asECallConvTypes.asCALL_CDECL);
        ConfigureEngine(engine);
        CompileScript(engine);
        var ctx = engine.CreateContext();
        //SetLineCallback
        var mod = engine.GetModule();
        if (mod is null)
            throw new Exception("the module is null");
        using var func = mod.GetFunctionByDecl("float calc(float,float)");
        if (func is null)
            throw new Exception("function haven't been found");
        
        // Prepare the script context with the function we wish to execute. Prepare()
        // must be called on the context before each new script function that will be
        // executed. Note, that if you intend to execute the same function several 
        // times, it might be a good idea to store the function returned by 
        // GetFunctionByDecl(), so that this relatively slow call can be skipped.
        ctx.Prepare(func);
        
        // Now we need to pass the parameters to the script function. 
        ctx.SetArgFloat(0, 3.14159265359f);
        ctx.SetArgFloat(1, 2.71828182846f);

        // Set the timeout before executing the function. Give the function 1 sec
        // to return before we'll abort it.
        // timeOut = timeGetTime() + 1000;

        // Execute the function
        Console.WriteLine("Executing the script.");
        Console.WriteLine("---");
        var state = (ContextState)ctx.Execute();
        Console.WriteLine("---");
        if( state != ContextState.Finished) {
            // The execution didn't finish as we had planned. Determine why.
            if (state == ContextState.Aborted)
                Console.WriteLine("The script was aborted before it could finish. Probably it timed out.");
            else if (state == ContextState.Exception) {
                Console.WriteLine("The script ended with an exception.");

                // Write some information about the script exception
                using var func1 = ctx.GetExceptionFunction();
                if (func1 is null)
                    return;
                sbyte* scriptSectionPtr = null;
                var info = ctx.GetExceptionInfo();
                var decl = func1.GetDeclaration();
                var modl = func1.GetModuleName();
                var desc = ctx.GetExceptionString();
                Console.WriteLine($"func: {decl}");
                Console.WriteLine($"modl: {modl}");
                Console.WriteLine($"sect: {info.SectionName}");
                Console.WriteLine($"line: {info.Line} col: {info.Column}");
                Console.WriteLine($"desc: {desc}");
            }
            else
                Console.WriteLine($"The script ended for some unforeseen reason ({state}).");
        }
        else {
            // Retrieve the return value from the context
            var returnValue = ctx.GetReturnFloat();
            Console.Write("The script function returned: ");
            Console.WriteLine(returnValue);
        }
        // We must release the contexts when no longer using them
        ctx.Dispose();

        // Shut down the engine
        engine.ShutDownAndDispose();
    }
}