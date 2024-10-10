
/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources.Properties;

using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;

using System.Diagnostics.Contracts;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerCondition
    public class THandlerCondition
    {
        #region Members
        public void Process (THandlerData data) => Select (data);
        #endregion

        #region Support
        bool Select (THandlerData data)
        {
            var res = false;

            Contract.Requires (data != null);

            //if (data.HasModule) {
            //    res = true;

            //    //EventSystem.CreateNewLocal (
            //    //    Resources.RES_MODULE_ID,
            //    //    "String",
            //    //     VARIABLE_SCOPE.SESSION,
            //    //     data.ModuleActionCondition);
            //}

            return res;
        }
        #endregion

        #region Static
        public static THandlerCondition Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
