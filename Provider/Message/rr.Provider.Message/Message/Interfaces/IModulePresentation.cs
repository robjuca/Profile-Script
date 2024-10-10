/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Infrastructure;
using rr.Provider.Resources;
//---------------------------//

namespace rr.Provider.Message
{
    //----- IModulePresentation
    public interface IModulePresentation : IPresentation, IDefault
    {
        void PublishInternalMessageHandler (TMessageInternal message);
    };
    //---------------------------//

}  // namespace
