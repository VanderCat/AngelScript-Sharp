#pragma once
#ifdef __cplusplus
	#define AS_EXTERN extern "C"
#else
	#define AS_EXTERN extern
#endif

typedef struct asScriptEngine asScriptEngine;
typedef struct asScriptModule asScriptModule;
typedef struct asScriptContext asScriptContext;
typedef struct asScriptGeneric asScriptGeneric;
typedef struct asScriptObject asScriptObject;
typedef struct asTypeInfo asTypeInfo;
typedef struct asScriptFunction asScriptFunction;
typedef struct asBinaryStream asBinaryStream;
typedef struct asJITCompilerAbstract asJITCompilerAbstract;
typedef struct asThreadManager asThreadManager;
typedef struct asLockableSharedBool asLockableSharedBool;
typedef struct asStringFactory asStringFactory;

#ifndef ANGELSCRIPT_H

#include <stddef.h>
#ifndef _MSC_VER
#include <stdint.h>
#endif

#if defined(WIN32) || defined(_WIN32) || defined(__CYGWIN__)
#if defined(ANGELSCRIPT_EXPORT)
#define AS_API __declspec(dllexport)
#elif defined(ANGELSCRIPT_DLL_LIBRARY_IMPORT)
#define AS_API __declspec(dllimport)
#else // statically linked library
#define AS_API
#endif
#elif defined(__GNUC__)
#if defined(ANGELSCRIPT_EXPORT)
#define AS_API __attribute__((visibility ("default")))
#else
#define AS_API
#endif
#else
#define AS_API
#endif


// asBYTE  =  8 bits
// asWORD  = 16 bits
// asDWORD = 32 bits
// asQWORD = 64 bits
// asPWORD = size of pointer
//
typedef signed char    asINT8;
typedef signed short   asINT16;
typedef signed int     asINT32;
typedef unsigned char  asBYTE;
typedef unsigned short asWORD;
typedef unsigned int   asUINT;
#if (defined(_MSC_VER) && _MSC_VER <= 1200) || defined(__S3E__) || (defined(_MSC_VER) && defined(__clang__))
// size_t is not really correct, since it only guaranteed to be large enough to hold the segment size.
// For example, on 16bit systems the size_t may be 16bits only even if pointers are 32bit. But nobody
// is likely to use MSVC6 to compile for 16bit systems anymore, so this should be ok.
typedef size_t         asPWORD;
#else
typedef uintptr_t      asPWORD;
#endif
#ifdef __LP64__
typedef unsigned int  asDWORD;
typedef unsigned long asQWORD;
typedef long asINT64;
#else
typedef unsigned long asDWORD;
#if !defined(_MSC_VER) && (defined(__GNUC__) || defined(__MWERKS__) || defined(__SUNPRO_CC) || defined(__psp2__))
typedef uint64_t asQWORD;
typedef int64_t asINT64;
#else
typedef unsigned __int64 asQWORD;
typedef __int64 asINT64;
#endif
#endif

enum asERetCodes
{
	asSUCCESS                              =  0,
	asERROR                                = -1,
	asCONTEXT_ACTIVE                       = -2,
	asCONTEXT_NOT_FINISHED                 = -3,
	asCONTEXT_NOT_PREPARED                 = -4,
	asINVALID_ARG                          = -5,
	asNO_FUNCTION                          = -6,
	asNOT_SUPPORTED                        = -7,
	asINVALID_NAME                         = -8,
	asNAME_TAKEN                           = -9,
	asINVALID_DECLARATION                  = -10,
	asINVALID_OBJECT                       = -11,
	asINVALID_TYPE                         = -12,
	asALREADY_REGISTERED                   = -13,
	asMULTIPLE_FUNCTIONS                   = -14,
	asNO_MODULE                            = -15,
	asNO_GLOBAL_VAR                        = -16,
	asINVALID_CONFIGURATION                = -17,
	asINVALID_INTERFACE                    = -18,
	asCANT_BIND_ALL_FUNCTIONS              = -19,
	asLOWER_ARRAY_DIMENSION_NOT_REGISTERED = -20,
	asWRONG_CONFIG_GROUP                   = -21,
	asCONFIG_GROUP_IS_IN_USE               = -22,
	asILLEGAL_BEHAVIOUR_FOR_TYPE           = -23,
	asWRONG_CALLING_CONV                   = -24,
	asBUILD_IN_PROGRESS                    = -25,
	asINIT_GLOBAL_VARS_FAILED              = -26,
	asOUT_OF_MEMORY                        = -27,
	asMODULE_IS_IN_USE                     = -28
};

// Engine properties
enum asEEngineProp
{
	asEP_ALLOW_UNSAFE_REFERENCES            = 1,
	asEP_OPTIMIZE_BYTECODE                  = 2,
	asEP_COPY_SCRIPT_SECTIONS               = 3,
	asEP_MAX_STACK_SIZE                     = 4,
	asEP_USE_CHARACTER_LITERALS             = 5,
	asEP_ALLOW_MULTILINE_STRINGS            = 6,
	asEP_ALLOW_IMPLICIT_HANDLE_TYPES        = 7,
	asEP_BUILD_WITHOUT_LINE_CUES            = 8,
	asEP_INIT_GLOBAL_VARS_AFTER_BUILD       = 9,
	asEP_REQUIRE_ENUM_SCOPE                 = 10,
	asEP_SCRIPT_SCANNER                     = 11,
	asEP_INCLUDE_JIT_INSTRUCTIONS           = 12,
	asEP_STRING_ENCODING                    = 13,
	asEP_PROPERTY_ACCESSOR_MODE             = 14,
	asEP_EXPAND_DEF_ARRAY_TO_TMPL           = 15,
	asEP_AUTO_GARBAGE_COLLECT               = 16,
	asEP_DISALLOW_GLOBAL_VARS               = 17,
	asEP_ALWAYS_IMPL_DEFAULT_CONSTRUCT      = 18,
	asEP_COMPILER_WARNINGS                  = 19,
	asEP_DISALLOW_VALUE_ASSIGN_FOR_REF_TYPE = 20,
	asEP_ALTER_SYNTAX_NAMED_ARGS            = 21,
	asEP_DISABLE_INTEGER_DIVISION           = 22,
	asEP_DISALLOW_EMPTY_LIST_ELEMENTS       = 23,
	asEP_PRIVATE_PROP_AS_PROTECTED          = 24,
	asEP_ALLOW_UNICODE_IDENTIFIERS          = 25,
	asEP_HEREDOC_TRIM_MODE                  = 26,
	asEP_MAX_NESTED_CALLS                   = 27,
	asEP_GENERIC_CALL_MODE                  = 28,
	asEP_INIT_STACK_SIZE                    = 29,
	asEP_INIT_CALL_STACK_SIZE               = 30,
	asEP_MAX_CALL_STACK_SIZE                = 31,
	asEP_IGNORE_DUPLICATE_SHARED_INTF       = 32,
	asEP_NO_DEBUG_OUTPUT                    = 33,
	asEP_DISABLE_SCRIPT_CLASS_GC            = 34,
	asEP_JIT_INTERFACE_VERSION              = 35,
	asEP_ALWAYS_IMPL_DEFAULT_COPY           = 36,
	asEP_ALWAYS_IMPL_DEFAULT_COPY_CONSTRUCT = 37,
	asEP_MEMBER_INIT_MODE                   = 38,
	asEP_BOOL_CONVERSION_MODE               = 39,
	asEP_FOREACH_SUPPORT                    = 40,

	asEP_LAST_PROPERTY
};

// Calling conventions
enum asECallConvTypes
{
	asCALL_CDECL             = 0,
	asCALL_STDCALL           = 1,
	asCALL_THISCALL_ASGLOBAL = 2,
	asCALL_THISCALL          = 3,
	asCALL_CDECL_OBJLAST     = 4,
	asCALL_CDECL_OBJFIRST    = 5,
	asCALL_GENERIC           = 6,
	asCALL_THISCALL_OBJLAST  = 7,
	asCALL_THISCALL_OBJFIRST = 8
};

