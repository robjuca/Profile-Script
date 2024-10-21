/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources.Properties;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerSpeech
    public class THandlerSpeech (THandlerData handlerData) : TOperationHandlerBase (handlerData)
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerSpeech) {
                    var definitionData = DefinitionData;

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        Services.SetScriptDataValue (definitionData);
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        Services.SetScriptDataValue (definitionData);
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateHandlerSpeech) {
                var definitionData = DefinitionData;

                // text
                definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                definitionData.AddVariableValue (HandlerSpeechData.SpeechTextVariableValue);

                SetScriptDataValue (definitionData);

                // text enable
                definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                definitionData.AddVariableValue (HandlerSpeechData.SpeechTextEnableVariableValue);

                SetScriptDataValue (definitionData);
            }
        }
        #endregion

        #region Static
        public static THandlerSpeech Create (THandlerData data) => new (data);
        #endregion
    };
    //---------------------------//

}  // namespace
