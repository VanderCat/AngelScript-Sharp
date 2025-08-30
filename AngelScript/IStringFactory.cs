using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AngelScript;

/// <summary>
/// The interface for the string factory
/// </summary>
/// <remarks>
/// This interface is used to manage the string constants that the scripts use.
/// If string constants should be supported the application must implement this object and register it with <see cref="ScriptEngine.RegisterStringFactory"/>. 
/// </remarks>
public unsafe interface IStringFactory {
    /// <summary>
    /// Called by engine to instantiate a string constant
    /// </summary>
    /// <param name="data">The content of the string</param>
    /// <param name="length">The length in bytes of the data buffer</param>
    /// <returns>The pointer to the instantiated string constant</returns>
    /// <remarks>
    /// The contents of data must be copied by the string factory, as the engine will not keep a copy of the original data.
    /// <br/><br/>
    /// The string factory can cache and return a pointer to the same instance multiple times if the same string content is requested multiple times.
    /// If the same instance is returned multiple times the string factory must keep track of the number of instances as ReleaseStringConstant will
    /// be called for each of them. 
    /// </remarks>
    public void* GetStringConstant(byte* data, uint length);
    /// <summary>
    /// Called by engine when the string constant is no longer used
    /// </summary>
    /// <param name="str">A negative value on error</param>
    /// <returns>A negative value on error</returns>
    /// <remarks>
    /// The engine will call this method for each pointer returned by GetStringConstant.
    /// If the string factory returns a pointer to the same instance multiple times,
    /// then the string instance can only be destroyed when the last call to ReleaseStringConstant for that pointer is made.
    /// </remarks>
    public asERetCodes ReleaseStringConstant(void* str);
    
    /// <summary>
    /// Called by engine to get the raw string data for serialization
    /// </summary>
    /// <param name="str">The same pointer returned by <see cref="GetStringConstant"/></param>
    /// <param name="data">A pointer to the data buffer that should be filled with the content</param>
    /// <param name="length">A pointer to the variable that should be set with the length of the data </param>
    /// <returns>A negative value on error</returns>
    /// <remarks>
    /// The engine will first call this with data set to null to retrieve the size of the buffer that must be allocated.
    /// Then the engine will call the method once more with the allocated data buffer to be filled with the content.
    /// The length should always be informed in number of bytes. 
    /// </remarks>
    public asERetCodes GetRawStringData(void* str, sbyte* data, uint* length);
    public asERetCodes Destroy();
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static asERetCodes StringFactoryReleaseStringConstant(void* str, void* userdata) {
        if (userdata is null)
            return asERetCodes.asERROR;
        var handle = GCHandle.FromIntPtr((IntPtr)userdata);
        if (handle.Target is not IStringFactory stringFactory)
            return asERetCodes.asERROR;
        try {
            return stringFactory.ReleaseStringConstant(str);
        } catch (Exception e) {
            return asERetCodes.asERROR;
        }
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void* StringFactoryGetStringConstant(byte* data, uint length, void* userdata) {
        if (userdata is null)
            return null;
        var handle = GCHandle.FromIntPtr((IntPtr)userdata);
        if (handle.Target is not IStringFactory stringFactory)
            return null;
        try {
            return stringFactory.GetStringConstant(data, length);
        } catch (Exception e) {
            return null;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static asERetCodes StringFactoryGetRawStringData(void* str, sbyte* data, uint* length, void* userdata) {
        if (userdata is null)
            return asERetCodes.asERROR;
        var handle = GCHandle.FromIntPtr((IntPtr)userdata);
        if (handle.Target is not IStringFactory stringFactory)
            return asERetCodes.asERROR;
        try {
            return stringFactory.GetRawStringData(str, data, length);
        } catch (Exception e) {
            return asERetCodes.asERROR;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static asERetCodes StringFactoryDestroy(void* userdata) {
        if (userdata is null)
            return asERetCodes.asERROR;
        var handle = GCHandle.FromIntPtr((IntPtr)userdata);
        if (handle.Target is not IStringFactory stringFactory)
            return asERetCodes.asERROR;
        try {
            return stringFactory.Destroy();
        } catch (Exception e) {
            return asERetCodes.asERROR;
        }
    }
}