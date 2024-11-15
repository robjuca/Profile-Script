/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using rr.Provider.Resources;
using rr.Provider.Services;
//---------------------------//

namespace rr.Handler.Model
{
    //----- TModelData
    public class TModelData : System.IEquatable<TModelData>, System.IComparable<TModelData>
    {
        #region Property
        public string ModelMessage => MessageModel.MessageName;
        public UHandlerModule Module { get; private set; }
        public bool HasModule => Module.Equals (UHandlerModule.NONE) is false;
        public int HandlerIndex { get; private set; }
        public bool IsModelWaiting => SpeechModel.WaitingFlag | ModuleModel.WaitingFlag | MessageModel.WaitingFlag;
        public IProviderServices Services { get; private set; }
        #endregion

        #region Models
        public TSpeechModel SpeechModel { get; private set; }
        public TModuleModel ModuleModel { get; private set; }
        public TMessageModel MessageModel { get; private set; }
        public TReceiverModel ReceiverModel { get; private set; }
        public TUserActionModel UserActionModel { get; private set; }
        #endregion

        #region Interface
        public int CompareTo (TModelData compareStep) => compareStep is null ? 1 : HandlerIndex.CompareTo (compareStep.HandlerIndex);
        public bool Equals (TModelData other) => other is not null && HandlerIndex.Equals (other.HandlerIndex);
        public override bool Equals (object other) => (other is TModelData data) && Equals (data);
        public override int GetHashCode () => HandlerIndex;
        #endregion

        #region Members
        public void EnableAllModels ()
        {
            SpeechModel.EnableModelFlag ();
            ModuleModel.EnableModelFlag ();
            MessageModel.EnableModelFlag ();
            ReceiverModel.EnableModelFlag ();
            UserActionModel.EnableModelFlag ();
        }

        public void ClearWaitingModels ()
        {
            SpeechModel.ClearWaitingFlag ();
            ModuleModel.ClearWaitingFlag ();
            MessageModel.ClearWaitingFlag ();
            ReceiverModel.ClearWaitingFlag ();
            UserActionModel.ClearWaitingFlag ();
        }

        public void ScriptReturnCode (TReturnCodeArgs eventArgs)
        {
            SpeechModel.ScriptReturnCode (eventArgs);
            ModuleModel.ScriptReturnCode (eventArgs);
            MessageModel.ScriptReturnCode (eventArgs);
            ReceiverModel.ScriptReturnCode (eventArgs);
            UserActionModel.ScriptReturnCode (eventArgs);
        }

        public void Process ()
        {
            ModuleModel.Process ();
            MessageModel.Process ();
            ReceiverModel.Process ();
            UserActionModel.Process ();
        }

        public void ProcessSpeech ()
        {
            SpeechModel.Process ();
        }

        public void Cleanup ()
        {
            SpeechModel.Cleanup ();
            ModuleModel.Cleanup ();
            MessageModel.Cleanup ();
            ReceiverModel.Cleanup ();
            UserActionModel.Cleanup ();
        }

        public void PumpHandlerIndex () => HandlerIndex++;
        public void SetHandlerIndex (int index) => HandlerIndex = index;
        public void ClearHandlerIndex () => HandlerIndex = 0;

        public TModelData Clone ()
        {
            var clone = Create (Services, Module);
            clone.SetHandlerIndex (HandlerIndex);
            clone.SpeechModel.CopyFrom (SpeechModel);
            clone.ModuleModel.CopyFrom (ModuleModel);
            clone.MessageModel.CopyFrom (MessageModel);
            clone.ReceiverModel.CopyFrom (ReceiverModel);
            clone.UserActionModel.CopyFrom (UserActionModel);

            return clone;
        }

        public void UpdateReceiverMessage (TModelData data)
        {
            if (data is not null) {
                ReceiverModel.AddVariableValue (MessageModel.NextMessageFlag ? data.MessageModel.VariableValue : MessageModel.VariableValue);
            }
        }
        #endregion

        #region Static
        public static TModelData Create (IProviderServices services, UHandlerModule handlerModule)
        {
            return new () {
                Services = services,
                HandlerIndex = 0,
                Module = handlerModule,
                SpeechModel = TSpeechModel.Create (services, handlerModule),
                ModuleModel = TModuleModel.Create (services, handlerModule),
                MessageModel = TMessageModel.Create (services, handlerModule),
                ReceiverModel = TReceiverModel.Create (services, handlerModule),
                UserActionModel = TUserActionModel.Create (services, handlerModule),
            };
        }
        #endregion
    };
    //---------------------------//

}  // namespace
