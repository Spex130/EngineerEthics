using UnityEngine;
using System.Collections;

public class convoNode : MonoBehaviour {

    /// <summary>
    /// This is a node in a tree structure meant to mostly load info into the textboxes and ask boxes.
    /// 
    /// 
    /// </summary>

    public enum nodeType {textOnly, question, both }; // An enum we use to determine how our textboxes should react to us.
    
    public nodeType myType = nodeType.textOnly;//Our personal version of our Enum to track ourselves with.


    [Tooltip("//An array of convoNodes. The order of the convonodes corresponds to the order of the answers in the endQuestionArray. Meant to make a tree structure out of the nodes.")]
    public convoNode[] nextNodeArray;

    [Tooltip("Tells whether or not we are a leaf at the end of our path's tree.")]
    public bool hasNext;
    public int choiceIndex = 0;//The ID of the next node we want to hea d toward
    public string[] convoTextArray;//The ENTIRE text you want the character to say

    public string question;//The question we ponder the the answer to.
    public string[] endQuestionArray;//The Answers you want the question to ask at the end.

    public ArrayList[] endBranchArray;// The resulsts of the answer you chose for the question.
    //This arraylist either contains an EVENT, a CONVONODE (which continues the conversation, or NOTHING.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Returns the default next node
    public convoNode getNextNode()
    {
        return nextNodeArray[0];
    }


    //Returns the node requested by the askBox
    public convoNode getNextNode(int index)
    {
        return nextNodeArray[index];
    }
}
