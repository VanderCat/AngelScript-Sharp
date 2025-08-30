namespace AngelScript.Interop;

[NativeTypeName("unsigned int")]
public enum asETypeModifiers : uint
{
    asTM_NONE = 0,
    asTM_INREF = 1,
    asTM_OUTREF = 2,
    asTM_INOUTREF = 3,
    asTM_CONST = 4,
}