// Object type flags
enum asEObjTypeFlags : asQWORD
{
	asOBJ_REF                         = (1<<0),
	asOBJ_VALUE                       = (1<<1),
	asOBJ_GC                          = (1<<2),
	asOBJ_POD                         = (1<<3),
	asOBJ_NOHANDLE                    = (1<<4),
	asOBJ_SCOPED                      = (1<<5),
	asOBJ_TEMPLATE                    = (1<<6),
	asOBJ_ASHANDLE                    = (1<<7),
	asOBJ_APP_CLASS                   = (1<<8),
	asOBJ_APP_CLASS_CONSTRUCTOR       = (1<<9),
	asOBJ_APP_CLASS_DESTRUCTOR        = (1<<10),
	asOBJ_APP_CLASS_ASSIGNMENT        = (1<<11),
	asOBJ_APP_CLASS_COPY_CONSTRUCTOR  = (1<<12),
	asOBJ_APP_CLASS_C                 = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR),
	asOBJ_APP_CLASS_CD                = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_DESTRUCTOR),
	asOBJ_APP_CLASS_CA                = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_ASSIGNMENT),
	asOBJ_APP_CLASS_CK                = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_CDA               = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_DESTRUCTOR + asOBJ_APP_CLASS_ASSIGNMENT),
	asOBJ_APP_CLASS_CDK               = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_DESTRUCTOR + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_CAK               = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_ASSIGNMENT + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_CDAK              = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_CONSTRUCTOR + asOBJ_APP_CLASS_DESTRUCTOR + asOBJ_APP_CLASS_ASSIGNMENT + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_D                 = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_DESTRUCTOR),
	asOBJ_APP_CLASS_DA                = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_DESTRUCTOR + asOBJ_APP_CLASS_ASSIGNMENT),
	asOBJ_APP_CLASS_DK                = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_DESTRUCTOR + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_DAK               = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_DESTRUCTOR + asOBJ_APP_CLASS_ASSIGNMENT + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_A                 = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_ASSIGNMENT),
	asOBJ_APP_CLASS_AK                = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_ASSIGNMENT + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_K                 = (asOBJ_APP_CLASS + asOBJ_APP_CLASS_COPY_CONSTRUCTOR),
	asOBJ_APP_CLASS_MORE_CONSTRUCTORS = (((asQWORD)1) << 31),
	asOBJ_APP_PRIMITIVE               = (1<<13),
	asOBJ_APP_FLOAT                   = (1<<14),
	asOBJ_APP_ARRAY                   = (1<<15),
	asOBJ_APP_CLASS_ALLINTS           = (1<<16),
	asOBJ_APP_CLASS_ALLFLOATS         = (1<<17),
	asOBJ_NOCOUNT                     = (1<<18),
	asOBJ_APP_CLASS_ALIGN8            = (1<<19),
	asOBJ_IMPLICIT_HANDLE             = (1<<20),
	asOBJ_APP_CLASS_UNION             = (((asQWORD)1)<<32),
	asOBJ_MASK_VALID_FLAGS            = 0x1801FFFFFul,
	// Internal flags
	asOBJ_SCRIPT_OBJECT               = (1<<21),
	asOBJ_SHARED                      = (1<<22),
	asOBJ_NOINHERIT                   = (1<<23),
	asOBJ_FUNCDEF                     = (1<<24),
	asOBJ_LIST_PATTERN                = (1<<25),
	asOBJ_ENUM                        = (1<<26),
	asOBJ_TEMPLATE_SUBTYPE            = (1<<27),
	asOBJ_TYPEDEF                     = (1<<28),
	asOBJ_ABSTRACT                    = (1<<29),
	asOBJ_APP_ALIGN16                 = (1<<30)
};

// Behaviours
enum asEBehaviours
{
	// Value object memory management
	asBEHAVE_CONSTRUCT,
	asBEHAVE_LIST_CONSTRUCT,
	asBEHAVE_DESTRUCT,

	// Reference object memory management
	asBEHAVE_FACTORY,
	asBEHAVE_LIST_FACTORY,
	asBEHAVE_ADDREF,
	asBEHAVE_RELEASE,
	asBEHAVE_GET_WEAKREF_FLAG,

	// Object operators
	asBEHAVE_TEMPLATE_CALLBACK,

	// Garbage collection behaviours
	asBEHAVE_FIRST_GC,
	 asBEHAVE_GETREFCOUNT = asBEHAVE_FIRST_GC,
	 asBEHAVE_SETGCFLAG,
	 asBEHAVE_GETGCFLAG,
	 asBEHAVE_ENUMREFS,
	 asBEHAVE_RELEASEREFS,
	asBEHAVE_LAST_GC = asBEHAVE_RELEASEREFS,

	asBEHAVE_MAX
};

// Context states
enum asEContextState
{
	asEXECUTION_FINISHED        = 0,
	asEXECUTION_SUSPENDED       = 1,
	asEXECUTION_ABORTED         = 2,
	asEXECUTION_EXCEPTION       = 3,
	asEXECUTION_PREPARED        = 4,
	asEXECUTION_UNINITIALIZED   = 5,
	asEXECUTION_ACTIVE          = 6,
	asEXECUTION_ERROR           = 7,
	asEXECUTION_DESERIALIZATION = 8
};

// Message types
enum asEMsgType
{
	asMSGTYPE_ERROR       = 0,
	asMSGTYPE_WARNING     = 1,
	asMSGTYPE_INFORMATION = 2
};

// Garbage collector flags
enum asEGCFlags
{
	asGC_FULL_CYCLE      = 1,
	asGC_ONE_STEP        = 2,
	asGC_DESTROY_GARBAGE = 4,
	asGC_DETECT_GARBAGE  = 8
};

// Token classes
enum asETokenClass
{
	asTC_UNKNOWN    = 0,
	asTC_KEYWORD    = 1,
	asTC_VALUE      = 2,
	asTC_IDENTIFIER = 3,
	asTC_COMMENT    = 4,
	asTC_WHITESPACE = 5
};

// Type id flags
enum asETypeIdFlags
{
	asTYPEID_VOID           = 0,
	asTYPEID_BOOL           = 1,
	asTYPEID_INT8           = 2,
	asTYPEID_INT16          = 3,
	asTYPEID_INT32          = 4,
	asTYPEID_INT64          = 5,
	asTYPEID_UINT8          = 6,
	asTYPEID_UINT16         = 7,
	asTYPEID_UINT32         = 8,
	asTYPEID_UINT64         = 9,
	asTYPEID_FLOAT          = 10,
	asTYPEID_DOUBLE         = 11,
	asTYPEID_OBJHANDLE      = 0x40000000,
	asTYPEID_HANDLETOCONST  = 0x20000000,
	asTYPEID_MASK_OBJECT    = 0x1C000000,
	asTYPEID_APPOBJECT      = 0x04000000,
	asTYPEID_SCRIPTOBJECT   = 0x08000000,
	asTYPEID_TEMPLATE       = 0x10000000,
	asTYPEID_MASK_SEQNBR    = 0x03FFFFFF
};

// Type modifiers
enum asETypeModifiers
{
	asTM_NONE     = 0,
	asTM_INREF    = 1,
	asTM_OUTREF   = 2,
	asTM_INOUTREF = 3,
	asTM_CONST    = 4
};

// GetModule flags
enum asEGMFlags
{
	asGM_ONLY_IF_EXISTS       = 0,
	asGM_CREATE_IF_NOT_EXISTS = 1,
	asGM_ALWAYS_CREATE        = 2
};

// Compile flags
enum asECompileFlags
{
	asCOMP_ADD_TO_MODULE = 1
};

