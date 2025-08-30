using System;

namespace AngelScript.Interop;

public unsafe partial struct asSBinaryStream
{
    [NativeTypeName("READSTREAM_t")]
    public delegate*unmanaged[Cdecl]<void*,uint,void*,int> readstreamFunc;

    [NativeTypeName("WRITESTREAM_t")]
    public delegate*unmanaged[Cdecl]<void*,uint,void*,int> writestreamFunc;

    [NativeTypeName("DESTROYUSERDATA_t")]
    public delegate*unmanaged[Cdecl]<void*,void> destroyFunc;

    public void* userdata;
}
