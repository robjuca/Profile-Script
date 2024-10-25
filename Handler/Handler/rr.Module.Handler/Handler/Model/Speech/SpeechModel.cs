﻿/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- TSpeechModel
    public class TSpeechModel : TModelBase
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
        TSpeechModel (IProviderServices services, UHandlerModule handlerModule)
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

        public void CopyFrom (TSpeechModel alias)
        {
            if (alias is not null) {
                AddSpeechTextVariableName (alias.SpeechTextVariableName);
                AddSpeechTextVariableValue (alias.SpeechTextVariableValue);
                AddSpeechTextEnableVariableName (alias.SpeechTextEnableVariableName);
                AddSpeechTextEnableVariableValue (alias.SpeechTextEnableVariableValue);

                EnableHandler (alias.IsEnable);
            }
        }
        #endregion

        #region Overrides
        public override void ScriptReturnCode (TReturnCodeArgs args)
        {
            if (args is not null) {
                if (Validate) {
                    var definitionData = DefinitionData;

                    if (args.IsSpeechDisable) {
                        definitionData.AddVariableName (SpeechTextEnableVariableName);
                        definitionData.AddVariableValue (Resources.RES_FALSE);

                        Services.SetScriptDataValue (definitionData.Clone ());
                    }

                    if (args.IsSpeechDone) {
                        definitionData.AddVariableName (SpeechTextVariableName);
                        definitionData.AddVariableValue (Resources.RES_EMPTY);

                        Services.SetScriptDataValue (definitionData.Clone ());
                    }
                }
            }
        }

        public override void Process ()
        {
            if (Validate) {
                var definitionData = DefinitionData;

                // text
                definitionData.AddVariableName (SpeechTextVariableName);
                definitionData.AddVariableValue (SpeechTextVariableValue);

                SetScriptDataValue (definitionData.Clone ());

                // text enable
                definitionData.AddVariableName (SpeechTextEnableVariableName);
                definitionData.AddVariableValue (SpeechTextEnableVariableValue);

                SetScriptDataValue (definitionData.Clone ());
            }
        }
        #endregion

        #region Static
        static public TSpeechModel Create (
           IProviderServices services,
           UHandlerModule handlerModule) => new (services, handlerModule);

        static public TSpeechModel Clone (TSpeechModel alias)
        {
            var handler = Create (alias.Services, alias.HandlerModule);
            handler.CopyFrom (alias);

            return handler;
        }
        #endregion
    };
    //---------------------------//

}  // namespace
