
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
    //----- TModelBase
    public abstract class TModelBase (IProviderServices services, UHandlerModule handlerModule, bool enableHandler = true)
    {
        #region Property
        protected bool IsEnable { get; private set; } = enableHandler;
        protected UHandlerModule HandlerModule { get; private set; } = handlerModule;
        protected IProviderServices Services { get; private set; } = services;
        protected bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
        #endregion

        #region Property
        // Text
        protected string VariableName { get; private set; }
        protected string VariableValue { get; private set; }

        // Text Enable
        protected string EnableVariableName { get; private set; }
        protected string EnableVariableValue { get; private set; }

        // Validate
        protected bool Validate => IsEnable & HasModule & ValidateNameValue & ValidateEnableNameValue;

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
        // Text
        protected void AddVariableName (string variableName) => VariableName = variableName;
        protected void AddVariableValue (string variableValue) => VariableValue = variableValue;
        protected bool ContainsNameValue (string nameValue) => VariableValue.Equals (nameValue);

        // Text Enable
        protected void AddEnableVariableName (string enableVariableName) => EnableVariableName = enableVariableName;
        protected void AddEnableVariableValue (string enableVariableValue) => EnableVariableValue = enableVariableValue;

        protected void CopyFrom (TModelBase alias)
        {
            if (alias is not null) {
                AddVariableName (alias.VariableName);
                AddVariableValue (alias.VariableValue);
                AddEnableVariableName (alias.EnableVariableName);
                AddEnableVariableValue (alias.EnableVariableValue);

                EnableHandler (alias.IsEnable);
            }
        }
        #endregion

        #region Members
        protected void EnableHandler (bool enable = true) => IsEnable = enable;

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
