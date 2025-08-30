using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[StructLayout(LayoutKind.Explicit)]
public unsafe struct asSFuncPtr {
    public asSFuncPtr(byte f = 0) {
        for(UIntPtr n = 0; n < 25; n++ )
            dummy[n] = 0;
        flag = f;
    }

    public void CopyMethodPtr(void* mthdPtr, UIntPtr size){
        for(UIntPtr n = 0; n < size; n++ )
            dummy[n] = ((byte*)mthdPtr)[n];
    }

    [FieldOffset(0)]
    private fixed byte dummy[25];

    //[FieldOffset(0)]
    //public asMETHOD_t mthd;
    [FieldOffset(0)]
    public IntPtr func = 0;
    [FieldOffset(25)]
    public byte flag; // 1 = generic, 2 = global func, 3 = method

    public static asSFuncPtr asFunctionPtr(IntPtr ptr) {
        // Mark this as a global function
        var sfuncptr = new asSFuncPtr(2);
        sfuncptr.func = ptr;
        return sfuncptr;
    }
}