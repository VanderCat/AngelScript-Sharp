#define ANGELSCRIPT_DLL_MANUAL_IMPORT
#include "../AngelScript.Native/sdk/angelscript/include/angelscript.h"
#include "angelscript_capi.h"

/// asScriptEngine
int asScriptEngine_AddRef(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->AddRef();
}

int asScriptEngine_Release(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->Release();
}

int asScriptEngine_ShutDownAndRelease(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ShutDownAndRelease();
}

int asScriptEngine_SetEngineProperty(asScriptEngine* engine, asEEngineProp property, asPWORD value) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetEngineProperty(property, value);
}

asPWORD asScriptEngine_GetEngineProperty(asScriptEngine* engine, asEEngineProp property) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetEngineProperty(property);
}

int asScriptEngine_SetMessageCallback(asScriptEngine* engine, const asSFuncPtr& callback, void* obj, asDWORD callConv) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetMessageCallback(callback, obj, callConv);
}
int asScriptEngine_GetMessageCallback(asScriptEngine* engine, asSFuncPtr *callback, void **obj, asDWORD *callConv) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetMessageCallback(callback, obj, callConv);
}
int asScriptEngine_ClearMessageCallback(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ClearMessageCallback();
}
int asScriptEngine_WriteMessage(asScriptEngine* engine, const char *section, int row, int col, asEMsgType type, const char *message) {
    return reinterpret_cast<asIScriptEngine*>(engine)->WriteMessage(section, row, col, type, message);
}

// JIT Compiler
int asScriptEngine_SetJITCompiler(asScriptEngine* engine, asJITCompilerAbstract* compiler) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetJITCompiler(reinterpret_cast<asIJITCompilerAbstract*>(compiler));
}
asJITCompilerAbstract *asScriptEngine_GetJITCompiler(asScriptEngine* engine) {
    return reinterpret_cast<asJITCompilerAbstract*>(reinterpret_cast<asIScriptEngine*>(engine)->GetJITCompiler());
}

// Global functions
int asScriptEngine_RegisterGlobalFunction(asScriptEngine* engine, const char *declaration, const asSFuncPtr &funcPointer, asDWORD callConv, void *auxiliary) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterGlobalFunction(declaration, funcPointer, callConv, auxiliary);
}
asUINT asScriptEngine_GetGlobalFunctionCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalFunctionCount();
}
asScriptFunction* asScriptEngine_GetGlobalFunctionByIndex(asScriptEngine* engine, asUINT index) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalFunctionByIndex(index));
}
asScriptFunction* asScriptEngine_GetGlobalFunctionByDecl(asScriptEngine* engine, const char *declaration) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalFunctionByDecl(declaration));
}

// Global properties
int asScriptEngine_RegisterGlobalProperty(asScriptEngine* engine, const char *declaration, void *pointer) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterGlobalProperty(declaration, pointer);
}
asUINT asScriptEngine_GetGlobalPropertyCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalPropertyCount();
}
int asScriptEngine_GetGlobalPropertyByIndex(asScriptEngine* engine, asUINT index, const char **name, const char **nameSpace, int *typeId, bool *isConst, const char **configGroup, void **pointer, asDWORD *accessMask) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalPropertyByIndex(index, name, nameSpace, typeId, isConst, configGroup, pointer, accessMask);
}
int asScriptEngine_GetGlobalPropertyIndexByName(asScriptEngine* engine, const char *name) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalPropertyIndexByName(name);
}
int asScriptEngine_GetGlobalPropertyIndexByDecl(asScriptEngine* engine, const char *decl) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetGlobalPropertyIndexByDecl(decl);
}

// Object types
int asScriptEngine_RegisterObjectType(asScriptEngine* engine, const char *obj, int byteSize, asQWORD flags) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterObjectType(obj, byteSize, flags);
}
int asScriptEngine_RegisterObjectProperty(asScriptEngine* engine, const char *obj, const char *declaration, int byteOffset, int compositeOffset, bool isCompositeIndirect) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterObjectProperty(obj, declaration, byteOffset, compositeOffset, isCompositeIndirect);
}
int asScriptEngine_RegisterObjectMethod(asScriptEngine* engine, const char *obj, const char *declaration, const asSFuncPtr &funcPointer, asDWORD callConv, void *auxiliary, int compositeOffset, bool isCompositeIndirect) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterObjectMethod(obj, declaration, funcPointer, callConv, auxiliary, compositeOffset, isCompositeIndirect);
}
int asScriptEngine_RegisterObjectBehaviour(asScriptEngine* engine, const char *obj, asEBehaviours behaviour, const char *declaration, const asSFuncPtr &funcPointer, asDWORD callConv, void *auxiliary, int compositeOffset, bool isCompositeIndirect) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterObjectBehaviour(obj, behaviour, declaration, funcPointer, callConv, auxiliary, compositeOffset, isCompositeIndirect);
}
int asScriptEngine_RegisterInterface(asScriptEngine* engine, const char *name) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterInterface(name);
}
int asScriptEngine_RegisterInterfaceMethod(asScriptEngine* engine, const char *intf, const char *declaration) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterInterfaceMethod(intf, declaration);
}
asUINT asScriptEngine_GetObjectTypeCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetObjectTypeCount();
}
asTypeInfo* asScriptEngine_GetObjectTypeByIndex(asScriptEngine* engine, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetObjectTypeByIndex(index));
}


class asIStringFactoryC final : public asIStringFactory {
public:
    asSStringFactory Interface;
    
    asIStringFactoryC(asSStringFactory interface) {
        Interface = interface;
    }
    
    const void *GetStringConstant(const char *data, asUINT length) override {
        return Interface.getStringConstantFunc(data, length, Interface.userdata);
    }
    int ReleaseStringConstant(const void *str) override
    {
        return Interface.releaseStringConstantFunc(str, Interface.userdata);
    }
    int GetRawStringData(const void *str, char *data, asUINT *length) const override
    {
        return Interface.getRawStringDataFunc(str, data, length, Interface.userdata);
    }

    ~asIStringFactoryC() override {
        Interface.destroyFunc(Interface.userdata);
    }
};

// String factory
int asScriptEngine_RegisterStringFactory(asScriptEngine* engine, const char *datatype, asStringFactory* factory) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterStringFactory(datatype, reinterpret_cast<asIStringFactory*>(factory));
}

int asScriptEngine_GetStringFactory(asScriptEngine* engine, asDWORD* typeModifiers, asStringFactory** factory) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetStringFactory(typeModifiers, reinterpret_cast<asIStringFactory**>(factory));
}

// Default array type
int asScriptEngine_RegisterDefaultArrayType(asScriptEngine* engine, const char *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterDefaultArrayType(type);
}
int asScriptEngine_GetDefaultArrayTypeId(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetDefaultArrayTypeId();
}

