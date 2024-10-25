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
    public class THandlerSpeech () : TOperationHandlerBase ()
    {
        #region Overrides
        public override void ScriptReturnCode (TReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerSpeech) {
                    var definitionData = DefinitionData;

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
            if (ValidateHandlerSpeech) {
                var definitionData = DefinitionData;

                // text
                definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                definitionData.AddVariableValue (HandlerSpeechData.SpeechTextVariableValue);

                SetScriptDataValue (definitionData.Clone ());

                // text enable
                definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                definitionData.AddVariableValue (HandlerSpeechData.SpeechTextEnableVariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        public static THandlerSpeech Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
