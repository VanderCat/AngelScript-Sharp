namespace AngelScript.Interop;

[NativeTypeName("unsigned int")]
public enum asETokenClass : uint
{
    asTC_UNKNOWN = 0,
    asTC_KEYWORD = 1,
    asTC_VALUE = 2,
    asTC_IDENTIFIER = 3,
    asTC_COMMENT = 4,
    asTC_WHITESPACE = 5,
}
