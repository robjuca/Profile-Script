/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
//---------------------------//

namespace rr.Provider.Message
{
    public class TNextModuleData
    {
        /*  
         
            var msg = TMessageInternal.CreateDefault (Module, UMessageAction.NEXT_MODULE);
            msg.SelectReceiverModule (UHandlerModule.PROCESS_DISPATCHER);
            msg.AddDestinationModule (UHandlerModule.OUTSIDE_OPE);
            msg.SelectParam (TParamInfo.Create(TNextModuleData instance))
          
           
            var msg = TMessageInternal.CreateDefault (CurrentModuleName, NextModuleAction);
            msg.SelectReceiverModule (ReceiverModuleName);
            msg.AddDestinationModule (NextModuleName);
            msg.SelectParam (TParamInfo.Create (this))

        */


        #region Property
        public UHandlerModule CurrentModuleName { get; private set; }
        public UHandlerModule NextModuleName { get; private set; }
        public UMessageName MessageName { get; private set; }
        public UMessageAction MessageAction { get; private set; }

        public UMessageAction NextModuleAction => UMessageAction.NEXT_MODULE;           // always
        public UHandlerModule ReceiverModuleName => UHandlerModule.PROCESS_DISPATCHER;  // always
        #endregion

        #region Members
        public void AddCurrentModule (UHandlerModule currentModule) => CurrentModuleName = currentModule;
        public void AddNextModule (UHandlerModule nextModule) => NextModuleName = nextModule;
        public void AddMessageName (UMessageName messageName) => MessageName = messageName;
        public void AddMessageAction (UMessageAction messageAction) => MessageAction = messageAction;
        #endregion

        #region Static
        static public TNextModuleData CreateDefault => new () {
            CurrentModuleName = UHandlerModule.NONE,
            NextModuleName = UHandlerModule.NONE,
            MessageName = UMessageName.NONE,
            MessageAction = UMessageAction.NONE,
        };

        static public TNextModuleData Create (UHandlerModule current, UHandlerModule next) => new () {
            CurrentModuleName = current,
            NextModuleName = next,
            MessageName = UMessageName.NONE,
            MessageAction = UMessageAction.NONE,
        };

        static public TNextModuleData Create (UHandlerModule current, UHandlerModule next, UMessageName messageName, UMessageAction messageAction) => new () {
            CurrentModuleName = current,
            NextModuleName = next,
            MessageName = messageName,
            MessageAction = messageAction
        }; 
        #endregion
    };
    //---------------------------//

}  // namespace