// Enums
int asScriptEngine_RegisterEnum(asScriptEngine* engine, const char *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterEnum(type);
}
int asScriptEngine_RegisterEnumValue(asScriptEngine* engine, const char *type, const char *name, int value) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterEnumValue(type, name, value);
}
asUINT asScriptEngine_GetEnumCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetEnumCount();
}
asTypeInfo* asScriptEngine_GetEnumByIndex(asScriptEngine* engine, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetEnumByIndex(index));
}

// Funcdefs
int asScriptEngine_RegisterFuncdef(asScriptEngine* engine, const char *decl) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterFuncdef(decl);
}
asUINT asScriptEngine_GetFuncdefCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetFuncdefCount();
}
asTypeInfo* asScriptEngine_GetFuncdefByIndex(asScriptEngine* engine, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetFuncdefByIndex(index));
}

// Typedefs
int asScriptEngine_RegisterTypedef(asScriptEngine* engine, const char *type, const char *decl) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RegisterTypedef(type, decl);
}
asUINT asScriptEngine_GetTypedefCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetTypedefCount();
}
asTypeInfo* asScriptEngine_GetTypedefByIndex(asScriptEngine* engine, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetTypedefByIndex(index));
}

// Configuration groups
int asScriptEngine_BeginConfigGroup(asScriptEngine* engine, const char *groupName) {
    return reinterpret_cast<asIScriptEngine*>(engine)->BeginConfigGroup(groupName);
}
int asScriptEngine_EndConfigGroup(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->EndConfigGroup();
}
int asScriptEngine_RemoveConfigGroup(asScriptEngine* engine, const char *groupName) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RemoveConfigGroup(groupName);
}
asDWORD asScriptEngine_SetDefaultAccessMask(asScriptEngine* engine, asDWORD defaultMask) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetDefaultAccessMask(defaultMask);
}
int asScriptEngine_SetDefaultNamespace(asScriptEngine* engine, const char *nameSpace) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetDefaultNamespace(nameSpace);
}
const char* asScriptEngine_GetDefaultNamespace(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetDefaultNamespace();
}

// Script modules
asScriptModule* asScriptEngine_GetModule(asScriptEngine* engine, const char *module, asEGMFlags flag) {
    return reinterpret_cast<asScriptModule*>(reinterpret_cast<asIScriptEngine*>(engine)->GetModule(module, flag));
}
int asScriptEngine_DiscardModule(asScriptEngine* engine, const char *module) {
    return reinterpret_cast<asIScriptEngine*>(engine)->DiscardModule(module);
}
asUINT asScriptEngine_GetModuleCount(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetModuleCount();
}
asScriptModule* asScriptEngine_GetModuleByIndex(asScriptEngine* engine, asUINT index) {
    return reinterpret_cast<asScriptModule*>(reinterpret_cast<asIScriptEngine*>(engine)->GetModuleByIndex(index));
}

// Script functions
int asScriptEngine_GetLastFunctionId(asScriptEngine* engine) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetLastFunctionId();
}
asScriptFunction* asScriptEngine_GetFunctionById(asScriptEngine* engine, int funcId) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptEngine*>(engine)->GetFunctionById(funcId));
}

// Type identification
int asScriptEngine_GetTypeIdByDecl(asScriptEngine* engine, const char *decl) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetTypeIdByDecl(decl);
}
const char* asScriptEngine_GetTypeDeclaration(asScriptEngine* engine, int typeId, bool includeNamespace) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetTypeDeclaration(typeId, includeNamespace);
}
int asScriptEngine_GetSizeOfPrimitiveType(asScriptEngine* engine, int typeId) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetSizeOfPrimitiveType(typeId);
}
asTypeInfo* asScriptEngine_GetTypeInfoById(asScriptEngine* engine, int typeId) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetTypeInfoById(typeId));
}
asTypeInfo* asScriptEngine_GetTypeInfoByName(asScriptEngine* engine, const char *name) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetTypeInfoByName(name));
}
asTypeInfo* asScriptEngine_GetTypeInfoByDecl(asScriptEngine* engine, const char *decl) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptEngine*>(engine)->GetTypeInfoByDecl(decl));
}

// Script execution
asScriptContext* asScriptEngine_CreateContext(asScriptEngine* engine) {
    return reinterpret_cast<asScriptContext*>(reinterpret_cast<asIScriptEngine*>(engine)->CreateContext());
}
void* asScriptEngine_CreateScriptObject(asScriptEngine* engine, const asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->CreateScriptObject(reinterpret_cast<const asITypeInfo*>(type));
}
void* asScriptEngine_CreateScriptObjectCopy(asScriptEngine* engine, void *obj, const asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->CreateScriptObjectCopy(obj, reinterpret_cast<const asITypeInfo*>(type));
}
void* asScriptEngine_CreateUninitializedScriptObject(asScriptEngine* engine, const asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->CreateUninitializedScriptObject(reinterpret_cast<const asITypeInfo*>(type));
}
asScriptFunction* asScriptEngine_CreateDelegate(asScriptEngine* engine, asScriptFunction *func, void *obj) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptEngine*>(engine)->CreateDelegate(reinterpret_cast<asIScriptFunction*>(func), obj));
}
int asScriptEngine_AssignScriptObject(asScriptEngine* engine, void *dstObj, void *srcObj, const asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->AssignScriptObject(dstObj, srcObj, reinterpret_cast<const asITypeInfo*>(type));
}
void asScriptEngine_ReleaseScriptObject(asScriptEngine* engine, void *obj, const asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ReleaseScriptObject(obj, reinterpret_cast<const asITypeInfo*>(type));
}
void asScriptEngine_AddRefScriptObject(asScriptEngine* engine, void *obj, const asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->AddRefScriptObject(obj, reinterpret_cast<const asITypeInfo*>(type));
}
int asScriptEngine_RefCastObject(asScriptEngine* engine, void *obj, asTypeInfo *fromType, asTypeInfo *toType, void **newPtr, bool useOnlyImplicitCast) {
    return reinterpret_cast<asIScriptEngine*>(engine)->RefCastObject(obj, reinterpret_cast<asITypeInfo*>(fromType), reinterpret_cast<asITypeInfo*>(toType), newPtr, useOnlyImplicitCast);
}
asLockableSharedBool* asScriptEngine_GetWeakRefFlagOfScriptObject(asScriptEngine* engine, void *obj, const asTypeInfo *type) {
    return reinterpret_cast<asLockableSharedBool*>(reinterpret_cast<asIScriptEngine*>(engine)->GetWeakRefFlagOfScriptObject(obj, reinterpret_cast<const asITypeInfo*>(type)));
}

// Context pooling
asScriptContext* asScriptEngine_RequestContext(asScriptEngine* engine) {
    return reinterpret_cast<asScriptContext*>(reinterpret_cast<asIScriptEngine*>(engine)->RequestContext());
}
void asScriptEngine_ReturnContext(asScriptEngine* engine, asScriptContext *ctx) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ReturnContext(reinterpret_cast<asIScriptContext*>(ctx));
}
int asScriptEngine_SetContextCallbacks(asScriptEngine* engine, asREQUESTCONTEXTFUNC_t requestCtx, asRETURNCONTEXTFUNC_t returnCtx, void *param) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetContextCallbacks(requestCtx, returnCtx, param);
}

