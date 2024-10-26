/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- TSpeechModel
    public class TSpeechModel : TModelBase
    {
        #region Constructor
        TSpeechModel (IProviderServices services, UHandlerModule handlerModule)
          : base (services, handlerModule)
        {
        }
        #endregion

        #region Overrides
        public override void ScriptReturnCode (TReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateBase) {
                    var definitionData = DefinitionData;

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (EnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        SetScriptDataValue (definitionData.Clone ());
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (VariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateBase & ValidateEnableNameValue) {
                var definitionData = DefinitionData;

                // text
                definitionData.AddVariableName (VariableName);
                definitionData.AddVariableValue (VariableValue);

                SetScriptDataValue (definitionData.Clone ());

                // text enable
                definitionData.AddVariableName (EnableVariableName);
                definitionData.AddVariableValue (EnableVariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        static public TSpeechModel Create (IProviderServices services, UHandlerModule handlerModule) => new (services, handlerModule);

        static public TSpeechModel Clone (TSpeechModel alias)
        {
            var handler = Create (alias.Services, alias.HandlerModule);
            handler.CopyFrom (alias);

            return handler;
        }
        #endregion
    };
    //---------------------------//

}  // namespace
