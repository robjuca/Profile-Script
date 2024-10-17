/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Message;
using rr.Provider.Resources;

using System.Collections.Generic;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerModuleCatalogue
    public class THandlerModuleCatalogue
    {
        #region Event
        //public static event EventHandler<THandlerData> NextModule;
        #endregion

        #region Constructor
        THandlerModuleCatalogue ()
        {
            HandlerDataList = [];

            HandlerModule = THandlerModule.Create ();
            HandlerMessage = THandlerMessage.Create ();
            HandlerSpeech = THandlerSpeech.Create ();

            ScriptReturnCode = TScriptReturnCode.Create ();

            ScriptReturnCode.ScriptReturnCodeDispatcher += OnScriptReturnCodeDispatcher;
        }
        #endregion

        #region Members
        public void Execute (THandlerData data)
        {
            HandlerSpeech.Process (data.HandlerSpeechData);
        }

        public void AddHandlerData (IList<THandlerData> data)
        {
            if (data is not null) {
                HandlerDataList = new List<THandlerData> (data);
                HandlerDataList.Sort ();
            }
        }

        public void ProcessScriptAction (TScriptActionDispatcherEventArgs eventArgs)
        {
            ScriptReturnCode.ProcessScriptReturnCode (eventArgs); // from Dispatcher
        }
        #endregion

        #region Event
        void OnScriptReturnCodeDispatcher (object sender, TScriptReturnCodeArgs eventArgs)
        {
            // from TScriptReturnCode

            HandlerSpeech.ActionReturnCode (eventArgs);
            //HandlerModule.ActionReturnCode (eventArgs);

            // speech done
            if (eventArgs.IsSpeechDone & Wait is false) {
                //ProcessCondition ();
            }

            // internal code
            if (eventArgs.IsUCodeId) {
            }

            // next step
            if (eventArgs.IsNextStep) {
                //NextAction ();
                //ProcessSpeech ();
            }

            // next module
            if (eventArgs.IsNextModule) {
                //if (HandlerData.HasNextModule) {
                //    NextModule?.Invoke (this, HandlerData);
                //}
            }
        }
        #endregion

        #region Property
        List<THandlerData> HandlerDataList { get; set; }
        int ActionIndex { get; set; }
        bool Code40 { get; set; }
        THandlerData HandlerData => HandlerDataList [ ActionIndex ];
        THandlerSpeech HandlerSpeech { get; set; }
        THandlerModule HandlerModule { get; set; }
        THandlerMessage HandlerMessage { get; set; }
        TScriptReturnCode ScriptReturnCode { get; set; }
        bool Wait { get; set; }
        UHandlerModule ParentModule { get; set; }
        #endregion

        #region Support
        void NextAction () => ActionIndex++;
        //void ProcessSpeech () => HandlerSpeech.Process (HandlerData);
        //void ProcessCondition () => HandlerModule.Process (HandlerData);
        #endregion

        #region Static
        public static THandlerModuleCatalogue Create (UHandlerModule parentModule) => new () { ParentModule = parentModule };
        #endregion
    };
    //---------------------------//

}  // namespace