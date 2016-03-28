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

    //This is the list of LITERALLY EVERYTHING in the scene that can generate a convonode.
    public NPC[] sceneInteractables;
    public Hashtable hashList;
    //We're going to convert all of the NPCs in the scene into a hashtable with the Keys based on the NPC names.
    //Unfortunately, this means that no NPC can share a name with another NPC. 

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

    public void loadEvent(eventScript newEvent)
    {
        string tempName;
        for (int i = 0; i < sceneInteractables.Length; i++)
        {
            tempName = sceneInteractables[i].name;//We take the name of our NPC
            if (newEvent.changeList.Contains(tempName))//If the name is found in the Event's list of NPCs who need stuff changed...
            {
                sceneInteractables[i].myConvo = (convoNode)newEvent.changeList[tempName];//...Then we
            }
        }
    }
}
