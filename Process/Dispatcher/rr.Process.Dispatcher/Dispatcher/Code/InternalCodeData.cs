
/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Profile.Resources;

using System;
//---------------------------//

namespace rr.Profile.Dispatcher
{
    public class TInternalCodeData
    {
        #region Property
        //public string BinaryCodeWordActionRequest => Convert.ToString (ActionRequestCodeWord [1], 2).PadLeft (8, '0') + Convert.ToString (ActionRequestCodeWord [0], 2).PadLeft (8, '0');
        //public string BinaryCodeWordActionResponse => Convert.ToString (ActionResponseCodeWord [1], 2).PadLeft (8, '0') + Convert.ToString (ActionResponseCodeWord [0], 2).PadLeft (8, '0');
        //public int CodeWordActionRequest { get => Convert.ToInt32 (BinaryCodeWordActionRequest, 2); }
        //public int CodeWordActionResponse { get => Convert.ToInt32 (BinaryCodeWordActionResponse, 2); }
        public TMessageAction MessageCode { get; private set; }
        public TPlateModule Sender { get; private set; }
        public TPlateModule Receiver { get; private set; }
        //public TProfileSpeech.KeyNames NameId { get; private set; }
        #endregion

        #region Constructor
        //TInternalCodeData (TPlateModule sender, TPlateModule receiver, TMessageAction messageCode, TProfileSpeech.KeyNames nameId)
        //{
        //    Sender = sender;
        //    Receiver = receiver;
        //    MessageCode = messageCode;
        //    NameId = nameId;

        //    SetupCodeWord (sender, receiver, messageCode);
        //}

        //TInternalCodeData (TPlateModule sameModule, TMessageAction messageCode, TProfileSpeech.KeyNames nameId)
        //{
        //    Sender = sameModule;
        //    Receiver = sameModule;
        //    MessageCode = messageCode;
        //    NameId = nameId;

        //    SetupCodeWord (sameModule, sameModule, messageCode);
        //}
        #endregion

        #region Members
        #endregion

        #region Property
        //byte [] ActionRequestCodeWord;
        //byte [] ActionResponseCodeWord;
        #endregion

        #region Support
        //void SetupCodeWord (TPlateModule sender, TPlateModule receiver, TMessageAction messageCode)
        //{
        //    ActionRequestCodeWord = new byte [] { 0, 0 };
        //    ActionResponseCodeWord = new byte [] { 0, 0 };

        //    byte bS = (byte) ((int) sender);
        //    byte bR = (byte) ((int) receiver);
        //    byte bM = (byte) ((int) messageCode);

        //    byte bAReq =  (int) TInternalActionCode.REQUEST;
        //    byte bARes =  (int) TInternalActionCode.RESPONSE;

        //    // sender, receiver
        //    ActionRequestCodeWord [0] = (byte) (bS + (bR << 4));
        //    ActionResponseCodeWord [0] = (byte) (bS + (bR << 4));

        //    // action Request
        //    ActionRequestCodeWord [1] = (byte) (bAReq + (bM << 4));

        //    // for action Response
        //    ActionResponseCodeWord [1] = (byte) (bARes + (bM << 4));
        //}
        #endregion

        #region Static
        //public static TInternalCodeData Create (
        //   TPlateModule sender,
        //   TPlateModule receiver,
        //   TMessageAction messageCode,
        //   TProfileSpeech.KeyNames nameId) => new TInternalCodeData (sender, receiver, messageCode, nameId);

        //public static TInternalCodeData Create (
        //  TPlateModule sameModule,
        //  TMessageAction messageCode,
        //  TProfileSpeech.KeyNames nameId) => new TInternalCodeData (sameModule, messageCode, nameId);
        #endregion
    };
    //---------------------------//

}  // namespace
