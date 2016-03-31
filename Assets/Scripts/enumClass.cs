using UnityEngine;
using System.Collections;


    #region convoNode Enums
    public enum nodeType { textOnly, question, both }; // An enum we use to determine how our textboxes should react to us.
    #endregion

    #region NPC Enums
    //Tells an NPC what it should be doing.
    public enum NPCState { Idle, Walking, Inanimate };
    
    //THIS tells us whether or not our NPC's origin boundaries should treat the origin point as the left side, center, or right side of the boundary.
    public enum BoundaryPositionType { LeftSide, Center, RightSide };
    #endregion

    #region eventScript Enums
    //Defines the type of Event. NPCEvents change around convoNodes of events. TimerEvents add a (poitive or negative) integer to the eventTracker's timer stat. They can also tell whether or not to activate TimerMode for the evenetTracker.
    public enum eventNodeType {NPCEvent, TimerEvent};

    #endregion

