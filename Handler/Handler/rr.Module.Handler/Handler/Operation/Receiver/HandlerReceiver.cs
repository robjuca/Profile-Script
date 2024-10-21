/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerReceiver
    public class THandlerReceiver (THandlerData handlerData) : TOperationHandlerBase (handlerData)
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerReceiver) {
                    var definitionData = DefinitionData;

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        Services.SetScriptDataValue (definitionData);
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        SetScriptDataValue (definitionData);
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateHandlerReceiver) {
                var definitionData = DefinitionData;

                // Receiver Module
                definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_NAME));
                definitionData.AddVariableValue (HandlerModuleData.ModuleVariableValue);

                SetScriptDataValue (definitionData);

                // Receiver Message
                definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_MESSAGE));
                definitionData.AddVariableValue (HandlerMessageData.MessageVariableValue);

                SetScriptDataValue (definitionData);
            }
        }
        #endregion

        #region Static
        public static THandlerReceiver Create (THandlerData data) => new (data);
        #endregion
    };
    //---------------------------//

}  // namespace
