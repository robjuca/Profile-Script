/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
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

        void CreateScriptDataValue<T> (IList <TScriptDefinitionData<T>> scriptData);
        IDataDefinition GetOrCreateScriptDataValue<T> (TScriptDefinitionData<T> definitionData);
        string GetScriptDataValue<T> (TScriptDefinitionData<T> definitionData);
        void SetScriptDataValue<T> (TScriptDefinitionData<T> definitionData);
    }
    //---------------------------//

}  // namespace
