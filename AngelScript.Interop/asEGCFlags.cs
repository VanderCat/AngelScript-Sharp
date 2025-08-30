namespace AngelScript.Interop;

[NativeTypeName("unsigned int")]
public enum asEGCFlags : uint
{
    asGC_FULL_CYCLE = 1,
    asGC_ONE_STEP = 2,
    asGC_DESTROY_GARBAGE = 4,
    asGC_DETECT_GARBAGE = 8,
}
