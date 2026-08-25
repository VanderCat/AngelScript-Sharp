namespace AngelScript;

public enum CallConvTypes : uint {
    CDecl = asECallConvTypes.asCALL_CDECL,
    StdCall = asECallConvTypes.asCALL_STDCALL,
    ThisCallAsGlobal = asECallConvTypes.asCALL_THISCALL_ASGLOBAL ,
    ThisCall = asECallConvTypes.asCALL_THISCALL,
    CDeclObjLast = asECallConvTypes.asCALL_CDECL_OBJLAST,
    CDeclObjFirst = asECallConvTypes.asCALL_CDECL_OBJFIRST,
    Generic = asECallConvTypes.asCALL_GENERIC,
    ThisCallObjLast = asECallConvTypes.asCALL_THISCALL_OBJLAST,
    ThisCallObjFirst = asECallConvTypes.asCALL_THISCALL_OBJFIRST,
}