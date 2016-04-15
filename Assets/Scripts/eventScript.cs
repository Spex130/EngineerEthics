using UnityEngine;
using System.Collections;


public class eventScript : MonoBehaviour {

    public string name;

    //Keylist and ValueList correspond to each other, and should obviously share the same length.

    [Tooltip("Used in NPCEvents and addQuestionEvents. Represents the names of the NPCs we want changed.\n\nIDs correspond to [valueList]'s so the sizes for both should match.")]
    public string[] keyList;
    [Tooltip("Used in NPCEvents. Represents the convoNode we want to give the NPC when this event is run through the eventTracker.\n\nIDs correspond to [keyList]'s so the sizes for both should match.")]
    public convoNode[] valueList;

    //This hashtable contains a list of all the NPCs we want changed with this event.
    public Hashtable changeList = new Hashtable();

    [Tooltip("Tells the eventTracker how to react to this node.\n\nDo note that NPCEvents still pass in Time variables, and they take effect immediately.\n\nTimerEvents deal only with passing in the EventNode itself. The eventTracker will activate this by itself later..")]
    public eventNodeType eventType = eventNodeType.NPCEvent;


    //TimerEvent variables
    [Tooltip("Used in TimerEvents & NPCEvents. How much to add or remove from the eventTracker's timer.")]
    public int adjustAmount = 0;

    [Tooltip("Used in TimerEvents & NPCEvents. Tells whether or not the Timer should be activated.")]
    public bool shouldActivateTimer = false;

    //addQuestionEvent variables
    [Tooltip("Used in addQuestionEvents. A list of answers to be added to the convoNodes of the NPCs listed in [keyList]. Also matches IDs with events stored in [eventsToAdd].\n\nMust match the size of the [keyList].")]
    public string[] answersToAdd;

    [Tooltip("Used in addQuestionEvents. A list of events to be added to the convoNodes of the NPCs listed in [keyList].\n\nMatches IDs with strings stored in [answersToAdd], which in turn matches IDs with the NPCs in [keyList].\n\nMust match the size of the [keyList].")]
    public eventScript[] eventsToAdd;

    [Tooltip("Used in addQuestionEvents. A list of convoNodes to be added to the convoNodes of the NPCs listed in [keyList].\n\nMatches IDs with strings stored in [answersToAdd], which in turn matches IDs with the NPCs in [keyList].\n\nMust match the size of the [keyList].\n\nThis makes Answers continue to more conversations.")]
    public convoNode[] nextNodesToAdd;

    //MultiEvent Variables.
    public bool Multi_NPCEvent;
    public bool Multi_TimerEvent;
    public bool Multi_AddQuestionEvent;
    public bool Multi_AddQuestionOneShotEvent;

	// Use this for initialization
	void Start () {
        populateHashtable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void populateHashtable()
    {
        if (keyList.Length == 0 || valueList.Length == 0)
        {
            print("Empty List!");
        }
        else {
            for (int i = 0; i < valueList.Length; i++)
            {
                changeList.Add(keyList[i], valueList[i]);
            }
        }
    }

    //OneshotClear is activated when the eventScript's eventType is addQuestionOneShot. This makes the event entirely incapable of adding any more answers to any other convonodes.
    public void oneshotClear()
    {
        for(int i = 0; i < keyList.Length; i++)
        {
            answersToAdd[i] = null;
            eventsToAdd[i] = null;
            nextNodesToAdd[i] = null;
        }
    }

}
