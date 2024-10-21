/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerReceiver
    public class THandlerReceiver
    {
        #region Members
        public void Process (THandlerData handlerData) => Select (handlerData);
        #endregion

        #region Support
        void Select (THandlerData handlerData)
        {
            if (handlerData is not null) {
                if (handlerData.HandlerModuleData.Validate & handlerData.HandlerMessageData.Validate) {
                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    // Receiver Module
                    definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_NAME));
                    definitionData.AddVariableValue (handlerData.HandlerModuleData.ModuleVariableValue);


                    // Receiver Message
                    definitionData.AddVariableName (ToString (UReceiverModule.RECEIVER_MODULE_MESSAGE));
                    definitionData.AddVariableValue (handlerData.HandlerMessageData.MessageVariableValue);

                    handlerData.Services.SetScriptDataValue (definitionData);
                }
            }
        }
        #endregion

        #region Support
        string ToString (UReceiverModule name) => TEnumExtension.AsString (name);
        #endregion

        #region Static
        public static THandlerReceiver Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
