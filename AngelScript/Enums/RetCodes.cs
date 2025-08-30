using System.ComponentModel;

namespace AngelScript;

public enum RetCode { 
    /// <summary>
    /// Success
    /// </summary>
    [Description("Success")]
    Success = asERetCodes.asSUCCESS,
    /// <summary>
    /// Failure
    /// </summary>
    [Description("Unknown failure")]
    Error = asERetCodes.asERROR,
    /// <summary>
    /// The context is active
    /// </summary>
    [Description("The context is active")]
    ContextActive = asERetCodes.asCONTEXT_ACTIVE,
    /// <summary>
    /// The context is not finished
    /// </summary>
    [Description("The context is not finished")]
    ContextNotFinished = asERetCodes.asCONTEXT_NOT_FINISHED,
    /// <summary>
    /// The context is not prepared
    /// </summary>
    [Description("The context is not prepared")]
    ContextNotPrepared = asERetCodes.asCONTEXT_NOT_PREPARED,
    /// <summary>
    /// Invalid argument
    /// </summary>
    [Description("Invalid argument")]
    InvalidArg = asERetCodes.asINVALID_ARG,
    /// <summary>
    /// The function was not found
    /// </summary>
    [Description("The function was not found")]
    NoFunction = asERetCodes.asNO_FUNCTION,
    /// <summary>
    /// Not supported
    /// </summary>
    [Description("Not supported")]
    NotSupported = asERetCodes.asNOT_SUPPORTED,
    /// <summary>
    /// Invalid name
    /// </summary>
    [Description("Invalid Name")]
    InvalidName = asERetCodes.asINVALID_NAME,
    /// <summary>
    /// The name is already taken
    /// </summary>
    [Description("The name is already taken")]
    NameTaken = asERetCodes.asNAME_TAKEN,
    /// <summary>
    /// Invalid declaration
    /// </summary>
    [Description("Invalid declaration")]
    InvalidDeclaration = asERetCodes.asINVALID_DECLARATION,
    /// <summary>
    /// Invalid object
    /// </summary>
    [Description("Invalid object")]
    InvalidObject = asERetCodes.asINVALID_OBJECT,
    /// <summary>
    /// Invalid type
    /// </summary>
    [Description("Invalid type")]
    InvalidType = asERetCodes.asINVALID_TYPE,
    /// <summary>
    /// Already registered
    /// </summary>
    [Description("Already registered")]
    AlreadyRegistered = asERetCodes.asALREADY_REGISTERED,
    /// <summary>
    /// Multiple matching functions
    /// </summary>
    [Description("Multiple matching functions")]
    MultipleFunctions = asERetCodes.asMULTIPLE_FUNCTIONS,
    /// <summary>
    /// The module was not found
    /// </summary>
    [Description("NoModule")]
    NoModule = asERetCodes.asNO_MODULE,
    /// <summary>
    /// The global variable was not found
    /// </summary>
    [Description("NoGlobalVar")]
    NoGlobalVar = asERetCodes.asNO_GLOBAL_VAR,
    /// <summary>
    /// Invalid configuration
    /// </summary>
    [Description("Inalid configuration")]
    InvalidConfiguration = asERetCodes.asINVALID_CONFIGURATION,
    /// <summary>
    /// Invalid interface
    /// </summary>
    [Description("Invalid interface")]
    InvalidInterface = asERetCodes.asINVALID_INTERFACE,
    /// <summary>
    /// All imported functions couldn't be bound
    /// </summary>
    [Description("All imported functions couldn't be bound")]
    CantBindAllFunction = asERetCodes.asCANT_BIND_ALL_FUNCTIONS,
    /// <summary>
    /// The array sub type has not been registered yet
    /// </summary>
    [Description("The array sub type has not been registered yet")]
    LowerArrayDimensionNotRegistered = asERetCodes.asLOWER_ARRAY_DIMENSION_NOT_REGISTERED,
    /// <summary>
    /// Wrong configuration group
    /// </summary>
    [Description("Wrong configuration group")]
    WrongConfigGroup = asERetCodes.asWRONG_CONFIG_GROUP,
    /// <summary>
    /// The configuration group is in use
    /// </summary>
    [Description("The configuration group is in use")]
    ConfigGroupIsInUse = asERetCodes.asCONFIG_GROUP_IS_IN_USE,
    /// <summary>
    /// Illegal behaviour for the type
    /// </summary>
    [Description("Illegal behaviour for the type")]
    IllegalBehaviourForType = asERetCodes.asILLEGAL_BEHAVIOUR_FOR_TYPE,
    /// <summary>
    /// The specified calling convention doesn't match the function/method pointer
    /// </summary>
    [Description("The specified calling convention doesn't match the function/method pointer")]
    WrongCallingConv = asERetCodes.asWRONG_CALLING_CONV,
    /// <summary>
    /// A build is currently in progress
    /// </summary>
    [Description("A build is currently in progress")]
    BuildInProgress = asERetCodes.asBUILD_IN_PROGRESS,
    /// <summary>
    /// The initialization of global variables failed
    /// </summary>
    [Description("The initialization of global variables failed")]
    InitGlobalVarsFailed = asERetCodes.asINIT_GLOBAL_VARS_FAILED,
    /// <summary>
    /// It wasn't possible to allocate the needed memory
    /// </summary>
    [Description("It wasn't possible to allocate the needed memory")]
    OutOfMemory = asERetCodes.asOUT_OF_MEMORY,
    /// <summary>
    /// The module is referred to by live objects or from the application
    /// </summary>
    [Description("The module is referred to by live objects or from the application")]
    ModuleIsInUse = asERetCodes.asMODULE_IS_IN_USE,
}

