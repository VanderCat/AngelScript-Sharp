using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int GETRAWSTRINGDATA_t([NativeTypeName("const void *")] void* str, [NativeTypeName("char *")] sbyte* data, [NativeTypeName("asUINT *")] uint* length, void* userdata);
