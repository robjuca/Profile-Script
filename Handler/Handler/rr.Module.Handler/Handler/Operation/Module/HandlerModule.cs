/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerModule
    public class THandlerModule
    {
        #region Members
        public void Process (THandlerModuleData handlerModuleData) => Select (handlerModuleData);
        #endregion

        #region Support
        void Select (THandlerModuleData handlerModuleData)
        {
            //if (handlerModuleData.HasModule) {
            //    res = true;

            //    //EventSystem.CreateNewLocal (
            //    //    Resources.RES_MODULE_ID,
            //    //    "String",
            //    //     VARIABLE_SCOPE.SESSION,
            //    //     handlerModuleData.ModuleActionCondition);
            //}

        }
        #endregion

        #region Static
        public static THandlerModule Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
