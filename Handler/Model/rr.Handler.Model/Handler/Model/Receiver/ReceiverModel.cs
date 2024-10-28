﻿/*----------------------------------------------------------------
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
    //----- TReceiverModel
    public class TReceiverModel : TModelBase
    {
        #region Constructor
        TReceiverModel (IProviderServices services, UHandlerModule handlerModule)
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
                        // Receiver Module
                        definitionData.AddVariableName (ReceiverToString (UReceiverModule.RECEIVER_MODULE_NAME));
                        definitionData.AddVariableValue (Resources.RES_NONE);

                        SetScriptDataValue (definitionData.Clone ());

                        // Receiver Message
                        definitionData.AddVariableName (ReceiverToString (UReceiverModule.RECEIVER_MODULE_MESSAGE));
                        definitionData.AddVariableValue (Resources.RES_NONE);

                        SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (ValidateBase) {
                var definitionData = DefinitionData;

                // Receiver Module
                definitionData.AddVariableName (ReceiverToString (UReceiverModule.RECEIVER_MODULE_NAME));
                definitionData.AddVariableValue (VariableValue);

                SetScriptDataValue (definitionData.Clone ());

                // Receiver Message
                definitionData.AddVariableName (ReceiverToString (UReceiverModule.RECEIVER_MODULE_MESSAGE));
                definitionData.AddVariableValue (VariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        static public TReceiverModel Create (IProviderServices services, UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace