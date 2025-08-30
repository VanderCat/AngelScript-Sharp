using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

internal static unsafe class Util {
    public static string? ConvertPtrToString(byte* ptr, Encoding encoding) {
        if (ptr is null)
            return null;
        return encoding.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
    }
    public static string? ConvertPtrToString(byte* ptr) => ConvertPtrToString(ptr, Encoding.UTF8);
}