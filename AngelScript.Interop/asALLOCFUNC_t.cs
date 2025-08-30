using System;
using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void* asALLOCFUNC_t([NativeTypeName("size_t")] UIntPtr param0);
