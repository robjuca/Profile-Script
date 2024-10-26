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
    //----- TModelCatalog
    public class TModelCatalog
    {
        #region Event
        //public static event EventHandler<TModelData> NextModule;
        #endregion

        #region Constructor
        TModelCatalog ()
        {
            ModelDataListIndex = 0;
            ModelDataList = [];

            //HandlerSpeech = THandlerSpeech.Create ();
            //HandlerModule = THandlerModule.Create ();
            //HandlerMessage = THandlerMessage.Create ();
            //ModelReceiver = TReceiverModel.Create ();

            ReturnCodeModel = TReturnCodeModel.Create ();

            ReturnCodeModel.ReturnCode += OnReturnCode;
        }
        #endregion

        #region Members
        public void Execute (string messageValue)
        {
            if (SelectMessageValue (messageValue)) {
                SelectHandlerData ();
                //ProcessSpeech ();
            }
        }

        public void AddModelData (TModelData data)
        {
            if (data is not null) {
                ModelDataList.Add (data);
                ModelDataList.Sort ();
            }
        }

        public void ProcessScriptReturnCode (TScriptActionDispatcherEventArgs eventArgs)
        {
            ReturnCodeModel.ProcessScriptReturnCode (eventArgs); // from Dispatcher
        }
        #endregion

        #region Event
        void OnReturnCode (object sender, TReturnCodeArgs eventArgs)
        {
            // from TReturnCodeModel
            //HandlerSpeech.ReturnCodeModel (eventArgs);
            //HandlerModule.ReturnCodeModel (eventArgs);
            //HandlerMessage.ReturnCodeModel (eventArgs);
            //ModelReceiver.ReturnCodeModel (eventArgs);

            // speech done
            if (eventArgs.IsSpeechDone) {
                //HandlerModule.Process ();
                //HandlerMessage.Process ();
                //ModelReceiver.Process ();

                if (HasMoreData) {
                    SelectHandlerData (pumpIndex: true);
                    //ProcessSpeech ();
                }
            }
        }
        #endregion

        #region Property
        List<TModelData> ModelDataList { get; set; }
        int ModelDataListIndex { get; set; }
        public bool HasMoreData => (ModelDataListIndex + 1) < ModelDataList.Count;

        TModelData CurrentModelData => ModelDataList [ ModelDataListIndex ];
        //THandlerSpeech HandlerSpeech { get; set; }
        //THandlerModule HandlerModule { get; set; }
        //THandlerMessage HandlerMessage { get; set; }
        TReceiverModel ModelReceiver { get; set; }
        TReturnCodeModel ReturnCodeModel { get; set; }
        UHandlerModule ParentModule { get; set; }
        #endregion

        #region Support
        void SelectHandlerData (bool pumpIndex = false)
        {
            ModelDataListIndex = pumpIndex ? (ModelDataListIndex + 1) : ModelDataListIndex;

            //HandlerSpeech.SelectHandlerData (CurrentModelData);
            //HandlerModule.SelectHandlerData (CurrentModelData);
            //HandlerMessage.SelectHandlerData (CurrentModelData);
            //ModelReceiver.SelectHandlerData (CurrentModelData);
        }

        //void ProcessSpeech () => HandlerSpeech.Process ();

        bool SelectMessageValue (string messageValue)
        {
            ModelDataListIndex = 0;

            //foreach (var data in ModelDataList) {
            //    if (data.HandlerMessageData.ContainsNameValue (messageValue)) {
            //        return true;
            //    }

            //    ModelDataListIndex++;
            //}

            return false;
        }
        #endregion

        #region Static
        public static TModelCatalog Create (UHandlerModule parentModule) => new () { ParentModule = parentModule };
        #endregion
    };
    //---------------------------//

}  // namespace