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
using rr.Provider.Resources.Properties;
using rr.Provider.Services;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;

using System.Collections.Generic;
using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Plate.DeviceInit
{
    #region Data
    //----- UHandler
    public enum UHandler
    {
        // Module Handler
        MODULE_NAME_DEVICE_INIT,            // must match with RECEIVER_MODULE_NAME
        MODULE_MESSAGE_DEVICE_INIT,         // message to decode (UMessageName) RECEIVER_MODULE_MESSAGE

        // Speech Handler
        SPEECH_TEXT_DEVICE_INIT,            // put text to play here
        SPEECH_ENABLE_DEVICE_INIT,          // to play text condition enable (true or false)
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

            HandlerDataList = [];
            HandlerModulePresentation = THandlerModulePresentation.Create (Module);

            //HandlerModulePresentation.NextModule += OnNextModule;
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

                        CreateHandlerData (); // just once
                    }
                }

                // PROFILE_LOADED
                if (message.IsAction (UMessageAction.PROFILE_LOADED)) {
                    ProfileLoad = true;
                    SelectActiveModule (Module);
                    //HandlerModulePresentation.Ready (HandlerDataList);

                    SelectGameState ();
                }

                // PROFILE_UNLOADED
                if (message.IsAction (UMessageAction.PROFILE_UNLOADED)) {
                    ProfileLoad = false;
                    //HandlerModulePresentation.Cleanup ();
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION (from Process_Dispatcher)
                    if (message.IsAction (UMessageAction.SCRIPT_ACTION)) {
                        if (message.RequestParam (out TActionDispatcherEventArgs eventArgs)) {
                            HandlerModulePresentation.ProcessAction (eventArgs);
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
        List<THandlerData> HandlerDataList { get; set; }
        THandlerModulePresentation HandlerModulePresentation { get; set; }
        string SpeechTextVariableName => TEnumExtension.AsString (UHandlerSpeech.SPEECH_TEXT_DEVICE_INIT);
        string SpeechTextEnableVariableName => TEnumExtension.AsString (UHandlerSpeech.SPEECH_ENABLE_DEVICE_INIT);
        #endregion

        #region Support
        void CreateHandlerData ()
        {
            HandlerDataList.Clear ();

            var handlerData = THandlerData.Create (Services, Module);

            #region module to handler

            #endregion

            #region message to module

            #endregion

            #region text to play
            // text to play (TextVariableName, TextVariableValue)
            handlerData.HandlerSpeechData.AddSpeechTextVariableName (SpeechTextVariableName);
            handlerData.HandlerSpeechData.AddSpeechTextVariableValue ("");

            // enable to play (EnableVariableName, EnableVariableValue)
            handlerData.HandlerSpeechData.AddSpeechTextEnableVariableName (SpeechTextEnableVariableName);
            handlerData.HandlerSpeechData.AddSpeechTextEnableVariableValue (Resources.RES_TRUE); 
            #endregion
        }

        void SelectGameState (string state = default)
        {
            switch (Services.RequestGameState (state)) {
                case SimulationGamestate.Briefing:
                    //HandlerModulePresentation.NextCodeId (UCodeId.C0DE_100, wait: true);
                    break;

                case SimulationGamestate.Flying:
                    //HandlerModulePresentation.NextCodeId (UCodeId.C0DE_200);
                    break;
            }
        }
        #endregion
    };
    //---------------------------//

}  // namespace
