/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerMessageData
    public class THandlerMessageData
    {
        #region Property
        public UHandlerModule HandlerModule { get; private set; }
        //public bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
        #endregion

        #region Static
        static public THandlerMessageData Create (UHandlerModule HandlerModule) => new () { HandlerModule = HandlerModule };
        #endregion
    };
    //---------------------------//

}  // namespace
