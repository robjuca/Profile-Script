﻿/*----------------------------------------------------------------
  Copyright (C) 2001 R&R Soft - All rights reserved.
  author: Roberto Oliveira Jucá    
----------------------------------------------------------------*/

//----- Include
//---------------------------//

namespace rr.Provider.Resources
{
    //----- UMessageName 
    public enum UMessageName
    {
        NONE,
        MSG_BEGIN,
        MSG_CLOSE,
        MSG_DONE,
        MSG_END,
        MSG_FUEL_CUTOFF,
        MSG_FUEL_GREEN,
        MSG_FUEL_YELLOW,
        MSG_OPEN,
        MSG_PHASE_FAF,
        MSG_START_LEFT,
        MSG_START_RIGHT,
        MSG_SWITCH_OFF,
        MSG_SWITCH_ON,
        MSG_WIN_CLOSE,
        MSG_WIN_OPEN,
    };
    //---------------------------//

    //----- UMessageValue 
    public enum UMessageValue
    {
        None,
        Begin,
        Close,
        Done,
        End,
        FuelCutoff,
        FuelGreen,
        FuelYellow,
        Open,
        PhaseFAF,
        StarLeft,
        StartRight,
        SwitchOff,
        SwitchOn,
        WinClose,
        WinOpen,
        Waiting,
    };
    //---------------------------//

}  // namespace