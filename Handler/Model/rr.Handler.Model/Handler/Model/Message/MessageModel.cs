/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
using rr.Provider.Services;
//---------------------------//

namespace rr.Handler.Model
{
    //----- TMessageModel
    public class TMessageModel : TModelBase
    {
        #region Property
        public string MessageName => ValidateBase ? VariableValue : string.Empty;
        #endregion

        #region Constructor
        TMessageModel (IProviderServices services, UHandlerModule handlerModule)
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

                    if (args.IsHandlersClear) {
                        // Message
                        definitionData.AddVariableName (VariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY_MODEL_MESSAGE_NAME_VALUE_DEFAULT);

                        SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateBase) {
                // only new values
                if (Services.ContainsDataValue (VariableName, VariableValue) is false) {
                    var definitionData = DefinitionData;

                    definitionData.AddVariableName (VariableName);
                    definitionData.AddVariableValue (VariableValue);

                    SetScriptDataValue (definitionData.Clone ());
                }
            }
        }

        public override void Cleanup ()
        {
            var definitionData = DefinitionData;
            definitionData.AddVariableName (VariableName);
            definitionData.AddVariableValue (Resources.RES_EMPTY_MODEL_MESSAGE_NAME_VALUE_DEFAULT);

            SetScriptDataValue (definitionData.Clone ());
        }
        #endregion

        #region Static
        static public TMessageModel Create (
            IProviderServices services,
            UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
