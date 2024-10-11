/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Module.Handler
{
    //----- THandlerSpeechData
    public class THandlerSpeechData
    {
        #region Property
        public UHandlerModule HandlerModule { get; private set; }
        public bool HasModule => HandlerModule.Equals (UHandlerModule.NONE) is false;
        public string SpeechText { get; private set; }
        public string SpeechResource { get; private set; }
        public string SpeechResourceEnable { get; private set; }
        public UReturnCodeId ReturnCode { get; set; }
        public bool CleanupEnable => ReturnCode.Equals (UReturnCodeId.SPEECH_DISABLE);
        public bool CleanupSpeech => ReturnCode.Equals (UReturnCodeId.SPEECH_DONE);
        #endregion

        #region Members
        public void AddText (string text) => SpeechText = text;
        public void AddSpeechResource (string data) => SpeechResource = data;
        public void AddSpeechResourceEnable (string data) => SpeechResourceEnable = data;
        #endregion

        #region Static
        static public THandlerSpeechData Create (IProviderServices services, UHandlerModule handler) => new () { HandlerModule = handler };
        #endregion
    };
    //---------------------------//

}  // namespace
