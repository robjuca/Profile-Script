/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;

using System;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerAction
    public class THandlerAction
    {
        #region Event
        public event EventHandler<THandlerActionArgs> ActionReturnCodeDispatcher;
        #endregion

        #region Members
        public void ProcessAction (TActionDispatcherEventArgs eventArgs) => Select (eventArgs);
        #endregion

        #region Support
        void Select (TActionDispatcherEventArgs eventArgs)
        {
            THandlerActionArgs args = THandlerActionArgs.CreateDefault ();

            // Clear RES_SPEECH_COMMIT (-50) 
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_SPEECH_DISABLE_CODE)) {
                args = THandlerActionArgs.Create (UReturnCodeId.SPEECH_DISABLE, eventArgs);
            }

            // Speech done (-40) 
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_SPEECH_DONE_CODE)) {
                args = THandlerActionArgs.Create (UReturnCodeId.SPEECH_DONE, eventArgs);
            }

            // Clear RES_MODULE_ID (-100) - Next Step
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_NEXT_STEP_CODE)) {
                args = THandlerActionArgs.Create (UReturnCodeId.NEXT_STEP, eventArgs);
            }

            // Step DONE (-80) - must go to next plate
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_NEXT_PLATE_CODE)) {
                args = THandlerActionArgs.Create (UReturnCodeId.NEXT_MODULE, eventArgs);
            }

            if (args.IsEmpty is false) {
                ActionReturnCodeDispatcher?.Invoke (this, args);
            }
        }
        #endregion

        #region Static
        public static THandlerAction Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
