using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
[return: NativeTypeName("const void *")]
public unsafe delegate void* asStringFactory_GetStringConstant([NativeTypeName("const char *")] sbyte* data, [NativeTypeName("asUINT")] uint length, void* userdata);
