using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asCIRCULARREFFUNC_t(asTypeInfo* param0, [NativeTypeName("const void *")] void* param1, void* param2);
