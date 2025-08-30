using System;
using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void asJITFunction(asSVMRegisters* registers, [NativeTypeName("asPWORD")] UIntPtr jitArg);
