using UnityEngine;
using System.Collections;

public class eventScript : MonoBehaviour {

    //Keylist and ValueList correspond to each other, and should obviously share the same length.
    public string[] keyList;//String name of the NPC we want changed
    public convoNode[] valueList;//A convoNode 

    //This hashtable contains a list of all the NPCs we want changed with this event.
    public Hashtable changeList;

	// Use this for initialization
	void Start () {
        populateHashtable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void populateHashtable()
    {
        if (keyList.Length != 0 || valueList.Length != 0)
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
