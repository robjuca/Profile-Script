/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Handler.Model
{
    //----- TModuleModel
    public class TModuleModel : TModelBase
    {
        #region Constructor
        TModuleModel (IProviderServices services, UHandlerModule handlerModule)
          : base (services, handlerModule)
        {
        }
        #endregion

        #region Overrides
        public override void ScriptReturnCode (TReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateBase) {
                    // do nothing
                }
            }
        }

        public override void Process ()
        {
            if (ValidateBase & ValidateEnableNameValue) {
                var definitionData = DefinitionData;

                // Variable Name and Value
                definitionData.AddVariableName (VariableName);
                definitionData.AddVariableValue (VariableValue);

                SetScriptDataValue (definitionData.Clone ());

                // Enable Variable Name and Value
                definitionData.AddVariableName (EnableVariableName);
                definitionData.AddVariableValue (EnableVariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        static public TModuleModel Create (IProviderServices services, UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
