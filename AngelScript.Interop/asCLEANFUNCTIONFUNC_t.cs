using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asCLEANFUNCTIONFUNC_t(asScriptFunction* param0);
