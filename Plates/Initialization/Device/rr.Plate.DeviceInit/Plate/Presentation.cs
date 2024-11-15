/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Handler.Model;
using rr.Library.EventAggregator;
using rr.Library.Extension;
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
    enum UVariableName               // (Variable Name)
    {
        // Module 
        DEVICE_INIT_MODULE,            // must match with RECEIVER_MODULE_NAME

        // Message
        DEVICE_INIT_MESSAGE,         // message to decode (UMessageName) RECEIVER_MODULE_MESSAGE

        // Speech 
        DEVICE_INIT_SPEECH,            // put text to play here
        DEVICE_INIT_SPEECH_ENABLE,          // to play text condition enable (true or false)
    };
    //---------------------------// 

    enum UUserActionCode
    {
        WAIT_DONE   = 210,
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

                    SelectGameState ();
                }

                // PROFILE_UNLOADED
                if (message.IsAction (UMessageAction.PROFILE_UNLOADED)) {
                    ProfileLoad = false;
                    ModelCatalogue.Cleanup ();
                    ClearActiveModule ();
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION (from Process_Dispatcher)
                    if (message.IsAction (UMessageAction.SCRIPT_ACTION)) {
                        if (message.RequestParam (out TScriptActionDispatcherEventArgs eventArgs)) {
                            // User Action (210) Wait message 
                            if (eventArgs.ContainsReturnCode (UUserActionCode.WAIT_DONE)) {
                                ModelCatalogue.ResetWaitingFlags ();
                                ModelCatalogue.Next ();

                                return;
                            }

                            // Model DONE (-80) - must go to next model
                            if (eventArgs.ContainsReturnCode (Resources.RES_NEXT_MODEL_CODE)) {
                                ModelCatalogue.Cleanup ();
                                ClearActiveModule ();

                                var data = TNextModuleData.Create (Module, UHandlerModule.GROUND_OPE);
                                data.AddMessageName (UMessageName.MSG_BEGIN);
                                data.AddMessageAction (UMessageAction.NEXT_MODULE);

                                var msg = TMessageInternal.CreateFrom (data);
                                Publish (msg.Clone ());

                                return;
                            }

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
        #endregion

        #region Property
        bool ProfileLoad { get; set; }
        TModelCatalog ModelCatalogue { get; set; }
        #endregion

        #region Support
        void CreateVariables ()
        {
            var data = TScriptDefinitionData.CreateDefault ();

            // Module 
            data.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_MODULE));
            data.AddVariableValue (Resources.RES_EMPTY_MODEL_MODULE_NAME_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());

            // Message 
            data.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_MESSAGE));
            data.AddVariableValue (Resources.RES_EMPTY_MODEL_MESSAGE_NAME_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());

            // Speech 
            data.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_SPEECH));
            data.AddVariableValue (Resources.RES_EMPTY_MODEL_SPEECH_NAME_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());

            data.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_SPEECH_ENABLE));
            data.AddVariableValue (Resources.RES_FALSE_MODEL_SPEECH_ENABLE_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());
        }

        void CreateHandlerData ()
        {
            var modelData = TModelData.Create (Services, Module);

            #region common
            // Module
            modelData.ModuleModel.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_MODULE));
            modelData.ModuleModel.AddVariableValue (ModuleName);

            // Message
            modelData.MessageModel.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_MESSAGE));

            // Speech
            modelData.SpeechModel.AddVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_SPEECH));
            modelData.SpeechModel.AddEnableVariableName (TEnumExtension.AsString (UVariableName.DEVICE_INIT_SPEECH_ENABLE));
            modelData.SpeechModel.AddEnableVariableValue (Resources.RES_TRUE);
            #endregion

            // Text and Message - Briefing
            #region Briefing
            modelData.SpeechModel.EnableWaitingFlag ();
            modelData.SpeechModel.AddVariableValue ("game state not ready, waiting for aircraft on ground");
            modelData.MessageModel.AddVariableValue (TEnumExtension.AsString (SimulationGamestate.Briefing));

            // only speech is enabled
            modelData.ModuleModel.DisableEnableFlag ();
            modelData.MessageModel.DisableEnableFlag ();
            modelData.ReceiverModel.DisableEnableFlag ();

            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 

            // all models enabled (restore default)
            modelData.EnableAllModels ();
            modelData.ClearWaitingModels ();
            #endregion

            // Text and Message - Flying
            #region Flying
            modelData.SpeechModel.AddVariableValue (
                    "check instructions: aircraft cold and dark mode\r\n park break on:\r\n honeycomb device : all switches: off\r\n all levers: idle:\r\n gear down:"
                );

            modelData.MessageModel.AddVariableValue (TEnumExtension.AsString (SimulationGamestate.Flying));

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion

            // Text and Message - Waiting...
            #region Waiting...
            modelData.SpeechModel.EnableWaitingFlag ();
            modelData.SpeechModel.AddVariableValue (
                    "when ready: set beacon switch on and off : \r\n waiting..."
                );

            modelData.MessageModel.AddVariableValue ("Waiting");
            modelData.ReceiverModel.EnableReceiverNameEmptyFlag ();
            modelData.UserActionModel.AddUserActionCode (UUserActionCode.WAIT_DONE);

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion

            // Text and Message - Done...
            #region Done...
            modelData.SpeechModel.ClearWaitingFlag ();
            modelData.SpeechModel.AddVariableValue (
                    "done"
                );

            modelData.MessageModel.AddVariableValue ("Done");
            modelData.UserActionModel.RemoveUserActionCode ();

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
        #endregion
    };
    //---------------------------//

}  // namespace
