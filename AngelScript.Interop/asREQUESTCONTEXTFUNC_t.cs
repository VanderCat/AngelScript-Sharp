using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate asScriptContext* asREQUESTCONTEXTFUNC_t(asScriptEngine* param0, void* param1);
