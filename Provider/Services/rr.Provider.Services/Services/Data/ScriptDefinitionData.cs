
/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using SPAD.neXt.Interfaces.Configuration;
//---------------------------//

namespace rr.Provider.Services
{
    //----- TScriptDefinitionData
    public class TScriptDefinitionData
    {
        #region Property
        public string Key => VariableName;
        public string VariableName { get; private set; }
        public string VariableRealName => "LOCAL:RR_" + VariableName;
        public string VariableValue { get; private set; }
        public IDataDefinition DataDefinition { get; private set; }
        public bool IsEmpty => string.IsNullOrEmpty (VariableName);
        #endregion

        #region Members
        public void AddVariableName (string variableName) => VariableName = variableName;
        public void AddVariableValue (string variableValue = default) => VariableValue = variableValue;
        public void AddDataDefinition (IDataDefinition dataDefinition) => DataDefinition = dataDefinition;
        #endregion

        #region Static
        static public TScriptDefinitionData CreateDefault () => new () { VariableName = string.Empty, VariableValue = string.Empty };
        #endregion
    };
    //---------------------------//

}  // namespace
