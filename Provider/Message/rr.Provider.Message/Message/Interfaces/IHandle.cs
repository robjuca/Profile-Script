/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
//---------------------------//

namespace rr.Provider.Message
{
    //----- IHandleMessageModule
    public interface IHandleMessageModule : IHandle<TMessageInternal>
    {
    };
    //---------------------------//
}