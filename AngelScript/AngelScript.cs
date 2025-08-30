using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

public static unsafe class AngelScript {
    public const int VERSION = 23900;
    /// <summary>
    /// Creates the script engine
    /// </summary>
    /// <param name="version">The library version. Should always be <see cref="VERSION"/></param>
    /// <returns>A script engine interface, or null on error</returns>
    /// <remarks>
    /// Call this function to create a new script engine. When you're done with the script engine, i.e.
    /// after you've executed all your scripts, you should call <see cref="ScriptEngine.ShutDownAndRelease"/> to cleanup
    /// any objects that may still be alive and free the engine object.
    /// <br/><br/>
    /// The version argument is there to allow AngelScript to validate that the application has been paired
    /// with the correct interface. This is especially important when linking dynamically against the library.
    /// If the version is incorrect a null pointer is returned. 
    /// </remarks>
    public static ScriptEngine CreateScriptEngine(uint version = VERSION) {
        var ptr = As.CreateScriptEngine(version);
        if (ptr is null)
            throw new Exception("Failed to start scripting engine");
        return ScriptEngine.FromPtr(ptr, false, true);
    }

    /// <summary>
    /// The currently active context
    /// </summary>
    /// <remarks>
    /// This property is most useful, as it allows to obtain a pointer
    /// to the context that is calling the function, and through that get the engine, or custom user data.
    /// <br/><br/>
    /// If the script library is compiled with multithread support, this function will return the context
    /// that is currently active in the thread that is being executed. It will thus work even if there are
    /// multiple threads executing scripts at the same time.
    /// <br/><br/>
    /// This function does not increase the reference count of the context. 
    /// </remarks>
    public static ScriptContext? ActiveContext {
        get {
            var ptr = GetActiveContext();
            if (ptr is null)
                return null;
            return new ScriptContext(ptr);
            //TODO:
            //return ScriptContext.FromPtr(ptr);
        }
    }

    /// <summary>
    /// The version of the compiled library
    /// </summary>
    /// <remarks>
    /// The returned string can be used for presenting the library version in a log file, or in the GUI. 
    /// </remarks>
    public static string Version {
        get {
            var ver = (byte*)GetLibraryVersion();
            var span = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ver);
            return Encoding.UTF8.GetString(span);
        }
    }

    /// <summary>
    /// The options used to compile the library
    /// </summary>
    /// <remarks>
    /// This can be used to identify at run-time different ways to configure the engine.
    /// For example, if the returned string contain the identifier AS_MAX_PORTABILITY then functions
    /// and methods must be registered with the asCALL_GENERIC calling convention
    /// </remarks>
    public static string Options {
        get {
            var ver = (byte*)GetLibraryOptions();
            var span = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ver);
            return Encoding.UTF8.GetString(span);
        }
    }

    public static class UnmanagedMemory {
        /// <summary>
        /// Allocate memory using the memory function registered with AngelScript
        /// </summary>
        /// <param name="size">The size of the buffer to allocate </param>
        /// <returns>A pointer to the allocated buffer, or null on error</returns>
        public static void* AllocMem(nuint size) => As.AllocMem(size);
        /// <summary>
        /// Deallocates memory using the memory function registered with AngelScript
        /// </summary>
        /// <param name="ptr">A pointer to the buffer to deallocate</param>
        public static void FreeMem(void* ptr) => As.FreeMem(ptr);
        
        private static Func<nuint, IntPtr>? _alloc;
        private static Action<IntPtr>? _free;

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static void* NativeAlloc(nuint size) => (void*)_alloc!(size);
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        public static void NativeFree(void* ptr) => _free!((IntPtr)ptr);
        
        /// <summary>
        /// Set the memory management functions that AngelScript should use
        /// </summary>
        /// <param name="alloc">The function that will be used to allocate memory</param>
        /// <param name="free">The function that will be used to free the memory</param>
        /// <remarks>
        /// Call this method to register the global memory allocation and deallocation functions that AngelScript should
        /// use for memory management. This function should be called before <see cref="AngelScript.CreateScriptEngine"/>.
        /// <br/><br/>
        /// If not called, AngelScript will use the malloc and free functions from the standard C library. 
        /// </remarks>
        public static void SetGlobalFunctions(Func<nuint, IntPtr> alloc, Action<IntPtr> free) {
            _alloc = alloc;
            _free = free;
            var result = SetGlobalMemoryFunctions(
                (IntPtr)(delegate*unmanaged[Cdecl]<nuint,void*>)&NativeAlloc, 
                (IntPtr)(delegate*unmanaged[Cdecl]<void*, void>)&NativeFree
                );
            if (result < 0) throw ((RetCode)result).GetException();
        }
        /// <summary>
        /// Remove previously registered memory management functions
        /// </summary>
        /// <remarks>Call this method to restore the default memory management functions</remarks>
        public static void ResetGlobalFunctions() {
            _alloc = null;
            _free = null;
            var result = ResetGlobalMemoryFunctions();
            if (result < 0) throw ((RetCode)result).GetException();
        }
    }
}