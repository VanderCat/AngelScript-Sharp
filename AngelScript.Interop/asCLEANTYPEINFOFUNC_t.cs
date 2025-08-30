using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asCLEANTYPEINFOFUNC_t(asTypeInfo* param0);
