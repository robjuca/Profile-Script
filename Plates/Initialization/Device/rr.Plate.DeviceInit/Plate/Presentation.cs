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
    //----- UVariableName
    public enum UVariableName               // (Variable Name)
    {
        // Module Handler
        MODULE_NAME_DEVICE_INIT,            // must match with RECEIVER_MODULE_NAME

        // Message to Module
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

            HandlerModuleCatalogue = THandlerModuleCatalogue.Create (HandlerModule);

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

                        // just once
                        CreateVariables ();
                        CreateHandlerData ();
                    }
                }

                // PROFILE_LOADED
                if (message.IsAction (UMessageAction.PROFILE_LOADED)) {
                    ProfileLoad = true;
                    SelectActiveModule (Module);
                    //HandlerModulePresentation.Ready (handlerDataList);

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
                        if (message.RequestParam (out TScriptActionDispatcherEventArgs eventArgs)) {
                            HandlerModuleCatalogue.ProcessScriptAction (eventArgs);
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
        THandlerModuleCatalogue HandlerModuleCatalogue { get; set; }
        #endregion

        #region Support
        void CreateVariables ()
        {
            var data = TScriptDefinitionData.CreateDefault ();

            // Module Handler
            data.AddVariableName (ToString (UVariableName.MODULE_NAME_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_EMPTY);
            Services.SetScriptDataValue (data);

            // Message to Module
            data.AddVariableName (ToString (UVariableName.MODULE_MESSAGE_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_EMPTY);
            Services.SetScriptDataValue (data);

            // Speech Handler
            data.AddVariableName (ToString (UVariableName.SPEECH_TEXT_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_EMPTY);
            Services.SetScriptDataValue (data);

            data.AddVariableName (ToString (UVariableName.SPEECH_ENABLE_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_FALSE);
            Services.SetScriptDataValue (data);
        }

        void CreateHandlerData ()
        {
            var handlerData = THandlerData.Create (Services, Module);

            #region common
            // Module
            handlerData.HandlerModuleData.AddModuleVariableName (ToString (UVariableName.MODULE_NAME_DEVICE_INIT));
            handlerData.HandlerModuleData.AddModuleVariableValue (TEnumExtension.AsString (Module));

            // Message
            handlerData.HandlerMessageData.AddMessageVariableName (ToString (UVariableName.MODULE_MESSAGE_DEVICE_INIT));

            // Speech
            handlerData.HandlerSpeechData.AddSpeechTextVariableName (ToString (UVariableName.SPEECH_TEXT_DEVICE_INIT));
            handlerData.HandlerSpeechData.AddSpeechTextEnableVariableName (ToString (UVariableName.SPEECH_ENABLE_DEVICE_INIT));
            handlerData.HandlerSpeechData.AddSpeechTextEnableVariableValue (Resources.RES_TRUE);
            #endregion

            // Text and Message - Briefing
            handlerData.HandlerSpeechData.AddSpeechTextVariableValue ("game state not ready, waiting for aircraft on ground");
            handlerData.HandlerMessageData.AddMessageVariableValue (TEnumExtension.AsString (SimulationGamestate.Briefing));

            // only speech is enabled
            handlerData.HandlerModuleData.EnableHandler (enable: false);
            handlerData.HandlerMessageData.EnableHandler (enable: false);

            handlerData.PumpHandlerIndex ();
            HandlerModuleCatalogue.AddHandlerData (handlerData.Clone ());  // add to list

            // Text and Message - Flying
            handlerData.HandlerSpeechData.AddSpeechTextVariableValue ("");
            handlerData.HandlerMessageData.AddMessageVariableValue (TEnumExtension.AsString (SimulationGamestate.Flying));

            // all handlers enabled
            handlerData.EnableAll ();

            handlerData.PumpHandlerIndex ();
            HandlerModuleCatalogue.AddHandlerData (handlerData.Clone ());  // add to list

            // Text and Message - Begin
            handlerData.HandlerSpeechData.AddSpeechTextVariableValue (
                "check instructions: aircraft cold and dark mode: park break on: honeycomb device:all switches: off all levers: idle gear down:when ready: set beacon switch on and off:waiting..."
            );
            handlerData.HandlerMessageData.AddMessageVariableValue (TEnumExtension.AsString (UMessageValue.Begin));

            handlerData.PumpHandlerIndex ();
            HandlerModuleCatalogue.AddHandlerData (handlerData.Clone ());  // add to list
        }

        void SelectGameState (string state = default)
        {
            switch (Services.RequestGameState (state)) {
                case SimulationGamestate.Briefing: {
                    HandlerModuleCatalogue.Execute (TEnumExtension.AsString (SimulationGamestate.Briefing));
                    break;
                }

                case SimulationGamestate.Flying: {
                    HandlerModuleCatalogue.Execute (TEnumExtension.AsString (SimulationGamestate.Flying));
                    break;
                }
            }
        }

        string ToString (UVariableName name)
        {
            return TEnumExtension.AsString (name);
        }
        #endregion
    };
    //---------------------------//

}  // namespace
