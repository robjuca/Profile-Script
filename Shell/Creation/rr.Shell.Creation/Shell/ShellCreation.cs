/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Bootstrapper;
using rr.Library.Extension;
using rr.Library.Types;
using rr.Provider.Message;
using rr.Provider.Resources;
using rr.Provider.Services;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Scripting;

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
//---------------------------//

namespace rr.Shell
{
    //----- TShellCreation
    public class TShellCreation : TBootstrapper, IScriptCreation
    {
        #region Constructor
        public TShellCreation ()
            : base ()
        {
            TypeName = GetType ().Name;
            TypeInfo = TTypeInfo.Create (TEnumExtension.NameOf (UHandlerModule.SHELL_CREATION), TypeName, ".ctor");
        }
        #endregion

        #region SpadNext
        void IScriptCreation.Initialize (IApplication app)
        {
            var msg = TMessageInternal.CreateDefault (sender: UHandlerModule.SHELL_CREATION, messageAction: UMessageAction.INITIALIZE);
            msg.SelectParam (TParamInfo.Create (app));
            msg.AddDestinationModule (UHandlerModule.SHELL_BOOTSTRAPPER);

            PublishBackgroundAsync (msg);
        }

        void IScriptCreation.Deinitialize ()
        {
        }
        #endregion

        #region Overrides
        protected override void AddService () => Batch.AddExportedValue<IProviderServices> (new TProviderServices ());

        public override void ConfigureCatalog ()
        {
            ModuleList = [];

            var services = GetInstance (typeof (IProviderServices)) as IProviderServices;

            if (services is not null) {
                services.DiscoverModules (ModuleList);

                // myself
                AddToCatalog (new AssemblyCatalog (GetType ().Assembly));

                // others modules
                foreach (Assembly assembly in ModuleList) {
                    AddToCatalog (new AssemblyCatalog (assembly));
                }

                // create all modules
                IEnumerable<object> modules = GetAllInstances (typeof (IDefault));
                services.ConfigureModules (modules);
            }
        }
        #endregion

        #region Property
        string TypeName { get; set; }
        TTypeInfo TypeInfo { get; set; }
        IList<Assembly> ModuleList { get; set; }
        #endregion
    };
    //---------------------------//

}  // namespace