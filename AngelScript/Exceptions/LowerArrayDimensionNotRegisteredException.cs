namespace AngelScript;

public class LowerArrayDimensionNotRegisteredException : Exception {
    public LowerArrayDimensionNotRegisteredException() : base() {}
    public LowerArrayDimensionNotRegisteredException(string? message) : base(message) {}
    public LowerArrayDimensionNotRegisteredException(string? message, Exception? innerException) : base(message, innerException) {}
}