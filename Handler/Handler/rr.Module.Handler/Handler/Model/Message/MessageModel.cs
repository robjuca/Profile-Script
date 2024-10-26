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
    //----- TMessageModel
    public class TMessageModel : TModelBase
    {
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
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateBase) {
                var definitionData = DefinitionData;

                // Message
                definitionData.AddVariableName (VariableName);
                definitionData.AddVariableValue (VariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
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
