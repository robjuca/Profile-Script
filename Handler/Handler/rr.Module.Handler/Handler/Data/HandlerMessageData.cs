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
    //----- THandlerMessageData
    public class THandlerMessageData : THandlerDataBase
    {
        #region Property
        // Variable Message Name
        public string MessageVariableName { get; private set; }

        // Variable Message Value
        public string MessageVariableValue { get; private set; }

        // Validate
        public bool Validate => HasModule & ValidateMessageNameAndValue;

        // Validate Message Name and Value
        public bool ValidateMessageNameAndValue
        {
            get
            {
                bool res = false;

                if (string.IsNullOrEmpty (MessageVariableName) is false
                    &
                    string.IsNullOrEmpty (MessageVariableValue) is false) {
                    res = true;
                }

                return res;
            }
        }
        #endregion

        #region Constructor
        THandlerMessageData (IProviderServices services, UHandlerModule handlerModule)
           : base (services, handlerModule)
        {
        }
        #endregion

        #region Members
        // Message
        public void AddMessageVariableName (string variableName) => MessageVariableName = variableName;
        public void AddMessageVariableValue (string variableValue) => MessageVariableValue = variableValue;
        #endregion

        #region Static
        static public THandlerMessageData Create (
            IProviderServices services,
            UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
