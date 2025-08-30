namespace AngelScript;

public class BuildFailedException : Exception {
    public BuildFailedException() : base() {}
    public BuildFailedException(string? message) : base(message) {}
    public BuildFailedException(string? message, Exception? innerException) : base(message, innerException) {}
}