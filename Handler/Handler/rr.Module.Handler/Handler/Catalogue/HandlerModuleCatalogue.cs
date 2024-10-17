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
            HandlerDataList = [];
            HandlerCondition = THandlerModule.Create ();
            HandlerAction = THandlerAction.Create ();
            HandlerSpeech = THandlerSpeech.Create ();

            HandlerAction.ActionReturnCodeDispatcher += OnActionReturnCodeDispatcher;
        }
        #endregion

        #region Members
        //public void Cleanup ()
        //{
        //    ActionIndex = 0;
        //    Code40 = true;

        //    HandlerDataList.Clear ();

        //    HandlerSpeech.Cleanup ();
        //    HandlerCondition.Cleanup ();
        //}

        public void AddModuleData (IList<THandlerData> data)
        {
            //Cleanup ();

            if (data is not null) {
                HandlerDataList = new List<THandlerData> (data);
                HandlerDataList.Sort ();
            }
        }

        public void ProcessAction (TActionDispatcherEventArgs eventArgs)
        {
            HandlerAction.ProcessAction (eventArgs); // from Dispatcher
        }
        #endregion

        #region Event
        void OnActionReturnCodeDispatcher (object sender, THandlerActionArgs eventArgs)
        {
            // from THandlerAction

            //HandlerSpeech.ActionReturnCode (eventArgs);
            //HandlerCondition.ActionReturnCode (eventArgs);

            // speech done
            if (eventArgs.IsCode40 & Wait is false) {
                //ProcessCondition ();
            }

            // internal code
            if (eventArgs.IsUCodeId) {
            }

            // next step
            if (eventArgs.IsNextStep) {
                NextAction ();
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
        THandlerModule HandlerCondition { get; set; }
        THandlerAction HandlerAction { get; set; }
        bool Wait { get; set; }
        UHandlerModule ParentModule { get; set; }
        #endregion

        #region Support
        void NextAction () => ActionIndex++;
        //void ProcessSpeech () => HandlerSpeech.Process (HandlerData);
        //void ProcessCondition () => HandlerCondition.Process (HandlerData);
        #endregion

        #region Static
        public static THandlerModuleCatalogue Create (UHandlerModule parentModule) => new () { ParentModule = parentModule };
        #endregion
    };
    //---------------------------//

}  // namespace