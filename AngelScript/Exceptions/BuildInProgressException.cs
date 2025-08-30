namespace AngelScript;

public class BuildInProgressException : Exception {
    public BuildInProgressException() : base() {}
    public BuildInProgressException(string? message) : base(message) {}
    public BuildInProgressException(string? message, Exception? innerException) : base(message, innerException) {}
}