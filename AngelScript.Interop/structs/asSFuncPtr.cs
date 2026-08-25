using System.Reflection;
using System.Runtime.InteropServices;

namespace AngelScript.Interop;

[StructLayout(LayoutKind.Explicit)]
public unsafe struct asSFuncPtr {
    public asSFuncPtr(Type f) {
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
    [FieldOffset(9)]
    public IntPtr handle = 0; //GCHandle for managed stuff 
    [FieldOffset(25)]
    public Type flag;

    public enum Type : byte {
        Generic = 1,
        Global = 2,
        Method = 3,
        Delegate = 4,
    }

    public static asSFuncPtr FromUnmanagedPtr(IntPtr ptr) => new(Type.Global) {
        func = ptr
    };

    public static asSFuncPtr FromUnmanagedCallersOnly(MethodInfo info) => new(Type.Global) {
        func = info.MethodHandle.GetFunctionPointer(),
    };

    public static asSFuncPtr FromUnmanagedCallersOnly<TParent>(string name) {
        var method = typeof(TParent).GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (method is null)
            throw new ArgumentException();

        return FromUnmanagedCallersOnly(method);
    }
}