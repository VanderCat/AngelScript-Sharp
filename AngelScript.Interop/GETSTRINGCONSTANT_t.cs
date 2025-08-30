using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
[return: NativeTypeName("const void *")]
public unsafe delegate void* GETSTRINGCONSTANT_t([NativeTypeName("const char *")] sbyte* data, [NativeTypeName("asUINT")] uint length, void* userdata);
