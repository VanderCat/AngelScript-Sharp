using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int RELEASESTRINGCONSTANT_t([NativeTypeName("const void *")] void* str, void* userdata);
