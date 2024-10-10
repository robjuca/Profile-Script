/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using SPAD.neXt.Interfaces;
using SPAD.neXt.Interfaces.Events;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerSpeech
    public class THandlerSpeech
    {
        #region Members
        public void Process (THandlerSpeechData data) => Select (data);

        public void ActionReturnCode (THandlerSpeechData data)
        {
            if (data is not null) {
                if (data.CleanupEnable) {
                    CleanupEnable (data);
                }

                if (data.CleanupSpeech) {
                    CleanupSpeech (data);
                }
            }
        }
        #endregion

        #region Support
        bool Select (THandlerSpeechData data)
        {
            var res = false;

            if (data is not null) {
                //if (data.HasModule) {
                //    res = true;
                //    EventSystem.CreateNewLocal (data.SpeechResource, "String", VARIABLE_SCOPE.SESSION, data.SpeechText);
                //    EventSystem.CreateNewLocal (data.SpeechEnableResource, "Int", VARIABLE_SCOPE.SESSION, 1);
                //}
            }

            return res;
        }

        void CleanupSpeech (THandlerSpeechData data) => EventSystem.CreateNewLocal (
                data.SpeechText,
                "String",
                VARIABLE_SCOPE.SESSION,
                string.Empty);

        void CleanupEnable (THandlerSpeechData data) => EventSystem.CreateNewLocal (
                data.SpeechEnableResource,
                "Int",
                VARIABLE_SCOPE.SESSION,
                0);
        #endregion

        #region Static
        public static THandlerSpeech Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
