using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asRETURNCONTEXTFUNC_t(asScriptEngine* param0, asScriptContext* param1, void* param2);
