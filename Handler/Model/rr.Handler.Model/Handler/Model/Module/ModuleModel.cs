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
        public override void Process ()
        {
            if (ValidateBase) {
                var definitionData = DefinitionData;

                // Variable Name and Value
                definitionData.AddVariableName (VariableName);
                definitionData.AddVariableValue (VariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }

        public override void Cleanup ()
        {
            var definitionData = DefinitionData;
            definitionData.AddVariableName (VariableName);
            definitionData.AddVariableValue (Resources.RES_EMPTY_MODEL_MODULE_NAME_VALUE_DEFAULT);

            SetScriptDataValue (definitionData.Clone ());
        }
        #endregion

        #region Static
        static public TModuleModel Create (IProviderServices services, UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
