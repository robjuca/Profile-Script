
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
    public class THandlerDataBase (IProviderServices services, UHandlerModule handlerModule)
    {
        #region Property
        public UHandlerModule HandlerModule { get; private set; } = handlerModule;
        public bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
        public IProviderServices Services { get; private set; } = services;
        #endregion
    };
    //---------------------------//

}  // namespace
