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
    //----- THandlerData
    public class THandlerData : System.IEquatable<THandlerData>, System.IComparable<THandlerData>
    {
        #region Property
        public UHandlerModule Module { get; private set; }
        public bool HasModule => Module.Equals (UHandlerModule.NONE) is false;
        public int HandlerIndex { get; private set; }
        public bool ValidateHandlerSpeech => HandlerSpeechData.Validate;
        public bool ValidateHandlerModule => HandlerModuleData.Validate;
        public bool ValidateHandlerMessage => HandlerMessageData.Validate;
        public bool ValidateHandlerReceiver => ValidateHandlerModule & ValidateHandlerMessage;

        public IProviderServices Services { get; set; }
        #endregion

        #region Handler Data
        public TSpeechModel HandlerSpeechData { get; private set; }
        public THandlerModuleData HandlerModuleData { get; private set; }
        public THandlerMessageData HandlerMessageData { get; private set; }
        #endregion

        #region Interface
        public int CompareTo (THandlerData compareStep) => compareStep is null ? 1 : HandlerIndex.CompareTo (compareStep.HandlerIndex);
        public bool Equals (THandlerData other) => other is not null && HandlerIndex.Equals (other.HandlerIndex);
        public override bool Equals (object other) => (other is THandlerData data) && Equals (data);
        public override int GetHashCode () => HandlerIndex;
        #endregion

        #region Members
        public void EnableAll ()
        {
            HandlerSpeechData.EnableHandler ();
            HandlerModuleData.EnableHandler ();
            HandlerMessageData.EnableHandler ();
        }

        public void PumpHandlerIndex () => HandlerIndex++;
        public void SetHandlerIndex (int index) => HandlerIndex = index;
        public void ClearPumpHandler () => HandlerIndex = 0;

        public THandlerData Clone ()
        {
            var clone = Create (Services, Module);
            clone.SetHandlerIndex (HandlerIndex);
            clone.HandlerSpeechData.CopyFrom (HandlerSpeechData);
            clone.HandlerModuleData.CopyFrom (HandlerModuleData);
            clone.HandlerMessageData.CopyFrom (HandlerMessageData);

            return clone;
        }
        #endregion

        #region Static
        public static THandlerData Create (IProviderServices services, UHandlerModule handlerModule)
        {
            return new () {
                Services = services,
                HandlerIndex = 0,
                Module = handlerModule,
                HandlerSpeechData = TSpeechModel.Create (services, handlerModule),
                HandlerModuleData = THandlerModuleData.Create (services, handlerModule),
                HandlerMessageData = THandlerMessageData.Create (services, handlerModule)
            };
        }
        #endregion
    };
    //---------------------------//

}  // namespace
