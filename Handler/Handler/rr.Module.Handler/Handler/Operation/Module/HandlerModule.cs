﻿/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerModule
    public class THandlerModule
    {
        #region Members
        public void Process (THandlerModuleData handlerData) => Select (handlerData);
        #endregion

        #region Support
        void Select (THandlerModuleData handlerData)
        {
            if (handlerData is not null) {
                if (handlerData.Validate) {
                    var definitionData = TScriptDefinitionData.CreateDefault ();

                    // Module
                    definitionData.AddVariableName (handlerData.ModuleVariableName);
                    definitionData.AddVariableValue (handlerData.ModuleVariableValue);

                    handlerData.Services.SetScriptDataValue (definitionData);
                }
            }
        }
        #endregion

        #region Static
        public static THandlerModule Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