// Function types
enum asEFuncType
{
	asFUNC_DUMMY     =-1,
	asFUNC_SYSTEM    = 0,
	asFUNC_SCRIPT    = 1,
	asFUNC_INTERFACE = 2,
	asFUNC_VIRTUAL   = 3,
	asFUNC_FUNCDEF   = 4,
	asFUNC_IMPORTED  = 5,
	asFUNC_DELEGATE  = 6,
	asFUNC_TEMPLATE  = 7
};

typedef void (*asFUNCTION_t)();
typedef void (*asGENFUNC_t)(asScriptGeneric *);
typedef void *(*asALLOCFUNC_t)(size_t);
typedef void (*asFREEFUNC_t)(void *);
typedef void (*asCLEANENGINEFUNC_t)(asScriptEngine *);
typedef void (*asCLEANMODULEFUNC_t)(asScriptModule *);
typedef void (*asCLEANCONTEXTFUNC_t)(asScriptContext *);
typedef void (*asCLEANFUNCTIONFUNC_t)(asScriptFunction *);
typedef void (*asCLEANTYPEINFOFUNC_t)(asTypeInfo *);
typedef void (*asCLEANSCRIPTOBJECTFUNC_t)(asScriptObject *);
typedef asScriptContext *(*asREQUESTCONTEXTFUNC_t)(asScriptEngine *, void *);
typedef void (*asRETURNCONTEXTFUNC_t)(asScriptEngine *, asScriptContext *, void *);
typedef void (*asCIRCULARREFFUNC_t)(asTypeInfo *, const void *, void *);

typedef struct asSVMRegisters asSVMRegisters;
typedef void (*asJITFunction)(asSVMRegisters* registers, asPWORD jitArg);

typedef struct asSFuncPtr asFuncPtr;
#endif

typedef int (*DESTROYUSERDATA_t)(void* userdata);

typedef int (*READSTREAM_t)(void* ptr, asUINT size, void* userdata);
typedef int (*WRITESTREAM_t)(const void* ptr, asUINT size, void* userdata);

typedef struct {
	READSTREAM_t readstreamFunc;
	WRITESTREAM_t writestreamFunc;
	DESTROYUSERDATA_t destroyFunc;
	void* userdata;
} asSBinaryStream;

typedef const void* (*GETSTRINGCONSTANT_t)(const char *data, asUINT length, void* userdata);
typedef int (*RELEASESTRINGCONSTANT_t)(const void *str, void* userdata);
typedef int (*GETRAWSTRINGDATA_t)(const void *str, char *data, asUINT *length, void* userdata);

struct asSStringFactory {
	GETSTRINGCONSTANT_t getStringConstantFunc;
	RELEASESTRINGCONSTANT_t releaseStringConstantFunc;
	GETRAWSTRINGDATA_t getRawStringDataFunc;
	DESTROYUSERDATA_t destroyFunc;
	void* userdata;
};

AS_EXTERN AS_API asScriptEngine* asCreateScriptEngine(asDWORD version);
AS_EXTERN AS_API const char* asGetLibraryVersion();
AS_EXTERN AS_API const char* asGetLibraryOptions();

// Context
AS_EXTERN AS_API asScriptContext* asGetActiveContext();

// Thread support
AS_EXTERN AS_API int asPrepareMultithread(asThreadManager* externalMgr);
AS_EXTERN AS_API void asUnprepareMultithread();
AS_EXTERN AS_API asThreadManager* asGetThreadManager();
AS_EXTERN AS_API void asAcquireExclusiveLock();
AS_EXTERN AS_API void asReleaseExclusiveLock();
AS_EXTERN AS_API void asAcquireSharedLock();
AS_EXTERN AS_API void asReleaseSharedLock();
AS_EXTERN AS_API int asAtomicInc(int &value);
AS_EXTERN AS_API int asAtomicDec(int &value);
AS_EXTERN AS_API int asThreadCleanup();

// Memory management
AS_EXTERN AS_API int asSetGlobalMemoryFunctions(asALLOCFUNC_t allocFunc, asFREEFUNC_t freeFunc);
AS_EXTERN AS_API int asResetGlobalMemoryFunctions();
AS_EXTERN AS_API void* asAllocMem(size_t size);
AS_EXTERN AS_API void asFreeMem(void *mem);

// Auxiliary
AS_EXTERN AS_API asLockableSharedBool* asCreateLockableSharedBool();

/// asScriptEngine
AS_EXTERN AS_API int asScriptEngine_AddRef(asScriptEngine* engine);
AS_EXTERN AS_API int asScriptEngine_Release(asScriptEngine* engine);
AS_EXTERN AS_API int asScriptEngine_ShutDownAndRelease(asScriptEngine* engine);
AS_EXTERN AS_API int asScriptEngine_SetEngineProperty(asScriptEngine* engine, asEEngineProp property, asPWORD value);
AS_EXTERN AS_API asPWORD asScriptEngine_GetEngineProperty(asScriptEngine* engine, asEEngineProp property);
AS_EXTERN AS_API int asScriptEngine_SetMessageCallback(asScriptEngine* engine, const asSFuncPtr& callback, void* obj, asDWORD callConv);
AS_EXTERN AS_API int asScriptEngine_GetMessageCallback(asScriptEngine* engine, asSFuncPtr *callback, void **obj, asDWORD *callConv);
AS_EXTERN AS_API int asScriptEngine_ClearMessageCallback(asScriptEngine* engine);
AS_EXTERN AS_API int asScriptEngine_WriteMessage(asScriptEngine* engine, const char *section, int row, int col, asEMsgType type, const char *message);
// JIT Compiler
AS_EXTERN AS_API int asScriptEngine_SetJITCompiler(asScriptEngine* engine, asJITCompilerAbstract* compiler);
AS_EXTERN AS_API asJITCompilerAbstract *asScriptEngine_GetJITCompiler(asScriptEngine* engine);
// Global functions
AS_EXTERN AS_API int asScriptEngine_RegisterGlobalFunction(asScriptEngine* engine, const char *declaration, const asSFuncPtr &funcPointer, asDWORD callConv, void *auxiliary);
AS_EXTERN AS_API asUINT asScriptEngine_GetGlobalFunctionCount(asScriptEngine* engine);
AS_EXTERN AS_API asScriptFunction* asScriptEngine_GetGlobalFunctionByIndex(asScriptEngine* engine, asUINT index);
AS_EXTERN AS_API asScriptFunction* asScriptEngine_GetGlobalFunctionByDecl(asScriptEngine* engine, const char *declaration);
// Global properties
AS_EXTERN AS_API int asScriptEngine_RegisterGlobalProperty(asScriptEngine* engine, const char *declaration, void *pointer);
AS_EXTERN AS_API asUINT asScriptEngine_GetGlobalPropertyCount(asScriptEngine* engine);
AS_EXTERN AS_API int asScriptEngine_GetGlobalPropertyByIndex(asScriptEngine* engine, asUINT index, const char **name, const char **nameSpace, int *typeId, bool *isConst, const char **configGroup, void **pointer, asDWORD *accessMask);
AS_EXTERN AS_API int asScriptEngine_GetGlobalPropertyIndexByName(asScriptEngine* engine, const char *name);
AS_EXTERN AS_API int asScriptEngine_GetGlobalPropertyIndexByDecl(asScriptEngine* engine, const char *decl);
// Object types
AS_EXTERN AS_API int asScriptEngine_RegisterObjectType(asScriptEngine* engine, const char *obj, int byteSize, asQWORD flags);
AS_EXTERN AS_API int asScriptEngine_RegisterObjectProperty(asScriptEngine* engine, const char *obj, const char *declaration, int byteOffset, int compositeOffset, bool isCompositeIndirect);
AS_EXTERN AS_API int asScriptEngine_RegisterObjectMethod(asScriptEngine* engine, const char *obj, const char *declaration, const asSFuncPtr &funcPointer, asDWORD callConv, void *auxiliary, int compositeOffset, bool isCompositeIndirect);
AS_EXTERN AS_API int asScriptEngine_RegisterObjectBehaviour(asScriptEngine* engine, const char *obj, asEBehaviours behaviour, const char *declaration, const asSFuncPtr &funcPointer, asDWORD callConv, void *auxiliary, int compositeOffset, bool isCompositeIndirect);
AS_EXTERN AS_API int asScriptEngine_RegisterInterface(asScriptEngine* engine, const char *name);
AS_EXTERN AS_API int asScriptEngine_RegisterInterfaceMethod(asScriptEngine* engine, const char *intf, const char *declaration);
AS_EXTERN AS_API asUINT asScriptEngine_GetObjectTypeCount(asScriptEngine* engine);
AS_EXTERN AS_API asTypeInfo* asScriptEngine_GetObjectTypeByIndex(asScriptEngine* engine, asUINT index);
// String factory
AS_EXTERN AS_API int asScriptEngine_RegisterStringFactory(asScriptEngine* engine, const char *datatype, asStringFactory* factory);
AS_EXTERN AS_API int asScriptEngine_GetStringFactory(asScriptEngine* engine, asDWORD* typeModifiers, asStringFactory** factory);

