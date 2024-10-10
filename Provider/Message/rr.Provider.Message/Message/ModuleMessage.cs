/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;
using rr.Library.Message;
using rr.Library.Types;
using rr.Provider.Resources;

using System.Collections.Generic;
//---------------------------//

namespace rr.Provider.Message
{
    //----- TMessageInternal
    public class TMessageInternal (TMessageData messageData) : TMessage<TMessageData> (messageData)
    {
        #region Property
        public UHandlerModule SenderModule => Data.SenderModule;
        public UHandlerModule ReceiverModule => Data.ReceiverModule;
        public UMessageAction MessageAction => Data.Action;
        public TParamInfo ParamInfo => Data.ParamInfo;
        public UHandlerModule [] RequestDestinationModules => Data.RequestDestinationModules;
        #endregion

        #region Members
        public void SelectSenderModule (UHandlerModule module) => Data.SelectSenderModule (module);
        public void SelectReceiverModule (UHandlerModule module) => Data.SelectReceiverModule (module);
        public void AddDestinationModule (UHandlerModule module) => Data.AddDestinationModule (module);
        public void AddDestinationModule (List<UHandlerModule> modules) => Data.AddDestinationModule (modules);
        public void AddDestinationModule (System.Array modules) => Data.AddDestinationModule (modules);

        public bool IsNotMeSender (TTypeInfo info) => Data.Group.IsNotMeSender (info);
        public bool IsModuleSender (TTypeInfo info) => Data.Group.IsModuleSender (info);
        public bool IsModuleSender (TModuleInfo info) => Data.Group.IsModuleSender (info.TypeInfo);
        public bool IsModuleSender (UHandlerModule module) => Data.Group.IsModuleSender (module);

        public bool IsNotMeReceiver (TTypeInfo info) => Data.Group.IsNotMeReceiver (info);
        public bool IsModuleReceiver (TTypeInfo info) => Data.Group.IsModuleReceiver (info);
        public bool IsModuleReceiver (TModuleInfo info) => Data.Group.IsModuleReceiver (info.TypeInfo);
        public bool IsModuleReceiver (UHandlerModule module) => Data.Group.IsModuleReceiver (module);

        public bool IsAction (UMessageAction action) => Data.IsAction (action);
        public void SelectAction (UMessageAction action) => Data.SelectAction (action);

        public void SelectParam (TParamInfo paramInfo) => Data.SelectParam (paramInfo);
        public bool RequestParam<T> (out T someObject) => Data.RequestParam<T> (out someObject);

        public bool ValidateReceiver (TTypeInfo info) => IsNotMeSender (info) && IsModuleReceiver (info);
        public void SwapReceiver (UHandlerModule module) => SelectReceiverModule (module);

        public TMessageInternal Clone ()
        {
            var clone = CreateDefault ();
            Data.CopyTo (clone.Data);

            return clone;
        }
        #endregion

        #region Static
        public static TMessageInternal CreateDefault ()
        {
            var msg = new TMessageInternal (TMessageData.CreateDefault ());
            msg.SelectReceiverModule (UHandlerModule.PROCESS_DISPATCHER);

            return msg;
        }

        public static TMessageInternal CreateDefault (UHandlerModule sender, UMessageAction messageAction)
        {
            var msg = CreateDefault ();
            msg.SelectSenderModule (sender);
            msg.SelectAction (messageAction);

            return msg;
        }
        #endregion
    };
    //---------------------------//

    //----- TMessageData
    public class TMessageData : TMessageData<TParamInfo, TModuleGroup, UMessageAction>
    {
        #region Property
        public UHandlerModule SenderModule => Group.SenderModule;
        public UHandlerModule ReceiverModule => Group.ReceiverModule;
        public UHandlerModule [] RequestDestinationModules => [ .. DestinationModules ];
        #endregion

        #region Constructor
        public TMessageData (
            TParamInfo paramInfo,
            TModuleGroup group,
            UMessageAction messageAction) : base (paramInfo, group, messageAction) => DestinationModules = [];
        #endregion

