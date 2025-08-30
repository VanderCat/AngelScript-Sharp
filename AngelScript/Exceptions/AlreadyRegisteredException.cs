namespace AngelScript;

public class AlreadyRegisteredException : Exception {
    public AlreadyRegisteredException() : base() {}
    public AlreadyRegisteredException(string? message) : base(message) {}
    public AlreadyRegisteredException(string? message, Exception? innerException) : base(message, innerException) {}
}