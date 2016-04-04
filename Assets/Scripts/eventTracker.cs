using UnityEngine;
using System.Collections;

/// <summary>
/// Okay, the long and short of it is that this class listens for "Events"
/// When an event happens, The event class contains a hashtable with Name keys and ConvoNode values.
/// The eventTracker finds matches in names, then passes each NPC its new ConvoNode based on the event.
/// 
/// For example, if you've already talked to your Boss, then everyone else should know that, right? This handles that.
/// 
/// This also handles "Timed Events." These are events where talking to people will subtract from your "Total Time Allotment."
/// If you have 15 "Minutes" to talk to 5 people and each conversation costs 5 "minutes" 
/// </summary>

public class eventTracker : MonoBehaviour {


    [Tooltip("This is the list of LITERALLY EVERYTHING in the scene that can generate a convonode. If it's an NPC relevant to the plot, put it here.")]
    public NPC[] sceneInteractables;
    public Hashtable hashList;
    //We're going to convert all of the NPCs in the scene into a hashtable with the Keys based on the NPC names.
    //Unfortunately, this means that no NPC can share a name with another NPC. 

    [Tooltip("This is our 'Time Countdown' currency holder. Whenever we do something that costs Time, we remove it from this value.")]
    public int timeLeft = 60;
    [Tooltip("This is the eventScript that gets activated when the timer hits 0.")]
    public eventScript timeEvent;
    [Tooltip("If the timeLeft variable hits zero while this bool is active, whatever's in our TimeEvent variable ")]
    public bool timerActivated = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void populateHashtable()
    {
        for (int i = 0; i < sceneInteractables.Length; i++)
        {
            hashList.Add(sceneInteractables[i].name, sceneInteractables[i]);
            //We add in each NPC to the list so it can be searched for by name later.
        }           
    }

    //This takes in an event and then applies the changes from that event to every NPC in the scene.
    public void loadEvent(eventScript newEvent)
    {

        
        
        switch (newEvent.eventType)
        {
            case eventNodeType.NPCEvent://NPCEvents change the NPC's speeches, and they change the eventTracker's timer settings.
                string tempName;
                for (int i = 0; i < sceneInteractables.Length; i++)//We check every NPC in the scene to see if it should be changed.
                {
                    tempName = sceneInteractables[i].name;//We take the name of our NPC
                    if (newEvent.changeList.Contains(tempName))//If the name is found in the Event's list of NPCs who need stuff changed...
                    {
                        sceneInteractables[i].myConvo = (convoNode)newEvent.changeList[tempName];//...Then we change that NPC's convoNode.
                    }
                }
                timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
                if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
                timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
            break;

            case eventNodeType.TimerEvent://TimerEvents only set up Events for the Timer to run later!
                timeEvent = newEvent;
                timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
                if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
                timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
                break;

            case eventNodeType.AddAnswerEvent:

            break;

            default:

            break;
        }

        
        
        //After we've done our Timer Math, check if our Event should be run!
        if (timeLeft <=0 && timerActivated)
        {
            timerActivated = false;
            string tempName;
                for (int i = 0; i < sceneInteractables.Length; i++)//We check every NPC in the scene to see if it should be changed.
            {
                tempName = sceneInteractables[i].name;//We take the name of our NPC
                if (newEvent.changeList.Contains(tempName))//If the name is found in the Event's list of NPCs who need stuff changed...
                {
                    sceneInteractables[i].myConvo = (convoNode)newEvent.changeList[tempName];//...Then we change that NPC's convoNode.
                }
            }
            timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
            if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
            timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
        }
       
    }
}
