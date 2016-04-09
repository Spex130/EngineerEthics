using UnityEngine;
using System.Collections;


public class convoNode : MonoBehaviour {

    /// <summary>
    /// This is a node in a tree structure meant to mostly load info into the textboxes and ask boxes.
    /// </summary>

    

    [Tooltip("We use this Enum to set how our TextBox or Askbox react to this ConvoNode loading.")]
    public nodeType myType = nodeType.textOnly;

    [Tooltip("The ENTIRE text you want the character to say. Each Array entry counts as a box. The display resets for each box.")]
    public string[] convoTextArray;

    #region Choice Stuff
    [Tooltip("The question that is asked of us. Is displayed at the top of the askBox when activated.")]
    public string question;

    [Tooltip("The ID of the next node we want to head toward. Used as the Index for the [endAnswerArray].\n\nIt also is used to activate corresponding entries in [nextNodeArray] and [endEventArray].")]
    public int choiceIndex = 0;

    [Tooltip("This array provides the text 'answers' to the [question] string")]
    public string[] endAnswerArray;


    [Tooltip("Tells whether or not we are a leaf at the end of our path's tree. Leaving this empty means we won't check the [nextNodeArray].")]
    public bool hasNext;
    [Tooltip("An array of convoNodes. The order of the convonodes corresponds to the order of the answers in the endAnswerArray. Meant to make a tree structure out of the nodes.\n\nMust be in size ≥ the [endAnswerArray], but null entries are accepted. They simply do nothing when chosen.")]
    public convoNode[] nextNodeArray;

    [Tooltip("An array of eventScripts. The order of the eventScripts corresponds to the order of the answers in the [endAnswerArray]. Allows for talking to NPCs to change how other NPCs talk to you.\n\nMust be in size ≥ the [endAnswerArray], but null entries are accepted. They simply do nothing when chosen.")]
    public eventScript[] endEventArray;// The results of the answer you chose for the question.
    public bool hasEndEvent = false;

    [Tooltip("If [hasEndEvent] is true this eventScript gets called when this convoNode terminates. It gets called whether the convoNode simply reads out text or if it asks you a question.\n\nDo note that Events activated by questions ALSO run, which means you can cause two events to run at the same time.")]
    public eventScript endEvent;

    #endregion

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

    //This allows us to dynamically add in answers from other places, which means less complexity for us to deal with.
    public void addQuestion(string answer, eventScript newEvent)
    {
        string[] tempAnswer = new string[endAnswerArray.Length + 1];//Make a temp array, since we can't resize arrays.
        for (int i = 0; i < endAnswerArray.Length; i++)
        {
            tempAnswer[i] = endAnswerArray[i];//Add everything in.
        }
        endAnswerArray = tempAnswer;//Set the old to the new.
        endAnswerArray[endAnswerArray.Length-1] = answer;//Add in our new Answer

        //Repeat
        eventScript[] tempEvent = new eventScript[endEventArray.Length];
        for (int i = 0; i < endEventArray.Length; i++)
        {
            tempEvent[i] = endEventArray[i];
        }
        endEventArray = tempEvent;
        endEventArray[endEventArray.Length-1] = newEvent;


    }

    public void addQuestion(string answer)//Adds an Answer to the array without making it link to an event.
    {
        string[] tempAnswer = new string[endAnswerArray.Length + 1];//Make a temp array, since we can't resize arrays.
        for (int i = 0; i < endAnswerArray.Length; i++)
        {
            tempAnswer[i] = endAnswerArray[i];//Add everything in.
        }
        endAnswerArray = tempAnswer;//Set the old to the new.
        endAnswerArray[endAnswerArray.Length-1] = answer;//Add in our new Answer

        //We still have to resize the array anyways though.
        eventScript[] tempEvent = new eventScript[endEventArray.Length];
        for (int i = 0; i < endEventArray.Length; i++)
        {
            tempEvent[i] = endEventArray[i];
        }
        endEventArray = tempEvent;


    }
}