// Default array type
AS_EXTERN AS_API int asScriptEngine_RegisterDefaultArrayType(asScriptEngine* engine, const char *type);
AS_EXTERN AS_API int asScriptEngine_GetDefaultArrayTypeId(asScriptEngine* engine);

// Enums
AS_EXTERN AS_API int asScriptEngine_RegisterEnum(asScriptEngine* engine, const char *type);
AS_EXTERN AS_API int asScriptEngine_RegisterEnumValue(asScriptEngine* engine, const char *type, const char *name, int value);
AS_EXTERN AS_API asUINT asScriptEngine_GetEnumCount(asScriptEngine* engine);
AS_EXTERN AS_API asTypeInfo *asScriptEngine_GetEnumByIndex(asScriptEngine* engine, asUINT index);

// Funcdefs
AS_EXTERN AS_API int asScriptEngine_RegisterFuncdef(asScriptEngine* engine, const char *decl);
AS_EXTERN AS_API asUINT asScriptEngine_GetFuncdefCount(asScriptEngine* engine);
AS_EXTERN AS_API asTypeInfo* asScriptEngine_GetFuncdefByIndex(asScriptEngine* engine, asUINT index);
// Typedefs
AS_EXTERN AS_API int asScriptEngine_RegisterTypedef(asScriptEngine* engine, const char *type, const char *decl);
AS_EXTERN AS_API asUINT asScriptEngine_GetTypedefCount(asScriptEngine* engine);
AS_EXTERN AS_API asTypeInfo* asScriptEngine_GetTypedefByIndex(asScriptEngine* engine, asUINT index);

// Configuration groups
AS_EXTERN AS_API int asScriptEngine_BeginConfigGroup(asScriptEngine* engine, const char *groupName);
AS_EXTERN AS_API int asScriptEngine_EndConfigGroup(asScriptEngine* engine);
AS_EXTERN AS_API int asScriptEngine_RemoveConfigGroup(asScriptEngine* engine, const char *groupName);
AS_EXTERN AS_API asDWORD asScriptEngine_SetDefaultAccessMask(asScriptEngine* engine, asDWORD defaultMask);
AS_EXTERN AS_API int asScriptEngine_SetDefaultNamespace(asScriptEngine* engine, const char *nameSpace);
AS_EXTERN AS_API const char* asScriptEngine_GetDefaultNamespace(asScriptEngine* engine);

// Script modules
AS_EXTERN AS_API asScriptModule* asScriptEngine_GetModule(asScriptEngine* engine, const char *module, asEGMFlags flag);
AS_EXTERN AS_API int asScriptEngine_DiscardModule(asScriptEngine* engine, const char *module);
AS_EXTERN AS_API asUINT asScriptEngine_GetModuleCount(asScriptEngine* engine);
AS_EXTERN AS_API asScriptModule* asScriptEngine_GetModuleByIndex(asScriptEngine* engine, asUINT index);

// Script functions
AS_EXTERN AS_API int asScriptEngine_GetLastFunctionId(asScriptEngine* engine);
AS_EXTERN AS_API asScriptFunction* asScriptEngine_GetFunctionById(asScriptEngine* engine, int funcId);

// Type identification
AS_EXTERN AS_API int asScriptEngine_GetTypeIdByDecl(asScriptEngine* engine, const char *decl);
AS_EXTERN AS_API const char* asScriptEngine_GetTypeDeclaration(asScriptEngine* engine, int typeId, bool includeNamespace);
AS_EXTERN AS_API int asScriptEngine_GetSizeOfPrimitiveType(asScriptEngine* engine, int typeId);
AS_EXTERN AS_API asTypeInfo* asScriptEngine_GetTypeInfoById(asScriptEngine* engine, int typeId);
AS_EXTERN AS_API asTypeInfo* asScriptEngine_GetTypeInfoByName(asScriptEngine* engine, const char *name);
AS_EXTERN AS_API asTypeInfo* asScriptEngine_GetTypeInfoByDecl(asScriptEngine* engine, const char *decl);

// Script execution
AS_EXTERN AS_API asScriptContext* asScriptEngine_CreateContext(asScriptEngine* engine);
AS_EXTERN AS_API void* asScriptEngine_CreateScriptObject(asScriptEngine* engine, const asTypeInfo *type);
AS_EXTERN AS_API void* asScriptEngine_CreateScriptObjectCopy(asScriptEngine* engine, void *obj, const asTypeInfo *type);
AS_EXTERN AS_API void* asScriptEngine_CreateUninitializedScriptObject(asScriptEngine* engine, const asTypeInfo *type);
AS_EXTERN AS_API asScriptFunction* asScriptEngine_CreateDelegate(asScriptEngine* engine, asScriptFunction *func, void *obj);
AS_EXTERN AS_API int asScriptEngine_AssignScriptObject(asScriptEngine* engine, void *dstObj, void *srcObj, const asTypeInfo *type);
AS_EXTERN AS_API void asScriptEngine_ReleaseScriptObject(asScriptEngine* engine, void *obj, const asTypeInfo *type);
AS_EXTERN AS_API void asScriptEngine_AddRefScriptObject(asScriptEngine* engine, void *obj, const asTypeInfo *type);
AS_EXTERN AS_API int asScriptEngine_RefCastObject(asScriptEngine* engine, void *obj, asTypeInfo *fromType, asTypeInfo *toType, void **newPtr, bool useOnlyImplicitCast);
AS_EXTERN AS_API asLockableSharedBool* asScriptEngine_GetWeakRefFlagOfScriptObject(asScriptEngine* engine, void *obj, const asTypeInfo *type);

// Context pooling
AS_EXTERN AS_API asScriptContext* asScriptEngine_RequestContext(asScriptEngine* engine);
AS_EXTERN AS_API void asScriptEngine_ReturnContext(asScriptEngine* engine, asScriptContext *ctx);
AS_EXTERN AS_API int asScriptEngine_SetContextCallbacks(asScriptEngine* engine, asREQUESTCONTEXTFUNC_t requestCtx, asRETURNCONTEXTFUNC_t returnCtx, void *param);

// String interpretation
AS_EXTERN AS_API asETokenClass asScriptEngine_ParseToken(asScriptEngine* engine, const char *string, size_t stringLength, asUINT *tokenLength);

