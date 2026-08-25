using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

public class MessageInfo {
    public string Section;
    public int Row;
    public int Column;
    public string Message;
    public MsgType Type;

    public unsafe MessageInfo(asSMessageInfo* mi) {
        Row = mi->row;
        Column = mi->col;
        Section = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(mi->section));
        Message = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(mi->message));
        Type = (MsgType)mi->type;
    }
}