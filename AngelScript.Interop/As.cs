using System;
using System.Runtime.InteropServices;

namespace AngelScript.Interop;

public static unsafe partial class As
{
    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asCreateScriptEngine", ExactSpelling = true)]
    public static extern asScriptEngine* CreateScriptEngine([NativeTypeName("asDWORD")] uint version);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asGetLibraryVersion", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* GetLibraryVersion();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asGetLibraryOptions", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* GetLibraryOptions();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asGetActiveContext", ExactSpelling = true)]
    public static extern asScriptContext* GetActiveContext();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asPrepareMultithread", ExactSpelling = true)]
    public static extern int PrepareMultithread(asThreadManager* externalMgr);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asUnprepareMultithread", ExactSpelling = true)]
    public static extern void UnprepareMultithread();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asGetThreadManager", ExactSpelling = true)]
    public static extern asThreadManager* GetThreadManager();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asAcquireExclusiveLock", ExactSpelling = true)]
    public static extern void AcquireExclusiveLock();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asReleaseExclusiveLock", ExactSpelling = true)]
    public static extern void ReleaseExclusiveLock();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asAcquireSharedLock", ExactSpelling = true)]
    public static extern void AcquireSharedLock();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asReleaseSharedLock", ExactSpelling = true)]
    public static extern void ReleaseSharedLock();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asAtomicInc", ExactSpelling = true)]
    public static extern int AtomicInc([NativeTypeName("int &")] int* value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asAtomicDec", ExactSpelling = true)]
    public static extern int AtomicDec([NativeTypeName("int &")] int* value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asThreadCleanup", ExactSpelling = true)]
    public static extern int ThreadCleanup();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asSetGlobalMemoryFunctions", ExactSpelling = true)]
    public static extern int SetGlobalMemoryFunctions([NativeTypeName("asALLOCFUNC_t")] IntPtr allocFunc, [NativeTypeName("asFREEFUNC_t")] IntPtr freeFunc);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asResetGlobalMemoryFunctions", ExactSpelling = true)]
    public static extern int ResetGlobalMemoryFunctions();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asAllocMem", ExactSpelling = true)]
    public static extern void* AllocMem([NativeTypeName("size_t")] UIntPtr size);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asFreeMem", ExactSpelling = true)]
    public static extern void FreeMem(void* mem);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asCreateLockableSharedBool", ExactSpelling = true)]
    public static extern asLockableSharedBool* CreateLockableSharedBool();

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_AddRef", ExactSpelling = true)]
    public static extern int ScriptEngine_AddRef(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_Release", ExactSpelling = true)]
    public static extern int ScriptEngine_Release(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ShutDownAndRelease", ExactSpelling = true)]
    public static extern int ScriptEngine_ShutDownAndRelease(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetEngineProperty", ExactSpelling = true)]
    public static extern int ScriptEngine_SetEngineProperty(asScriptEngine* engine, asEEngineProp property, [NativeTypeName("asPWORD")] UIntPtr value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetEngineProperty", ExactSpelling = true)]
    [return: NativeTypeName("asPWORD")]
    public static extern UIntPtr ScriptEngine_GetEngineProperty(asScriptEngine* engine, asEEngineProp property);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetMessageCallback", ExactSpelling = true)]
    public static extern int ScriptEngine_SetMessageCallback(asScriptEngine* engine, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* callback, void* obj, [NativeTypeName("asDWORD")] uint callConv);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetMessageCallback", ExactSpelling = true)]
    public static extern int ScriptEngine_GetMessageCallback(asScriptEngine* engine, asSFuncPtr* callback, void** obj, [NativeTypeName("asDWORD *")] uint* callConv);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ClearMessageCallback", ExactSpelling = true)]
    public static extern int ScriptEngine_ClearMessageCallback(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_WriteMessage", ExactSpelling = true)]
    public static extern int ScriptEngine_WriteMessage(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* section, int row, int col, asEMsgType type, [NativeTypeName("const char *")] sbyte* message);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetJITCompiler", ExactSpelling = true)]
    public static extern int ScriptEngine_SetJITCompiler(asScriptEngine* engine, asJITCompilerAbstract* compiler);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetJITCompiler", ExactSpelling = true)]
    public static extern asJITCompilerAbstract* ScriptEngine_GetJITCompiler(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterGlobalFunction", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterGlobalFunction(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* declaration, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* funcPointer, [NativeTypeName("asDWORD")] uint callConv, void* auxiliary);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalFunctionCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetGlobalFunctionCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalFunctionByIndex", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptEngine_GetGlobalFunctionByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalFunctionByDecl", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptEngine_GetGlobalFunctionByDecl(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* declaration);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterGlobalProperty", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterGlobalProperty(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* declaration, void* pointer);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalPropertyCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetGlobalPropertyCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalPropertyByIndex", ExactSpelling = true)]
    public static extern int ScriptEngine_GetGlobalPropertyByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index, [NativeTypeName("const char **")] sbyte** name, [NativeTypeName("const char **")] sbyte** nameSpace, int* typeId, bool* isConst, [NativeTypeName("const char **")] sbyte** configGroup, void** pointer, [NativeTypeName("asDWORD *")] uint* accessMask);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalPropertyIndexByName", ExactSpelling = true)]
    public static extern int ScriptEngine_GetGlobalPropertyIndexByName(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGlobalPropertyIndexByDecl", ExactSpelling = true)]
    public static extern int ScriptEngine_GetGlobalPropertyIndexByDecl(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterObjectType", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterObjectType(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* obj, int byteSize, [NativeTypeName("asQWORD")] ulong flags);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterObjectProperty", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterObjectProperty(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* obj, [NativeTypeName("const char *")] sbyte* declaration, int byteOffset, int compositeOffset, bool isCompositeIndirect);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterObjectMethod", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterObjectMethod(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* obj, [NativeTypeName("const char *")] sbyte* declaration, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* funcPointer, [NativeTypeName("asDWORD")] uint callConv, void* auxiliary, int compositeOffset, bool isCompositeIndirect);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterObjectBehaviour", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterObjectBehaviour(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* obj, asEBehaviours behaviour, [NativeTypeName("const char *")] sbyte* declaration, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* funcPointer, [NativeTypeName("asDWORD")] uint callConv, void* auxiliary, int compositeOffset, bool isCompositeIndirect);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterInterface", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterInterface(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterInterfaceMethod", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterInterfaceMethod(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* intf, [NativeTypeName("const char *")] sbyte* declaration);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetObjectTypeCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetObjectTypeCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetObjectTypeByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetObjectTypeByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterStringFactory", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterStringFactory(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* datatype, asStringFactory* factory);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetStringFactory", ExactSpelling = true)]
    public static extern int ScriptEngine_GetStringFactory(asScriptEngine* engine, [NativeTypeName("asDWORD *")] uint* typeModifiers, asStringFactory** factory);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterDefaultArrayType", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterDefaultArrayType(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetDefaultArrayTypeId", ExactSpelling = true)]
    public static extern int ScriptEngine_GetDefaultArrayTypeId(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterEnum", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterEnum(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterEnumValue", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterEnumValue(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("const char *")] sbyte* name, int value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetEnumCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetEnumCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetEnumByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetEnumByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterFuncdef", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterFuncdef(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetFuncdefCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetFuncdefCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetFuncdefByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetFuncdefByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RegisterTypedef", ExactSpelling = true)]
    public static extern int ScriptEngine_RegisterTypedef(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypedefCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetTypedefCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypedefByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetTypedefByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_BeginConfigGroup", ExactSpelling = true)]
    public static extern int ScriptEngine_BeginConfigGroup(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* groupName);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_EndConfigGroup", ExactSpelling = true)]
    public static extern int ScriptEngine_EndConfigGroup(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RemoveConfigGroup", ExactSpelling = true)]
    public static extern int ScriptEngine_RemoveConfigGroup(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* groupName);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetDefaultAccessMask", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD")]
    public static extern uint ScriptEngine_SetDefaultAccessMask(asScriptEngine* engine, [NativeTypeName("asDWORD")] uint defaultMask);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetDefaultNamespace", ExactSpelling = true)]
    public static extern int ScriptEngine_SetDefaultNamespace(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* nameSpace);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetDefaultNamespace", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptEngine_GetDefaultNamespace(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetModule", ExactSpelling = true)]
    public static extern asScriptModule* ScriptEngine_GetModule(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* module, asEGMFlags flag);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_DiscardModule", ExactSpelling = true)]
    public static extern int ScriptEngine_DiscardModule(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetModuleCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptEngine_GetModuleCount(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetModuleByIndex", ExactSpelling = true)]
    public static extern asScriptModule* ScriptEngine_GetModuleByIndex(asScriptEngine* engine, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetLastFunctionId", ExactSpelling = true)]
    public static extern int ScriptEngine_GetLastFunctionId(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetFunctionById", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptEngine_GetFunctionById(asScriptEngine* engine, int funcId);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypeIdByDecl", ExactSpelling = true)]
    public static extern int ScriptEngine_GetTypeIdByDecl(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypeDeclaration", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptEngine_GetTypeDeclaration(asScriptEngine* engine, int typeId, bool includeNamespace);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetSizeOfPrimitiveType", ExactSpelling = true)]
    public static extern int ScriptEngine_GetSizeOfPrimitiveType(asScriptEngine* engine, int typeId);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypeInfoById", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetTypeInfoById(asScriptEngine* engine, int typeId);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypeInfoByName", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetTypeInfoByName(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetTypeInfoByDecl", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptEngine_GetTypeInfoByDecl(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_CreateContext", ExactSpelling = true)]
    public static extern asScriptContext* ScriptEngine_CreateContext(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_CreateScriptObject", ExactSpelling = true)]
    public static extern void* ScriptEngine_CreateScriptObject(asScriptEngine* engine, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_CreateScriptObjectCopy", ExactSpelling = true)]
    public static extern void* ScriptEngine_CreateScriptObjectCopy(asScriptEngine* engine, void* obj, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_CreateUninitializedScriptObject", ExactSpelling = true)]
    public static extern void* ScriptEngine_CreateUninitializedScriptObject(asScriptEngine* engine, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_CreateDelegate", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptEngine_CreateDelegate(asScriptEngine* engine, asScriptFunction* func, void* obj);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_AssignScriptObject", ExactSpelling = true)]
    public static extern int ScriptEngine_AssignScriptObject(asScriptEngine* engine, void* dstObj, void* srcObj, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ReleaseScriptObject", ExactSpelling = true)]
    public static extern void ScriptEngine_ReleaseScriptObject(asScriptEngine* engine, void* obj, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_AddRefScriptObject", ExactSpelling = true)]
    public static extern void ScriptEngine_AddRefScriptObject(asScriptEngine* engine, void* obj, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RefCastObject", ExactSpelling = true)]
    public static extern int ScriptEngine_RefCastObject(asScriptEngine* engine, void* obj, asTypeInfo* fromType, asTypeInfo* toType, void** newPtr, bool useOnlyImplicitCast);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetWeakRefFlagOfScriptObject", ExactSpelling = true)]
    public static extern asLockableSharedBool* ScriptEngine_GetWeakRefFlagOfScriptObject(asScriptEngine* engine, void* obj, [NativeTypeName("const asTypeInfo *")] asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_RequestContext", ExactSpelling = true)]
    public static extern asScriptContext* ScriptEngine_RequestContext(asScriptEngine* engine);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ReturnContext", ExactSpelling = true)]
    public static extern void ScriptEngine_ReturnContext(asScriptEngine* engine, asScriptContext* ctx);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetContextCallbacks", ExactSpelling = true)]
    public static extern int ScriptEngine_SetContextCallbacks(asScriptEngine* engine, [NativeTypeName("asREQUESTCONTEXTFUNC_t")] IntPtr requestCtx, [NativeTypeName("asRETURNCONTEXTFUNC_t")] IntPtr returnCtx, void* param3);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ParseToken", ExactSpelling = true)]
    public static extern asETokenClass ScriptEngine_ParseToken(asScriptEngine* engine, [NativeTypeName("const char *")] sbyte* @string, [NativeTypeName("size_t")] UIntPtr stringLength, [NativeTypeName("asUINT *")] uint* tokenLength);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GarbageCollect", ExactSpelling = true)]
    public static extern int ScriptEngine_GarbageCollect(asScriptEngine* engine, [NativeTypeName("asDWORD")] uint flags, [NativeTypeName("asUINT")] uint numIterations);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetGCStatistics", ExactSpelling = true)]
    public static extern void ScriptEngine_GetGCStatistics(asScriptEngine* engine, [NativeTypeName("asUINT *")] uint* currentSize, [NativeTypeName("asUINT *")] uint* totalDestroyed, [NativeTypeName("asUINT *")] uint* totalDetected, [NativeTypeName("asUINT *")] uint* newObjects, [NativeTypeName("asUINT *")] uint* totalNewDestroyed);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_NotifyGarbageCollectorOfNewObject", ExactSpelling = true)]
    public static extern int ScriptEngine_NotifyGarbageCollectorOfNewObject(asScriptEngine* engine, void* obj, asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetObjectInGC", ExactSpelling = true)]
    public static extern int ScriptEngine_GetObjectInGC(asScriptEngine* engine, [NativeTypeName("asUINT")] uint idx, [NativeTypeName("asUINT *")] uint* seqNbr, void** obj, asTypeInfo** type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GCEnumCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_GCEnumCallback(asScriptEngine* engine, void* reference);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ForwardGCEnumReferences", ExactSpelling = true)]
    public static extern void ScriptEngine_ForwardGCEnumReferences(asScriptEngine* engine, void* @ref, asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_ForwardGCReleaseReferences", ExactSpelling = true)]
    public static extern void ScriptEngine_ForwardGCReleaseReferences(asScriptEngine* engine, void* @ref, asTypeInfo* type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetCircularRefDetectedCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetCircularRefDetectedCallback(asScriptEngine* engine, [NativeTypeName("asCIRCULARREFFUNC_t")] IntPtr callback, void* param2);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetUserData", ExactSpelling = true)]
    public static extern void* ScriptEngine_SetUserData(asScriptEngine* engine, void* data, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_GetUserData", ExactSpelling = true)]
    public static extern void* ScriptEngine_GetUserData(asScriptEngine* engine, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetEngineUserDataCleanupCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetEngineUserDataCleanupCallback(asScriptEngine* engine, [NativeTypeName("asCLEANENGINEFUNC_t")] IntPtr callback, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetModuleUserDataCleanupCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetModuleUserDataCleanupCallback(asScriptEngine* engine, [NativeTypeName("asCLEANMODULEFUNC_t")] IntPtr callback, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetContextUserDataCleanupCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetContextUserDataCleanupCallback(asScriptEngine* engine, [NativeTypeName("asCLEANCONTEXTFUNC_t")] IntPtr callback, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetFunctionUserDataCleanupCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetFunctionUserDataCleanupCallback(asScriptEngine* engine, [NativeTypeName("asCLEANFUNCTIONFUNC_t")] IntPtr callback, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetTypeInfoUserDataCleanupCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetTypeInfoUserDataCleanupCallback(asScriptEngine* engine, [NativeTypeName("asCLEANTYPEINFOFUNC_t")] IntPtr callback, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetScriptObjectUserDataCleanupCallback", ExactSpelling = true)]
    public static extern void ScriptEngine_SetScriptObjectUserDataCleanupCallback(asScriptEngine* engine, [NativeTypeName("asCLEANSCRIPTOBJECTFUNC_t")] IntPtr callback, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptEngine_SetTranslateAppExceptionCallback", ExactSpelling = true)]
    public static extern int ScriptEngine_SetTranslateAppExceptionCallback(asScriptEngine* engine, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* callback, void* param2, int callConv);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetEngine", ExactSpelling = true)]
    public static extern asScriptEngine* ScriptModule_GetEngine(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_SetName", ExactSpelling = true)]
    public static extern void ScriptModule_SetName(asScriptModule* module, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetName", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptModule_GetName(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_Discard", ExactSpelling = true)]
    public static extern void ScriptModule_Discard(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_AddScriptSection", ExactSpelling = true)]
    public static extern int ScriptModule_AddScriptSection(asScriptModule* module, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* code, [NativeTypeName("size_t")] UIntPtr codeLength, int lineOffset);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_Build", ExactSpelling = true)]
    public static extern int ScriptModule_Build(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_CompileFunction", ExactSpelling = true)]
    public static extern int ScriptModule_CompileFunction(asScriptModule* module, [NativeTypeName("const char *")] sbyte* sectionName, [NativeTypeName("const char *")] sbyte* code, int lineOffset, [NativeTypeName("asDWORD")] uint compileFlags, asScriptFunction** outFunc);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_CompileGlobalVar", ExactSpelling = true)]
    public static extern int ScriptModule_CompileGlobalVar(asScriptModule* module, [NativeTypeName("const char *")] sbyte* sectionName, [NativeTypeName("const char *")] sbyte* code, int lineOffset);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_SetAccessMask", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD")]
    public static extern uint ScriptModule_SetAccessMask(asScriptModule* module, [NativeTypeName("asDWORD")] uint accessMask);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_SetDefaultNamespace", ExactSpelling = true)]
    public static extern int ScriptModule_SetDefaultNamespace(asScriptModule* module, [NativeTypeName("const char *")] sbyte* nameSpace);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetDefaultNamespace", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptModule_GetDefaultNamespace(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetFunctionCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptModule_GetFunctionCount(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetFunctionByIndex", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptModule_GetFunctionByIndex(asScriptModule* module, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetFunctionByDecl", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptModule_GetFunctionByDecl(asScriptModule* module, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetFunctionByName", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptModule_GetFunctionByName(asScriptModule* module, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_RemoveFunction", ExactSpelling = true)]
    public static extern int ScriptModule_RemoveFunction(asScriptModule* module, asScriptFunction* func);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_ResetGlobalVars", ExactSpelling = true)]
    public static extern int ScriptModule_ResetGlobalVars(asScriptModule* module, asScriptContext* ctx);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetGlobalVarCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptModule_GetGlobalVarCount(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetGlobalVarIndexByName", ExactSpelling = true)]
    public static extern int ScriptModule_GetGlobalVarIndexByName(asScriptModule* module, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetGlobalVarIndexByDecl", ExactSpelling = true)]
    public static extern int ScriptModule_GetGlobalVarIndexByDecl(asScriptModule* module, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetGlobalVarDeclaration", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptModule_GetGlobalVarDeclaration(asScriptModule* module, [NativeTypeName("asUINT")] uint index, bool includeNamespace);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetGlobalVar", ExactSpelling = true)]
    public static extern int ScriptModule_GetGlobalVar(asScriptModule* module, [NativeTypeName("asUINT")] uint index, [NativeTypeName("const char **")] sbyte** name, [NativeTypeName("const char **")] sbyte** nameSpace, int* typeId, bool* isConst);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetAddressOfGlobalVar", ExactSpelling = true)]
    public static extern void* ScriptModule_GetAddressOfGlobalVar(asScriptModule* module, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_RemoveGlobalVar", ExactSpelling = true)]
    public static extern int ScriptModule_RemoveGlobalVar(asScriptModule* module, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetObjectTypeCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptModule_GetObjectTypeCount(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetObjectTypeByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptModule_GetObjectTypeByIndex(asScriptModule* module, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetTypeIdByDecl", ExactSpelling = true)]
    public static extern int ScriptModule_GetTypeIdByDecl(asScriptModule* module, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetTypeInfoByName", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptModule_GetTypeInfoByName(asScriptModule* module, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetTypeInfoByDecl", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptModule_GetTypeInfoByDecl(asScriptModule* module, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetEnumCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptModule_GetEnumCount(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetEnumByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptModule_GetEnumByIndex(asScriptModule* module, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetTypedefCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptModule_GetTypedefCount(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetTypedefByIndex", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptModule_GetTypedefByIndex(asScriptModule* module, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetImportedFunctionCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptModule_GetImportedFunctionCount(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetImportedFunctionIndexByDecl", ExactSpelling = true)]
    public static extern int ScriptModule_GetImportedFunctionIndexByDecl(asScriptModule* module, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetImportedFunctionDeclaration", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptModule_GetImportedFunctionDeclaration(asScriptModule* module, [NativeTypeName("asUINT")] uint importIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetImportedFunctionSourceModule", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptModule_GetImportedFunctionSourceModule(asScriptModule* module, [NativeTypeName("asUINT")] uint importIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_BindImportedFunction", ExactSpelling = true)]
    public static extern int ScriptModule_BindImportedFunction(asScriptModule* module, [NativeTypeName("asUINT")] uint importIndex, asScriptFunction* func);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_UnbindImportedFunction", ExactSpelling = true)]
    public static extern int ScriptModule_UnbindImportedFunction(asScriptModule* module, [NativeTypeName("asUINT")] uint importIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_BindAllImportedFunctions", ExactSpelling = true)]
    public static extern int ScriptModule_BindAllImportedFunctions(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_UnbindAllImportedFunctions", ExactSpelling = true)]
    public static extern int ScriptModule_UnbindAllImportedFunctions(asScriptModule* module);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_SaveByteCode", ExactSpelling = true)]
    public static extern int ScriptModule_SaveByteCode(asScriptModule* module, asBinaryStream* @out, bool stripDebugInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_LoadByteCode", ExactSpelling = true)]
    public static extern int ScriptModule_LoadByteCode(asScriptModule* module, asBinaryStream* @in, bool* wasDebugInfoStripped);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_SetUserData", ExactSpelling = true)]
    public static extern void* ScriptModule_SetUserData(asScriptModule* module, void* data, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptModule_GetUserData", ExactSpelling = true)]
    public static extern void* ScriptModule_GetUserData(asScriptModule* module, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetEngine", ExactSpelling = true)]
    public static extern asScriptEngine* TypeInfo_GetEngine(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetConfigGroup", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* TypeInfo_GetConfigGroup(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetAccessMask", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD")]
    public static extern uint TypeInfo_GetAccessMask(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetModule", ExactSpelling = true)]
    public static extern asScriptModule* TypeInfo_GetModule(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_AddRef", ExactSpelling = true)]
    public static extern int TypeInfo_AddRef(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_Release", ExactSpelling = true)]
    public static extern int TypeInfo_Release(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetName", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* TypeInfo_GetName(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetNamespace", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* TypeInfo_GetNamespace(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetBaseType", ExactSpelling = true)]
    public static extern asTypeInfo* TypeInfo_GetBaseType(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_DerivesFrom", ExactSpelling = true)]
    public static extern bool TypeInfo_DerivesFrom(asTypeInfo* typeInfo, [NativeTypeName("const asTypeInfo *")] asTypeInfo* objType);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetFlags", ExactSpelling = true)]
    [return: NativeTypeName("asQWORD")]
    public static extern ulong TypeInfo_GetFlags(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetSize", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetSize(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetTypeId", ExactSpelling = true)]
    public static extern int TypeInfo_GetTypeId(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetSubTypeId", ExactSpelling = true)]
    public static extern int TypeInfo_GetSubTypeId(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint subTypeIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetSubType", ExactSpelling = true)]
    public static extern asTypeInfo* TypeInfo_GetSubType(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint subTypeIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetSubTypeCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetSubTypeCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetInterfaceCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetInterfaceCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetInterface", ExactSpelling = true)]
    public static extern asTypeInfo* TypeInfo_GetInterface(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_Implements", ExactSpelling = true)]
    public static extern bool TypeInfo_Implements(asTypeInfo* typeInfo, [NativeTypeName("const asTypeInfo *")] asTypeInfo* objType);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetFactoryCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetFactoryCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetFactoryByIndex", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetFactoryByIndex(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetFactoryByDecl", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetFactoryByDecl(asTypeInfo* typeInfo, [NativeTypeName("const char *")] sbyte* decl);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetMethodCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetMethodCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetMethodByIndex", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetMethodByIndex(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index, bool getVirtual);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetMethodByName", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetMethodByName(asTypeInfo* typeInfo, [NativeTypeName("const char *")] sbyte* name, bool getVirtual);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetMethodByDecl", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetMethodByDecl(asTypeInfo* typeInfo, [NativeTypeName("const char *")] sbyte* decl, bool getVirtual);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetPropertyCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetPropertyCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetProperty", ExactSpelling = true)]
    public static extern int TypeInfo_GetProperty(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index, [NativeTypeName("const char **")] sbyte** name, int* typeId, bool* isPrivate, bool* isProtected, int* offset, bool* isReference, [NativeTypeName("asDWORD *")] uint* accessMask, int* compositeOffset, bool* isCompositeIndirect, bool* isConst);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetPropertyDeclaration", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* TypeInfo_GetPropertyDeclaration(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index, bool includeNamespace);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetBehaviourCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetBehaviourCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetBehaviourByIndex", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetBehaviourByIndex(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index, asEBehaviours* outBehaviour);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetChildFuncdefCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetChildFuncdefCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetChildFuncdef", ExactSpelling = true)]
    public static extern asTypeInfo* TypeInfo_GetChildFuncdef(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetParentType", ExactSpelling = true)]
    public static extern asTypeInfo* TypeInfo_GetParentType(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetEnumValueCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint TypeInfo_GetEnumValueCount(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetEnumValueByIndex", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* TypeInfo_GetEnumValueByIndex(asTypeInfo* typeInfo, [NativeTypeName("asUINT")] uint index, int* outValue);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetTypedefTypeId", ExactSpelling = true)]
    public static extern int TypeInfo_GetTypedefTypeId(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetFuncdefSignature", ExactSpelling = true)]
    public static extern asScriptFunction* TypeInfo_GetFuncdefSignature(asTypeInfo* typeInfo);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_SetUserData", ExactSpelling = true)]
    public static extern void* TypeInfo_SetUserData(asTypeInfo* typeInfo, void* data, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asTypeInfo_GetUserData", ExactSpelling = true)]
    public static extern void* TypeInfo_GetUserData(asTypeInfo* typeInfo, [NativeTypeName("asPWORD")] UIntPtr type);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_AddRef", ExactSpelling = true)]
    public static extern int ScriptContext_AddRef(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_Release", ExactSpelling = true)]
    public static extern int ScriptContext_Release(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetEngine", ExactSpelling = true)]
    public static extern asScriptEngine* ScriptContext_GetEngine(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_Prepare", ExactSpelling = true)]
    public static extern int ScriptContext_Prepare(asScriptContext* context, asScriptFunction* func);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_Unprepare", ExactSpelling = true)]
    public static extern int ScriptContext_Unprepare(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_Execute", ExactSpelling = true)]
    public static extern int ScriptContext_Execute(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_Abort", ExactSpelling = true)]
    public static extern int ScriptContext_Abort(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_Suspend", ExactSpelling = true)]
    public static extern int ScriptContext_Suspend(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetState", ExactSpelling = true)]
    public static extern asEContextState ScriptContext_GetState(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_PushState", ExactSpelling = true)]
    public static extern int ScriptContext_PushState(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_PopState", ExactSpelling = true)]
    public static extern int ScriptContext_PopState(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_IsNested", ExactSpelling = true)]
    public static extern bool ScriptContext_IsNested(asScriptContext* context, [NativeTypeName("asUINT *")] uint* nestCount = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetObject", ExactSpelling = true)]
    public static extern int ScriptContext_SetObject(asScriptContext* context, void* obj);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgByte", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgByte(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, [NativeTypeName("asBYTE")] byte value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgWord", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgWord(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, [NativeTypeName("asWORD")] ushort value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgDWord", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgDWord(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, [NativeTypeName("asDWORD")] uint value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgQWord", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgQWord(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, [NativeTypeName("asQWORD")] ulong value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgFloat", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgFloat(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, float value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgDouble", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgDouble(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, double value);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgAddress", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgAddress(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, void* addr);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgObject", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgObject(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, void* obj);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetArgVarType", ExactSpelling = true)]
    public static extern int ScriptContext_SetArgVarType(asScriptContext* context, [NativeTypeName("asUINT")] uint arg, void* ptr, int typeId);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetAddressOfArg", ExactSpelling = true)]
    public static extern void* ScriptContext_GetAddressOfArg(asScriptContext* context, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnByte", ExactSpelling = true)]
    [return: NativeTypeName("asBYTE")]
    public static extern byte ScriptContext_GetReturnByte(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnWord", ExactSpelling = true)]
    [return: NativeTypeName("asWORD")]
    public static extern ushort ScriptContext_GetReturnWord(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnDWord", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD")]
    public static extern uint ScriptContext_GetReturnDWord(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnQWord", ExactSpelling = true)]
    [return: NativeTypeName("asQWORD")]
    public static extern ulong ScriptContext_GetReturnQWord(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnFloat", ExactSpelling = true)]
    public static extern float ScriptContext_GetReturnFloat(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnDouble", ExactSpelling = true)]
    public static extern double ScriptContext_GetReturnDouble(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnAddress", ExactSpelling = true)]
    public static extern void* ScriptContext_GetReturnAddress(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetReturnObject", ExactSpelling = true)]
    public static extern void* ScriptContext_GetReturnObject(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetAddressOfReturnValue", ExactSpelling = true)]
    public static extern void* ScriptContext_GetAddressOfReturnValue(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetException", ExactSpelling = true)]
    public static extern int ScriptContext_SetException(asScriptContext* context, [NativeTypeName("const char *")] sbyte* info, bool allowCatch = true);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetExceptionLineNumber", ExactSpelling = true)]
    public static extern int ScriptContext_GetExceptionLineNumber(asScriptContext* context, int* column = null, [NativeTypeName("const char **")] sbyte** sectionName = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetExceptionFunction", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptContext_GetExceptionFunction(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetExceptionString", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptContext_GetExceptionString(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_WillExceptionBeCaught", ExactSpelling = true)]
    public static extern bool ScriptContext_WillExceptionBeCaught(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetExceptionCallback", ExactSpelling = true)]
    public static extern int ScriptContext_SetExceptionCallback(asScriptContext* context, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* callback, void* obj, int callConv);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_ClearExceptionCallback", ExactSpelling = true)]
    public static extern void ScriptContext_ClearExceptionCallback(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetLineCallback", ExactSpelling = true)]
    public static extern int ScriptContext_SetLineCallback(asScriptContext* context, [NativeTypeName("const asSFuncPtr &")] asSFuncPtr* callback, void* obj, int callConv);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_ClearLineCallback", ExactSpelling = true)]
    public static extern void ScriptContext_ClearLineCallback(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetCallstackSize", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptContext_GetCallstackSize(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetFunction", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptContext_GetFunction(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetLineNumber", ExactSpelling = true)]
    public static extern int ScriptContext_GetLineNumber(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel = 0, int* column = null, [NativeTypeName("const char **")] sbyte** sectionName = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetVarCount", ExactSpelling = true)]
    public static extern int ScriptContext_GetVarCount(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetVar", ExactSpelling = true)]
    public static extern int ScriptContext_GetVar(asScriptContext* context, [NativeTypeName("asUINT")] uint varIndex, [NativeTypeName("asUINT")] uint stackLevel, [NativeTypeName("const char **")] sbyte** name, int* typeId = null, asETypeModifiers* typeModifiers = null, bool* isVarOnHeap = null, int* stackOffset = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetVarDeclaration", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptContext_GetVarDeclaration(asScriptContext* context, [NativeTypeName("asUINT")] uint varIndex, [NativeTypeName("asUINT")] uint stackLevel = 0, bool includeNamespace = false);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetAddressOfVar", ExactSpelling = true)]
    public static extern void* ScriptContext_GetAddressOfVar(asScriptContext* context, [NativeTypeName("asUINT")] uint varIndex, [NativeTypeName("asUINT")] uint stackLevel = 0, bool dontDereference = false, bool returnAddressOfUnitializedObjects = false);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_IsVarInScope", ExactSpelling = true)]
    public static extern bool ScriptContext_IsVarInScope(asScriptContext* context, [NativeTypeName("asUINT")] uint varIndex, [NativeTypeName("asUINT")] uint stackLevel = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetThisTypeId", ExactSpelling = true)]
    public static extern int ScriptContext_GetThisTypeId(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetThisPointer", ExactSpelling = true)]
    public static extern void* ScriptContext_GetThisPointer(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetSystemFunction", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptContext_GetSystemFunction(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetUserData", ExactSpelling = true)]
    public static extern void* ScriptContext_SetUserData(asScriptContext* context, void* data, [NativeTypeName("asPWORD")] UIntPtr type = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetUserData", ExactSpelling = true)]
    public static extern void* ScriptContext_GetUserData(asScriptContext* context, [NativeTypeName("asPWORD")] UIntPtr type = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_StartDeserialization", ExactSpelling = true)]
    public static extern int ScriptContext_StartDeserialization(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_FinishDeserialization", ExactSpelling = true)]
    public static extern int ScriptContext_FinishDeserialization(asScriptContext* context);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_PushFunction", ExactSpelling = true)]
    public static extern int ScriptContext_PushFunction(asScriptContext* context, asScriptFunction* func, void* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetStateRegisters", ExactSpelling = true)]
    public static extern int ScriptContext_GetStateRegisters(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel, asScriptFunction** callingSystemFunction, asScriptFunction** initialFunction, [NativeTypeName("asDWORD *")] uint* origStackPointer, [NativeTypeName("asDWORD *")] uint* argumentsSize, [NativeTypeName("asQWORD *")] ulong* valueRegister, void** objectRegister, asTypeInfo** objectTypeRegister);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetCallStateRegisters", ExactSpelling = true)]
    public static extern int ScriptContext_GetCallStateRegisters(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel, [NativeTypeName("asDWORD *")] uint* stackFramePointer, asScriptFunction** currentFunction, [NativeTypeName("asDWORD *")] uint* programPointer, [NativeTypeName("asDWORD *")] uint* stackPointer, [NativeTypeName("asDWORD *")] uint* stackIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetStateRegisters", ExactSpelling = true)]
    public static extern int ScriptContext_SetStateRegisters(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel, asScriptFunction* callingSystemFunction, asScriptFunction* initialFunction, [NativeTypeName("asDWORD")] uint origStackPointer, [NativeTypeName("asDWORD")] uint argumentsSize, [NativeTypeName("asQWORD")] ulong valueRegister, void* objectRegister, asTypeInfo* objectTypeRegister);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_SetCallStateRegisters", ExactSpelling = true)]
    public static extern int ScriptContext_SetCallStateRegisters(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel, [NativeTypeName("asDWORD")] uint stackFramePointer, asScriptFunction* currentFunction, [NativeTypeName("asDWORD")] uint programPointer, [NativeTypeName("asDWORD")] uint stackPointer, [NativeTypeName("asDWORD")] uint stackIndex);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetArgsOnStackCount", ExactSpelling = true)]
    public static extern int ScriptContext_GetArgsOnStackCount(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptContext_GetArgOnStack", ExactSpelling = true)]
    public static extern int ScriptContext_GetArgOnStack(asScriptContext* context, [NativeTypeName("asUINT")] uint stackLevel, [NativeTypeName("asUINT")] uint arg, int* typeId, [NativeTypeName("asUINT *")] uint* flags, void** address);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetEngine", ExactSpelling = true)]
    public static extern asScriptEngine* ScriptGeneric_GetEngine(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetFunction", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptGeneric_GetFunction(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetAuxiliary", ExactSpelling = true)]
    public static extern void* ScriptGeneric_GetAuxiliary(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetObject", ExactSpelling = true)]
    public static extern void* ScriptGeneric_GetObject(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetObjectTypeId", ExactSpelling = true)]
    public static extern int ScriptGeneric_GetObjectTypeId(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgCount", ExactSpelling = true)]
    public static extern int ScriptGeneric_GetArgCount(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgTypeId", ExactSpelling = true)]
    public static extern int ScriptGeneric_GetArgTypeId(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg, [NativeTypeName("asDWORD *")] uint* flags = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgByte", ExactSpelling = true)]
    [return: NativeTypeName("asBYTE")]
    public static extern byte ScriptGeneric_GetArgByte(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgWord", ExactSpelling = true)]
    [return: NativeTypeName("asWORD")]
    public static extern ushort ScriptGeneric_GetArgWord(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgDWord", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD")]
    public static extern uint ScriptGeneric_GetArgDWord(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgQWord", ExactSpelling = true)]
    [return: NativeTypeName("asQWORD")]
    public static extern ulong ScriptGeneric_GetArgQWord(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgFloat", ExactSpelling = true)]
    public static extern float ScriptGeneric_GetArgFloat(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgDouble", ExactSpelling = true)]
    public static extern double ScriptGeneric_GetArgDouble(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgAddress", ExactSpelling = true)]
    public static extern void* ScriptGeneric_GetArgAddress(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetArgObject", ExactSpelling = true)]
    public static extern void* ScriptGeneric_GetArgObject(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetAddressOfArg", ExactSpelling = true)]
    public static extern void* ScriptGeneric_GetAddressOfArg(asScriptGeneric* generic, [NativeTypeName("asUINT")] uint arg);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetReturnTypeId", ExactSpelling = true)]
    public static extern int ScriptGeneric_GetReturnTypeId(asScriptGeneric* generic, [NativeTypeName("asDWORD *")] uint* flags = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnByte", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnByte(asScriptGeneric* generic, [NativeTypeName("asBYTE")] byte val);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnWord", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnWord(asScriptGeneric* generic, [NativeTypeName("asWORD")] ushort val);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnDWord", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnDWord(asScriptGeneric* generic, [NativeTypeName("asDWORD")] uint val);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnQWord", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnQWord(asScriptGeneric* generic, [NativeTypeName("asQWORD")] ulong val);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnFloat", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnFloat(asScriptGeneric* generic, float val);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnDouble", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnDouble(asScriptGeneric* generic, double val);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnAddress", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnAddress(asScriptGeneric* generic, void* addr);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_SetReturnObject", ExactSpelling = true)]
    public static extern int ScriptGeneric_SetReturnObject(asScriptGeneric* generic, void* obj);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptGeneric_GetAddressOfReturnLocation", ExactSpelling = true)]
    public static extern void* ScriptGeneric_GetAddressOfReturnLocation(asScriptGeneric* generic);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_AddRef", ExactSpelling = true)]
    public static extern int ScriptObject_AddRef(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_Release", ExactSpelling = true)]
    public static extern int ScriptObject_Release(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetWeakRefFlag", ExactSpelling = true)]
    public static extern asLockableSharedBool* ScriptObject_GetWeakRefFlag(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetTypeId", ExactSpelling = true)]
    public static extern int ScriptObject_GetTypeId(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetObjectType", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptObject_GetObjectType(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetPropertyCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptObject_GetPropertyCount(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetPropertyTypeId", ExactSpelling = true)]
    public static extern int ScriptObject_GetPropertyTypeId(asScriptObject* @object, [NativeTypeName("asUINT")] uint prop);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetPropertyName", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptObject_GetPropertyName(asScriptObject* @object, [NativeTypeName("asUINT")] uint prop);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetAddressOfProperty", ExactSpelling = true)]
    public static extern void* ScriptObject_GetAddressOfProperty(asScriptObject* @object, [NativeTypeName("asUINT")] uint prop);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetEngine", ExactSpelling = true)]
    public static extern asScriptEngine* ScriptObject_GetEngine(asScriptObject* @object);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_CopyFrom", ExactSpelling = true)]
    public static extern int ScriptObject_CopyFrom(asScriptObject* @object, [NativeTypeName("const asScriptObject *")] asScriptObject* other);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_SetUserData", ExactSpelling = true)]
    public static extern void* ScriptObject_SetUserData(asScriptObject* @object, void* data, [NativeTypeName("asPWORD")] UIntPtr type = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptObject_GetUserData", ExactSpelling = true)]
    public static extern void* ScriptObject_GetUserData(asScriptObject* @object, [NativeTypeName("asPWORD")] UIntPtr type = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetEngine", ExactSpelling = true)]
    public static extern asScriptEngine* ScriptFunction_GetEngine(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_AddRef", ExactSpelling = true)]
    public static extern int ScriptFunction_AddRef(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_Release", ExactSpelling = true)]
    public static extern int ScriptFunction_Release(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetId", ExactSpelling = true)]
    public static extern int ScriptFunction_GetId(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetFuncType", ExactSpelling = true)]
    public static extern asEFuncType ScriptFunction_GetFuncType(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetModuleName", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetModuleName(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetModule", ExactSpelling = true)]
    public static extern asScriptModule* ScriptFunction_GetModule(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetConfigGroup", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetConfigGroup(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetAccessMask", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD")]
    public static extern uint ScriptFunction_GetAccessMask(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetAuxiliary", ExactSpelling = true)]
    public static extern void* ScriptFunction_GetAuxiliary(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetObjectType", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptFunction_GetObjectType(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetObjectName", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetObjectName(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetName", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetName(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetNamespace", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetNamespace(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetDeclaration", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetDeclaration(asScriptFunction* function, bool includeObjectName = true, bool includeNamespace = false, bool includeParamNames = false);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsReadOnly", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsReadOnly(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsPrivate", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsPrivate(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsProtected", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsProtected(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsFinal", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsFinal(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsOverride", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsOverride(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsShared", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsShared(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsExplicit", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsExplicit(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsProperty", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsProperty(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsVariadic", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsVariadic(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetParamCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptFunction_GetParamCount(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetParam", ExactSpelling = true)]
    public static extern int ScriptFunction_GetParam(asScriptFunction* function, [NativeTypeName("asUINT")] uint index, int* typeId, [NativeTypeName("asDWORD *")] uint* flags = null, [NativeTypeName("const char **")] sbyte** name = null, [NativeTypeName("const char **")] sbyte** defaultArg = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetReturnTypeId", ExactSpelling = true)]
    public static extern int ScriptFunction_GetReturnTypeId(asScriptFunction* function, [NativeTypeName("asDWORD *")] uint* flags = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetSubTypeCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptFunction_GetSubTypeCount(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetSubTypeId", ExactSpelling = true)]
    public static extern int ScriptFunction_GetSubTypeId(asScriptFunction* function, [NativeTypeName("asUINT")] uint subTypeIndex = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetSubType", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptFunction_GetSubType(asScriptFunction* function, [NativeTypeName("asUINT")] uint subTypeIndex = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetTypeId", ExactSpelling = true)]
    public static extern int ScriptFunction_GetTypeId(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_IsCompatibleWithTypeId", ExactSpelling = true)]
    public static extern bool ScriptFunction_IsCompatibleWithTypeId(asScriptFunction* function, int typeId);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetDelegateObject", ExactSpelling = true)]
    public static extern void* ScriptFunction_GetDelegateObject(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetDelegateObjectType", ExactSpelling = true)]
    public static extern asTypeInfo* ScriptFunction_GetDelegateObjectType(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetDelegateFunction", ExactSpelling = true)]
    public static extern asScriptFunction* ScriptFunction_GetDelegateFunction(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetVarCount", ExactSpelling = true)]
    [return: NativeTypeName("asUINT")]
    public static extern uint ScriptFunction_GetVarCount(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetVar", ExactSpelling = true)]
    public static extern int ScriptFunction_GetVar(asScriptFunction* function, [NativeTypeName("asUINT")] uint index, [NativeTypeName("const char **")] sbyte** name, int* typeId = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetVarDecl", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* ScriptFunction_GetVarDecl(asScriptFunction* function, [NativeTypeName("asUINT")] uint index, bool includeNamespace = false);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_FindNextLineWithCode", ExactSpelling = true)]
    public static extern int ScriptFunction_FindNextLineWithCode(asScriptFunction* function, int line);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetDeclaredAt", ExactSpelling = true)]
    public static extern int ScriptFunction_GetDeclaredAt(asScriptFunction* function, [NativeTypeName("const char **")] sbyte** scriptSection, int* row, int* col);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetLineEntryCount", ExactSpelling = true)]
    public static extern int ScriptFunction_GetLineEntryCount(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetLineEntry", ExactSpelling = true)]
    public static extern int ScriptFunction_GetLineEntry(asScriptFunction* function, [NativeTypeName("asUINT")] uint index, int* row, int* col, [NativeTypeName("const char **")] sbyte** sectionName, [NativeTypeName("const asDWORD **")] uint** byteCode);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetByteCode", ExactSpelling = true)]
    [return: NativeTypeName("asDWORD *")]
    public static extern uint* ScriptFunction_GetByteCode(asScriptFunction* function, [NativeTypeName("asUINT *")] uint* length = null);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_SetJITFunction", ExactSpelling = true)]
    public static extern int ScriptFunction_SetJITFunction(asScriptFunction* function, [NativeTypeName("asJITFunction")] IntPtr jitFunc);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetJITFunction", ExactSpelling = true)]
    [return: NativeTypeName("asJITFunction")]
    public static extern IntPtr ScriptFunction_GetJITFunction(asScriptFunction* function);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_SetUserData", ExactSpelling = true)]
    public static extern void* ScriptFunction_SetUserData(asScriptFunction* function, void* userData, [NativeTypeName("asPWORD")] UIntPtr type = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asScriptFunction_GetUserData", ExactSpelling = true)]
    public static extern void* ScriptFunction_GetUserData(asScriptFunction* function, [NativeTypeName("asPWORD")] UIntPtr type = 0);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asBinaryStream_Create", ExactSpelling = true)]
    public static extern asBinaryStream* BinaryStream_Create(asSBinaryStream @interface);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asBinaryStream_Destroy", ExactSpelling = true)]
    public static extern void BinaryStream_Destroy(asBinaryStream* stream);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asStringFactory_Create", ExactSpelling = true)]
    public static extern asStringFactory* StringFactory_Create(asSStringFactory @interface);

    [DllImport("AngelScript.Native", CallingConvention = CallingConvention.Cdecl, EntryPoint = "asStringFactory_Destroy", ExactSpelling = true)]
    public static extern void StringFactory_Destroy(asStringFactory* stream);
}
