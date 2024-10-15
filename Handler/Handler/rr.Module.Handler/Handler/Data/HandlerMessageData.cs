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
    //----- THandlerMessageData
    public class THandlerMessageData : THandlerDataBase
    {
        #region Constructor
        THandlerMessageData (IProviderServices services, UHandlerModule handlerModule)
           : base (services, handlerModule)
        {
        } 
        #endregion

        #region Static
        static public THandlerMessageData Create (
            IProviderServices services,
            UHandlerModule handlerModule) => new (services, handlerModule);
        #endregion
    };
    //---------------------------//

}  // namespace
