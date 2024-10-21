/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources.Properties;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerModule
    public class THandlerModule (THandlerData handlerData) : TOperationHandlerBase (handlerData)
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null ) {
                if (ValidateHandlerModule) {
                    var definitionData = DefinitionData;

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextEnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        HandlerSpeechData.Services.SetScriptDataValue (definitionData.Clone ());
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (HandlerSpeechData.SpeechTextVariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        HandlerSpeechData.Services.SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateHandlerModule) {
                var definitionData = DefinitionData;

                // Module
                definitionData.AddVariableName (HandlerModuleData.ModuleVariableName);
                definitionData.AddVariableValue (HandlerModuleData.ModuleVariableValue);

                SetScriptDataValue (definitionData);
            }
        }
        #endregion

        #region Static
        public static THandlerModule Create (THandlerData data) => new (data);
        #endregion
    };
    //---------------------------//

}  // namespace
