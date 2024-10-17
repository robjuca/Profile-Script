
/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;

using SPAD.neXt.Interfaces;
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
        public string VariableRealName => (UseLocalOnly ? "Local:" : "LOCAL:RR_") + VariableName;
        public string VariableValue { get; private set; }
        public IDataDefinition DataDefinition { get; private set; }
        public bool IsEmpty => string.IsNullOrEmpty (VariableName);
        public bool UseLocalOnly { get; private set; }
        public SimulationGamestate GameState => TEnumExtension.ToEnum<SimulationGamestate> (VariableValue);
        #endregion

        #region Members
        public void AddVariableName (string variableName, bool useLocal = false)
        {
            VariableName = variableName;
            UseLocalOnly = useLocal;
        }

        public void AddVariableValue (string variableValue = default) => VariableValue = variableValue;
        public void AddDataDefinition (IDataDefinition dataDefinition) => DataDefinition = dataDefinition;

        public TScriptDefinitionData Clone ()
        {
            var clone = Create (VariableName, UseLocalOnly);
            clone.AddVariableValue (VariableValue);
            clone.AddDataDefinition (DataDefinition);

            return clone;
        }
        #endregion

        #region Static
        static public TScriptDefinitionData CreateDefault () => new () { VariableName = string.Empty, VariableValue = string.Empty };

        static public TScriptDefinitionData Create (
            string variableName,
            string variableValue) => new () { VariableName = variableName, VariableValue = variableValue };

        static public TScriptDefinitionData Create (
            string variableName,
            bool useLocal = false) => new () { VariableName = variableName, UseLocalOnly = useLocal, VariableValue = string.Empty };
        #endregion
    };
    //---------------------------//

}  // namespace
