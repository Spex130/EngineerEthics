using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class askBoxScript : MonoBehaviour {

	//Visualization variables
	public GameObject textStartPoint; //Tells where our text will begin generating.

    public textBoxScript textBoxPartner; //Our askBox's textbox partner to switch between

    public string question;//The question we ponder the the answer to.
	public string[] choiceArray; //Holds the strings we want to display in the textbox.
	private char[] charArray; //Holds our strings, converted to characters

	public int lineLength = 25; //Number of characters per line
	public float kerning = 1f;//Space between characters
	public float vSpace = 6f;//Space between lines

	//Display Speed variables
	public float textSpeed = 15f;
	public float endLineBlinkSpeed = 1f;
	private float resetTextSpeed;
	[SerializeField] private float tinyTimer = 1f;
	private float resetTimer;

	//Keeps track of our cursor so we know where we are in the string.
	[SerializeField] private int horiIndex = 0;
	[SerializeField] private int vertIndex = 0;
	[SerializeField] private int progressIndex = 0;

	public int choiceID = 0;//How we publically track what choice we're making. This controls where the cursor will be as well.

	[SerializeField] private Text[] textArray;//An array of Text objects spaced evenly. Will eventually be filled with a dynamic amount of choices for us to choose from.

	//Determines whether or not we should show the blinking line ending.
	private bool showEndLine = false;
	//Determines whether or not we have finished displaying the line
	private bool isFinishedDisplaying = false;
	//Determine whether or not we should be able to see the textbox at all.
	public bool showBox = false;
	//The textbox we want to edit.
	public UnityEngine.UI.Image textbox;
	
	public Text text;//Text that we use to display our cursor
    public Text qText;//Text which we use to display our actual QUESTION

	// Use this for initialization
	void Start () {
		resetTimer = tinyTimer;
		resetTextSpeed = textSpeed;
		init();
	}
	
	// Update is called once per frame
	void Update () {
		showTextboxCheck();
		printLoop();
		interactLoop();
	}

	public void init()
	{
		progressIndex = 0;
		text.text = "";
        qText.text = question;
		tinyTimer = resetTimer;
		textSpeed = resetTextSpeed;
		charArray = new char[choiceArray[progressIndex].Length];//We get the length of the string in our current conversationPoint.
		isFinishedDisplaying = false;
		//prepStrings();
		horiIndex = 0;
		vertIndex = 0;

		textArray = new Text[choiceArray.Length];

        //Our basic Text is used as a cursor. The rest of the texts are stored in the textArray

		for(int i = 0; i < choiceArray.Length; i++)//Here we instantiate ALL of the new text objects
		{
			Text temp = (Text)Instantiate(text, textStartPoint.transform.position, transform.rotation);
			//temp.text = "TEST";
			temp.transform.SetParent(textStartPoint.transform, false);
			temp.alignment = TextAnchor.MiddleLeft;
			temp.transform.localPosition = new Vector3(0, 0 - (vSpace * i), 0);
			textArray[i] = temp;
		}
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

	//This method acts as an update loop for when the text box should be displaying normal conversation text.
	public void printLoop()
	{
		if (showBox)
		{//If we're even active...
			tinyTimer -= Time.deltaTime * textSpeed; //Run the timer.

			//Every time we time down to zero, do the thing.
			if (tinyTimer <= 0)
			{
				if (progressIndex < choiceArray.Length)//This allows us to increment our texts properly.
				{
					if (horiIndex < charArray.Length)//If we haven't filled out to the end of our singular line...
					{
						charArray = choiceArray[progressIndex].ToCharArray();
						textSpeed = resetTextSpeed;
						//Create a character at the horizontal length
						textArray[progressIndex].text = textArray[progressIndex].text + charArray[horiIndex];//This controls which actual TEXT object we're writing to.
						horiIndex++;
						if (horiIndex % lineLength == 0 && horiIndex != charArray.Length)//Check if we've hit the line length by checking if your horiIndex is divisible by our linelength
						{
							switch (charArray[horiIndex])//Special cases for line breaks if we hit the line length with our one-line responses....
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
						else if(horiIndex >= charArray.Length)//If we've finished printing out one choice, reset and go to the next choice.
						{
							horiIndex = 0;
							vertIndex = 0;
							progressIndex++;
						}
					}
				}
				else//We only arrive here AFTER we've printed out all of our options.
				{
					isFinishedDisplaying = true;
				}

				tinyTimer = resetTimer;
			}
		}
	}

	public void makeChoice()
	{
		isFinishedDisplaying = false;
		deleteText();
		disableBox();
		showBox = false;

	}

	public void deleteText(){
		for(int i = 0; i < textArray.Length; i++){
			Destroy(textArray[i].gameObject);
		}
	}

	public void interactLoop()
	{
		if (isFinishedDisplaying) { 
			text.alignment = TextAnchor.MiddleRight;
			text.transform.position = new Vector3(textArray[choiceID].transform.position.x - 1, textArray[choiceID].transform.position.y, 0);
			text.text = "○";

			if (Input.GetKeyUp(KeyCode.Space))
			{
				makeChoice();
			}

			if (Input.GetKeyUp(KeyCode.UpArrow) && choiceID != 0)
			{
				choiceID--;
			}
			if (Input.GetKeyUp(KeyCode.DownArrow) && choiceID < choiceArray.Length-1)
			{
				choiceID++;
			}
		}
	}

}
