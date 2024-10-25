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
    public class THandlerModule () : TOperationHandlerBase ()
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerModule) {
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

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        public static THandlerModule Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
