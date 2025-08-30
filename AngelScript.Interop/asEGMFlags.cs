namespace AngelScript.Interop;

[NativeTypeName("unsigned int")]
public enum asEGMFlags : uint
{
    asGM_ONLY_IF_EXISTS = 0,
    asGM_CREATE_IF_NOT_EXISTS = 1,
    asGM_ALWAYS_CREATE = 2,
}
