/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources.Properties;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerMessage
    public class THandlerMessage (THandlerData handlerData) : TOperationHandlerBase (handlerData)
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerMessage) {
                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        Services.SetScriptDataValue (definitionData.Clone ());
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        Services.SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateHandlerMessage) {
                var definitionData = DefinitionData;

                // Message
                definitionData.AddVariableName (HandlerMessageData.MessageVariableName);
                definitionData.AddVariableValue (HandlerMessageData.MessageVariableValue);

                SetScriptDataValue (definitionData);
            }
        }
        #endregion

        #region Static
        public static THandlerMessage Create (THandlerData data) => new (data);
        #endregion
    };
    //---------------------------//

}  // namespace
