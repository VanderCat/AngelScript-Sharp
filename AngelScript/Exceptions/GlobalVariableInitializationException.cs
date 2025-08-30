namespace AngelScript;

public class GlobalVariableInitializationException : Exception {
    public GlobalVariableInitializationException() : base() {}
    public GlobalVariableInitializationException(string? message) : base(message) {}
    public GlobalVariableInitializationException(string? message, Exception? innerException) : base(message, innerException) {}
}