/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerMessage
    public class THandlerMessage
    {
        #region Members
        public void Process (THandlerMessageData handlerData) => Select (handlerData);
        #endregion

        #region Support
        void Select (THandlerMessageData handlerData)
        {
            if (handlerData is not null) {
                if (handlerData.Validate) {
                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    // Message
                    definitionData.AddVariableName (handlerData.MessageVariableName);
                    definitionData.AddVariableValue (handlerData.MessageVariableValue);

                    handlerData.Services.SetScriptDataValue (definitionData);
                }
            }
        }
        #endregion

        #region Static
        public static THandlerMessage Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
