/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerReceiver
    public class THandlerReceiver () : TOperationHandlerBase ()
    {
        #region Overrides
        public override void ScriptReturnCode (TScriptReturnCodeArgs args)
        {
            if (args is not null) {
                if (ValidateHandlerReceiver) {
                    var definitionData = DefinitionData;

                    if (args.IsHandlersClear) {
                        // Receiver Module
                        definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_NAME));
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        SetScriptDataValue (definitionData.Clone ());

                        // Receiver Message
                        definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_MESSAGE));
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateHandlerReceiver) {
                var definitionData = DefinitionData;

                // Receiver Module
                definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_NAME));
                definitionData.AddVariableValue (HandlerModuleData.ModuleVariableValue);

                SetScriptDataValue (definitionData.Clone ());

                // Receiver Message
                definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_MESSAGE));
                definitionData.AddVariableValue (HandlerMessageData.MessageVariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        public static THandlerReceiver Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
