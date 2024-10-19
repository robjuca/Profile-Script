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
    //----- THandlerSpeechData
    public class THandlerSpeechData : THandlerDataBase
    {
        #region Property
        // Text
        public string SpeechTextVariableName { get; private set; }
        public string SpeechTextVariableValue { get; private set; }

        // Text Enable
        public string SpeechTextEnableVariableName { get; private set; }
        public string SpeechTextEnableVariableValue { get; private set; }

        // Validate
        public bool Validate => IsEnable & HasModule & ValidateText & ValidateTextEnable;

        // Validate Text
        public bool ValidateText
        {
            get
            {
                bool res = false;

                if (string.IsNullOrEmpty (SpeechTextVariableName) is false
                    &
                    string.IsNullOrEmpty (SpeechTextVariableValue) is false) {
                    res = true;
                }

                return res;
            }
        }

        // Validate Text Enable
        public bool ValidateTextEnable
        {
            get
            {
                bool res = false;

                if (string.IsNullOrEmpty (SpeechTextEnableVariableName) is false
                    &
                    string.IsNullOrEmpty (SpeechTextEnableVariableValue) is false) {
                    res = true;
                }

                return res;
            }
        }
        #endregion

        #region Constructor
        THandlerSpeechData (IProviderServices services, UHandlerModule handlerModule)
          : base (services, handlerModule)
        {
        }
        #endregion

        #region Members
        // Text
        public void AddSpeechTextVariableName (string textVariableName) => SpeechTextVariableName = textVariableName;
        public void AddSpeechTextVariableValue (string textVariableValue) => SpeechTextVariableValue = textVariableValue;

        // Text Enable
        public void AddSpeechTextEnableVariableName (string textEnableVariableName) => SpeechTextEnableVariableName = textEnableVariableName;
        public void AddSpeechTextEnableVariableValue (string textEnableVariableValue) => SpeechTextEnableVariableValue = textEnableVariableValue;

        public void CopyFrom (THandlerSpeechData alias)
        {
            if (alias is not null) {
                AddSpeechTextVariableName (alias.SpeechTextVariableName);
                AddSpeechTextVariableValue (alias.SpeechTextVariableValue);
                AddSpeechTextEnableVariableName (alias.SpeechTextEnableVariableName);
                AddSpeechTextEnableVariableValue (alias.SpeechTextEnableVariableValue);
            }
        }
        #endregion

        #region Static
        static public THandlerSpeechData Create (
           IProviderServices services,
           UHandlerModule handlerModule) => new (services, handlerModule);

        static public THandlerSpeechData Clone (THandlerSpeechData alias)
        {
            var handler = Create (alias.Services, alias.HandlerModule);
            handler.CopyFrom (alias);

            return handler;
        }
        #endregion
    };
    //---------------------------//

}  // namespace