// String interpretation
asETokenClass asScriptEngine_ParseToken(asScriptEngine* engine, const char *string, size_t stringLength, asUINT *tokenLength) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ParseToken(string, stringLength, tokenLength);
}

// Garbage collection
int asScriptEngine_GarbageCollect(asScriptEngine* engine, asDWORD flags, asUINT numIterations) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GarbageCollect(flags, numIterations);
}
void asScriptEngine_GetGCStatistics(asScriptEngine* engine, asUINT *currentSize, asUINT *totalDestroyed, asUINT *totalDetected, asUINT *newObjects, asUINT *totalNewDestroyed) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetGCStatistics(currentSize, totalDestroyed, totalDetected, newObjects, totalNewDestroyed);
}
int asScriptEngine_NotifyGarbageCollectorOfNewObject(asScriptEngine* engine, void *obj, asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->NotifyGarbageCollectorOfNewObject(obj, reinterpret_cast<asITypeInfo*>(type));
}
int asScriptEngine_GetObjectInGC(asScriptEngine* engine, asUINT idx, asUINT *seqNbr, void **obj, asTypeInfo **type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetObjectInGC(idx, seqNbr, obj, reinterpret_cast<asITypeInfo**>(type));
}
void asScriptEngine_GCEnumCallback(asScriptEngine* engine, void *reference) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GCEnumCallback(reference);
}
void asScriptEngine_ForwardGCEnumReferences(asScriptEngine* engine, void *ref, asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ForwardGCEnumReferences(ref, reinterpret_cast<asITypeInfo*>(type));
}
void asScriptEngine_ForwardGCReleaseReferences(asScriptEngine* engine, void *ref, asTypeInfo *type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->ForwardGCReleaseReferences(ref, reinterpret_cast<asITypeInfo*>(type));
}
void asScriptEngine_SetCircularRefDetectedCallback(asScriptEngine* engine, asCIRCULARREFFUNC_t callback, void *param) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetCircularRefDetectedCallback(callback, param);
}

// User data
void* asScriptEngine_SetUserData(asScriptEngine* engine, void *data, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetUserData(data, type);
}
void* asScriptEngine_GetUserData(asScriptEngine* engine, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->GetUserData(type);
}
void asScriptEngine_SetEngineUserDataCleanupCallback(asScriptEngine* engine, asCLEANENGINEFUNC_t callback, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetEngineUserDataCleanupCallback(callback, type);
}
void asScriptEngine_SetModuleUserDataCleanupCallback(asScriptEngine* engine, asCLEANMODULEFUNC_t callback, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetModuleUserDataCleanupCallback(callback, type);
}
void asScriptEngine_SetContextUserDataCleanupCallback(asScriptEngine* engine, asCLEANCONTEXTFUNC_t callback, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetContextUserDataCleanupCallback(callback, type);
}
void asScriptEngine_SetFunctionUserDataCleanupCallback(asScriptEngine* engine, asCLEANFUNCTIONFUNC_t callback, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetFunctionUserDataCleanupCallback(callback, type);
}
void asScriptEngine_SetTypeInfoUserDataCleanupCallback(asScriptEngine* engine, asCLEANTYPEINFOFUNC_t callback, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetTypeInfoUserDataCleanupCallback(callback, type);
}
void asScriptEngine_SetScriptObjectUserDataCleanupCallback(asScriptEngine* engine, asCLEANSCRIPTOBJECTFUNC_t callback, asPWORD type) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetScriptObjectUserDataCleanupCallback(callback, type);
}

// Exception handling
int asScriptEngine_SetTranslateAppExceptionCallback(asScriptEngine* engine, const asSFuncPtr &callback, void *param, int callConv) {
    return reinterpret_cast<asIScriptEngine*>(engine)->SetTranslateAppExceptionCallback(callback, param, callConv);
}

/// asIScriptModule
asScriptEngine* asScriptModule_GetEngine(asScriptModule* module) {
    return reinterpret_cast<asScriptEngine*>(reinterpret_cast<asIScriptModule*>(module)->GetEngine());
}
void asScriptModule_SetName(asScriptModule* module, const char *name) {
    return reinterpret_cast<asIScriptModule*>(module)->SetName(name);
}
const char* asScriptModule_GetName(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetName();
}
void asScriptModule_Discard(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->Discard();
}

// Compilation
int asScriptModule_AddScriptSection(asScriptModule* module, const char *name, const char *code, size_t codeLength, int lineOffset) {
    return reinterpret_cast<asIScriptModule*>(module)->AddScriptSection(name, code, codeLength, lineOffset);
}
int asScriptModule_Build(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->Build();
}
int asScriptModule_CompileFunction(asScriptModule* module, const char *sectionName, const char *code, int lineOffset, asDWORD compileFlags, asScriptFunction **outFunc) {
    return reinterpret_cast<asIScriptModule*>(module)->CompileFunction(sectionName, code, lineOffset, compileFlags, reinterpret_cast<asIScriptFunction**>(outFunc));
}
int asScriptModule_CompileGlobalVar(asScriptModule* module, const char *sectionName, const char *code, int lineOffset) {
    return reinterpret_cast<asIScriptModule*>(module)->CompileGlobalVar(sectionName, code, lineOffset);
}
asDWORD asScriptModule_SetAccessMask(asScriptModule* module, asDWORD accessMask) {
    return reinterpret_cast<asIScriptModule*>(module)->SetAccessMask(accessMask);
}
int asScriptModule_SetDefaultNamespace(asScriptModule* module, const char *nameSpace) {
    return reinterpret_cast<asIScriptModule*>(module)->SetDefaultNamespace(nameSpace);
}
const char* asScriptModule_GetDefaultNamespace(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetDefaultNamespace();
}

// Functions
asUINT asScriptModule_GetFunctionCount(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetFunctionCount();
}
asScriptFunction* asScriptModule_GetFunctionByIndex(asScriptModule* module, asUINT index) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptModule*>(module)->GetFunctionByIndex(index));
}
asScriptFunction* asScriptModule_GetFunctionByDecl(asScriptModule* module, const char *decl) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptModule*>(module)->GetFunctionByDecl(decl));
}
asScriptFunction* asScriptModule_GetFunctionByName(asScriptModule* module, const char *name) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptModule*>(module)->GetFunctionByName(name));
}
int asScriptModule_RemoveFunction(asScriptModule* module, asScriptFunction *func) {
    return reinterpret_cast<asIScriptModule*>(module)->RemoveFunction(reinterpret_cast<asIScriptFunction*>(func));
}

