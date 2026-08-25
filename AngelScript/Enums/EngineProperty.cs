using System.ComponentModel;

namespace AngelScript;

public enum EngineProperty : uint {
    [Description("Allow unsafe references. Default: false.")]
    AllowUnsafeReferences = asEEngineProp.asEP_ALLOW_UNSAFE_REFERENCES,
    [Description("Optimize byte code. Default: true.")]
    OptimizeBytecode = asEEngineProp.asEP_OPTIMIZE_BYTECODE,
    [Description("Copy script section memory. Default: true.")]
    CopyScriptSections = asEEngineProp.asEP_COPY_SCRIPT_SECTIONS,
    [Description("Maximum stack size in bytes for script contexts. Default: 0 (no limit).")]
    MaxStackSize = asEEngineProp.asEP_MAX_STACK_SIZE,
    [Description("Interpret single quoted strings as character literals. Default: false. ")]
    UseCharacterLiterals = asEEngineProp.asEP_USE_CHARACTER_LITERALS,
    [Description("Allow linebreaks in string constants. Default: false.")]
    AllowMultilineStrings = asEEngineProp.asEP_ALLOW_MULTILINE_STRINGS,
    [Description("Allow script to declare implicit handle types. Default: false.")]
    AllowImplicitHandleTypes = asEEngineProp.asEP_ALLOW_IMPLICIT_HANDLE_TYPES,
    [Description("Remove SUSPEND instructions between each statement. Default: false.")]
    BuildWithoutLineCues = asEEngineProp.asEP_BUILD_WITHOUT_LINE_CUES,
    [Description("Initialize global variables after a build. Default: true. ")]
    InitGlobalVarsAfterBuild = asEEngineProp.asEP_INIT_GLOBAL_VARS_AFTER_BUILD,
    [Description("When set the enum values must be prefixed with the enum type. Default: false.")]
    RequireEnumScope = asEEngineProp.asEP_REQUIRE_ENUM_SCOPE,
    [Description("Select scanning method: 0 - ASCII, 1 - UTF8. Default: 1 (UTF8).")]
    ScriptScanner = asEEngineProp.asEP_SCRIPT_SCANNER,
    [Description("When set extra bytecode instructions needed for JIT compiled funcions will be included. Default: false.")]
    IncludeJITInstructions = asEEngineProp.asEP_INCLUDE_JIT_INSTRUCTIONS,
    [Description("Select string encoding for literals: 0 - UTF8/ASCII, 1 - UTF16. Default: 0 (UTF8)")]
    StringEncoding = asEEngineProp.asEP_STRING_ENCODING,
    [Description("Enable or disable property accessors: 0 - no accessors, 1 - app registered accessors only, property keyword optional, 2 - app and script created accessors, property keyword optional, 3 - app and script created accesors, property keyword required. Default: 3.")]
    PropertyAccessorMode = asEEngineProp.asEP_PROPERTY_ACCESSOR_MODE,
    [Description("Format default array in template form in messages and declarations. Default: false. ")]
    ExpandDefArrayToTemplate = asEEngineProp.asEP_EXPAND_DEF_ARRAY_TO_TMPL,
    [Description("Enable or disable automatic garbage collection. Default: true. ")]
    AutoGarbageCollect = asEEngineProp.asEP_AUTO_GARBAGE_COLLECT,
    [Description("Disallow the use of global variables in the script. Default: false.")]
    DisallowGlobalVars = asEEngineProp.asEP_DISALLOW_GLOBAL_VARS,
    [Description("Determine if the default constructor is provided automatically by compiler. 0 - as per language spec, 1 - always, 2 - never. Default: 0.")]
    AlwaysImplementDefaultConstructor = asEEngineProp.asEP_ALWAYS_IMPL_DEFAULT_CONSTRUCT,
    [Description("Set how warnings should be treated: 0 - dismiss, 1 - emit, 2 - treat as error. Default: 1.")]
    CompilerWarnings = asEEngineProp.asEP_COMPILER_WARNINGS,
    [Description("Disallow value assignment for reference types to avoid ambiguity. Default: false. ")]
    DisallowValueAssignForRefType = asEEngineProp.asEP_DISALLOW_VALUE_ASSIGN_FOR_REF_TYPE,
    [Description("Change the script syntax for named arguments: 0 - no change, 1 - accept '=' but warn, 2 - accept '=' without warning. Default: 0. ")]
    AlterSyntaxNamedArgs = asEEngineProp.asEP_ALTER_SYNTAX_NAMED_ARGS,
    [Description("When true, the / and /= operators will perform floating-point division (i.e. 1/2 = 0.5 instead of 0). Default: false. ")]
    DisableIntegerDivisin = asEEngineProp.asEP_DISABLE_INTEGER_DIVISION,
    [Description("When true, the initialization lists may not contain empty elements. Default: false. ")]
    DisallowEmptyListElements = asEEngineProp.asEP_DISALLOW_EMPTY_LIST_ELEMENTS,
    [Description("When true, private properties behave like protected properties. Default: false.")]
    PrivatePropAsProtected = asEEngineProp.asEP_PRIVATE_PROP_AS_PROTECTED,
    [Description("When true, the compiler will not give an error if identifiers contain characters with byte value above 127, thus permit identifiers to contain international characters. Default: false. ")]
    AllowUnicodeIdentifiers = asEEngineProp.asEP_ALLOW_UNICODE_IDENTIFIERS,
    [Description("Define how heredoc strings will be trimmed by the compiler: 0 - never trim, 1 - trim if multiple lines, 2 - always trim. Default: 1. ")]
    HeredocTrimMode = asEEngineProp.asEP_HEREDOC_TRIM_MODE,
    [Description("Define the maximum number of nested calls the script engine will allow. Default: 100. ")]
    MaxNestedCalls = asEEngineProp.asEP_MAX_NESTED_CALLS,
    [Description("Define how generic calling convention treats handles: 0 - ignore auto handles, 1 - treat them the same way as native calling convention. Default: 1. ")]
    GenericCallMode = asEEngineProp.asEP_GENERIC_CALL_MODE,
    [Description("Initial stack size in bytes for script contexts. Default: 4096. ")]
    InitStackSize = asEEngineProp.asEP_INIT_STACK_SIZE,
    [Description("Initial call stack size for script contexts. Default: 10. ")]
    InitCallStackSize = asEEngineProp.asEP_INIT_CALL_STACK_SIZE,
    [Description("Maximum call stack size for script contexts. Default: 0 (no limit) ")]
    MaxCallStackSize = asEEngineProp.asEP_MAX_CALL_STACK_SIZE,
    [Description("Ignore multiple declarations of shared interface. Default: false. ")]
    IgnoreDuplicateSharedIntf = asEEngineProp.asEP_IGNORE_DUPLICATE_SHARED_INTF,
    [Description("Don't write debug output when library is compiled with AS_DEBUG. Default: false. ")]
    NoDebugOutput = asEEngineProp.asEP_NO_DEBUG_OUTPUT,
    [Description("Disable GC for classes compiled from scripts. Default: false. ")]
    DisableScriptClassGC = asEEngineProp.asEP_DISABLE_SCRIPT_CLASS_GC,
    [Description("Set the JIT interface version used. 1 - JIT compiler uses asJITCompiler, 2 - JIT compiler uses asJITCompilerV2. Default: 1.")]
    JITInterfaceVersion = asEEngineProp.asEP_JIT_INTERFACE_VERSION,
    [Description("Determine if the default copy behaviour is provided automatically by compiler. 0 - as per language spec, 1 - always, 2 - never. Default: 0.")]
    AlwaysImplementDefaultCopy = asEEngineProp.asEP_ALWAYS_IMPL_DEFAULT_COPY,
    [Description("Determine if the default copy constructor is provided automatically by compiler. 0 - as per language spec, 1 - always, 2 - never. Default: 0.")]
    AlwaysImplementDefaultCopyConstructor = asEEngineProp.asEP_ALWAYS_IMPL_DEFAULT_COPY_CONSTRUCT,
    [Description("Determine how class members with init expressions are handled. 0 - pre 2.38.0, members with init expr in declaration are initialized after super(), 1 - all members initialized in beginning, except if explicitly initialized in body. Default: 1. ")]
    MemberInitMode = asEEngineProp.asEP_MEMBER_INIT_MODE,
    [Description("Determine how boolean conversion are done. 0 - only use opImplConv for registered value type, 1 - use also opConv in contextual conversion even for reference types. Default: 1. ")]
    BoolConversionMode = asEEngineProp.asEP_BOOL_CONVERSION_MODE,
    [Description("Enable foreach support. Default: true.")]
    ForeachSupport = asEEngineProp.asEP_FOREACH_SUPPORT,
}