// Garbage collection
AS_EXTERN AS_API int asScriptEngine_GarbageCollect(asScriptEngine* engine, asDWORD flags, asUINT numIterations);
AS_EXTERN AS_API void asScriptEngine_GetGCStatistics(asScriptEngine* engine, asUINT *currentSize, asUINT *totalDestroyed, asUINT *totalDetected, asUINT *newObjects, asUINT *totalNewDestroyed);
AS_EXTERN AS_API int asScriptEngine_NotifyGarbageCollectorOfNewObject(asScriptEngine* engine, void *obj, asTypeInfo *type);
AS_EXTERN AS_API int asScriptEngine_GetObjectInGC(asScriptEngine* engine, asUINT idx, asUINT *seqNbr, void **obj, asTypeInfo **type);
AS_EXTERN AS_API void asScriptEngine_GCEnumCallback(asScriptEngine* engine, void *reference);
AS_EXTERN AS_API void asScriptEngine_ForwardGCEnumReferences(asScriptEngine* engine, void *ref, asTypeInfo *type);
AS_EXTERN AS_API void asScriptEngine_ForwardGCReleaseReferences(asScriptEngine* engine, void *ref, asTypeInfo *type);
AS_EXTERN AS_API void asScriptEngine_SetCircularRefDetectedCallback(asScriptEngine* engine, asCIRCULARREFFUNC_t callback, void *param);

// User data
AS_EXTERN AS_API void* asScriptEngine_SetUserData(asScriptEngine* engine, void *data, asPWORD type);
AS_EXTERN AS_API void* asScriptEngine_GetUserData(asScriptEngine* engine, asPWORD type);
AS_EXTERN AS_API void asScriptEngine_SetEngineUserDataCleanupCallback(asScriptEngine* engine, asCLEANENGINEFUNC_t callback, asPWORD type);
AS_EXTERN AS_API void asScriptEngine_SetModuleUserDataCleanupCallback(asScriptEngine* engine, asCLEANMODULEFUNC_t callback, asPWORD type);
AS_EXTERN AS_API void asScriptEngine_SetContextUserDataCleanupCallback(asScriptEngine* engine, asCLEANCONTEXTFUNC_t callback, asPWORD type);
AS_EXTERN AS_API void asScriptEngine_SetFunctionUserDataCleanupCallback(asScriptEngine* engine, asCLEANFUNCTIONFUNC_t callback, asPWORD type);
AS_EXTERN AS_API void asScriptEngine_SetTypeInfoUserDataCleanupCallback(asScriptEngine* engine, asCLEANTYPEINFOFUNC_t callback, asPWORD type);
AS_EXTERN AS_API void asScriptEngine_SetScriptObjectUserDataCleanupCallback(asScriptEngine* engine, asCLEANSCRIPTOBJECTFUNC_t callback, asPWORD type);

// Exception handling
AS_EXTERN AS_API int asScriptEngine_SetTranslateAppExceptionCallback(asScriptEngine* engine, const asSFuncPtr &callback, void *param, int callConv);

/// asIScriptModule
AS_EXTERN AS_API asScriptEngine* asScriptModule_GetEngine(asScriptModule* module);
AS_EXTERN AS_API void asScriptModule_SetName(asScriptModule* module, const char *name);
AS_EXTERN AS_API const char* asScriptModule_GetName(asScriptModule* module);
AS_EXTERN AS_API void asScriptModule_Discard(asScriptModule* module);

// Compilation
AS_EXTERN AS_API int asScriptModule_AddScriptSection(asScriptModule* module, const char *name, const char *code, size_t codeLength, int lineOffset);
AS_EXTERN AS_API int asScriptModule_Build(asScriptModule* module);
AS_EXTERN AS_API int asScriptModule_CompileFunction(asScriptModule* module, const char *sectionName, const char *code, int lineOffset, asDWORD compileFlags, asScriptFunction **outFunc);
AS_EXTERN AS_API int asScriptModule_CompileGlobalVar(asScriptModule* module, const char *sectionName, const char *code, int lineOffset);
AS_EXTERN AS_API asDWORD asScriptModule_SetAccessMask(asScriptModule* module, asDWORD accessMask);
AS_EXTERN AS_API int asScriptModule_SetDefaultNamespace(asScriptModule* module, const char *nameSpace);
AS_EXTERN AS_API const char* asScriptModule_GetDefaultNamespace(asScriptModule* module);

// Functions
AS_EXTERN AS_API asUINT asScriptModule_GetFunctionCount(asScriptModule* module);
AS_EXTERN AS_API asScriptFunction* asScriptModule_GetFunctionByIndex(asScriptModule* module, asUINT index);
AS_EXTERN AS_API asScriptFunction* asScriptModule_GetFunctionByDecl(asScriptModule* module, const char *decl);
AS_EXTERN AS_API asScriptFunction* asScriptModule_GetFunctionByName(asScriptModule* module, const char *name);
AS_EXTERN AS_API int asScriptModule_RemoveFunction(asScriptModule* module, asScriptFunction *func);

// Global variables
AS_EXTERN AS_API int asScriptModule_ResetGlobalVars(asScriptModule* module, asScriptContext *ctx);
AS_EXTERN AS_API asUINT asScriptModule_GetGlobalVarCount(asScriptModule* module);
AS_EXTERN AS_API int asScriptModule_GetGlobalVarIndexByName(asScriptModule* module, const char *name);
AS_EXTERN AS_API int asScriptModule_GetGlobalVarIndexByDecl(asScriptModule* module, const char *decl);
AS_EXTERN AS_API const char* asScriptModule_GetGlobalVarDeclaration(asScriptModule* module, asUINT index, bool includeNamespace);
AS_EXTERN AS_API int asScriptModule_GetGlobalVar(asScriptModule* module, asUINT index, const char **name, const char **nameSpace, int *typeId, bool *isConst);
AS_EXTERN AS_API void* asScriptModule_GetAddressOfGlobalVar(asScriptModule* module, asUINT index);
AS_EXTERN AS_API int asScriptModule_RemoveGlobalVar(asScriptModule* module, asUINT index);

// Type identification
AS_EXTERN AS_API asUINT asScriptModule_GetObjectTypeCount(asScriptModule* module);
AS_EXTERN AS_API asTypeInfo* asScriptModule_GetObjectTypeByIndex(asScriptModule* module, asUINT index);
AS_EXTERN AS_API int asScriptModule_GetTypeIdByDecl(asScriptModule* module, const char *decl);
AS_EXTERN AS_API asTypeInfo* asScriptModule_GetTypeInfoByName(asScriptModule* module, const char *name);
AS_EXTERN AS_API asTypeInfo* asScriptModule_GetTypeInfoByDecl(asScriptModule* module, const char *decl);

// Enums
AS_EXTERN AS_API asUINT asScriptModule_GetEnumCount(asScriptModule* module);
AS_EXTERN AS_API asTypeInfo* asScriptModule_GetEnumByIndex(asScriptModule* module, asUINT index);

// Typedefs
AS_EXTERN AS_API asUINT asScriptModule_GetTypedefCount(asScriptModule* module);
AS_EXTERN AS_API asTypeInfo* asScriptModule_GetTypedefByIndex(asScriptModule* module, asUINT index);

// Dynamic binding between modules
AS_EXTERN AS_API asUINT asScriptModule_GetImportedFunctionCount(asScriptModule* module);
AS_EXTERN AS_API int asScriptModule_GetImportedFunctionIndexByDecl(asScriptModule* module, const char *decl);
AS_EXTERN AS_API const char* asScriptModule_GetImportedFunctionDeclaration(asScriptModule* module, asUINT importIndex);
AS_EXTERN AS_API const char* asScriptModule_GetImportedFunctionSourceModule(asScriptModule* module, asUINT importIndex);
AS_EXTERN AS_API int asScriptModule_BindImportedFunction(asScriptModule* module, asUINT importIndex, asScriptFunction *func);
AS_EXTERN AS_API int asScriptModule_UnbindImportedFunction(asScriptModule* module, asUINT importIndex);
AS_EXTERN AS_API int asScriptModule_BindAllImportedFunctions(asScriptModule* module);
AS_EXTERN AS_API int asScriptModule_UnbindAllImportedFunctions(asScriptModule* module);