// Global variables
int asScriptModule_ResetGlobalVars(asScriptModule* module, asScriptContext *ctx) {
    return reinterpret_cast<asIScriptModule*>(module)->ResetGlobalVars(reinterpret_cast<asIScriptContext*>(ctx));
}
asUINT asScriptModule_GetGlobalVarCount(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetGlobalVarCount();
}
int asScriptModule_GetGlobalVarIndexByName(asScriptModule* module, const char *name) {
    return reinterpret_cast<asIScriptModule*>(module)->GetGlobalVarIndexByName(name);
}
int asScriptModule_GetGlobalVarIndexByDecl(asScriptModule* module, const char *decl) {
    return reinterpret_cast<asIScriptModule*>(module)->GetGlobalVarIndexByDecl(decl);
}
const char* asScriptModule_GetGlobalVarDeclaration(asScriptModule* module, asUINT index, bool includeNamespace) {
    return reinterpret_cast<asIScriptModule*>(module)->GetGlobalVarDeclaration(index, includeNamespace);
}
int asScriptModule_GetGlobalVar(asScriptModule* module, asUINT index, const char **name, const char **nameSpace, int *typeId, bool *isConst) {
    return reinterpret_cast<asIScriptModule*>(module)->GetGlobalVar(index, name, nameSpace, typeId, isConst);
}
void* asScriptModule_GetAddressOfGlobalVar(asScriptModule* module, asUINT index) {
    return reinterpret_cast<asIScriptModule*>(module)->GetAddressOfGlobalVar(index);
}
int asScriptModule_RemoveGlobalVar(asScriptModule* module, asUINT index) {
    return reinterpret_cast<asIScriptModule*>(module)->RemoveGlobalVar(index);
}

// Type identification
asUINT asScriptModule_GetObjectTypeCount(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetObjectTypeCount();
}
asTypeInfo* asScriptModule_GetObjectTypeByIndex(asScriptModule* module, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptModule*>(module)->GetObjectTypeByIndex(index));
}
int asScriptModule_GetTypeIdByDecl(asScriptModule* module, const char *decl) {
    return reinterpret_cast<asIScriptModule*>(module)->GetTypeIdByDecl(decl);
}
asTypeInfo* asScriptModule_GetTypeInfoByName(asScriptModule* module, const char *name) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptModule*>(module)->GetTypeInfoByName(name));
}
asTypeInfo* asScriptModule_GetTypeInfoByDecl(asScriptModule* module, const char *decl) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptModule*>(module)->GetTypeIdByDecl(decl));
}

// Enums
asUINT asScriptModule_GetEnumCount(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetEnumCount();
}
asTypeInfo* asScriptModule_GetEnumByIndex(asScriptModule* module, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptModule*>(module)->GetEnumByIndex(index));
}

// Typedefs
asUINT asScriptModule_GetTypedefCount(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetTypedefCount();
}
asTypeInfo* asScriptModule_GetTypedefByIndex(asScriptModule* module, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptModule*>(module)->GetTypedefByIndex(index));
}

// Dynamic binding between modules
asUINT asScriptModule_GetImportedFunctionCount(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->GetImportedFunctionCount();
}
int asScriptModule_GetImportedFunctionIndexByDecl(asScriptModule* module, const char *decl) {
    return reinterpret_cast<asIScriptModule*>(module)->GetImportedFunctionIndexByDecl(decl);
}
const char* asScriptModule_GetImportedFunctionDeclaration(asScriptModule* module, asUINT importIndex) {
    return reinterpret_cast<asIScriptModule*>(module)->GetImportedFunctionDeclaration(importIndex);
}
const char* asScriptModule_GetImportedFunctionSourceModule(asScriptModule* module, asUINT importIndex) {
    return reinterpret_cast<asIScriptModule*>(module)->GetImportedFunctionSourceModule(importIndex);
}
int asScriptModule_BindImportedFunction(asScriptModule* module, asUINT importIndex, asScriptFunction *func) {
    return reinterpret_cast<asIScriptModule*>(module)->BindImportedFunction(importIndex, reinterpret_cast<asIScriptFunction*>(func));
}
int asScriptModule_UnbindImportedFunction(asScriptModule* module, asUINT importIndex) {
    return reinterpret_cast<asIScriptModule*>(module)->UnbindImportedFunction(importIndex);
}
int asScriptModule_BindAllImportedFunctions(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->BindAllImportedFunctions();
}
int asScriptModule_UnbindAllImportedFunctions(asScriptModule* module) {
    return reinterpret_cast<asIScriptModule*>(module)->BindAllImportedFunctions();
}

// Byte code saving and loading
int asScriptModule_SaveByteCode(asScriptModule* module, asBinaryStream *out, bool stripDebugInfo) {
    return reinterpret_cast<asIScriptModule*>(module)->SaveByteCode(reinterpret_cast<asIBinaryStream*>(out), stripDebugInfo);
}
int asScriptModule_LoadByteCode(asScriptModule* module, asBinaryStream *in, bool *wasDebugInfoStripped) {
    return reinterpret_cast<asIScriptModule*>(module)->LoadByteCode(reinterpret_cast<asIBinaryStream*>(in), wasDebugInfoStripped);
}

// User data
void *asScriptModule_SetUserData(asScriptModule* module, void *data, asPWORD type) {
    return reinterpret_cast<asIScriptModule*>(module)->SetUserData(data, type);
}
void *asScriptModule_GetUserData(asScriptModule* module, asPWORD type) {
    return reinterpret_cast<asIScriptModule*>(module)->GetUserData(type);
}

/// asITypeInfo
// Miscellaneous
asScriptEngine* asTypeInfo_GetEngine(asTypeInfo* typeInfo) {
    return reinterpret_cast<asScriptEngine*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetEngine());
}
const char* asTypeInfo_GetConfigGroup(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetConfigGroup();
}
asDWORD asTypeInfo_GetAccessMask(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetAccessMask();
}
asScriptModule* asTypeInfo_GetModule(asTypeInfo* typeInfo) {
    return reinterpret_cast<asScriptModule*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetModule());
}

// Memory management
int asTypeInfo_AddRef(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->AddRef();
}
int asTypeInfo_Release(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->Release();
}

// Type info
const char* asTypeInfo_GetName(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetName();
}
const char* asTypeInfo_GetNamespace(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetNamespace();
}
asTypeInfo* asTypeInfo_GetBaseType(asTypeInfo* typeInfo) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetBaseType());
}
bool asTypeInfo_DerivesFrom(asTypeInfo* typeInfo, const asTypeInfo *objType) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->DerivesFrom(reinterpret_cast<const asITypeInfo*>(objType));
}
asQWORD asTypeInfo_GetFlags(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetFlags();
}
asUINT asTypeInfo_GetSize(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetSize();
}
int asTypeInfo_GetTypeId(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetTypeId();
}
int asTypeInfo_GetSubTypeId(asTypeInfo* typeInfo, asUINT subTypeIndex) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetSubTypeId(subTypeIndex);
}
asTypeInfo* asTypeInfo_GetSubType(asTypeInfo* typeInfo, asUINT subTypeIndex) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetSubType(subTypeIndex));
}
asUINT asTypeInfo_GetSubTypeCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetSubTypeCount();
}

