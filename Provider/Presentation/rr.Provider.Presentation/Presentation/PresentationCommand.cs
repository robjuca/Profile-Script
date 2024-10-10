/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Types;
using rr.Provider.Message;

using System;
//---------------------------//

namespace rr.Provider.Presentation
{
    public class TPresentationCommand : IDelegateCommand
    {
        #region IDelegateCommand Members
        public DelegateCommand<TMessageInternal> PublishInternalMessage
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public TPresentationCommand (IModulePresentation presentation)
        {
            if (presentation is not null) {
                PublishInternalMessage = new DelegateCommand<TMessageInternal> (presentation.PublishInternalMessageHandler);
            }
        }
        #endregion

        #region Static
        public static TPresentationCommand Create (IModulePresentation presentation) => new (presentation);
        #endregion
    };
    //---------------------------//

}  // namespace