/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Infrastructure;
using rr.Library.Types;
using rr.Provider.Message;
//---------------------------//

namespace rr.Provider.Presentation
{
    public interface IDelegateCommand : IPresentationCommand
    {
        DelegateCommand<TMessageInternal> PublishInternalMessage
        {
            get;
        }
    };
    //---------------------------//

}  // namespace