// Interfaces
asUINT asTypeInfo_GetInterfaceCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetInterfaceCount();
}
asTypeInfo* asTypeInfo_GetInterface(asTypeInfo* typeInfo, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetInterface(index));
}
bool asTypeInfo_Implements(asTypeInfo* typeInfo, const asTypeInfo *objType) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->Implements(reinterpret_cast<const asITypeInfo*>(objType));
}

// Factories
asUINT asTypeInfo_GetFactoryCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetFactoryCount();
}
asScriptFunction* asTypeInfo_GetFactoryByIndex(asTypeInfo* typeInfo, asUINT index) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetFactoryByIndex(index));
}
asScriptFunction* asTypeInfo_GetFactoryByDecl(asTypeInfo* typeInfo, const char *decl) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetFactoryByDecl(decl));
}

// Methods
asUINT asTypeInfo_GetMethodCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetMethodCount();
}
asScriptFunction* asTypeInfo_GetMethodByIndex(asTypeInfo* typeInfo, asUINT index, bool getVirtual) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetMethodByIndex(index, getVirtual));
}
asScriptFunction* asTypeInfo_GetMethodByName(asTypeInfo* typeInfo, const char *name, bool getVirtual) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetMethodByName(name, getVirtual));
}
asScriptFunction* asTypeInfo_GetMethodByDecl(asTypeInfo* typeInfo, const char *decl, bool getVirtual) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetMethodByDecl(decl, getVirtual));
}

// Properties
asUINT asTypeInfo_GetPropertyCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetPropertyCount();
}
int asTypeInfo_GetProperty(asTypeInfo* typeInfo, asUINT index, const char **name, int *typeId, bool *isPrivate, bool *isProtected, int *offset, bool *isReference, asDWORD *accessMask, int *compositeOffset, bool *isCompositeIndirect, bool *isConst) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetProperty(index, name, typeId, isPrivate, isProtected, offset, isReference, accessMask, compositeOffset, isCompositeIndirect, isConst);
}
const char* asTypeInfo_GetPropertyDeclaration(asTypeInfo* typeInfo, asUINT index, bool includeNamespace) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetPropertyDeclaration(index, includeNamespace);
}

// Behaviours
asUINT asTypeInfo_GetBehaviourCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetBehaviourCount();
}
asScriptFunction* asTypeInfo_GetBehaviourByIndex(asTypeInfo* typeInfo, asUINT index, asEBehaviours *outBehaviour) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetBehaviourByIndex(index, outBehaviour));
}

// Child types
asUINT asTypeInfo_GetChildFuncdefCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetChildFuncdefCount();
}
asTypeInfo* asTypeInfo_GetChildFuncdef(asTypeInfo* typeInfo, asUINT index) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetChildFuncdef(index));
}
asTypeInfo* asTypeInfo_GetParentType(asTypeInfo* typeInfo) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetParentType());
}

// Enums
asUINT asTypeInfo_GetEnumValueCount(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetEnumValueCount();
}
const char* asTypeInfo_GetEnumValueByIndex(asTypeInfo* typeInfo, asUINT index, int *outValue) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetEnumValueByIndex(index, outValue);
}

// Typedef
int asTypeInfo_GetTypedefTypeId(asTypeInfo* typeInfo) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetTypedefTypeId();
}

// Funcdef
asScriptFunction* asTypeInfo_GetFuncdefSignature(asTypeInfo* typeInfo) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asITypeInfo*>(typeInfo)->GetFuncdefSignature());
}

// User data
void* asTypeInfo_SetUserData(asTypeInfo* typeInfo, void *data, asPWORD type) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->SetUserData(data, type);
}
void* asTypeInfo_GetUserData(asTypeInfo* typeInfo, asPWORD type) {
    return reinterpret_cast<asITypeInfo*>(typeInfo)->GetUserData(type);
}

/// asIScriptContext
// Memory management
int asScriptContext_AddRef(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->AddRef();
}
int asScriptContext_Release(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->Release();
}

// Miscellaneous
asScriptEngine *asScriptContext_GetEngine(asScriptContext* context) {
    return reinterpret_cast<asScriptEngine*>(reinterpret_cast<asIScriptContext*>(context)->GetEngine());
}

// Execution
int             asScriptContext_Prepare(asScriptContext* context, asScriptFunction *func) {
    return reinterpret_cast<asIScriptContext*>(context)->Prepare(reinterpret_cast<asIScriptFunction*>(func));
}
int             asScriptContext_Unprepare(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->Unprepare();
}
int             asScriptContext_Execute(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->Execute();
}
int             asScriptContext_Abort(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->Abort();
}
int             asScriptContext_Suspend(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->Suspend();
}
asEContextState asScriptContext_GetState(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetState();
}
int             asScriptContext_PushState(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->PushState();
}
int             asScriptContext_PopState(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->PopState();
}
bool            asScriptContext_IsNested(asScriptContext* context, asUINT *nestCount) {
    return reinterpret_cast<asIScriptContext*>(context)->IsNested(nestCount);
}

// Object pointer for calling class methods
int   asScriptContext_SetObject(asScriptContext* context, void *obj) {
    return reinterpret_cast<asIScriptContext*>(context)->SetObject(obj);
}

// Arguments
int   asScriptContext_SetArgByte(asScriptContext* context, asUINT arg, asBYTE value) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgByte(arg, value);
}
int   asScriptContext_SetArgWord(asScriptContext* context, asUINT arg, asWORD value) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgWord(arg, value);
}
int   asScriptContext_SetArgDWord(asScriptContext* context, asUINT arg, asDWORD value) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgDWord(arg, value);
}
int   asScriptContext_SetArgQWord(asScriptContext* context, asUINT arg, asQWORD value) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgQWord(arg, value);
}
int   asScriptContext_SetArgFloat(asScriptContext* context, asUINT arg, float value) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgFloat(arg, value);
}
int   asScriptContext_SetArgDouble(asScriptContext* context, asUINT arg, double value) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgDouble(arg, value);
}
int   asScriptContext_SetArgAddress(asScriptContext* context ,asUINT arg, void *addr) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgAddress(arg, addr);
}
int   asScriptContext_SetArgObject(asScriptContext* context, asUINT arg, void *obj) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgObject(arg, obj);
}
int   asScriptContext_SetArgVarType(asScriptContext* context, asUINT arg, void *ptr, int typeId) {
    return reinterpret_cast<asIScriptContext*>(context)->SetArgVarType(arg, ptr, typeId);
}
void *asScriptContext_GetAddressOfArg(asScriptContext* context, asUINT arg) {
    return reinterpret_cast<asIScriptContext*>(context)->GetAddressOfArg(arg);
}

