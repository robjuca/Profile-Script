/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;

using System.Collections.Generic;
//---------------------------//

namespace rr.Handler.Model
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

            ReturnCodeModel = TReturnCodeModel.Create ();
            ReturnCodeModel.ReturnCode += OnReturnCode;
        }
        #endregion

        #region Members
        public void Execute (string messageValue)
        {
            if (SelectMessageValue (messageValue)) {
                SelectModelDataIndex ();
                ProcessSpeech ();
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

        public void Cleanup ()
        {
            ModelDataList.Clear ();
            ModelDataListIndex = 0;
        }
        #endregion

        #region Event
        void OnReturnCode (object sender, TReturnCodeArgs eventArgs)
        {
            // from TReturnCodeModel
            CurrentModelData.ScriptReturnCode (eventArgs);

            // speech done
            if (eventArgs.IsSpeechDone) {
                CurrentModelData.Process ();

                if (HasMoreData) {
                    SelectModelDataIndex (pumpIndex: true);
                    ProcessSpeech ();
                }
            }
        }
        #endregion

        #region Property
        List<TModelData> ModelDataList { get; set; }
        int ModelDataListIndex { get; set; }
        TModelData CurrentModelData => ModelDataList [ ModelDataListIndex ];
        TReturnCodeModel ReturnCodeModel { get; set; }
        UHandlerModule ParentModule { get; set; }
        bool HasMoreData => !CurrentModelData.IsModelWaiting && (ModelDataListIndex + 1) < ModelDataList.Count;
        #endregion

        #region Support
        void SelectModelDataIndex (bool pumpIndex = false) => ModelDataListIndex = pumpIndex ? (ModelDataListIndex + 1) : ModelDataListIndex;
        void ProcessSpeech () => CurrentModelData.SpeechModel.Process ();

        bool SelectMessageValue (string messageValue)
        {
            ModelDataListIndex = 0;

            foreach (var data in ModelDataList) {
                if (data.MessageModel.ContainsNameValue (messageValue)) {
                    return true;
                }

                ModelDataListIndex++;
            }

            return false;
        }
        #endregion

        #region Static
        public static TModelCatalog Create (UHandlerModule parentModule) => new () { ParentModule = parentModule };
        #endregion
    };
    //---------------------------//

}  // namespace