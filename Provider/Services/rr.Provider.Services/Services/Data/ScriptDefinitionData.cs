
/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using SPAD.neXt.Interfaces.Configuration;

using System;
using System.Reflection;
//---------------------------//

namespace rr.Provider.Services
{
    //----- TScriptDefinitionData<T>
    public class TScriptDefinitionData<T>
    {
        #region Property
        public string Key => VariableName;
        public T VariableScriptEnum { get; private set; }
        public string VariableName => Enum.GetName (typeof (T), VariableScriptEnum);
        public string VariableRealName => "LOCAL:RR_" + VariableName;
        public string VariableValue { get; private set; }
        public IDataDefinition DataDefinition { get; private set; }
        public bool IsEmpty => string.IsNullOrEmpty (VariableRealName);
        #endregion

        #region Members
        public void AddVariableScriptEnum (T variableScriptEnum) => VariableScriptEnum = variableScriptEnum;
        public void AddVariableValue (string variableValue) => VariableValue = variableValue;
        public void AddDataDefinition (IDataDefinition dataDefinition) => DataDefinition = dataDefinition;
        public TScriptDefinitionData<Enum> Clone () => TScriptDefinitionData<Enum>.Create (VariableScriptEnum, typeof(T));

        #endregion

        #region Static
        static public TScriptDefinitionData<T> CreateDefault () => new () { VariableValue = default };

        static public TScriptDefinitionData<Enum> Create (T enumValue, Type info)
        {



            var clone = new TScriptDefinitionData<Enum> ();

            clone.AddVariableScriptEnum ((info)enumValue);
            clone.AddVariableValue (alias.VariableValue);   
            clone.AddDataDefinition (alias.DataDefinition);

            return clone;
        }
        #endregion
    };
    //---------------------------//

}  // namespace
