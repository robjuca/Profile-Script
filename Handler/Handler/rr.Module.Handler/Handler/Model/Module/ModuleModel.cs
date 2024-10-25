/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
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
        //public override void ScriptReturnCode (TReturnCodeArgs args)
        //{
        //    if (args is not null) {
        //        if (ValidateHandlerModule) {
        //        }
        //    }
        //}

        //public override void Process ()
        //{
        //    if (ValidateHandlerModule) {
        //        var definitionData = DefinitionData;

        //        // Module
        //        definitionData.AddVariableName (HandlerModuleData.ModuleVariableName);
        //        definitionData.AddVariableValue (HandlerModuleData.ModuleVariableValue);

        //        SetScriptDataValue (definitionData.Clone ());
        //    }
        }
        #endregion

        #region Static
        static public TModuleModel Create (
            IProviderServices services,
            UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
