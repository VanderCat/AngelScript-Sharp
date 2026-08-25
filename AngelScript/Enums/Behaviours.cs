using System.ComponentModel;

namespace AngelScript;

public enum Behaviour : uint {
    [Description("Constructor.")]
    Construct = asEBehaviours.asBEHAVE_CONSTRUCT,
    [Description("Constructor used exclusively for initialization lists.")]
    ListConstruct = asEBehaviours.asBEHAVE_LIST_CONSTRUCT,
    [Description("Destructor.")]
    Destruct = asEBehaviours.asBEHAVE_DESTRUCT,
    [Description("Factory.")]
    Factory = asEBehaviours.asBEHAVE_FACTORY,
    [Description("Factory used exclusively for initialization lists.")]
    ListFactory = asEBehaviours.asBEHAVE_LIST_FACTORY,
    [Description("AddRef.")]
    AddRef = asEBehaviours.asBEHAVE_ADDREF,
    [Description("Release.")]
    Release = asEBehaviours.asBEHAVE_RELEASE,
    [Description("Obtain weak ref flag.")]
    WeakRefFlag = asEBehaviours.asBEHAVE_GET_WEAKREF_FLAG,
    [Description("Callback for validating template instances.")]
    TemplateCallback = asEBehaviours.asBEHAVE_TEMPLATE_CALLBACK,
    [Description("(GC) Get reference count ")]
    GetRefCount = asEBehaviours.asBEHAVE_GETREFCOUNT,
    [Description("(GC) Set GC flag")]
    SetGCFlag = asEBehaviours.asBEHAVE_SETGCFLAG,
    [Description("(GC) Get GC flag")]
    GetGCFlag = asEBehaviours.asBEHAVE_GETGCFLAG,
    [Description("(GC) Enumerate held references")]
    EnumRefs = asEBehaviours.asBEHAVE_ENUMREFS,
    [Description("(GC) Release all references")]
    ReleaseRefs = asEBehaviours.asBEHAVE_RELEASEREFS,
}