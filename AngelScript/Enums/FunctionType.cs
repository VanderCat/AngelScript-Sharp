using System.ComponentModel;

namespace AngelScript;

public enum FunctionType {
    [Description("An application registered function.")]
    Dummy = asEFuncType.asFUNC_DUMMY,
    [Description("An application registered function.")]
    System = asEFuncType.asFUNC_SYSTEM,
    [Description("A script implemented function.")]
    Script = asEFuncType.asFUNC_SCRIPT,
    [Description("An interface method.")]
    Interface = asEFuncType.asFUNC_INTERFACE,
    [Description("A virtual method for script classes.")]
    Virtual = asEFuncType.asFUNC_VIRTUAL,
    [Description("A function definition.")]
    FuncDef = asEFuncType.asFUNC_FUNCDEF,
    [Description("An imported function.")]
    Imported = asEFuncType.asFUNC_IMPORTED,
    [Description("A function delegate.")]
    Delegate = asEFuncType.asFUNC_DELEGATE,
    [Description("A template function.")]
    Template = asEFuncType.asFUNC_TEMPLATE,
}