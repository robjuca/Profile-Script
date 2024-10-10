﻿/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Library.Types;
using rr.Profile.Message;
using rr.Profile.Presentation;
using rr.Profile.Resources;
using rr.Profile.Resources.Properties;
using rr.Profile.Steps;

using System.Collections.Generic;
using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Plate
{
    //----- TOutsideOpePresentation
    [Export (typeof (IDefault))]
    public class TOutsideOpePresentation : TModulePresentation
    {
        #region Constructor
        [ImportingConstructor]
        public TOutsideOpePresentation (IEventAggregator eventAggregator)
            : base (eventAggregator, TPlateModule.OUTSIDE_OPE)
        {
            Speech = [];
            Plates = TPlatesPresentation.Create (Module);

            Plates.NextModule += OnNextModule;
        }
        #endregion

        #region Override
        public override void Handle (TMessageInternal message)
        {
            if (message.ValidateReceiver (TypeInfo)) {
                // PROFILE_LOADED
                if (message.IsAction (TMessageAction.PROFILE_LOADED)) {
                    AddSpeech ();
                }

                // PROFILE_UNLOADED
                if (message.IsAction (TMessageAction.PROFILE_UNLOADED)) {
                    ClearActiveModule ();

                    Speech.Clear ();
                    Plates.Cleanup ();
                }

                // NEXT_MODULE
                if (message.IsAction (TMessageAction.NEXT_MODULE)) {
                    if (message.RequestParam (out TStepData data)) {
                        ClearActiveModule ();

                        if (IsMySelf (data.NextModule)) {
                            SelectActiveModule (Module);

                            AddSpeech ();
                            Plates.Ready (Speech);
                            Plates.StepIn (TStepId.OutsideOpeInit);
                        }
                    }
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION
                    if (message.IsAction (TMessageAction.SCRIPT_ACTION)) {
                        if (message.RequestParam (out TActionDispatcherEventArgs eventArgs)) {
                            Plates.StepAction (eventArgs);
                        }
                    }
                }
            }
        }
        #endregion

        #region Event
        void OnNextModule (object sender, TStepData data)
        {
            if (IsActiveModule) {
                ClearActiveModule ();

                var message = TMessageInternal.CreateDefault (Module, TMessageAction.NEXT_MODULE);
                message.SelectParam (TParamInfo.Create (data));
                message.AddDestinationModule (data.NextModule);

                Publish (message);
            }
        }
        #endregion

        #region Property
        List<TSpeechData> Speech { get; set; }
        TPlatesPresentation Plates { get; set; }
        #endregion

        #region Support
        void AddSpeech ()
        {
            Speech.Clear ();

            var data = TSpeechData.Create (TPlateModule.OUTSIDE_OPE, TStepId.OutsideOpeInit);
            data.AddSpeech (Resources.RES_OutsideOpeInit);
            Speech.Add (data);

            data = TSpeechData.Create (TPlateModule.OUTSIDE_OPE, TStepId.OutsideOpeCode, TInternalCode.ENGAGED);
            data.AddSpeech (Resources.RES_OutsideOpeCode);
            data.AddNextModule (TPlateModule.DOOR_OPE);
            Speech.Add (data);

            //data = TSpeechData.Create (TPlateModule.OUTSIDE_OPE, TStepId.OutsideOpeDone);
            //data.AddSpeech (Resources.RES_OutsideOpeDone);
            //data.AddNextModule (TPlateModule.DOOR_OPE);
            //Speech.Add (data);
        }
        #endregion
    };
    //---------------------------//

}  // namespace
