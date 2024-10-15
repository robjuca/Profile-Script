/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Library.Extension;
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
        #endregion

        #region Handler Data
        public THandlerSpeechData HandlerSpeechData { get; private set; }
        public THandlerModuleData HandlerModuleData { get; private set; }
        public THandlerMessageData HandlerMessageData { get; set; }
        #endregion

        #region Interface
        public int CompareTo (THandlerData compareStep) => compareStep is null ? 1 : TEnumExtension.AsInt (Module).CompareTo (TEnumExtension.AsInt (compareStep.Module));
        public bool Equals (THandlerData other) => other is not null && Module.Equals (other.Module);
        public override bool Equals (object other) => (other is THandlerData data) && Equals (data);
        public override int GetHashCode () => (int) Module;
        #endregion

        #region Static
        public static THandlerData Create (IProviderServices services, UHandlerModule handlerModule)
        {
            return new () {
                Module = handlerModule,
                HandlerSpeechData = THandlerSpeechData.Create (services, handlerModule),
                HandlerModuleData = THandlerModuleData.Create (services, handlerModule),
                HandlerMessageData = THandlerMessageData.Create (services, handlerModule)
            };
        }
        #endregion
    };
    //---------------------------//

}  // namespace
