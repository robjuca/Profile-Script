/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Library.Infrastructure;

using System.Threading;
using System.Threading.Tasks;
//---------------------------//

namespace rr.Provider.Message
{
    //----- THandleMessage
    public class THandleMessage (IEventAggregator eventAggregator) : TPresentation (eventAggregator), IHandleMessageModule
    {
        #region Interface
        public async Task HandleAsync (TMessageInternal message, CancellationToken cancellationToken)
        {
            await MessageHandle (message);
        }
        #endregion

        #region Members
        protected async Task<bool> MessageHandle (TMessageInternal message)
        {
            if (message is not null) {
                Handle (message);
            }

            await Task.CompletedTask;

            return (true);
        }
        #endregion

        #region Overrides
        public virtual void Handle (TMessageInternal message)
        {
        }
        #endregion
    };
    //---------------------------//

}  // namespace
