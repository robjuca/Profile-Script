/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Configuration;

using System;
using System.Collections.Generic;
using System.Reflection;
//---------------------------//

namespace rr.Provider.Services
{
    //----- IProviderServices
    public interface IProviderServices
    {
        Array AllHandlerModule { get; }

        void DiscoverModules (IList<Assembly> modules);
        void ConfigureModules (IEnumerable<object> modules);

        void CreateScriptDataValue (IList <TScriptDefinitionData> scriptData);
        IDataDefinition GetOrCreateScriptDataValue (TScriptDefinitionData definitionData);
        string GetScriptDataValue (TScriptDefinitionData definitionData, string defaultValue = default);
        void SetScriptDataValue (TScriptDefinitionData definitionData);

        SimulationGamestate RequestGameState (string state = default);
    }
    //---------------------------//

}  // namespace