public static class RetCodeExt {
    /// <summary>
    /// Get RetCode description
    /// </summary>
    /// <returns>Description</returns>
    public static string? GetDescription(this RetCode retCode) {
        var enumType = typeof(RetCode);
        var memberInfos = enumType.GetMember(retCode.ToString());

        var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
        if (enumValueMemberInfo is null)
            return null;

        var valueAttributes = enumValueMemberInfo
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        var descObj = valueAttributes.FirstOrDefault();
        if (descObj is DescriptionAttribute desc)
            return desc.Description;
        return null;
    }

    /// <summary>
    /// Gets a generic exception for this retcode
    /// </summary>
    /// <param name="retCode">a return code</param>
    public static Exception GetException(this RetCode retCode) {
        var desc = retCode.GetDescription()??"No description";
        switch (retCode) {
            case RetCode.Success:
            case RetCode.Error:
            case RetCode.ContextActive:
            case RetCode.ContextNotFinished:
            case RetCode.ContextNotPrepared:
                return new Exception(desc);
            case RetCode.InvalidArg:
                return new ArgumentException(desc);
            case RetCode.NoFunction:
            case RetCode.NotSupported:
                return new NotSupportedException(desc);
            case RetCode.InvalidName:
            case RetCode.NameTaken:
            case RetCode.InvalidDeclaration:
            case RetCode.InvalidObject:
            case RetCode.InvalidType:
            case RetCode.AlreadyRegistered:
                return new ArgumentException(desc);
            case RetCode.MultipleFunctions:
                
            case RetCode.NoModule:
            case RetCode.NoGlobalVar:
                return new ArgumentException(desc);
            case RetCode.InvalidConfiguration:
                
            case RetCode.InvalidInterface:

            case RetCode.CantBindAllFunction:

            case RetCode.LowerArrayDimensionNotRegistered:

            case RetCode.WrongConfigGroup:

            case RetCode.ConfigGroupIsInUse:

            case RetCode.IllegalBehaviourForType:
            case RetCode.WrongCallingConv:
                return new ArgumentException(desc);
            case RetCode.BuildInProgress:
            
            case RetCode.InitGlobalVarsFailed:
                return new ArgumentException(desc);
            case RetCode.OutOfMemory:
                return new InsufficientMemoryException(desc);
            case RetCode.ModuleIsInUse:
            
            default:
                return new ArgumentOutOfRangeException(nameof(retCode), retCode, null);
        }
    }
}