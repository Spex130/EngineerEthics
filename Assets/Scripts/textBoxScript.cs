﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textBoxScript : MonoBehaviour {



//Visualization variables
	public GameObject textStartPoint; //Tells where our text will begin generating.

	public string[] convArray; //Holds the strings we want to display in the textbox.
	private char[] charArray; //Holds our strings, converted to characters
	private int conversationPoint = 0;//Tells us how many nodes into the conversation we are.

	public int lineLength = 25; //Number of characters per line
	public float kerning = 1f;//Space between characters
	public float vSpace = 1f;//Space between lines

//Display Speed variables
	public float textSpeed = 15f;
	public float endLineBlinkSpeed = 1f;
	private float resetTextSpeed;
	private float tinyTimer = 1f;
	private float resetTimer;

//Keeps track of our cursor so we know where we are in the string.
	private int horiIndex = 0;
	private int vertIndex = 0;

	//Branching option variables
	public bool isQuestion = false;//Tells us whether or not this text snippet has a question at the end.
	public bool isAskingQuestion = false;//Tells us whether or not we're in question mode
	public string[] choiceArray; // The strings that make up the answers we can make
								 //public convoNode currentNode; //This is the node we are currently pulling info from

	public int questionLine = 0; //Used to track which line we are currently making.
	public int choiceIndex = 0;//Used for indicating what choice we're thinking of making.

//Determines whether or not we should show the blinking line ending.
	private bool showEndLine = false;
//Determines whether or not we have finished displaying the line
	private bool isFinishedDisplaying = false;
//Determine whether or not we should be able to see the textbox at all.
	public bool showBox = false;
//The textbox we want to edit.
	public UnityEngine.UI.Image textbox;
//Text so we have something to edit
	public Text text;

	// Use this for initialization
	void Start () {
		init();
	}
	
	// Update is called once per frame
	void Update () {
		showTextboxCheck();
		printLoop();

		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			progressConv();
		}

	}

	//The initialization function for when our program has started running.
	public void init()
	{
		text.text = "";
		resetTimer = tinyTimer;
		resetTextSpeed = textSpeed;
		charArray = new char[convArray[conversationPoint].Length];//We get the length of the string in our current conversationPoint.
		isFinishedDisplaying = false;
		conversationPoint = 0;
		prepStrings();
	}

	//Initialize after the program has started
	public void reInit()
	{
		text.text = "";
		tinyTimer = resetTimer;
		textSpeed = resetTextSpeed;
		conversationPoint = 0;
		charArray = new char[convArray[conversationPoint].Length];//We get the length of the string in our current conversationPoint.
		isFinishedDisplaying = false;
		prepStrings();
		horiIndex = 0;
		vertIndex = 0;
	}

	//Makes the box show after resetting it.
	public void startConvo()
	{
		showBox = true;
		reInit();
		textbox.enabled = true;
		text.enabled = true;
	}


	//Overloaded enable that allows for new conversations to be loaded in.
	public void startConvo(string[] newConv)
	{
		showBox = true;
		convArray = newConv;
		reInit();
		enableBox();
	}


	public void endConvo()//Ends the conversation! Use this at the end of EVERYTHING.
	{
		showBox = false;
		disableBox();
	}
	public void enableBox()//This enables the textbox so you can see it.
	{
		textbox.enabled = true;
		text.enabled = true;
	}

	public void disableBox()//This disables the textbox so that you can't see it.
	{
		textbox.enabled = false;
		text.enabled = false;
	}

	//Resets the Text so that the next line can display properly.
	public void resetDisplay()
	{
		tinyTimer = resetTimer;
		textSpeed = resetTextSpeed;
		prepStrings();
		text.text = "";
		showEndLine = false;
		isFinishedDisplaying = false;
		conversationPoint++;
		charArray = new char[convArray[conversationPoint].Length];//We get the length of the string in our current conversationPoint.
		prepStrings();
	}

	public void showTextboxCheck()
	{
		if (!showBox)
		{
			disableBox();
		}
		else
		{
			enableBox();
		}
	}

	//Go 1 step forward in the conversation
	public void progressConv()
	{
		if (showBox) {//If we're actually active....
			if (horiIndex == charArray.Length)
			{ //If our line is finished displaying...
				if (conversationPoint < convArray.Length - 1)
				{//...And we aren't at the end of our conversation, then do this.
					resetDisplay();

				}
				else//If we ARE at the end of our conversation, close the box. OR move to the question at the end.
				{
					if (isQuestion)
					{

					}
					else
					{
						endConvo();
					}
				}
			}
			else//Otherwise, MAKE THE LINE FINISH DISPLAYING.
			{
				while (horiIndex < charArray.Length)
				{
					text.text = text.text + charArray[horiIndex];
					horiIndex++;
					if (horiIndex % lineLength == 0 && horiIndex != charArray.Length)//Check if we've hit the line length by checking if your horiIndex is divisible by our linelength
					{
						switch (charArray[horiIndex])
						{

							case ' ':
								text.text = text.text + "\n"; //We've hit an end, NEXT LINE.
								vertIndex++;//Move down a line if we've hit the end.
								break;
							default:
								text.text = text.text + "-\n"; //We've hit an end, NEXT LINE.
								vertIndex++;//Move down a line if we've hit the end.
								break;
						}
					}
				}
			}
		}
	}

	public void askQuestion()
	{
		if (showBox)
		{//If we're actually active....
			isAskingQuestion = true;//Set us to Ask mode


		}
	}


	public void prepStrings()
	{
		if (conversationPoint < convArray.Length)
		{
			//Convert all of the strings of our current conversationPoint to characters
			charArray = convArray[conversationPoint].ToCharArray();
			horiIndex = 0;
			vertIndex = 0;
		}
	}

	//This method acts as an update loop for when the text box should be displaying normal conversation text.
	public void printLoop()
	{
		if (showBox) {//If we're even active...
			tinyTimer -= Time.deltaTime * textSpeed;

			//Every time we time down to zero, do the thing.
			if (tinyTimer <= 0)
			{

				if (horiIndex < charArray.Length)
				{
					textSpeed = resetTextSpeed;
					//Create a character at the horizontal length
					text.text = text.text + charArray[horiIndex];
					horiIndex++;
					if (horiIndex % lineLength == 0 && horiIndex != charArray.Length)//Check if we've hit the line length by checking if your horiIndex is divisible by our linelength
					{
						switch (charArray[horiIndex])
						{

							case ' ':
								text.text = text.text + "\n"; //We've hit an end, NEXT LINE.
								vertIndex++;//Move down a line if we've hit the end.
								break;
							default:
								text.text = text.text + "-\n"; //We've hit an end, NEXT LINE.
								vertIndex++;//Move down a line if we've hit the end.
								break;
						}
					}
					tinyTimer = resetTimer;
				}

				else
				{
					isFinishedDisplaying = true;
					textSpeed = endLineBlinkSpeed;
					showEndLine = !showEndLine;
					if (showEndLine)
					{
						text.text = text.text + " ■";
					}
					else
					{
						text.text = text.text.Remove(text.text.Length - 2);
					}
					tinyTimer = resetTimer;

				}
			}
		}
	}

}
