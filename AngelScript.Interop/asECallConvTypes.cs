namespace AngelScript.Interop;

[NativeTypeName("unsigned int")]
public enum asECallConvTypes : uint
{
    asCALL_CDECL = 0,
    asCALL_STDCALL = 1,
    asCALL_THISCALL_ASGLOBAL = 2,
    asCALL_THISCALL = 3,
    asCALL_CDECL_OBJLAST = 4,
    asCALL_CDECL_OBJFIRST = 5,
    asCALL_GENERIC = 6,
    asCALL_THISCALL_OBJLAST = 7,
    asCALL_THISCALL_OBJFIRST = 8,
}