// Byte code saving and loading
AS_EXTERN AS_API int asScriptModule_SaveByteCode(asScriptModule* module, asBinaryStream *out, bool stripDebugInfo);
AS_EXTERN AS_API int asScriptModule_LoadByteCode(asScriptModule* module, asBinaryStream *in, bool *wasDebugInfoStripped);

// User data
AS_EXTERN AS_API void *asScriptModule_SetUserData(asScriptModule* module, void *data, asPWORD type);
AS_EXTERN AS_API void *asScriptModule_GetUserData(asScriptModule* module, asPWORD type);

/// asITypeInfo
// Miscellaneous
AS_EXTERN AS_API asScriptEngine* asTypeInfo_GetEngine(asTypeInfo* typeInfo);
AS_EXTERN AS_API const char* asTypeInfo_GetConfigGroup(asTypeInfo* typeInfo);
AS_EXTERN AS_API asDWORD asTypeInfo_GetAccessMask(asTypeInfo* typeInfo);
AS_EXTERN AS_API asScriptModule* asTypeInfo_GetModule(asTypeInfo* typeInfo);

// Memory management
AS_EXTERN AS_API int asTypeInfo_AddRef(asTypeInfo* typeInfo);
AS_EXTERN AS_API int asTypeInfo_Release(asTypeInfo* typeInfo);

// Type info
AS_EXTERN AS_API const char* asTypeInfo_GetName(asTypeInfo* typeInfo);
AS_EXTERN AS_API const char* asTypeInfo_GetNamespace(asTypeInfo* typeInfo);
AS_EXTERN AS_API asTypeInfo* asTypeInfo_GetBaseType(asTypeInfo* typeInfo);
AS_EXTERN AS_API bool asTypeInfo_DerivesFrom(asTypeInfo* typeInfo, const asTypeInfo *objType);
AS_EXTERN AS_API asQWORD asTypeInfo_GetFlags(asTypeInfo* typeInfo);
AS_EXTERN AS_API asUINT asTypeInfo_GetSize(asTypeInfo* typeInfo);
AS_EXTERN AS_API int asTypeInfo_GetTypeId(asTypeInfo* typeInfo);
AS_EXTERN AS_API int asTypeInfo_GetSubTypeId(asTypeInfo* typeInfo, asUINT subTypeIndex);
AS_EXTERN AS_API asTypeInfo* asTypeInfo_GetSubType(asTypeInfo* typeInfo, asUINT subTypeIndex);
AS_EXTERN AS_API asUINT asTypeInfo_GetSubTypeCount(asTypeInfo* typeInfo);

// Interfaces
AS_EXTERN AS_API asUINT asTypeInfo_GetInterfaceCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API asTypeInfo* asTypeInfo_GetInterface(asTypeInfo* typeInfo, asUINT index);
AS_EXTERN AS_API bool asTypeInfo_Implements(asTypeInfo* typeInfo, const asTypeInfo *objType);

// Factories
AS_EXTERN AS_API asUINT asTypeInfo_GetFactoryCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetFactoryByIndex(asTypeInfo* typeInfo, asUINT index);
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetFactoryByDecl(asTypeInfo* typeInfo, const char *decl);

// Methods
AS_EXTERN AS_API asUINT asTypeInfo_GetMethodCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetMethodByIndex(asTypeInfo* typeInfo, asUINT index, bool getVirtual);
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetMethodByName(asTypeInfo* typeInfo, const char *name, bool getVirtual);
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetMethodByDecl(asTypeInfo* typeInfo, const char *decl, bool getVirtual);

// Properties
AS_EXTERN AS_API asUINT asTypeInfo_GetPropertyCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API int asTypeInfo_GetProperty(asTypeInfo* typeInfo, asUINT index, const char **name, int *typeId, bool *isPrivate, bool *isProtected, int *offset, bool *isReference, asDWORD *accessMask, int *compositeOffset, bool *isCompositeIndirect, bool *isConst);
AS_EXTERN AS_API const char* asTypeInfo_GetPropertyDeclaration(asTypeInfo* typeInfo, asUINT index, bool includeNamespace);

// Behaviours
AS_EXTERN AS_API asUINT asTypeInfo_GetBehaviourCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetBehaviourByIndex(asTypeInfo* typeInfo, asUINT index, asEBehaviours *outBehaviour);

// Child types
AS_EXTERN AS_API asUINT asTypeInfo_GetChildFuncdefCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API asTypeInfo* asTypeInfo_GetChildFuncdef(asTypeInfo* typeInfo, asUINT index);
AS_EXTERN AS_API asTypeInfo* asTypeInfo_GetParentType(asTypeInfo* typeInfo);

// Enums
AS_EXTERN AS_API asUINT asTypeInfo_GetEnumValueCount(asTypeInfo* typeInfo);
AS_EXTERN AS_API const char* asTypeInfo_GetEnumValueByIndex(asTypeInfo* typeInfo, asUINT index, int *outValue);

// Typedef
AS_EXTERN AS_API int asTypeInfo_GetTypedefTypeId(asTypeInfo* typeInfo);

// Funcdef
AS_EXTERN AS_API asScriptFunction* asTypeInfo_GetFuncdefSignature(asTypeInfo* typeInfo);

// User data
AS_EXTERN AS_API void* asTypeInfo_SetUserData(asTypeInfo* typeInfo, void *data, asPWORD type);
AS_EXTERN AS_API void* asTypeInfo_GetUserData(asTypeInfo* typeInfo, asPWORD type);

/// asIScriptContext
// Memory management
AS_EXTERN AS_API int asScriptContext_AddRef(asScriptContext* context);
AS_EXTERN AS_API int asScriptContext_Release(asScriptContext* context);

// Miscellaneous
AS_EXTERN AS_API asScriptEngine* asScriptContext_GetEngine(asScriptContext* context);

// Execution
AS_EXTERN AS_API int             asScriptContext_Prepare(asScriptContext* context, asScriptFunction *func);
AS_EXTERN AS_API int             asScriptContext_Unprepare(asScriptContext* context);
AS_EXTERN AS_API int             asScriptContext_Execute(asScriptContext* context);
AS_EXTERN AS_API int             asScriptContext_Abort(asScriptContext* context);
AS_EXTERN AS_API int             asScriptContext_Suspend(asScriptContext* context);
AS_EXTERN AS_API asEContextState asScriptContext_GetState(asScriptContext* context);
AS_EXTERN AS_API int             asScriptContext_PushState(asScriptContext* context);
AS_EXTERN AS_API int             asScriptContext_PopState(asScriptContext* context);
AS_EXTERN AS_API bool            asScriptContext_IsNested(asScriptContext* context, asUINT *nestCount = 0);

// Object pointer for calling class methods
AS_EXTERN AS_API int   asScriptContext_SetObject(asScriptContext* context, void *obj);

// Arguments
AS_EXTERN AS_API int   asScriptContext_SetArgByte(asScriptContext* context, asUINT arg, asBYTE value);
AS_EXTERN AS_API int   asScriptContext_SetArgWord(asScriptContext* context, asUINT arg, asWORD value);
AS_EXTERN AS_API int   asScriptContext_SetArgDWord(asScriptContext* context, asUINT arg, asDWORD value);
AS_EXTERN AS_API int   asScriptContext_SetArgQWord(asScriptContext* context, asUINT arg, asQWORD value);
AS_EXTERN AS_API int   asScriptContext_SetArgFloat(asScriptContext* context, asUINT arg, float value);
AS_EXTERN AS_API int   asScriptContext_SetArgDouble(asScriptContext* context, asUINT arg, double value);
AS_EXTERN AS_API int   asScriptContext_SetArgAddress(asScriptContext* context ,asUINT arg, void *addr);
AS_EXTERN AS_API int   asScriptContext_SetArgObject(asScriptContext* context, asUINT arg, void *obj);
AS_EXTERN AS_API int   asScriptContext_SetArgVarType(asScriptContext* context, asUINT arg, void *ptr, int typeId);
AS_EXTERN AS_API void *asScriptContext_GetAddressOfArg(asScriptContext* context, asUINT arg);

