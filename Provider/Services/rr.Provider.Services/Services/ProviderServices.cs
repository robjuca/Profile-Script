/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;
using rr.Provider.Resources;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Configuration;
using SPAD.neXt.Interfaces.Events;

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
//---------------------------//

namespace rr.Provider.Services
{
    //----- TProviderServices
    [Export (typeof (IProviderServices))]
    [method: ImportingConstructor]
    public class TProviderServices () : IProviderServices
    {
        #region Interface
        #region Property
        public Array AllHandlerModule => HandlerModuleList.ToArray ();
        #endregion

        #region Members
        public void DiscoverModules (IList<Assembly> modules)
        {
            if (modules is not null) {
                modules.Clear ();

                var dir = new DirectoryInfo (@"D:\SPAD.neXt\AddOns");

                if (dir.Exists) {
                    foreach (FileInfo fi in dir.GetFiles ()) {
                        if (fi.Extension.Equals (".dll")) {
                            if (fi.FullName.Contains ("rr.S") | fi.FullName.Contains ("rr.P")) {
                                try {
                                    Assembly assembly = Assembly.LoadFile (fi.FullName);

                                    if (assembly.GetType ("rr.Module.Catalog.TModuleCatalog") is not null) {
                                        modules.Add (assembly);
                                    }
                                }

                                catch (Exception) {
                                    // do nothing 
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ConfigureModules (IEnumerable<object> modules)
        {
            HandlerModuleList.Clear ();

            foreach (var item in modules) {
                if (item is IModule) {
                    HandlerModuleList.Add ((item as IModule).HandlerModule);
                }
            }
        }

        public void CreateScriptDataValue (IList<TScriptDefinitionData> scriptDataList)
        {
            if (scriptDataList is not null) {
                foreach (var variableData in scriptDataList) {
                    SetScriptDataValue (variableData);
                }
            }
        }

        public IDataDefinition GetOrCreateScriptDataValue (TScriptDefinitionData definitionData)
        {
            if (DefinitionDataList.TryGetValue (definitionData.Key, out var data)) {
                return data.DataDefinition;
            }

            definitionData.AddDataDefinition (EventSystem.GetDataDefinition (RealName (definitionData.VariableName)));

            if (definitionData.UseLocalOnly) {
                definitionData.AddDataDefinition (EventSystem.GetDataDefinition ("LOCAL:" + definitionData.VariableName));
            }

            DefinitionDataList.Add (definitionData.Key, definitionData.Clone ());

            return definitionData.DataDefinition;
        }

        public string GetScriptDataValue (TScriptDefinitionData definitionData, string defaultValue = default)
        {
            var data = GetOrCreateScriptDataValue (definitionData);
            var obj = data.GetRawValue ();

            return obj is null ? string.Empty : obj.ToString ();
        }

        public void SetScriptDataValue (TScriptDefinitionData definitionData)
        {
            var data = GetOrCreateScriptDataValue (definitionData);

            if (data is not null) {
                var obj = data.GetRawValue ();

                if (obj is not null) {
                    if (obj.ToString ().Equals (definitionData.VariableValue) is false) {
                        data.SetRawValue (definitionData.VariableValue);
                    }
                }
            }
        }

        public SimulationGamestate RequestGameState (string state = default)
        {
            var definitionData = TScriptDefinitionData.Create ("GAMESTATE", useLocal: true);

            if (string.IsNullOrEmpty (state)) {
                var variableValue = GetScriptDataValue (definitionData);
                definitionData.AddVariableValue (variableValue);

                return definitionData.GameState;
            }

            return TEnumExtension.ToEnum<SimulationGamestate> (state);
        }
        #endregion
        #endregion

        #region Property
        List<UHandlerModule> HandlerModuleList { get; set; } = [];
        Dictionary<string, TScriptDefinitionData> DefinitionDataList { get; set; } = [];
        #endregion

        #region Support
        string RealName (string name) => "LOCAL:RR_" + name;
        #endregion
    };
    //---------------------------//

}  // namespace
