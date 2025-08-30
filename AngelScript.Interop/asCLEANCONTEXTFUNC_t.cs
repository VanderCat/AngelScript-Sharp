using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asCLEANCONTEXTFUNC_t(asScriptContext* param0);
