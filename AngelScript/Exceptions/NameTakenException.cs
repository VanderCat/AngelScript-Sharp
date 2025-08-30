namespace AngelScript;

public class NameTakenException : Exception {
    public NameTakenException() : base() {}
    public NameTakenException(string? message) : base(message) {}
    public NameTakenException(string? message, Exception? innerException) : base(message, innerException) {}
}