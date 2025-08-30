namespace AngelScript;

public enum TypeModifiers : uint {
    /// No modification.
    None = asETypeModifiers.asTM_NONE,
        
    /// Input reference.
    InRef = asETypeModifiers.asTM_INREF,

    ///Output reference.
    OutRef = asETypeModifiers.asTM_OUTREF,

    ///In/out reference.
    InOutRef = asETypeModifiers.asTM_INOUTREF,

    ///Read only. 
    Const = asETypeModifiers.asTM_CONST,
}