// Return value
AS_EXTERN AS_API asBYTE  asScriptContext_GetReturnByte(asScriptContext* context);
AS_EXTERN AS_API asWORD  asScriptContext_GetReturnWord(asScriptContext* context);
AS_EXTERN AS_API asDWORD asScriptContext_GetReturnDWord(asScriptContext* context);
AS_EXTERN AS_API asQWORD asScriptContext_GetReturnQWord(asScriptContext* context);
AS_EXTERN AS_API float   asScriptContext_GetReturnFloat(asScriptContext* context);
AS_EXTERN AS_API double  asScriptContext_GetReturnDouble(asScriptContext* context);
AS_EXTERN AS_API void   *asScriptContext_GetReturnAddress(asScriptContext* context);
AS_EXTERN AS_API void   *asScriptContext_GetReturnObject(asScriptContext* context);
AS_EXTERN AS_API void   *asScriptContext_GetAddressOfReturnValue(asScriptContext* context);

// Exception handling
AS_EXTERN AS_API int                asScriptContext_SetException(asScriptContext* context, const char *info, bool allowCatch = true);
AS_EXTERN AS_API int                asScriptContext_GetExceptionLineNumber(asScriptContext* context, int *column = 0, const char **sectionName = 0);
AS_EXTERN AS_API asScriptFunction *asScriptContext_GetExceptionFunction(asScriptContext* context);
AS_EXTERN AS_API const char *       asScriptContext_GetExceptionString(asScriptContext* context);
AS_EXTERN AS_API bool               asScriptContext_WillExceptionBeCaught(asScriptContext* context);
AS_EXTERN AS_API int                asScriptContext_SetExceptionCallback(asScriptContext* context, const asSFuncPtr &callback, void *obj, int callConv);
AS_EXTERN AS_API void               asScriptContext_ClearExceptionCallback(asScriptContext* context);

// Debugging
AS_EXTERN AS_API int                asScriptContext_SetLineCallback(asScriptContext* context, const asSFuncPtr &callback, void *obj, int callConv);
AS_EXTERN AS_API void               asScriptContext_ClearLineCallback(asScriptContext* context);
AS_EXTERN AS_API asUINT             asScriptContext_GetCallstackSize(asScriptContext* context);
AS_EXTERN AS_API asScriptFunction *asScriptContext_GetFunction(asScriptContext* context, asUINT stackLevel = 0);
AS_EXTERN AS_API int                asScriptContext_GetLineNumber(asScriptContext* context, asUINT stackLevel = 0, int *column = 0, const char **sectionName = 0);
AS_EXTERN AS_API int                asScriptContext_GetVarCount(asScriptContext* context, asUINT stackLevel = 0);
AS_EXTERN AS_API int                asScriptContext_GetVar(asScriptContext* context, asUINT varIndex, asUINT stackLevel, const char** name, int* typeId = 0, asETypeModifiers* typeModifiers = 0, bool* isVarOnHeap = 0, int* stackOffset = 0);
AS_EXTERN AS_API const char        *asScriptContext_GetVarDeclaration(asScriptContext* context, asUINT varIndex, asUINT stackLevel = 0, bool includeNamespace = false);
AS_EXTERN AS_API void              *asScriptContext_GetAddressOfVar(asScriptContext* context, asUINT varIndex, asUINT stackLevel = 0, bool dontDereference = false, bool returnAddressOfUnitializedObjects = false);
AS_EXTERN AS_API bool               asScriptContext_IsVarInScope(asScriptContext* context, asUINT varIndex, asUINT stackLevel = 0);
AS_EXTERN AS_API int                asScriptContext_GetThisTypeId(asScriptContext* context, asUINT stackLevel = 0);
AS_EXTERN AS_API void              *asScriptContext_GetThisPointer(asScriptContext* context, asUINT stackLevel = 0);
AS_EXTERN AS_API asScriptFunction *asScriptContext_GetSystemFunction(asScriptContext* context);

// User data
AS_EXTERN AS_API void *asScriptContext_SetUserData(asScriptContext* context, void *data, asPWORD type = 0);
AS_EXTERN AS_API void *asScriptContext_GetUserData(asScriptContext* context, asPWORD type = 0);

// Serialization
AS_EXTERN AS_API int asScriptContext_StartDeserialization(asScriptContext* context);
AS_EXTERN AS_API int asScriptContext_FinishDeserialization(asScriptContext* context);
AS_EXTERN AS_API int asScriptContext_PushFunction(asScriptContext* context, asScriptFunction *func, void *object);
AS_EXTERN AS_API int asScriptContext_GetStateRegisters(asScriptContext* context, asUINT stackLevel, asScriptFunction **callingSystemFunction, asScriptFunction **initialFunction, asDWORD *origStackPointer, asDWORD *argumentsSize, asQWORD *valueRegister, void **objectRegister, asTypeInfo **objectTypeRegister);
AS_EXTERN AS_API int asScriptContext_GetCallStateRegisters(asScriptContext* context, asUINT stackLevel, asDWORD *stackFramePointer, asScriptFunction **currentFunction, asDWORD *programPointer, asDWORD *stackPointer, asDWORD *stackIndex);
AS_EXTERN AS_API int asScriptContext_SetStateRegisters(asScriptContext* context, asUINT stackLevel, asScriptFunction *callingSystemFunction, asScriptFunction *initialFunction, asDWORD origStackPointer, asDWORD argumentsSize, asQWORD valueRegister, void *objectRegister, asTypeInfo *objectTypeRegister);
AS_EXTERN AS_API int asScriptContext_SetCallStateRegisters(asScriptContext* context, asUINT stackLevel, asDWORD stackFramePointer, asScriptFunction *currentFunction, asDWORD programPointer, asDWORD stackPointer, asDWORD stackIndex);
AS_EXTERN AS_API int asScriptContext_GetArgsOnStackCount(asScriptContext* context, asUINT stackLevel);
AS_EXTERN AS_API int asScriptContext_GetArgOnStack(asScriptContext* context, asUINT stackLevel, asUINT arg, int* typeId, asUINT *flags, void** address);


///asIScriptGeneric
// Miscellaneous
AS_EXTERN AS_API asScriptEngine   *asScriptGeneric_GetEngine(asScriptGeneric* generic);
AS_EXTERN AS_API asScriptFunction *asScriptGeneric_GetFunction(asScriptGeneric* generic);
AS_EXTERN AS_API void              *asScriptGeneric_GetAuxiliary(asScriptGeneric* generic);

// Object
AS_EXTERN AS_API void   *asScriptGeneric_GetObject(asScriptGeneric* generic);
AS_EXTERN AS_API int     asScriptGeneric_GetObjectTypeId(asScriptGeneric* generic);

