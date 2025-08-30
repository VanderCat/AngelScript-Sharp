using System;

namespace AngelScript.Interop;

public unsafe partial struct asSStringFactory
{
    [NativeTypeName("GETSTRINGCONSTANT_t")]
    public IntPtr getStringConstantFunc;

    [NativeTypeName("RELEASESTRINGCONSTANT_t")]
    public IntPtr releaseStringConstantFunc;

    [NativeTypeName("GETRAWSTRINGDATA_t")]
    public IntPtr getRawStringDataFunc;

    [NativeTypeName("DESTROYUSERDATA_t")]
    public IntPtr destroyFunc;

    public void* userdata;
}
