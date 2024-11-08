/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.EventAggregator;
using rr.Library.Extension;
using rr.Library.Types;
using rr.Provider.Message;
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Provider.Presentation
{
    //----- TModulePresentation
    public class TModulePresentation : THandleMessage, IModulePresentation
    {
        #region Property
        protected IDelegateCommand DelegateCommand => PresentationCommand as IDelegateCommand;
        protected TModuleInfo Sender => TModuleInfo.Create (TypeInfo);
        protected TModuleInfo JustMe => Sender;
        protected UHandlerModule Module { get; private set; }
        protected string ModuleName => TEnumExtension.AsString (Module);  
        protected bool IsActiveModule => ActiveModule.Equals (Module);
        protected IProviderServices Services { get; private set; }
        protected System.Array AllModules => Services.AllHandlerModule;

        #endregion

        #region Constructor
        protected TModulePresentation (IEventAggregator eventAggregator, IProviderServices services, UHandlerModule moduleName)
            : base (eventAggregator)
        {
            TypeName = GetType ().Name;
            Module = moduleName;
            //ActiveModule = UHandlerModule.NONE;

            SetPresentationCommand (this);

            ChangeTypeInfo (moduleName);

            Services = services;
        }
        #endregion

        #region Interface
        public void PublishInternalMessageHandler (TMessageInternal message) => PublishOnBackgroundThreadAsync (message);
        #endregion

        #region Members
        protected bool IsMySelf (UHandlerModule module) => Module.Equals (module);
        protected void Publish (TMessageInternal message) => DelegateCommand.PublishInternalMessage.Execute (message);
        protected void SetPresentationCommand (IModulePresentation presentation) => SetPresentationCommand (TPresentationCommand.Create (presentation));
        protected void ChangeTypeInfo (UHandlerModule module) => ChangeTypeInfo (TTypeInfo.Create (TEnumExtension.NameOf (module), TypeName, "TypeInfo"));
        protected void ChangeTypeInfo (string moduleName) => ChangeTypeInfo (TTypeInfo.Create (moduleName, TypeName, "TypeInfo"));
        protected void SelectActiveModule (UHandlerModule moduleName) => ActiveModule = moduleName;
        //protected void ClearActiveModule () => ActiveModule = UHandlerModule.NONE;
       
        #endregion

        #region Property
        UHandlerModule ActiveModule { get; set; }
        #endregion
    };
    //---------------------------//

}  // namespace
