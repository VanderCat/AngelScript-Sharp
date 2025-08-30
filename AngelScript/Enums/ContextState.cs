using System.ComponentModel;

namespace AngelScript;

public enum ContextState : uint {
    [Description("The context has successfully completed the execution")]
    Finished = asEContextState.asEXECUTION_FINISHED,
    [Description("The execution is suspended and can be resumed")]
    Suspended = asEContextState.asEXECUTION_SUSPENDED,
    [Description("The execution was aborted by the application")]
    Aborted = asEContextState.asEXECUTION_ABORTED,
    [Description("The execution was terminated by an unhandled script exception")]
    Exception = asEContextState.asEXECUTION_EXCEPTION,
    [Description("The context has been prepared for a new execution")]
    Prepared = asEContextState.asEXECUTION_PREPARED,
    [Description("The context is not initialized")]
    Uninitialized = asEContextState.asEXECUTION_UNINITIALIZED,
    [Description("The context is currently executing a function call")]
    Active = asEContextState.asEXECUTION_ACTIVE,
    [Description("The context has encountered an error and must be reinitialized")]
    Error = asEContextState.asEXECUTION_ERROR,
    [Description("The context is currently in deserialization mode")]
    Deserialization = asEContextState.asEXECUTION_DESERIALIZATION,
}