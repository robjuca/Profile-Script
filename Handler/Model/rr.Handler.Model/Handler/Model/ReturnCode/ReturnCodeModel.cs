/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;

using System;
//---------------------------//

namespace rr.Handler.Model
{
    //----- TReturnCodeModel
    public class TReturnCodeModel
    {
        #region Event
        public event EventHandler<TReturnCodeArgs> ReturnCode;
        #endregion

        #region Members
        public void ProcessScriptReturnCode (TScriptActionDispatcherEventArgs eventArgs) => Select (eventArgs);
        #endregion

        #region Support
        void Select (TScriptActionDispatcherEventArgs eventArgs)
        {
            TReturnCodeArgs args = TReturnCodeArgs.CreateDefault ();

            // Clear RES_SPEECH_COMMIT (-50) 
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_SPEECH_DISABLE_CODE)) {
                if (m_40Wainting is false) {
                    m_40Wainting = true;
                    m_50Waiting = false;
                    args = TReturnCodeArgs.Create (UReturnCodeId.SPEECH_DISABLE, eventArgs);
                    
                }

            }

            // Speech done (-40) 
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_SPEECH_DONE_CODE)) {

                if (m_50Waiting is false) {
                    m_50Waiting = true;
                    m_40Wainting = false;
                    args = TReturnCodeArgs.Create (UReturnCodeId.SPEECH_DONE, eventArgs);
                }


               

            }

            // Clear RES_MODULE_ID (-100) - Next Step
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_NEXT_STEP_CODE)) {
                args = TReturnCodeArgs.Create (UReturnCodeId.NEXT_STEP, eventArgs);
            }

            // Step DONE (-80) - must go to next plate
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_NEXT_PLATE_CODE)) {
                args = TReturnCodeArgs.Create (UReturnCodeId.NEXT_MODULE, eventArgs);
            }

            // Step DONE (-400) - clear Receiver variables, clear Module Message variables, clear Module variables
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_HANDLERS_CLEAR_CODE)) {
                args = TReturnCodeArgs.Create (UReturnCodeId.HANDLERS_CLEAR, eventArgs);
            }

            if (args.Validate) {
                ReturnCode?.Invoke (this, args);
            }
        }
        #endregion

        bool m_40Wainting;
        bool m_50Waiting;

        #region Static
        public static TReturnCodeModel Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
