namespace AngelScript.Interop;

public unsafe struct asSMessageInfo {
    [NativeTypeName("const char *")]
    public byte* section;

    public int row;

    public int col;

    public asEMsgType type;

    [NativeTypeName("const char *")]
    public byte* message;
}