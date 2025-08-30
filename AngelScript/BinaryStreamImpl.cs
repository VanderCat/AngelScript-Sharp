using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AngelScript;

internal unsafe class BinaryStreamImpl {
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static int Read(void* ptr, asUINT size, void* userdata) {
        try {
            var handle = GCHandle.FromIntPtr((IntPtr)userdata);
        
            if (handle.Target is not Stream stream)
                return (int)RetCode.InvalidObject;
            
            stream.ReadExactly(new Span<byte>((byte*)ptr, (int)size));
        }
        catch {
            return (int)RetCode.Error;
        }
        return 0;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static int Write(void* ptr, asUINT size, void* userdata) {
        try {
            var handle = GCHandle.FromIntPtr((IntPtr)userdata);
        
            if (handle.Target is not Stream stream)
                return (int)RetCode.InvalidObject;
            
            stream.Write(new ReadOnlySpan<byte>((byte*)ptr, (int)size));
        }
        catch {
            return (int)RetCode.Error;
        }
        return 0;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void Destroy(void* userdata) {
        try {
            var handle = GCHandle.FromIntPtr((IntPtr)userdata);

            if (handle.Target is IDisposable disposable)
                disposable.Dispose();
            
            handle.Free();
        }
        catch {
            // ignored
        }
    }
}

public static unsafe class BinaryStreamExt {
    public static asBinaryStream* ToBinaryStream(this Stream stream) {
        return BinaryStream_Create(new asSBinaryStream {
            userdata = (void*)GCHandle.ToIntPtr(GCHandle.Alloc(stream, GCHandleType.Normal)),
            destroyFunc = &BinaryStreamImpl.Destroy,
            readstreamFunc = &BinaryStreamImpl.Read,
            writestreamFunc = &BinaryStreamImpl.Write
        });
    }
}