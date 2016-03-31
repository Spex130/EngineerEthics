using UnityEngine;
using System.Collections;


public class eventScript : MonoBehaviour {

    public string name;

    //Keylist and ValueList correspond to each other, and should obviously share the same length.

    [Tooltip("Represents the names of the NPCs we want changed.\n\nIDs correspond to [valueList]'s so the sizes for both should match.")]
    public string[] keyList;
    [Tooltip("Represents the convoNode we want to give the NPC when this event is run through the eventTracker.\n\nIDs correspond to [keyList]'s so the sizes for both should match.")]
    public convoNode[] valueList;

    //This hashtable contains a list of all the NPCs we want changed with this event.
    public Hashtable changeList = new Hashtable();

    [Tooltip("Tells the eventTracker how to react to this node.\n\nDo note that NPCEvents still pass in Time variables, and they take effect immediately.\n\nTimerEvents deal only with passing in the EventNode itself. The eventTracker will activate this by itself later..")]
    public eventNodeType eventType = eventNodeType.NPCEvent;

    [Tooltip("How much to add or remove from the eventTracker's timer.")]
    public int adjustAmount = 0;

    [Tooltip("Tells whether or not the Timer should be activated.")]
    public bool shouldActivateTimer = false;


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

}
