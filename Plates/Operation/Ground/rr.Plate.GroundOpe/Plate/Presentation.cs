﻿/*----------------------------------------------------------------
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

using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Plate
{
    #region Data
    //----- UVariableName
    enum UVariableName               // (Variable Name)
    {
        // Module 
        GROUND_OPE_MODULE,            // must match with RECEIVER_MODULE_NAME

        // Message 
        GROUND_OPE_MESSAGE,         // message to decode (UMessageName) RECEIVER_MODULE_MESSAGE

        // Speech 
        GROUND_OPE_SPEECH,            // put text to play here
        GROUND_OPE_SPEECH_ENABLE,          // to play text condition enable (true or false)
    };
    //---------------------------// 
    #endregion

    //----- TGroundOpePresentation
    [Export (typeof (IDefault))]
    public class TGroundOpePresentation : TModulePresentation, IModule
    {
        #region IModule
        public UHandlerModule HandlerModule => Module;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public TGroundOpePresentation (
           IEventAggregator eventAggregator, IProviderServices services)
               : base (eventAggregator, services, UHandlerModule.GROUND_OPE)
        {
            ModelCatalogue = TModelCatalog.Create (HandlerModule);
        }
        #endregion

        #region Override
        public override void Handle (TMessageInternal message)
        {
            if (message.ValidateReceiver (TypeInfo)) {
                // PROFILE_LOADED
                if (message.IsAction (UMessageAction.PROFILE_LOADED)) {
                    ProfileLoad = true;
                }

                // PROFILE_UNLOADED
                if (message.IsAction (UMessageAction.PROFILE_UNLOADED)) {
                    ProfileLoad = false;
                    ModelCatalogue.Cleanup ();
                    ClearActiveModule ();
                }

                // NEXT_MODULE
                if (message.IsAction (UMessageAction.NEXT_MODULE)) {
                    ClearActiveModule ();

                    if (IsMySelf (message.ReceiverModule)) {
                        if (ProfileLoad) {
                            SelectActiveModule (Module);

                            CreateVariables ();
                            CreateHandlerData ();

                            ModelCatalogue.Execute ();
                        }
                    }
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION (from Process_Dispatcher)
                    if (message.IsAction (UMessageAction.SCRIPT_ACTION)) {
                        if (message.RequestParam (out TScriptActionDispatcherEventArgs eventArgs)) {
                            // Model DONE (-80) - must go to next model
                            if (eventArgs.ContainsReturnCode (Resources.RES_NEXT_MODEL_CODE)) {
                                ClearActiveModule ();

                                var msg = TMessageInternal.CreateDefault (Module, UMessageAction.NEXT_MODULE);
                                msg.SelectReceiverModule (UHandlerModule.PROCESS_DISPATCHER);
                                msg.AddDestinationModule (UHandlerModule.OUTSIDE_OPE);

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

        #region Property
        bool ProfileLoad { get; set; }
        TModelCatalog ModelCatalogue { get; set; }
        #endregion

        #region Support
        void CreateVariables ()
        {
            var data = TScriptDefinitionData.CreateDefault ();

            // Module Model
            data.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_MODULE));
            data.AddVariableValue (Resources.RES_EMPTY_MODEL_MODULE_NAME_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());

            // Message Model
            data.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_MESSAGE));
            data.AddVariableValue (Resources.RES_EMPTY_MODEL_MESSAGE_NAME_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());

            // Speech Handler
            data.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_SPEECH));
            data.AddVariableValue (Resources.RES_EMPTY_MODEL_SPEECH_NAME_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());

            data.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_SPEECH_ENABLE));
            data.AddVariableValue (Resources.RES_FALSE_MODEL_SPEECH_ENABLE_VALUE_DEFAULT);
            Services.SetScriptDataValue (data.Clone ());
        }

        void CreateHandlerData ()
        {
            var modelData = TModelData.Create (Services, Module);

            #region common
            // Module
            modelData.ModuleModel.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_MODULE));
            modelData.ModuleModel.AddVariableValue (ModuleName);

            // Message
            modelData.MessageModel.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_MESSAGE));

            // Speech
            modelData.SpeechModel.AddVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_SPEECH));
            modelData.SpeechModel.AddEnableVariableName (TEnumExtension.AsString (UVariableName.GROUND_OPE_SPEECH_ENABLE));
            modelData.SpeechModel.AddEnableVariableValue (Resources.RES_TRUE);
            #endregion

            // Text and Message - Begin...
            #region Waiting...
            modelData.SpeechModel.AddVariableValue (
                    "ground operation start"
                );

            modelData.MessageModel.AddVariableValue (TEnumExtension.NameOf (UMessageValue.Begin));

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion

            // Text and Message - Done...
            #region Done...
            modelData.SpeechModel.AddVariableValue (
                    "ground operation done"
                );

            modelData.MessageModel.AddVariableValue (TEnumExtension.NameOf (UMessageValue.Done));

            modelData.PumpHandlerIndex ();
            ModelCatalogue.AddModelData (modelData.Clone ());  // add to list 
            #endregion
        }
        #endregion
    };
    //---------------------------//

}  // namespace
