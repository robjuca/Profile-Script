﻿/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Provider.Message;
using rr.Provider.Presentation;
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;
using rr.Provider.Services;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Shell
{
    //----- TBootstrapperPresentation
    [Export (typeof (IDefault))]
    [method: ImportingConstructor]
    public class TBootstrapperPresentation (IEventAggregator events, IProviderServices services)
        : TModulePresentation (events, services, UHandlerModule.SHELL_BOOTSTRAPPER)
    {
        #region Overrides
        public override void Handle (TMessageInternal message)
        {
            if (message.ValidateReceiver (TypeInfo)) {
                // INITIALIZE
                if (message.IsAction (UMessageAction.INITIALIZE)) {
                    if (message.RequestParam (out IApplication app)) {
                        app.SubscribeToSystemEvent (SPADSystemEvents.ProfileChanged, OnProfileChanged);

                        message.SelectSenderModule (Module);
                        message.SelectReceiverModule (UHandlerModule.PROCESS_DISPATCHER);
                        message.AddDestinationModule (AllModules);

                        Publish (message.Clone ());
                    }
                }
            }
        }
        #endregion

        #region Event
        public void OnProfileChanged (object sender, ISPADEventArgs e)
        {
            if (e.NewValue is string newVal) {
                ProfileLoad = newVal.Equals (Resources.RES_PROFILE_NAME);

                var message = TMessageInternal.CreateDefault (sender: Module, messageAction: UMessageAction.PROFILE_LOADED);
                message.AddDestinationModule (AllModules);

                if (ProfileLoad) {
                    Initialize (); // Only when profile is loaded.
                }

                else {
                    message.SelectAction (UMessageAction.PROFILE_UNLOADED);
                }

                Publish (message);
            }
        }
        #endregion

        #region Property
        bool ProfileLoad { get; set; }
        #endregion

        #region Support
        void Initialize ()
        {
            // Receiver
            var dataList = new List<TScriptDefinitionData> ();
            var scriptData = TScriptDefinitionData.CreateDefault ();

            var variablesNames = Enum.GetNames (typeof (UReceiverModule));

            foreach (string name in variablesNames) {
                scriptData.AddVariableName (name);
                scriptData.AddVariableValue (Resources.RES_FALSE);

                dataList.Add (scriptData.Clone ());
            }

            Services.CreateScriptDataValue (dataList); // create variables

            // User Action Variable
            scriptData.AddVariableName (Resources.RES_USER_ACTION_CODE);
            scriptData.AddVariableValue (Resources.RES_ZERO_STRING);

            Services.SetScriptDataValue (scriptData);
        }
        #endregion
    };
    //---------------------------//

}  // namespace