// Arguments
AS_EXTERN AS_API int     asScriptGeneric_GetArgCount(asScriptGeneric* generic);
AS_EXTERN AS_API int     asScriptGeneric_GetArgTypeId(asScriptGeneric* generic, asUINT arg, asDWORD *flags = 0);
AS_EXTERN AS_API asBYTE  asScriptGeneric_GetArgByte(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API asWORD  asScriptGeneric_GetArgWord(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API asDWORD asScriptGeneric_GetArgDWord(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API asQWORD asScriptGeneric_GetArgQWord(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API float   asScriptGeneric_GetArgFloat(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API double  asScriptGeneric_GetArgDouble(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API void   *asScriptGeneric_GetArgAddress(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API void   *asScriptGeneric_GetArgObject(asScriptGeneric* generic, asUINT arg);
AS_EXTERN AS_API void   *asScriptGeneric_GetAddressOfArg(asScriptGeneric* generic, asUINT arg);

// Return value
AS_EXTERN AS_API int     asScriptGeneric_GetReturnTypeId(asScriptGeneric* generic, asDWORD *flags = 0);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnByte(asScriptGeneric* generic, asBYTE val);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnWord(asScriptGeneric* generic, asWORD val);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnDWord(asScriptGeneric* generic, asDWORD val);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnQWord(asScriptGeneric* generic, asQWORD val);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnFloat(asScriptGeneric* generic, float val);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnDouble(asScriptGeneric* generic, double val);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnAddress(asScriptGeneric* generic, void *addr);
AS_EXTERN AS_API int     asScriptGeneric_SetReturnObject(asScriptGeneric* generic, void *obj);
AS_EXTERN AS_API void   *asScriptGeneric_GetAddressOfReturnLocation(asScriptGeneric* generic);

/// asIScriptObject
// Memory management
AS_EXTERN AS_API int                    asScriptObject_AddRef(asScriptObject* object);
AS_EXTERN AS_API int                    asScriptObject_Release(asScriptObject* object);
AS_EXTERN AS_API asLockableSharedBool *asScriptObject_GetWeakRefFlag(asScriptObject* object);

// Type info
AS_EXTERN AS_API int            asScriptObject_GetTypeId(asScriptObject* object);
AS_EXTERN AS_API asTypeInfo   *asScriptObject_GetObjectType(asScriptObject* object);

// Class properties
AS_EXTERN AS_API asUINT      asScriptObject_GetPropertyCount(asScriptObject* object);
AS_EXTERN AS_API int         asScriptObject_GetPropertyTypeId(asScriptObject* object, asUINT prop);
AS_EXTERN AS_API const char *asScriptObject_GetPropertyName(asScriptObject* object, asUINT prop);
AS_EXTERN AS_API void       *asScriptObject_GetAddressOfProperty(asScriptObject* object, asUINT prop);

// Miscellaneous
AS_EXTERN AS_API asScriptEngine *asScriptObject_GetEngine(asScriptObject* object);
AS_EXTERN AS_API int              asScriptObject_CopyFrom(asScriptObject* object, const asScriptObject *other);

// User data
AS_EXTERN AS_API void *asScriptObject_SetUserData(asScriptObject* object, void *data, asPWORD type = 0);
AS_EXTERN AS_API void *asScriptObject_GetUserData(asScriptObject* object, asPWORD type = 0);

/// asIScriptFunction
AS_EXTERN AS_API asScriptEngine *asScriptFunction_GetEngine(asScriptFunction* function);

// Memory management
AS_EXTERN AS_API int              asScriptFunction_AddRef(asScriptFunction* function);
AS_EXTERN AS_API int              asScriptFunction_Release(asScriptFunction* function);

// Miscellaneous
AS_EXTERN AS_API int              asScriptFunction_GetId(asScriptFunction* function);
AS_EXTERN AS_API asEFuncType      asScriptFunction_GetFuncType(asScriptFunction* function);
AS_EXTERN AS_API const char      *asScriptFunction_GetModuleName(asScriptFunction* function);
AS_EXTERN AS_API asScriptModule *asScriptFunction_GetModule(asScriptFunction* function);
AS_EXTERN AS_API const char      *asScriptFunction_GetConfigGroup(asScriptFunction* function);
AS_EXTERN AS_API asDWORD          asScriptFunction_GetAccessMask(asScriptFunction* function);
AS_EXTERN AS_API void            *asScriptFunction_GetAuxiliary(asScriptFunction* function);

// Function signature
AS_EXTERN AS_API asTypeInfo     *asScriptFunction_GetObjectType(asScriptFunction* function);
AS_EXTERN AS_API const char      *asScriptFunction_GetObjectName(asScriptFunction* function);
AS_EXTERN AS_API const char      *asScriptFunction_GetName(asScriptFunction* function);
AS_EXTERN AS_API const char      *asScriptFunction_GetNamespace(asScriptFunction* function);
AS_EXTERN AS_API const char      *asScriptFunction_GetDeclaration(asScriptFunction* function, bool includeObjectName = true, bool includeNamespace = false, bool includeParamNames = false);
AS_EXTERN AS_API bool             asScriptFunction_IsReadOnly(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsPrivate(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsProtected(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsFinal(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsOverride(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsShared(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsExplicit(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsProperty(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsVariadic(asScriptFunction* function);
AS_EXTERN AS_API asUINT           asScriptFunction_GetParamCount(asScriptFunction* function);
AS_EXTERN AS_API int              asScriptFunction_GetParam(asScriptFunction* function, asUINT index, int *typeId, asDWORD *flags = 0, const char **name = 0, const char **defaultArg = 0);
AS_EXTERN AS_API int              asScriptFunction_GetReturnTypeId(asScriptFunction* function, asDWORD *flags = 0);

// Template functions
AS_EXTERN AS_API asUINT           asScriptFunction_GetSubTypeCount(asScriptFunction* function);
AS_EXTERN AS_API int              asScriptFunction_GetSubTypeId(asScriptFunction* function, asUINT subTypeIndex = 0);
AS_EXTERN AS_API asTypeInfo     *asScriptFunction_GetSubType(asScriptFunction* function, asUINT subTypeIndex = 0);

// Type id for function pointers
AS_EXTERN AS_API int              asScriptFunction_GetTypeId(asScriptFunction* function);
AS_EXTERN AS_API bool             asScriptFunction_IsCompatibleWithTypeId(asScriptFunction* function, int typeId);

// Delegates
AS_EXTERN AS_API void              *asScriptFunction_GetDelegateObject(asScriptFunction* function);
AS_EXTERN AS_API asTypeInfo       *asScriptFunction_GetDelegateObjectType(asScriptFunction* function);
AS_EXTERN AS_API asScriptFunction *asScriptFunction_GetDelegateFunction(asScriptFunction* function);

// Debug information
AS_EXTERN AS_API asUINT           asScriptFunction_GetVarCount(asScriptFunction* function);
AS_EXTERN AS_API int              asScriptFunction_GetVar(asScriptFunction* function, asUINT index, const char **name, int *typeId = 0);
AS_EXTERN AS_API const char      *asScriptFunction_GetVarDecl(asScriptFunction* function, asUINT index, bool includeNamespace = false);
AS_EXTERN AS_API int              asScriptFunction_FindNextLineWithCode(asScriptFunction* function, int line);
AS_EXTERN AS_API int              asScriptFunction_GetDeclaredAt(asScriptFunction* function, const char** scriptSection, int* row, int* col);
AS_EXTERN AS_API int              asScriptFunction_GetLineEntryCount(asScriptFunction* function);
AS_EXTERN AS_API int              asScriptFunction_GetLineEntry(asScriptFunction* function, asUINT index, int* row, int* col, const char** sectionName, const asDWORD** byteCode);

// For JIT compilation
AS_EXTERN AS_API asDWORD         *asScriptFunction_GetByteCode(asScriptFunction* function, asUINT *length = 0);
AS_EXTERN AS_API int              asScriptFunction_SetJITFunction(asScriptFunction* function, asJITFunction jitFunc);
AS_EXTERN AS_API asJITFunction    asScriptFunction_GetJITFunction(asScriptFunction* function);

// User data
AS_EXTERN AS_API void            *asScriptFunction_SetUserData(asScriptFunction* function, void *userData, asPWORD type = 0);
AS_EXTERN AS_API void            *asScriptFunction_GetUserData(asScriptFunction* function, asPWORD type = 0);

#ifdef __cpluspluc
class asIStringFactoryC;
class asIBinaryStreamC;
#endif


AS_EXTERN AS_API asBinaryStream* asBinaryStream_Create(asSBinaryStream interface);
AS_EXTERN AS_API void asBinaryStream_Destroy(asBinaryStream* stream);
AS_EXTERN AS_API asStringFactory* asStringFactory_Create(asSStringFactory interface);
AS_EXTERN AS_API void asStringFactory_Destroy(asStringFactory* stream);