// Return value
asBYTE  asScriptContext_GetReturnByte(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnByte();
}
asWORD  asScriptContext_GetReturnWord(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnWord();
}
asDWORD asScriptContext_GetReturnDWord(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnDWord();
}
asQWORD asScriptContext_GetReturnQWord(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnQWord();
}
float   asScriptContext_GetReturnFloat(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnFloat();
}
double  asScriptContext_GetReturnDouble(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnDouble();
}
void   *asScriptContext_GetReturnAddress(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnAddress();
}
void   *asScriptContext_GetReturnObject(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetReturnObject();
}
void   *asScriptContext_GetAddressOfReturnValue(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetAddressOfReturnValue();
}

// Exception handling
int                asScriptContext_SetException(asScriptContext* context, const char *info, bool allowCatch) {
    return reinterpret_cast<asIScriptContext*>(context)->SetException(info, allowCatch);
}
int                asScriptContext_GetExceptionLineNumber(asScriptContext* context, int *column, const char **sectionName) {
    return reinterpret_cast<asIScriptContext*>(context)->GetExceptionLineNumber(column, sectionName);
}
asScriptFunction *asScriptContext_GetExceptionFunction(asScriptContext* context) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptContext*>(context)->GetExceptionFunction());
}
const char *       asScriptContext_GetExceptionString(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetExceptionString();
}
bool               asScriptContext_WillExceptionBeCaught(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->WillExceptionBeCaught();
}
int                asScriptContext_SetExceptionCallback(asScriptContext* context, const asSFuncPtr &callback, void *obj, int callConv) {
    return reinterpret_cast<asIScriptContext*>(context)->SetExceptionCallback(callback, obj, callConv);
}
void               asScriptContext_ClearExceptionCallback(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->ClearExceptionCallback();
}

// Debugging
int                asScriptContext_SetLineCallback(asScriptContext* context, const asSFuncPtr &callback, void *obj, int callConv) {
    return reinterpret_cast<asIScriptContext*>(context)->SetLineCallback(callback, obj, callConv);
}
void               asScriptContext_ClearLineCallback(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->ClearLineCallback();
}
asUINT             asScriptContext_GetCallstackSize(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->GetCallstackSize();
}
asScriptFunction* asScriptContext_GetFunction(asScriptContext* context, asUINT stackLevel) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptContext*>(context)->GetFunction(stackLevel));
}
int                asScriptContext_GetLineNumber(asScriptContext* context, asUINT stackLevel, int *column, const char **sectionName) {
    return reinterpret_cast<asIScriptContext*>(context)->GetLineNumber(stackLevel, column, sectionName);
}
int                asScriptContext_GetVarCount(asScriptContext* context, asUINT stackLevel) {
    return reinterpret_cast<asIScriptContext*>(context)->GetVarCount(stackLevel);
}
int                asScriptContext_GetVar(asScriptContext* context, asUINT varIndex, asUINT stackLevel, const char** name, int* typeId, asETypeModifiers* typeModifiers, bool* isVarOnHeap, int* stackOffset) {
    return reinterpret_cast<asIScriptContext*>(context)->GetVar(varIndex, stackLevel, name, typeId, typeModifiers, isVarOnHeap, stackOffset);
}
const char        *asScriptContext_GetVarDeclaration(asScriptContext* context, asUINT varIndex, asUINT stackLevel, bool includeNamespace) {
    return reinterpret_cast<asIScriptContext*>(context)->GetVarDeclaration(varIndex, stackLevel, includeNamespace);
}
void              *asScriptContext_GetAddressOfVar(asScriptContext* context, asUINT varIndex, asUINT stackLevel, bool dontDereference, bool returnAddressOfUnitializedObjects) {
    return reinterpret_cast<asIScriptContext*>(context)->GetAddressOfVar(varIndex, stackLevel, dontDereference, returnAddressOfUnitializedObjects);
}
bool               asScriptContext_IsVarInScope(asScriptContext* context, asUINT varIndex, asUINT stackLevel) {
    return reinterpret_cast<asIScriptContext*>(context)->IsVarInScope(varIndex, stackLevel);
}
int                asScriptContext_GetThisTypeId(asScriptContext* context, asUINT stackLevel) {
    return reinterpret_cast<asIScriptContext*>(context)->GetThisTypeId(stackLevel);
}
void              *asScriptContext_GetThisPointer(asScriptContext* context, asUINT stackLevel) {
    return reinterpret_cast<asIScriptContext*>(context)->GetThisPointer(stackLevel);
}
asScriptFunction *asScriptContext_GetSystemFunction(asScriptContext* context) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptContext*>(context)->GetSystemFunction());
}

// User data
void *asScriptContext_SetUserData(asScriptContext* context, void *data, asPWORD type) {
    return reinterpret_cast<asIScriptContext*>(context)->SetUserData(data, type);
}
void *asScriptContext_GetUserData(asScriptContext* context, asPWORD type) {
    return reinterpret_cast<asIScriptContext*>(context)->GetUserData(type);
}

// Serialization
int asScriptContext_StartDeserialization(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->StartDeserialization();
}
int asScriptContext_FinishDeserialization(asScriptContext* context) {
    return reinterpret_cast<asIScriptContext*>(context)->FinishDeserialization();
}
int asScriptContext_PushFunction(asScriptContext* context, asScriptFunction *func, void *object) {
    return reinterpret_cast<asIScriptContext*>(context)->PushFunction(reinterpret_cast<asIScriptFunction*>(func), object);
}
int asScriptContext_GetStateRegisters(asScriptContext* context, asUINT stackLevel, asScriptFunction **callingSystemFunction, asScriptFunction **initialFunction, asDWORD *origStackPointer, asDWORD *argumentsSize, asQWORD *valueRegister, void **objectRegister, asTypeInfo **objectTypeRegister) {
    return reinterpret_cast<asIScriptContext*>(context)->GetStateRegisters(stackLevel, reinterpret_cast<asIScriptFunction**>(callingSystemFunction), reinterpret_cast<asIScriptFunction**>(initialFunction), origStackPointer, argumentsSize, valueRegister, objectRegister, reinterpret_cast<asITypeInfo**>(objectTypeRegister));
}
int asScriptContext_GetCallStateRegisters(asScriptContext* context, asUINT stackLevel, asDWORD *stackFramePointer, asScriptFunction **currentFunction, asDWORD *programPointer, asDWORD *stackPointer, asDWORD *stackIndex) {
    return reinterpret_cast<asIScriptContext*>(context)->GetCallStateRegisters(stackLevel, stackFramePointer, reinterpret_cast<asIScriptFunction**>(currentFunction), programPointer, stackPointer, stackIndex);
}
int asScriptContext_SetStateRegisters(asScriptContext* context, asUINT stackLevel, asScriptFunction *callingSystemFunction, asScriptFunction *initialFunction, asDWORD origStackPointer, asDWORD argumentsSize, asQWORD valueRegister, void *objectRegister, asTypeInfo *objectTypeRegister) {
    return reinterpret_cast<asIScriptContext*>(context)->SetStateRegisters(stackLevel, reinterpret_cast<asIScriptFunction*>(callingSystemFunction), reinterpret_cast<asIScriptFunction*>(initialFunction), origStackPointer, argumentsSize, valueRegister, objectRegister, reinterpret_cast<asITypeInfo*>(objectTypeRegister));
}
int asScriptContext_SetCallStateRegisters(asScriptContext* context, asUINT stackLevel, asDWORD stackFramePointer, asScriptFunction *currentFunction, asDWORD programPointer, asDWORD stackPointer, asDWORD stackIndex) {
    return reinterpret_cast<asIScriptContext*>(context)->SetCallStateRegisters(stackLevel, stackFramePointer, reinterpret_cast<asIScriptFunction*>(currentFunction), programPointer, stackPointer, stackIndex);
}
int asScriptContext_GetArgsOnStackCount(asScriptContext* context, asUINT stackLevel) {
    return reinterpret_cast<asIScriptContext*>(context)->GetArgsOnStackCount(stackLevel);
}
int asScriptContext_GetArgOnStack(asScriptContext* context, asUINT stackLevel, asUINT arg, int* typeId, asUINT *flags, void** address) {
    return reinterpret_cast<asIScriptContext*>(context)->GetArgOnStack(stackLevel, arg, typeId, flags, address);
}

