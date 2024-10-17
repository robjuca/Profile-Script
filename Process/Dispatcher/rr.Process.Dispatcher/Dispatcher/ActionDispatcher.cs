/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Library.Types;
using rr.Provider.Message;
using rr.Provider.Presentation;
using rr.Provider.Resources;
using rr.Provider.Services;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;
using SPAD.neXt.Interfaces.Scripting;

using System;
using System.ComponentModel.Composition;
//---------------------------//

namespace rr.Process.Dispatcher
{
    [Export (typeof (IDefault))]
    //----- TActionDispatcher
    public class TActionDispatcher : TModulePresentation
    {
        #region Constructor
        [ImportingConstructor]
        public TActionDispatcher (IEventAggregator eventAggregator, IProviderServices services)
            : base (eventAggregator, services, UHandlerModule.PROCESS_DISPATCHER)
        {
            TActionScriptDispatch.ActionDispatcher += OnActionDispatcher;
        }
        #endregion

        #region Override
        public override void Handle (TMessageInternal message)
        {
            if (message.IsModuleReceiver (JustMe)) {
                var modules = message.RequestDestinationModules;

                foreach (var module in modules) {
                    message.SwapReceiver (module);

                    Publish (message.Clone ());
                }
            }
        }
        #endregion

        #region Event
        public void OnActionDispatcher (object sender, TScriptActionDispatcherEventArgs eventArgs)
        {
            var message = TMessageInternal.CreateDefault (Module, UMessageAction.SCRIPT_ACTION);
            message.SelectParam (TParamInfo.Create (eventArgs));

            foreach (UHandlerModule module in AllModules) {
                message.SwapReceiver (module);

                Publish (message.Clone ());
            }
        }
        #endregion

        #region Spad.next action
        public class TActionScriptDispatch : IScriptAction
        {
            #region Interface
            public void Execute (IApplication app, ISPADEventArgs eventArgs)
            {
                ActionDispatcher?.Invoke (this, TScriptActionDispatcherEventArgs.Create (eventArgs));
            }
            #endregion

            #region Event
            public static event EventHandler<TScriptActionDispatcherEventArgs> ActionDispatcher;
            #endregion
        };
        #endregion
    };
    //---------------------------//

}  // namespace
