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
            HandlerReceiver = TReceiverModel.Create ();

            ScriptReturnCode = TReturnCodeModel.Create ();

            ScriptReturnCode.ScriptReturnCodeDispatcher += OnScriptReturnCodeDispatcher;
        }
        #endregion

        #region Members
        public void Execute (string messageValue)
        {
            if (SelectMessageValue (messageValue)) {
                SelectHandlerData ();
                ProcessSpeech ();
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
        void OnScriptReturnCodeDispatcher (object sender, TReturnCodeArgs eventArgs)
        {
            // from TReturnCodeModel
            HandlerSpeech.ScriptReturnCode (eventArgs);
            HandlerModule.ScriptReturnCode (eventArgs);
            HandlerMessage.ScriptReturnCode (eventArgs);
            HandlerReceiver.ScriptReturnCode (eventArgs);

            // speech done
            if (eventArgs.IsSpeechDone) {
                HandlerModule.Process ();
                HandlerMessage.Process ();
                HandlerReceiver.Process ();

                if (HasMoreData) {
                    SelectHandlerData (pumpIndex: true);
                    ProcessSpeech ();
                }
            }
        }
        #endregion

        #region Property
        List<THandlerData> HandlerDataList { get; set; }
        int HandlerDataIndex { get; set; }
        public bool HasMoreData => (HandlerDataIndex + 1) < HandlerDataList.Count;

        THandlerData HandlerData => HandlerDataList [ HandlerDataIndex ];
        THandlerSpeech HandlerSpeech { get; set; }
        THandlerModule HandlerModule { get; set; }
        THandlerMessage HandlerMessage { get; set; }
        TReceiverModel HandlerReceiver { get; set; }
        TReturnCodeModel ScriptReturnCode { get; set; }
        UHandlerModule ParentModule { get; set; }
        #endregion

        #region Support
        void SelectHandlerData (bool pumpIndex = false)
        {
            HandlerDataIndex = pumpIndex ? (HandlerDataIndex + 1) : HandlerDataIndex;

            HandlerSpeech.SelectHandlerData (HandlerData);
            HandlerModule.SelectHandlerData (HandlerData);
            HandlerMessage.SelectHandlerData (HandlerData);
            HandlerReceiver.SelectHandlerData (HandlerData);
        }

        void ProcessSpeech () => HandlerSpeech.Process ();

        bool SelectMessageValue (string messageValue)
        {
            HandlerDataIndex = 0;

            foreach (var data in HandlerDataList) {
                if (data.HandlerMessageData.ContainsNameValue (messageValue)) {
                    return true;
                }

                HandlerDataIndex++;
            }

            return false;
        }
        #endregion

        #region Static
        public static THandlerModuleCatalogue Create (UHandlerModule parentModule) => new () { ParentModule = parentModule };
        #endregion
    };
    //---------------------------//

}  // namespace