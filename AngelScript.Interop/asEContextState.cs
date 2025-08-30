namespace AngelScript.Interop;

[NativeTypeName("unsigned int")]
public enum asEContextState : uint
{
    asEXECUTION_FINISHED = 0,
    asEXECUTION_SUSPENDED = 1,
    asEXECUTION_ABORTED = 2,
    asEXECUTION_EXCEPTION = 3,
    asEXECUTION_PREPARED = 4,
    asEXECUTION_UNINITIALIZED = 5,
    asEXECUTION_ACTIVE = 6,
    asEXECUTION_ERROR = 7,
    asEXECUTION_DESERIALIZATION = 8,
}
