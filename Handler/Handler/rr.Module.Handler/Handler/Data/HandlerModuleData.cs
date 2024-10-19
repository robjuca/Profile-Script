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
    //----- THandlerModuleData
    public class THandlerModuleData : THandlerDataBase
    {
        #region Property
        // Variable Module Name
        public string ModuleVariableName { get; private set; }

        // Variable Module Value
        public string ModuleVariableValue { get; private set; }

        // Validate
        public bool Validate => IsEnable & HasModule & ValidateModuleNameAndValue;

        // Validate Module Name and Value
        public bool ValidateModuleNameAndValue
        {
            get
            {
                bool res = false;

                if (string.IsNullOrEmpty (ModuleVariableName) is false
                    &
                    string.IsNullOrEmpty (ModuleVariableValue) is false) {
                    res = true;
                }

                return res;
            }
        }
        #endregion

        #region Constructor
        THandlerModuleData (IProviderServices services, UHandlerModule handlerModule)
          : base (services, handlerModule)
        {
        }
        #endregion

        #region Members
        // Module
        public void AddModuleVariableName (string variableName) => ModuleVariableName = variableName;
        public void AddModuleVariableValue (string variableValue) => ModuleVariableValue = variableValue;

        public void CopyFrom (THandlerModuleData alias)
        {
            if (alias is not null) {
                AddModuleVariableName (alias.ModuleVariableName);
                AddModuleVariableValue (alias.ModuleVariableValue);

                EnableHandler (alias.IsEnable);
            }
        }
        #endregion

        #region Static
        static public THandlerModuleData Create (
            IProviderServices services,
            UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
