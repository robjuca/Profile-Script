/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Action.Steps;
using rr.Library.EventAggregator;
using rr.Library.Types;
using rr.Provider.Message;
using rr.Provider.Presentation;
using rr.Provider.Resources;
using rr.Provider.Resources.Properties;

using SPAD.neXt.Interfaces;

using System.Collections.Generic;
using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Plate
{
    //----- TGroundOpePresentation
    [Export (typeof (IDefault))]
    public class TGroundOpePresentation : TModulePresentation, IModule
    {
        #region IModule
        public UModuleId ModuleId => Module;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public TGroundOpePresentation (
           IEventAggregator eventAggregator, IProviderServices services)
               : base (eventAggregator, services, UModuleId.GROUND_OPE)
        {
            ModuleData = [];
            ActionPresentation = TModuleActionPresentation.Create (Module);

            ActionPresentation.NextModule += OnNextModule;
        }
        #endregion

        #region Override
        public override void Handle (TMessageInternal message)
        {
            if (message.ValidateReceiver (TypeInfo)) {
                // INITIALIZE
                if (message.IsAction (UMessageAction.INITIALIZE)) {
                    CreateModuleData (); // just once
                }

                // PROFILE_UNLOADED
                if (message.IsAction (UMessageAction.PROFILE_UNLOADED)) {
                    ClearActiveModule ();
                    ActionPresentation.Cleanup ();
                }

                // NEXT_MODULE
                if (message.IsAction (UMessageAction.NEXT_MODULE)) {
                    if (message.RequestParam (out TModuleData data)) {
                        ClearActiveModule ();

                        if (IsMySelf (data.NextModule)) {
                            SelectActiveModule (Module);
                            ActionPresentation.Ready (ModuleData);
                            ActionPresentation.NextCodeId (UCodeId.C0DE_100);
                        }
                    }
                }

                if (IsActiveModule) {
                    // SCRIPT_ACTION
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
        void OnNextModule (object sender, TModuleData data)
        {
            if (IsActiveModule) {
                ClearActiveModule ();

                var message = TMessageInternal.CreateDefault (Module, UMessageAction.NEXT_MODULE);
                message.SelectParam (TParamInfo.Create (data));
                message.AddDestinationModule (data.NextModule);

                Publish (message);
            }
        }
        #endregion

        #region Property
        List<TModuleData> ModuleData { get; set; }
        TModuleActionPresentation ActionPresentation { get; set; }
        #endregion

        #region Support
        void CreateModuleData ()
        {
            ModuleData.Clear ();

            var data = TModuleData.Create (Module, UCodeId.C0DE_100);
            data.AddSpeech (Resources.RES_GroundOpeInit);
            ModuleData.Add (data);

            data = TModuleData.Create (Module, UCodeId.C0DE_200);
            data.AddSpeech (Resources.RES_GroundOpeCode);
            ModuleData.Add (data);
            data.AddNextModule (UModuleId.OUTSIDE_OPE);
        }
        #endregion
    };
    //---------------------------//

}  // namespace
