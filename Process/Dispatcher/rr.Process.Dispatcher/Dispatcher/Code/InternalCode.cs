/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Profile.Resources;
//---------------------------//

namespace rr.Profile.Dispatcher
{
    //----- TInternalCode
    public class TInternalCode
    {
        #region Property
        public TInternalCodeData Code { get; private set; }
        #endregion

        #region Constructor
        TInternalCode ()
        {
        }
        #endregion

        #region Members
        //public void CreateCode (
        //    TPlateModule sender,
        //    TPlateModule receiver,
        //    TMessageAction messageCode,
        //    TProfileSpeech.KeyNames nameId) => Code = TInternalCodeData.Create (sender, receiver, messageCode, nameId);

        //public void CreateCode (
        //   TPlateModule sameModule,
        //   TMessageAction messageCode, 
        //   TProfileSpeech.KeyNames nameId) => Code = TInternalCodeData.Create (sameModule, messageCode, nameId);
        #endregion

        #region Static
        public static TInternalCode Create () => new ();
        #endregion
    };
    //---------------------------//

}  // namespace
