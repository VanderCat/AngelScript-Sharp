using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asCLEANMODULEFUNC_t(asScriptModule* param0);
