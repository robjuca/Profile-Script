/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
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
            HandlerDataIndex = 0;
            HandlerDataList = [];

            HandlerSpeech = THandlerSpeech.Create ();
            HandlerModule = THandlerModule.Create ();
            HandlerMessage = THandlerMessage.Create ();
            HandlerReceiver = THandlerReceiver.Create ();

            ScriptReturnCode = TScriptReturnCode.Create ();

            ScriptReturnCode.ScriptReturnCodeDispatcher += OnScriptReturnCodeDispatcher;
        }
        #endregion

        #region Members
        public void Execute (string messageValue)
        {
            if (SelectMessageValue (messageValue)) {
                ProcessAll ();
            }
        }

        public void AddHandlerData (THandlerData data)
        {
            if (data is not null) {
                HandlerDataList.Add (data);
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

            HandlerSpeech.ScriptReturnCode (eventArgs);
            //HandlerModule.ScriptReturnCode (eventArgs);

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
        int HandlerDataIndex { get; set; }
        bool Code40 { get; set; }
        THandlerData HandlerData => HandlerDataList [ HandlerDataIndex ];
        THandlerSpeech HandlerSpeech { get; set; }
        THandlerModule HandlerModule { get; set; }
        THandlerMessage HandlerMessage { get; set; }
        THandlerReceiver HandlerReceiver { get; set; }
        TScriptReturnCode ScriptReturnCode { get; set; }
        bool Wait { get; set; }
        UHandlerModule ParentModule { get; set; }
        #endregion

        #region Support
        void ProcessAll ()
        {
            HandlerSpeech.Process (HandlerData.HandlerSpeechData);
            HandlerModule.Process (HandlerData.HandlerModuleData);
            HandlerMessage.Process (HandlerData.HandlerMessageData);
            HandlerReceiver.Process (HandlerData);
        }

        bool SelectMessageValue (string messageValue)
        {
            HandlerDataIndex = 0;

            foreach (var data in HandlerDataList) {
                if (data.HandlerMessageData.ContainsMessageValue (messageValue)) {
                    return true; 
                }

                HandlerDataIndex++;
            }

            return false;
        }
        //void NextAction () => ActionIndex++;
        //void ProcessSpeech () => HandlerSpeech.Process (HandlerData);
        //void ProcessCondition () => HandlerModule.Process (HandlerData);
        #endregion

        #region Static
        public static THandlerModuleCatalogue Create (UHandlerModule parentModule) => new () { ParentModule = parentModule };
        #endregion
    };
    //---------------------------//

}  // namespace