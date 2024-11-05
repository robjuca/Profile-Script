/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Library.Extension;
using rr.Handler.Model;
using rr.Provider.Message;
using rr.Provider.Presentation;
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
using rr.Provider.Services;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;

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

            ModelCatalogue = TModelCatalog.Create (HandlerModule);

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
                    }
                }

                // PROFILE_LOADED
                if (message.IsAction (UMessageAction.PROFILE_LOADED)) {
                    ProfileLoad = true;
                    SelectActiveModule (Module);

                    CreateVariables ();
                    CreateHandlerData ();

                    //HandlerModulePresentation.Ready (handlerDataList);

                    SelectGameState ();
                }

                // PROFILE_UNLOADED
                if (message.IsAction (UMessageAction.PROFILE_UNLOADED)) {
                    ProfileLoad = false;
                    ModelCatalogue.Cleanup ();
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION (from Process_Dispatcher)
                    if (message.IsAction (UMessageAction.SCRIPT_ACTION)) {
                        if (message.RequestParam (out TScriptActionDispatcherEventArgs eventArgs)) {
                            ModelCatalogue.ProcessScriptReturnCode (eventArgs);
                        }
                    }
                }
            }
        }
        #endregion

        #region Event
        public void OnGameStateChanged (object sender, ISPADEventArgs e)
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
        TModelCatalog ModelCatalogue { get; set; }
        #endregion

        #region Support
        void CreateVariables ()
        {
            var data = TScriptDefinitionData.CreateDefault ();

            // Module Handler
            data.AddVariableName (ToString (UVariableName.MODULE_NAME_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_EMPTY_VALUE_NAME_DEVICE_INIT);
            Services.SetScriptDataValue (data.Clone ());

            // Message to Module
            data.AddVariableName (ToString (UVariableName.MODULE_MESSAGE_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_EMPTY_VALUE_MESSAGE_DEVICE_INIT);
            Services.SetScriptDataValue (data.Clone ());

            // Speech Handler
            data.AddVariableName (ToString (UVariableName.SPEECH_TEXT_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_EMPTY_VALUE_SPEECH_TEXT_DEVICE_INIT);
            Services.SetScriptDataValue (data.Clone ());

            data.AddVariableName (ToString (UVariableName.SPEECH_ENABLE_DEVICE_INIT));
            data.AddVariableValue (Resources.RES_FALSE_VALUE_SPEECH_ENABLE_DEVICE_INIT);
            Services.SetScriptDataValue (data.Clone ());
        }

        void CreateHandlerData ()
        {
            var modelData = TModelData.Create (Services, Module);

            #region common
            // Module
            modelData.ModuleModel.AddVariableName (ToString (UVariableName.MODULE_NAME_DEVICE_INIT));
            modelData.ModuleModel.AddVariableValue (TEnumExtension.AsString (Module));

            // Message
            modelData.MessageModel.AddVariableName (ToString (UVariableName.MODULE_MESSAGE_DEVICE_INIT));

            // Speech
            modelData.SpeechModel.AddVariableName (ToString (UVariableName.SPEECH_TEXT_DEVICE_INIT));
            modelData.SpeechModel.AddEnableVariableName (ToString (UVariableName.SPEECH_ENABLE_DEVICE_INIT));
            modelData.SpeechModel.AddEnableVariableValue (Resources.RES_TRUE);
            #endregion

            // Text and Message - Briefing
            #region Briefing
            modelData.SpeechModel.EnableWaitingFlag ();
            modelData.SpeechModel.AddVariableValue ("game state not ready, waiting for aircraft on ground");
            modelData.MessageModel.AddVariableValue (TEnumExtension.AsString (SimulationGamestate.Briefing));

            // only speech is enabled
            modelData.ModuleModel.DisableModel ();
            modelData.MessageModel.DisableModel ();

            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion

            // Text and Message - Flying
            #region Flying
            modelData.SpeechModel.AddVariableValue (
                    "check instructions: aircraft cold and dark mode\r\n park break on:\r\n honeycomb device : all switches: off\r\n all levers: idle:\r\n gear down:"
                );

            modelData.MessageModel.AddVariableValue (TEnumExtension.AsString (SimulationGamestate.Flying));

            // all models enabled
            modelData.EnableAllModels ();
            modelData.ClearWaitingModels ();

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion

            // Text and Message - Waiting...
            #region Waiting...
            modelData.SpeechModel.AddVariableValue (
                    "when ready: set beacon switch on and off : \r\n waiting..."
                );

            modelData.MessageModel.AddVariableValue ("Waiting");

            // all models enabled
            modelData.EnableAllModels ();

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion

            // Text and Message - Done...
            #region Done...
            modelData.SpeechModel.AddVariableValue (
                    "done"
                );

            //modelData.MessageModel.AddVariableValue ("Done");

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion
        }

        public void SelectGameState (string state = default)
        {
            switch (Services.RequestGameState (state)) {
                case SimulationGamestate.Briefing: {
                    ModelCatalogue.Execute (TEnumExtension.AsString (SimulationGamestate.Briefing));
                    break;
                }

                case SimulationGamestate.Flying: {
                    ModelCatalogue.Execute (TEnumExtension.AsString (SimulationGamestate.Flying));
                    break;
                }
            }
        }

        string ToString (UVariableName name) => TEnumExtension.AsString (name);
        #endregion
    };
    //---------------------------//

}  // namespace
