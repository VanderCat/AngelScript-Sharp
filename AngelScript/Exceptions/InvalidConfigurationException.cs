namespace AngelScript;

public class InvalidConfigurationException : Exception {
    public InvalidConfigurationException() : base() {}
    public InvalidConfigurationException(string? message) : base(message) {}
    public InvalidConfigurationException(string? message, Exception? innerException) : base(message, innerException) {}
}