        #region Members
        public void SelectSenderModule (UHandlerModule module) => Group.SelectSenderModule (module);
        public void SelectReceiverModule (UHandlerModule module) => Group.SelectReceiverModule (module);
        public void AddDestinationModule (UHandlerModule module) => DestinationModules.Add (module);
        public void ClearDestinationModule () => DestinationModules.Clear ();
        public bool IsAction (UMessageAction action) => Action.Equals (action);
        public void SelectAction (UMessageAction action) => Select (action);
        public bool RequestParam<T> (out T someObject) => ParamInfo.RequestParam<T> (out someObject);

        public void AddDestinationModule (List<UHandlerModule> modules)
        {
            DestinationModules.Clear ();
            DestinationModules.AddRange (modules);
        }

        public void AddDestinationModule (System.Array modules)
        {
            DestinationModules.Clear ();
            DestinationModules.AddRange (modules as IEnumerable<UHandlerModule>);
        }

        public void CopyTo (TMessageData data)
        {
            Group.CopyTo (data.Group);

            data.SelectAction (Action);
            data.SelectParam (ParamInfo);

            data.DestinationModules.AddRange (DestinationModules);
        }
        #endregion

        #region Property
        List<UHandlerModule> DestinationModules { get; set; }
        #endregion

        #region Static
        public static TMessageData CreateDefault () => new (
            TParamInfo.Create (new object ()),
            TModuleGroup.CreateDefault (),
            UMessageAction.NONE);

        public static TMessageData Create (TParamInfo paramInfo, TModuleGroup group, UMessageAction action) => new (
            paramInfo,
            group,
            action);

        public static TMessageData Create (TModuleGroup group, UMessageAction action) => new (
            TParamInfo.Create (new object ()),
            group,
            action);
        #endregion
    };
    //---------------------------//

    //----- TModuleGroup
    public class TModuleGroup (
        TModuleInfo sender,
        TModuleInfo receiver) : TModuleInfoGroup<TModuleInfo, TModuleInfo> (sender, receiver)
    {
        #region Property
        public UHandlerModule SenderModule => TEnumExtension.ToEnum<UHandlerModule> (Sender.TypeInfo.ModuleName);
        public UHandlerModule ReceiverModule => TEnumExtension.ToEnum<UHandlerModule> (Receiver.TypeInfo.ModuleName);
        #endregion

        #region Members
        public void SelectSenderModule (UHandlerModule module) => Sender.TypeInfo.SelectModuleName (TEnumExtension.NameOf (module));
        public void SelectReceiverModule (UHandlerModule module) => Receiver.TypeInfo.SelectModuleName (TEnumExtension.NameOf (module));

        public bool IsNotMeSender (TTypeInfo info) => Sender.TypeInfo.NotMe (info);
        public bool IsModuleSender (TTypeInfo info) => Sender.TypeInfo.IsSame (info);
        public bool IsModuleSender (UHandlerModule module) => IsModuleSender (TEnumExtension.NameOf (module));
        public bool IsModuleSender (string moduleName) => Sender.IsModule (moduleName);

        public bool IsNotMeReceiver (TTypeInfo info) => (Receiver.TypeInfo.NotMe (info));
        public bool IsModuleReceiver (TTypeInfo info) => Receiver.TypeInfo.IsSame (info);
        public bool IsModuleReceiver (UHandlerModule module) => IsModuleReceiver (TEnumExtension.NameOf (module));
        public bool IsModuleReceiver (string moduleName) => Receiver.IsModule (moduleName);

        public void CopyTo (TModuleGroup group)
        {
            Sender.CopyTo (group.Sender);
            Receiver.CopyTo (group.Receiver);
        }
        #endregion

        #region Static
        public static TModuleGroup CreateDefault () => new (TModuleInfo.CreateDefault (), TModuleInfo.CreateDefault ());
        public static TModuleGroup Create (TModuleInfo sender, TModuleInfo receiver) => new (sender, receiver);
        #endregion
    };
    //---------------------------//

}  // namespace