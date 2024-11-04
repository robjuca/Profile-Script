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
                args = TReturnCodeArgs.Create (UReturnCodeId.SPEECH_DISABLE, eventArgs);
            }

            // Speech done (-40) 
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_SPEECH_DONE_CODE)) {
                args = TReturnCodeArgs.Create (UReturnCodeId.SPEECH_DONE, eventArgs);
            }

            // Model DONE (-80) - must go to next model
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_NEXT_MODEL_CODE)) {
                args = TReturnCodeArgs.Create (UReturnCodeId.NEXT_MODEL, eventArgs);
            }

            //  DONE (-400) - clear Receiver variables, clear Module Message variables, clear Module variables
            if (eventArgs.ActionReturnCode.ToString ().Equals (Resources.RES_HANDLERS_CLEAR_CODE)) {
                args = TReturnCodeArgs.Create (UReturnCodeId.HANDLERS_CLEAR, eventArgs);
            }

            if (args.Validate) {
                ReturnCode?.Invoke (this, args);
            }
        }
        #endregion

        #region Static
        public static TReturnCodeModel Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
