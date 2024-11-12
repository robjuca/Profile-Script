
/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
using rr.Provider.Services;
//---------------------------//

namespace rr.Handler.Model
{
    //----- TModelBase
    public abstract class TModelBase (IProviderServices services, UHandlerModule handlerModule, bool enableModel = true)
    {
        #region Property
        protected bool ModelEnabledFlag { get; private set; } = enableModel;
        public bool WaitingFlag { get; private set; }
        public bool NextMessageFlag { get; private set; }
        public string UserActionCode { get; private set; }
        protected UHandlerModule HandlerModule { get; private set; } = handlerModule;
        protected IProviderServices Services { get; private set; } = services;
        protected bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
        protected string ModuleName => HasModule ? TEnumExtension.AsString (HandlerModule) : string.Empty;
        #endregion

        #region Property
        // Variable Name/Value
        public string VariableName { get; private set; }
        public string VariableValue { get; private set; }

        // Enable Variable Name/Value
        protected string EnableVariableName { get; private set; }
        protected string EnableVariableValue { get; private set; }

        // Validate
        protected bool ValidateBase => ModelEnabledFlag & HasModule & ValidateNameValue;
        protected bool HasUserActionCode => !string.IsNullOrEmpty (UserActionCode) | !string.IsNullOrWhiteSpace (UserActionCode);

        // Validate Text
        protected bool ValidateNameValue
        {
            get
            {
                bool res = false;

                if (string.IsNullOrEmpty (VariableName) is false
                    &
                    string.IsNullOrEmpty (VariableValue) is false) {
                    res = true;
                }

                return res;
            }
        }

        // Validate Text Enable
        protected bool ValidateEnableNameValue
        {
            get
            {
                bool res = false;

                if (string.IsNullOrEmpty (EnableVariableName) is false
                    &
                    string.IsNullOrEmpty (EnableVariableValue) is false) {
                    res = true;
                }

                return res;
            }
        }
        #endregion

        #region Members
        // Variable Name/Value
        public void AddVariableName (string variableName) => VariableName = variableName;
        public void AddVariableValue (string variableValue) => VariableValue = variableValue;
        public bool ContainsNameValue (string nameValue) => VariableValue.Equals (nameValue);

        // Variable Name/Value Enable
        public void AddEnableVariableName (string enableVariableName) => EnableVariableName = enableVariableName;
        public void AddEnableVariableValue (string enableVariableValue) => EnableVariableValue = enableVariableValue;

        // User Action Code
        public void AddUserActionCode (string userCode) => UserActionCode = userCode;
        public void RemoveUserActionCode () => UserActionCode = Resources.RES_ZERO_STRING;

        public void CopyFrom (TModelBase alias)
        {
            if (alias is not null) {
                AddVariableName (alias.VariableName);
                AddVariableValue (alias.VariableValue);
                AddEnableVariableName (alias.EnableVariableName);
                AddEnableVariableValue (alias.EnableVariableValue);

                CopyEnableModelFlag (alias.ModelEnabledFlag);
                CopyWaitingFlag (alias.WaitingFlag);
                CopyNextMessageFlag (alias.NextMessageFlag);

                AddUserActionCode (alias.UserActionCode);
            }
        }
        #endregion

        #region Members
        public void EnableModelFlag () => ModelEnabledFlag = true;
        public void DisableEnableFlag () => ModelEnabledFlag = false;
        public void CopyEnableModelFlag (bool enable) => ModelEnabledFlag = enable;
        public void ClearWaitingFlag () => WaitingFlag = false;
        public void EnableWaitingFlag () => WaitingFlag = true;
        public void CopyWaitingFlag (bool waiting) => WaitingFlag = waiting;
        public void EnableNextMessageFlag () => NextMessageFlag = true;
        public void ClearNextMessageFlag () => NextMessageFlag = false;
        public void CopyNextMessageFlag (bool flag) => NextMessageFlag = flag;

        protected TScriptDefinitionData DefinitionData => TScriptDefinitionData.CreateDefault ();
        #endregion

        #region Virtual
        public virtual void Process () { }
        public virtual void ScriptReturnCode (TReturnCodeArgs args) { }
        #endregion

        #region Support
        protected void SetScriptDataValue (TScriptDefinitionData definitionData) => Services.SetScriptDataValue (definitionData);
        protected string ReceiverToString (UReceiverModule name) => TEnumExtension.AsString (name);
        #endregion
    };
    //---------------------------//

}  // namespace
