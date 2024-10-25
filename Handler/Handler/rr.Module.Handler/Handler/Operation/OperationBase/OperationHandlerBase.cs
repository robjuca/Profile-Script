/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- TOperationHandlerBase
    public abstract class TOperationHandlerBase ()
    {
        #region Property
        protected IProviderServices Services => HandlerData.Services;
        protected TModelData HandlerData { get; private set; }
        protected TModuleModel HandlerModuleData => HandlerData.HandlerModuleData;
        protected TMessageModel HandlerMessageData => HandlerData.HandlerMessageData;
        protected TSpeechModel HandlerSpeechData => HandlerData.HandlerSpeechData;

        protected bool ValidateHandlerSpeech => HandlerData.ValidateHandlerSpeech;
        protected bool ValidateHandlerModule => HandlerData.ValidateHandlerModule;
        protected bool ValidateHandlerMessage => HandlerData.ValidateHandlerMessage;
        protected bool ValidateHandlerReceiver => HandlerData.ValidateHandlerReceiver;

        protected TScriptDefinitionData DefinitionData => TScriptDefinitionData.CreateDefault ();
        #endregion

        #region Members
        public void SelectHandlerData (TModelData handlerData) => HandlerData = handlerData; 
        #endregion

        #region Virtual
        public virtual void Process ( ) { }
        public virtual void ScriptReturnCode (TReturnCodeArgs args) { }
        #endregion

        #region Support
        protected void SetScriptDataValue (TScriptDefinitionData definitionData) => Services.SetScriptDataValue (definitionData);
        protected string ToString (UReceiverModule name) => TEnumExtension.AsString (name);
        #endregion

    };
    //---------------------------//

}  // namespace
