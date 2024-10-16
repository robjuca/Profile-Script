/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerSpeech
    public class THandlerSpeech
    {
        #region Members
        public void Process (THandlerSpeechData handlerData) => Select (handlerData);

        //public void ActionReturnCode (THandlerSpeechData data)
        //{
        //    //if (handlerData is not null) {
        //    //    if (handlerData.CleanupEnable) {
        //    //        CleanupEnable (handlerData);
        //    //    }

        //    //    if (handlerData.CleanupSpeech) {
        //    //        CleanupSpeech (handlerData);
        //    //    }
        //    //}
        //}
        #endregion

        #region Support
        void Select (THandlerSpeechData handlerData)
        {
            if (handlerData is not null) {
                if (handlerData.Validate) {
                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    // text
                    definitionData.AddVariableName (handlerData.SpeechTextVariableName);
                    definitionData.AddVariableValue (handlerData.SpeechTextVariableValue);

                    handlerData.Services.SetScriptDataValue (definitionData);

                    // text enable
                    definitionData.AddVariableName (handlerData.SpeechTextEnableVariableName);
                    definitionData.AddVariableValue (handlerData.SpeechTextEnableVariableValue);

                    handlerData.Services.SetScriptDataValue (definitionData);
                }
            }
        }

        //void CleanupSpeech (THandlerSpeechData handlerData) => EventSystem.CreateNewLocal (
        //        handlerData.SpeechText,
        //        "String",
        //        VARIABLE_SCOPE.SESSION,
        //        string.Empty);

        //void CleanupEnable (THandlerSpeechData handlerData) => EventSystem.CreateNewLocal (
        //        handlerData.SpeechResourceEnable,
        //        "Int",
        //        VARIABLE_SCOPE.SESSION,
        //        0);
        #endregion

        #region Static
        public static THandlerSpeech Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