///asIScriptGeneric
// Miscellaneous
asScriptEngine   *asScriptGeneric_GetEngine(asScriptGeneric* generic) {
    return reinterpret_cast<asScriptEngine*>(reinterpret_cast<asIScriptGeneric*>(generic)->GetEngine());
}
asScriptFunction *asScriptGeneric_GetFunction(asScriptGeneric* generic) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptGeneric*>(generic)->GetFunction());
}
void              *asScriptGeneric_GetAuxiliary(asScriptGeneric* generic) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetAuxiliary();
}

// Object
void   *asScriptGeneric_GetObject(asScriptGeneric* generic) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetObject();
}
int     asScriptGeneric_GetObjectTypeId(asScriptGeneric* generic) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetObjectTypeId();
}

// Arguments
int     asScriptGeneric_GetArgCount(asScriptGeneric* generic) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgCount();
}
int     asScriptGeneric_GetArgTypeId(asScriptGeneric* generic, asUINT arg, asDWORD *flags) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgTypeId(arg, flags);
}
asBYTE  asScriptGeneric_GetArgByte(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgByte(arg);
}
asWORD  asScriptGeneric_GetArgWord(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgWord(arg);
}
asDWORD asScriptGeneric_GetArgDWord(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgDWord(arg);
}
asQWORD asScriptGeneric_GetArgQWord(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgQWord(arg);
}
float   asScriptGeneric_GetArgFloat(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgFloat(arg);
}
double  asScriptGeneric_GetArgDouble(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgDouble(arg);
}
void   *asScriptGeneric_GetArgAddress(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgAddress(arg);
}
void   *asScriptGeneric_GetArgObject(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetArgObject(arg);
}
void   *asScriptGeneric_GetAddressOfArg(asScriptGeneric* generic, asUINT arg) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetAddressOfArg(arg);
}

// Return value
int     asScriptGeneric_GetReturnTypeId(asScriptGeneric* generic, asDWORD *flags) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetReturnTypeId(flags);
}
int     asScriptGeneric_SetReturnByte(asScriptGeneric* generic, asBYTE val) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnByte(val);
}
int     asScriptGeneric_SetReturnWord(asScriptGeneric* generic, asWORD val) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnWord(val);
}
int     asScriptGeneric_SetReturnDWord(asScriptGeneric* generic, asDWORD val) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnDWord(val);
}
int     asScriptGeneric_SetReturnQWord(asScriptGeneric* generic, asQWORD val) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnQWord(val);
}
int     asScriptGeneric_SetReturnFloat(asScriptGeneric* generic, float val) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnFloat(val);
}
int     asScriptGeneric_SetReturnDouble(asScriptGeneric* generic, double val) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnDouble(val);
}
int     asScriptGeneric_SetReturnAddress(asScriptGeneric* generic, void *addr) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnAddress(addr);
}
int     asScriptGeneric_SetReturnObject(asScriptGeneric* generic, void *obj) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->SetReturnObject(obj);
}
void   *asScriptGeneric_GetAddressOfReturnLocation(asScriptGeneric* generic) {
    return reinterpret_cast<asIScriptGeneric*>(generic)->GetAddressOfReturnLocation();
}

/// asIScriptObject
// Memory management
int                    asScriptObject_AddRef(asScriptObject* object) {
    return reinterpret_cast<asIScriptObject*>(object)->AddRef();
}
int                    asScriptObject_Release(asScriptObject* object) {
    return reinterpret_cast<asIScriptObject*>(object)->Release();
}
asLockableSharedBool *asScriptObject_GetWeakRefFlag(asScriptObject* object) {
    return reinterpret_cast<asLockableSharedBool*>(reinterpret_cast<asIScriptObject*>(object)->GetWeakRefFlag());
}

// Type info
int            asScriptObject_GetTypeId(asScriptObject* object) {
    return reinterpret_cast<asIScriptObject*>(object)->GetTypeId();
}
asTypeInfo   *asScriptObject_GetObjectType(asScriptObject* object) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptObject*>(object)->GetObjectType());
}

// Class properties
asUINT      asScriptObject_GetPropertyCount(asScriptObject* object) {
    return reinterpret_cast<asIScriptObject*>(object)->GetPropertyCount();
}
int         asScriptObject_GetPropertyTypeId(asScriptObject* object, asUINT prop) {
    return reinterpret_cast<asIScriptObject*>(object)->GetPropertyTypeId(prop);
}
const char *asScriptObject_GetPropertyName(asScriptObject* object, asUINT prop) {
    return reinterpret_cast<asIScriptObject*>(object)->GetPropertyName(prop);
}
void       *asScriptObject_GetAddressOfProperty(asScriptObject* object, asUINT prop) {
    return reinterpret_cast<asIScriptObject*>(object)->GetAddressOfProperty(prop);
}

// Miscellaneous
asScriptEngine *asScriptObject_GetEngine(asScriptObject* object) {
    return reinterpret_cast<asScriptEngine*>(reinterpret_cast<asIScriptObject*>(object)->GetEngine());
}
int              asScriptObject_CopyFrom(asScriptObject* object, const asScriptObject *other) {
    return reinterpret_cast<asIScriptObject*>(object)->CopyFrom(reinterpret_cast<const asIScriptObject*>(other));
}

// User data
void *asScriptObject_SetUserData(asScriptObject* object, void *data, asPWORD type) {
    return reinterpret_cast<asIScriptObject*>(object)->SetUserData(data, type);
}
void *asScriptObject_GetUserData(asScriptObject* object, asPWORD type) {
    return reinterpret_cast<asIScriptObject*>(object)->GetUserData(type);
}

