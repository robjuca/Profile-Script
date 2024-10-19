
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
    //----- THandlerDataBase
    public abstract class THandlerDataBase (IProviderServices services, UHandlerModule handlerModule, bool enableHandler = true)
    {
        #region Property
        public bool IsEnable { get; private set; } = enableHandler;
        public UHandlerModule HandlerModule { get; private set; } = handlerModule;
        public IProviderServices Services { get; private set; } = services;
        public bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
        #endregion

        #region Members
        public void EnableHandler (bool enable = true) => IsEnable = enable; 
        #endregion
    };
    //---------------------------//

}  // namespace
