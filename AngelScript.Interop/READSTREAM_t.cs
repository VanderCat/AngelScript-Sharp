using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int READSTREAM_t(void* ptr, [NativeTypeName("asUINT")] uint size, void* userdata);
