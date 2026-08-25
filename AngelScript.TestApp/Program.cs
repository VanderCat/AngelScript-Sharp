using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AngelScript;
using AngelScript.Interop;

internal unsafe class Program {
    // [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void MessageCallback(MessageInfo msg) {
        var type = "ERR ";
        if( msg.Type == MsgType.Warning ) 
            type = "WARN";
        else if( msg.Type == MsgType.Information ) 
            type = "INFO";
        Console.WriteLine($"{msg.Section} ({msg.Row}:{msg.Column}) {type} : {msg.Message}");
    }
    
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static void ConstructString(void** mem) {
        var str = "";
        var handle = GCHandle.Alloc(str, GCHandleType.Normal);
        *mem = (void*)GCHandle.ToIntPtr(handle);
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static IntPtr* AssignString(IntPtr* source, IntPtr* from) {
        if (GCHandle.FromIntPtr(*source).Target is not string other || GCHandle.FromIntPtr(*from).Target is not string self)
            return null;
        Console.WriteLine($"{other} = {self}");
        return from;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static void DestructString(void** thisPointer) {
        var handle = GCHandle.FromIntPtr((IntPtr)(*thisPointer));
        handle.Free();
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static void ConstructStringUtf16(char** mem) {
        var str = (char*)AngelScript.AngelScript.UnmanagedMemory.AllocMem(2);
        str[0] = '\0';
        *mem = str;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static void AssignStringUtf16(asScriptGeneric* gen) {
        char** a = (char**)As.ScriptGeneric_GetArgObject(gen, 0);
        char** self = (char**)As.ScriptGeneric_GetObject(gen);
        *self = *a;
        As.ScriptGeneric_SetReturnAddress(gen, self);
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    static void DestructStringUtf16(void** thisPointer) {
        AngelScript.AngelScript.UnmanagedMemory.FreeMem(*thisPointer);
    }

    class StringFactory : IStringFactory {
        public void* GetStringConstant(ReadOnlySpan<char> data) {
            var str = new string(data);
            var handle = GCHandle.Alloc(str);
            return (void*)GCHandle.ToIntPtr(handle);
        }

        public RetCode ReleaseStringConstant(void* str) {
            if (str is null)
                return RetCode.InvalidArg;
            var handle = GCHandle.FromIntPtr((IntPtr)str);
            handle.Free();
            return RetCode.Success;
        }

        public RetCode GetRawStringData(void* str, char* data, uint* length) {
            if (str is null)
                return RetCode.InvalidObject;

            var strHandle = GCHandle.FromIntPtr((IntPtr)str);
            if (strHandle.Target is not string sharpStr)
                return RetCode.Error;
            if (length is not null) 
                *length = (uint)sharpStr.Length;

            if (data is not null) {
                var span = sharpStr.AsSpan();
                fixed(char* ptr = span)
                    Unsafe.CopyBlock(data, ptr, *length*2);
            }

            return RetCode.Success;
        }
    }
    
    class StringFactoryUTF16Raw : IStringFactory {
        public void* GetStringConstant(ReadOnlySpan<char> data) {
            var str = (char*)AngelScript.AngelScript.UnmanagedMemory.AllocMem((nuint)data.Length*2+2);
            fixed(char* ptr = data)
                Unsafe.CopyBlockUnaligned(str, ptr, (uint)data.Length*2);
            str[data.Length] = '\0';
            return str;
        }

        public RetCode ReleaseStringConstant(void* str) {
            AngelScript.AngelScript.UnmanagedMemory.FreeMem(str);
            return RetCode.Success;
        }

        public RetCode GetRawStringData(void* str, char* data, uint* length) {
            if (str is null)
                return RetCode.InvalidObject;
            var stuff = MemoryMarshal.CreateReadOnlySpanFromNullTerminated((char*)str);
            if (length is not null) 
                *length = (uint)stuff.Length*2;

            if (data is not null) 
                Unsafe.CopyBlockUnaligned(data, str, *length);

            return RetCode.Success;
        }
    }

    private static void RegisterStdString(ScriptEngine engine) {
        var strName = "string"u8;
        var flags = ObjectTypeFlags.Value | ObjectTypeFlags.AppClassCA;
        fixed (byte* ptr = strName) {
            engine.RegisterObjectType(ptr, Unsafe.SizeOf<IntPtr>(), flags);
            engine.RegisterStringFactory(ptr, new StringFactoryUTF16Raw());
            fixed (byte* decl = "void f()"u8) {
                var constructPtr = asSFuncPtr.FromUnmanagedCallersOnly<Program>(nameof(ConstructStringUtf16));
                engine.RegisterObjectBehaviour(ptr, Behaviour.Construct, decl, &constructPtr, CallConvTypes.CDeclObjLast);
                var destructPtr = asSFuncPtr.FromUnmanagedCallersOnly<Program>(nameof(DestructStringUtf16));
                engine.RegisterObjectBehaviour(ptr, Behaviour.Destruct, decl, &destructPtr, CallConvTypes.CDeclObjLast);
            }
            fixed (byte* decl = "string& opAssign(const string &in other)"u8) {
                var assignPtr = asSFuncPtr.FromUnmanagedCallersOnly<Program>(nameof(AssignStringUtf16));
                engine.RegisterObjectMethod(ptr, decl, &assignPtr, CallConvTypes.Generic);
            }
            //As.ScriptEngine_RegisterObjectMethod(engine, ptr, "string &opAssign(const string &in)", asMETHODPR(string, operator =, (const string&), string&), asCALL_THISCALL);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void PrintString(char* ptr) {
        Console.WriteLine(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr).ToString());
    }
    private static void PrintString2(string str) {
        Console.WriteLine(str);
    }
    
    private static void FloatPrinter(float meow) {
        //throw new Exception("Test error");
        Console.WriteLine($"The application requested to print {meow}");
    }
    
    private static void ConfigureEngine(ScriptEngine engine) {
        engine.SetEngineProperty(EngineProperty.StringEncoding, 1); //CSharp strings use UTF-16
        // Register the script string type
        // Look at the implementation for this function for more information  
        // on how to register a custom string type, and other object types.
        RegisterStdString(engine);
        var printFuncPtr = asSFuncPtr.FromUnmanagedCallersOnly<Program>(nameof(PrintString));
        fixed (byte* decl = "void print(string &in)\0"u8)
            engine.RegisterGlobalFunction(decl, &printFuncPtr, asECallConvTypes.asCALL_CDECL, null);
        engine.RegisterGlobalFunction(FloatPrinter);

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
        NativeLibrary.SetDllImportResolver(Assembly.GetAssembly(typeof(AngelScript.Interop.As)),
            (name, assembly, path) => {
                return NativeLibrary.Load(Path.Join(Directory.GetCurrentDirectory(), "runtimes/linux-x64/native/lib" + name + ".so"));
            });
        var engine = AngelScript.AngelScript.CreateScriptEngine();
        engine.SetMessageCallback(MessageCallback);
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