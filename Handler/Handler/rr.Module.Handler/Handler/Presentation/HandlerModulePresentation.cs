/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;

using System;
using System.Collections.Generic;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerModulePresentation
    public class THandlerModulePresentation
    {
        #region Event
        public event EventHandler<THandlerData> NextModule;
        #endregion

        #region Constructor
        THandlerModulePresentation (UHandlerModule parentModule)
        {
            HandlerModuleAction = THandlerModuleAction.Create (parentModule);

            //THandlerModuleAction.NextModule += OnNextModule;
        }
        #endregion

        #region Members
        

        public void Ready (IList<THandlerData> data)
        {
            HandlerModuleAction.AddModuleData (data);
        }

        public void ProcessAction (TActionDispatcherEventArgs eventArgs)
        {
            HandlerModuleAction.ProcessAction (eventArgs);
        }

        //public void NextCodeId (UCodeId code, bool wait = false)
        //{
        //    HandlerModuleAction.NextCodeId (code, wait);
        //}
        #endregion

        #region Event
        void OnNextModule (object sender, THandlerData data)
        {
            NextModule?.Invoke (this, data);
        }
        #endregion

        #region Property
        THandlerModuleAction HandlerModuleAction { get; set; }
        #endregion

        #region Static
        public static THandlerModulePresentation Create (UHandlerModule parentModule) => new (parentModule);
        #endregion
    };
    //---------------------------//

}  // namespace
