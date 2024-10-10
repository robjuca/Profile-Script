/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;

using System;
//---------------------------//

namespace rr.Module.Handler
{
    #region Enum
    public enum UReturnCodeId
    {
        NONE = 0,
        SPEECH_DISABLE,
        SPEECH_DONE,
        NEXT_STEP,
        NEXT_MODULE,
        INTERNAL_CODE,
    };
    //---------------------------//
    #endregion

    //----- THandlerActionArgs
    public class THandlerActionArgs : EventArgs
    {
        #region Property
        public UReturnCodeId CodeType { get; set; }
        public UHandlerModule Module { get; set; }
        public int ActionReturnCode { get; set; }
        public bool IsCode40 => CodeType.Equals (UReturnCodeId.SPEECH_DONE);
        public bool IsNextStep => CodeType.Equals (UReturnCodeId.NEXT_STEP);
        public bool IsNextModule => CodeType.Equals (UReturnCodeId.NEXT_MODULE);
        public bool IsUCodeId => CodeType.Equals (UReturnCodeId.INTERNAL_CODE);
        public bool IsEmpty => CodeType.Equals (UReturnCodeId.NONE);
        #endregion

        #region Members
        public bool HasReturnCode (UReturnCodeId codeType) => CodeType.Equals (codeType);
        #endregion

        #region Static
        public static THandlerActionArgs CreateDefault ()
        {
            var obj = new THandlerActionArgs {
                //CodeType = UReturnCodeId.NONE,
                //Module = UHandlerModule.NONE,
                ActionReturnCode = 0
            };

            return obj;
        }
        public static THandlerActionArgs Create (UReturnCodeId codeType, TActionDispatcherEventArgs eventArgs)
        {
            var obj = new THandlerActionArgs {
                CodeType = codeType,
                Module = eventArgs.Module,
                ActionReturnCode = eventArgs.ActionReturnCode
            };

            return obj;
        }
        #endregion
    };
    //---------------------------//

}  // namespace
