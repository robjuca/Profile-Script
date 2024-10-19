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
    //----- THandlerSpeech
    public class THandlerSpeech
    {
        #region Members
        public void Process (THandlerSpeechData handlerData) => Select (handlerData);

        public void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null & HandlerSpeechData is not null) {
                if (HandlerSpeechData.Validate) {
                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        HandlerSpeechData.Services.SetScriptDataValue (definitionData);
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        HandlerSpeechData.Services.SetScriptDataValue (definitionData);
                    }
                }
            }
        }
        #endregion

        #region Property
        THandlerSpeechData HandlerSpeechData { get; set; }
        #endregion

        #region Support
        void Select (THandlerSpeechData data)
        {
            if (data is not null) {
                if (data.Validate) {
                    HandlerSpeechData = THandlerSpeechData.Clone (data);

                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    // text
                    definitionData.AddVariableName (data.SpeechTextVariableName);
                    definitionData.AddVariableValue (data.SpeechTextVariableValue);

                    data.Services.SetScriptDataValue (definitionData);

                    // text enable
                    definitionData.AddVariableName (data.SpeechTextEnableVariableName);
                    definitionData.AddVariableValue (data.SpeechTextEnableVariableValue);

                    data.Services.SetScriptDataValue (definitionData);
                }
            }
        }
        #endregion

        #region Static
        public static THandlerSpeech Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
