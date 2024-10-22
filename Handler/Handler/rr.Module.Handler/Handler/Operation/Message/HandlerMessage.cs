/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources.Properties;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerMessage
    public class THandlerMessage () : TOperationHandlerBase ()
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerMessage) {
                }
            }
        }

        public override void Process ()
        {
            if (ValidateHandlerMessage) {
                var definitionData = DefinitionData;

                // Message
                definitionData.AddVariableName (HandlerMessageData.MessageVariableName);
                definitionData.AddVariableValue (HandlerMessageData.MessageVariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        public static THandlerMessage Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
