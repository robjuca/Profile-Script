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
    public abstract class TOperationHandlerBase (THandlerData handlerData)
    {
        #region Property
        protected IProviderServices Services => HandlerData.Services;
        protected THandlerData HandlerData { get; private set; } = handlerData;
        protected THandlerModuleData HandlerModuleData => HandlerData.HandlerModuleData;
        protected THandlerMessageData HandlerMessageData => HandlerData.HandlerMessageData;
        protected THandlerSpeechData HandlerSpeechData => HandlerData.HandlerSpeechData;

        protected bool ValidateHandlerSpeech => HandlerData.ValidateHandlerSpeech;
        protected bool ValidateHandlerModule => HandlerData.ValidateHandlerModule;
        protected bool ValidateHandlerMessage => HandlerData.ValidateHandlerMessage;
        protected bool ValidateHandlerReceiver => HandlerData.ValidateHandlerReceiver;

        protected TScriptDefinitionData DefinitionData => TScriptDefinitionData.CreateDefault ();
        #endregion

        #region Virtual
        public virtual void Process ()
        {
        }

        public virtual void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
        }
        #endregion

        #region Support
        protected void SetScriptDataValue (TScriptDefinitionData definitionData) => Services.SetScriptDataValue (definitionData.Clone ());
        protected string ToString (UReceiverModule name) => TEnumExtension.AsString (name);
        #endregion

    };
    //---------------------------//

}  // namespace
