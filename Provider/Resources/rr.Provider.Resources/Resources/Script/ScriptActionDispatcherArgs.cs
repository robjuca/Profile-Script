/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
using SPAD.neXt.Interfaces.Events;

using System;
using System.Collections.Generic;
//---------------------------//

namespace rr.Provider.Resources
{
    //----- TScriptActionDispatcherEventArgs
    public class TScriptActionDispatcherEventArgs : EventArgs
    {
        #region Property
        public UHandlerModule Module { get; private set; }
        public int ActionReturnCode { get; private set; }
        public ISPADEventArgs SNEventArgs { get; private set; }
        #endregion

        #region Constructor
        TScriptActionDispatcherEventArgs (ISPADEventArgs eventArgs)
        {
            ActionReturnCode = 0;

            if (eventArgs is not null) {
                if (eventArgs.NewValue is int newVal) {
                    ActionReturnCode = newVal;
                    SNEventArgs = eventArgs;

                    SelecUHandlerModule ();
                }
            }
        }

        static TScriptActionDispatcherEventArgs ()
        {
            /// ROW /COL (SCRIPT PANEL)
            /// SCRIPT_1_1 -> PROFILE (NONE)     SCRIPT_1_2 -> GROUND_OPE,    SCRIPT_1_3 -> BATTERY_OPE,      SCRIPT_1_4 -> AIRCRAFT_OPE,
            /// SCRIPT_2_1 -> DEVICE_INIT,       SCRIPT_2_2 -> OUTSIDE_OPE,   SCRIPT_2_3 -> FUEL_OPE,         SCRIPT_2_4 -> FLYING_OPE,     SCRIPT_2_8 ->  GROUND_EVENT,
            /// SCRIPT_3_1 -> SETUP_INIT,        SCRIPT_3_2 -> DOOR_OPE,      SCRIPT_3_3 -> ENGINE_OPE,                                     SCRIPT_3_8 ->  FLYING_EVENT,


            m_Plates = [];
            m_Plates.Add ("SCRIPT_1_2", UHandlerModule.GROUND_OPE);
            m_Plates.Add ("SCRIPT_1_3", UHandlerModule.BATTERY_OPE);
            m_Plates.Add ("SCRIPT_1_4", UHandlerModule.AIRCRAFT_OPE);
            m_Plates.Add ("SCRIPT_2_1", UHandlerModule.DEVICE_INIT);
            m_Plates.Add ("SCRIPT_2_2", UHandlerModule.OUTSIDE_OPE);
            m_Plates.Add ("SCRIPT_2_3", UHandlerModule.FUEL_OPE);
            m_Plates.Add ("SCRIPT_2_4", UHandlerModule.FLYING_OPE);
            m_Plates.Add ("SCRIPT_2_8", UHandlerModule.GROUND_EVENT);
            m_Plates.Add ("SCRIPT_3_2", UHandlerModule.DOOR_OPE);
            m_Plates.Add ("SCRIPT_3_3", UHandlerModule.ENGINE_OPE);
            m_Plates.Add ("SCRIPT_3_8", UHandlerModule.FLYING_EVENT);
        }
        #endregion

        #region Fields
        readonly static Dictionary<string, UHandlerModule>                        m_Plates;
        #endregion

        #region Support
        void SelecUHandlerModule ()
        {
            if (SNEventArgs is not null) {
                if (SNEventArgs.EventName is string plate) {
                    foreach (var item in m_Plates) {
                        if (item.Key.Equals (plate)) {
                            Module = item.Value;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region Static
        public static TScriptActionDispatcherEventArgs Create (ISPADEventArgs eventArgs) => new (eventArgs);
        #endregion
    };
    //---------------------------//

}  // namespace
