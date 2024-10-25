
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
    //----- TModelBase
    public abstract class TModelBase (IProviderServices services, UHandlerModule handlerModule, bool enableHandler = true)
    {
        #region Property
        protected bool IsEnable { get; private set; } = enableHandler;
        protected UHandlerModule HandlerModule { get; private set; } = handlerModule;
        protected IProviderServices Services { get; private set; } = services;
        protected bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
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
        #endregion
    };
    //---------------------------//

}  // namespace
