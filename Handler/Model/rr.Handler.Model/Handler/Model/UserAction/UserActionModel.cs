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
    //----- TUserActionModel
    public class TUserActionModel : TModelBase
    {
        #region Constructor
        TUserActionModel (IProviderServices services, UHandlerModule handlerModule)
          : base (services, handlerModule)
        {
        }
        #endregion

        #region Overrides
        public override void Process ()
        {
            var definitionData = DefinitionData;

            // User Action Code
            definitionData.AddVariableName (Resources.RES_USER_ACTION_CODE);
            definitionData.AddVariableValue (HasUserActionCode ? UserActionCode : Resources.RES_ZERO_STRING);

            SetScriptDataValue (definitionData.Clone ());
        }
        #endregion

        #region Static
        static public TUserActionModel Create (IProviderServices services, UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
