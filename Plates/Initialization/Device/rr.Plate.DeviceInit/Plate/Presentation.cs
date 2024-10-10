/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Library.Extension;
using rr.Module.Handler;
using rr.Provider.Message;
using rr.Provider.Presentation;
using rr.Provider.Resources;
using rr.Provider.Services;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;

using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Plate.DeviceInit
{
    #region Data
    //----- UHandlerSpeech 
    public enum UHandlerSpeech
    {
        SPEECH_DEVICE_INIT,
        SPEECH_ENABLE_DEVICE_INIT,
    };
    //---------------------------// 
    #endregion

    //----- TDeviceInitPresentation
    [Export (typeof (IDefault))]
    public class TDeviceInitPresentation : TModulePresentation, IModule
    {
        #region IModule
        public UHandlerModule HandlerModule => Module;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public TDeviceInitPresentation (
           IEventAggregator eventAggregator, IProviderServices services)
               : base (eventAggregator, services, UHandlerModule.DEVICE_INIT)
        {
            SelectActiveModule (Module);

            //ModuleData = [];
            ActionPresentation = THandlerModulePresentation.Create (Module);

            //ActionPresentation.NextModule += OnNextModule;
        }
        #endregion

        #region Override
        public override void Handle (TMessageInternal message)
        {
            if (message.ValidateReceiver (TypeInfo)) {
                // INITIALIZE
                if (message.IsAction (UMessageAction.INITIALIZE)) {
                    if (message.RequestParam (out IApplication app)) {
                        app.SubscribeToSystemEvent (SPADSystemEvents.GameStateChanged, OnGameStateChanged);

                        CreateModuleData (); // just once
                    }
                }

                // PROFILE_LOADED
                if (message.IsAction (UMessageAction.PROFILE_LOADED)) {
                    ProfileLoad = true;
                    SelectActiveModule (Module);
                    //ActionPresentation.Ready (ModuleData);

                    SelectGameState (EventSystem.GetDataDefinition ("LOCAL:GAMESTATE")?.GetValue ().ToString ());
                }

                // PROFILE_UNLOADED
                if (message.IsAction (UMessageAction.PROFILE_UNLOADED)) {
                    ProfileLoad = false;
                    //ActionPresentation.Cleanup ();
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION (from Process_Dispatcher)
                    if (message.IsAction (UMessageAction.SCRIPT_ACTION)) {
                        if (message.RequestParam (out TActionDispatcherEventArgs eventArgs)) {
                            ActionPresentation.ProcessAction (eventArgs);
                        }
                    }
                }
            }
        }
        #endregion

        #region Event
        void OnGameStateChanged (object sender, ISPADEventArgs e)
        {
            if (e.NewValue is int newVal) {
                if (ProfileLoad) {
                    SelectGameState (newVal.ToString ());
                }
            }
        }

        //void OnNextModule (object sender, TModuleData data)
        //{
        //    if (IsActiveModule) {
        //        ClearActiveModule ();

        //        var message = TMessageInternal.CreateDefault (Module, UMessageAction.NEXT_MODULE);
        //        message.SelectParam (TParamInfo.Create (data));
        //        message.AddDestinationModule (data.NextModule);

        //        Publish (message);
        //    }
        //}
        #endregion

        #region Property
        bool ProfileLoad { get; set; }
        //List<TModuleHandlerData> ModuleData { get; set; }
        THandlerModulePresentation ActionPresentation { get; set; }
        #endregion

        #region Support
        void CreateModuleData ()
        {
            //ModuleData.Clear ();

            //var data = TModuleData.Create (Module, UCodeId.C0DE_100);
            //data.AddSpeech (Resources.RES_DeviceInitBriefing);
            //ModuleData.Add (data);

            //data = TModuleData.Create (Module, UCodeId.C0DE_200);
            //data.AddSpeech (Resources.RES_DeviceInitReady);
            //ModuleData.Add (data);

            //data = TModuleData.Create (Module, UCodeId.C0DE_300);
            //data.AddSpeech (Resources.RES_DeviceInitEnd);
            //ModuleData.Add (data);

            //data = TModuleData.Create (Module, UCodeId.C0DE_400);
            //data.AddSpeech (Resources.RES_DeviceInitDone);
            //data.AddNextModule (UModuleId.GROUND_OPE);
            //ModuleData.Add (data);
        }

        void SelectGameState (string state)
        {
            var gameState = TEnumExtension.ToEnum<SimulationGamestate> (state);


            // Briefing
            if (gameState.Equals(SimulationGamestate.Briefing)) {
                //ActionPresentation.NextCodeId (UCodeId.C0DE_100, wait: true);
            }

            // Ready (Flying)
            if (gameState.Equals (SimulationGamestate.Flying)) {
                //ActionPresentation.NextCodeId (UCodeId.C0DE_200);
            }
        }
        #endregion
    };


    //---------------------------//

}  // namespace