/// asIScriptFunction
asScriptEngine *asScriptFunction_GetEngine(asScriptFunction* function) {
    return reinterpret_cast<asScriptEngine*>(reinterpret_cast<asIScriptFunction*>(function)->GetEngine());
}

// Memory management
int              asScriptFunction_AddRef(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->AddRef();
}
int              asScriptFunction_Release(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->Release();
}

// Miscellaneous
int              asScriptFunction_GetId(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetId();
}
asEFuncType      asScriptFunction_GetFuncType(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetFuncType();
}
const char      *asScriptFunction_GetModuleName(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetModuleName();
}
asScriptModule *asScriptFunction_GetModule(asScriptFunction* function) {
    return reinterpret_cast<asScriptModule*>(reinterpret_cast<asIScriptFunction*>(function)->GetModule());
}
const char      *asScriptFunction_GetConfigGroup(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetConfigGroup();
}
asDWORD          asScriptFunction_GetAccessMask(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetAccessMask();
}
void            *asScriptFunction_GetAuxiliary(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetAuxiliary();
}

// Function signature
asTypeInfo     *asScriptFunction_GetObjectType(asScriptFunction* function) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptFunction*>(function)->GetObjectType());
}
const char      *asScriptFunction_GetObjectName(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetObjectName();
}
const char      *asScriptFunction_GetName(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetName();
}
const char      *asScriptFunction_GetNamespace(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetNamespace();
}
const char      *asScriptFunction_GetDeclaration(asScriptFunction* function, bool includeObjectName, bool includeNamespace, bool includeParamNames) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetDeclaration(includeObjectName, includeNamespace, includeParamNames);
}
bool             asScriptFunction_IsReadOnly(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsReadOnly();
}
bool             asScriptFunction_IsPrivate(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsPrivate();
}
bool             asScriptFunction_IsProtected(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsProtected();
}
bool             asScriptFunction_IsFinal(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsFinal();
}
bool             asScriptFunction_IsOverride(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsOverride();
}
bool             asScriptFunction_IsShared(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsShared();
}
bool             asScriptFunction_IsExplicit(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsExplicit();
}
bool             asScriptFunction_IsProperty(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsProperty();
}
bool             asScriptFunction_IsVariadic(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsVariadic();
}
asUINT           asScriptFunction_GetParamCount(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetParamCount();
}
int              asScriptFunction_GetParam(asScriptFunction* function, asUINT index, int *typeId, asDWORD *flags, const char **name, const char **defaultArg) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetParam(index, typeId, flags, name, defaultArg);
}
int              asScriptFunction_GetReturnTypeId(asScriptFunction* function, asDWORD *flags) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetReturnTypeId(flags);
}

// Template functions
asUINT           asScriptFunction_GetSubTypeCount(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetSubTypeCount();
}
int              asScriptFunction_GetSubTypeId(asScriptFunction* function, asUINT subTypeIndex) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetSubTypeId(subTypeIndex);
}
asTypeInfo     *asScriptFunction_GetSubType(asScriptFunction* function, asUINT subTypeIndex) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptFunction*>(function)->GetSubType(subTypeIndex));
}

// Type id for function pointers
int              asScriptFunction_GetTypeId(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetTypeId();
}
bool             asScriptFunction_IsCompatibleWithTypeId(asScriptFunction* function, int typeId) {
    return reinterpret_cast<asIScriptFunction*>(function)->IsCompatibleWithTypeId(typeId);
}

// Delegates
void              *asScriptFunction_GetDelegateObject(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetDelegateObject();
}
asTypeInfo       *asScriptFunction_GetDelegateObjectType(asScriptFunction* function) {
    return reinterpret_cast<asTypeInfo*>(reinterpret_cast<asIScriptFunction*>(function)->GetDelegateObjectType());
}
asScriptFunction *asScriptFunction_GetDelegateFunction(asScriptFunction* function) {
    return reinterpret_cast<asScriptFunction*>(reinterpret_cast<asIScriptFunction*>(function)->GetDelegateFunction());
}

// Debug information
asUINT           asScriptFunction_GetVarCount(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetVarCount();
}
int              asScriptFunction_GetVar(asScriptFunction* function, asUINT index, const char **name, int *typeId) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetVar(index,name,typeId);
}
const char      *asScriptFunction_GetVarDecl(asScriptFunction* function, asUINT index, bool includeNamespace) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetVarDecl(index, includeNamespace);
}
int              asScriptFunction_FindNextLineWithCode(asScriptFunction* function, int line) {
    return reinterpret_cast<asIScriptFunction*>(function)->FindNextLineWithCode(line);
}
int              asScriptFunction_GetDeclaredAt(asScriptFunction* function, const char** scriptSection, int* row, int* col) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetDeclaredAt(scriptSection, row, col);
}
int              asScriptFunction_GetLineEntryCount(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetLineEntryCount();
}
int              asScriptFunction_GetLineEntry(asScriptFunction* function, asUINT index, int* row, int* col, const char** sectionName, const asDWORD** byteCode) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetLineEntry(index, row, col, sectionName, byteCode);
}

// For JIT compilation
asDWORD         *asScriptFunction_GetByteCode(asScriptFunction* function, asUINT *length) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetByteCode(length);
}
int              asScriptFunction_SetJITFunction(asScriptFunction* function, asJITFunction jitFunc) {
    return reinterpret_cast<asIScriptFunction*>(function)->SetJITFunction(jitFunc);
}
asJITFunction    asScriptFunction_GetJITFunction(asScriptFunction* function) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetJITFunction();
}

// User data
void            *asScriptFunction_SetUserData(asScriptFunction* function, void *userData, asPWORD type) {
    return reinterpret_cast<asIScriptFunction*>(function)->SetUserData(userData, type);
}
void            *asScriptFunction_GetUserData(asScriptFunction* function, asPWORD type) {
    return reinterpret_cast<asIScriptFunction*>(function)->GetUserData(type);
}

class asBinaryStreamC : public asIBinaryStream {
    public:
    asSBinaryStream Interface;
    asBinaryStreamC(asSBinaryStream interface) {
        Interface = interface;
    }
    int Read(void *ptr, asUINT size) override {
        return Interface.readstreamFunc(ptr, size, Interface.userdata);
    }  
    int Write(const void *ptr, asUINT size) override {
        return Interface.writestreamFunc(ptr, size, Interface.userdata);
    }
};

asBinaryStream* asBinaryStream_Create(asSBinaryStream interface) {
    asIBinaryStream* iface = new asBinaryStreamC(interface);
    return reinterpret_cast<asBinaryStream*>(iface);
}

void asBinaryStream_Destroy(asBinaryStream* stream) {
    delete reinterpret_cast<asIBinaryStream*>(stream);
}

asStringFactory* asStringFactory_Create(asSStringFactory interface) {
    asIStringFactory* iface = new asIStringFactoryC(interface);
    return reinterpret_cast<asStringFactory*>(iface);
}

void asStringFactory_Destroy(asStringFactory* stream) {
    delete reinterpret_cast<asIStringFactory*>(stream);
}
