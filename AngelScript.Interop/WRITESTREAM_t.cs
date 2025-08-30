using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int WRITESTREAM_t([NativeTypeName("const void *")] void* ptr, [NativeTypeName("asUINT")] uint size, void* userdata);
