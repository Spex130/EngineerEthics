using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class textBoxScript : MonoBehaviour {

    //[SerializeField]

//Visualization variables
	public GameObject textStartPoint; //Tells where our text will begin generating.

    public convoNode currentNode; //This is the node we are currently pulling info from
    public eventTracker sceneEventTracker;//This is our scene's eventTracker. It gets fed what to do depending on if the convoNode contains an event.
    public askBoxScript askBoxPartner; //Our textBox's askBox partner to switch between
    public playerChar player;//Our connection to the player in the scene.

    public bool showDebug;
    public float singleCharLength = .24f;
    private string tempText;

    public string[] convArray; //Holds the strings we want to display in the textbox.
    [SerializeField] private char[] charArray; //Holds our strings, converted to characters
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
    private bool isSkipping = false;

//Keeps track of our cursor so we know where we are in the string.
	[SerializeField] private int horiIndex = 0;
	private int vertIndex = 0;

	//Branching option variables
	public string[] choiceArray; // The strings that make up the answers we can make
	

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
    //NPC Face that we show during conversation
    public Image NPCFace;
    public RectTransform textStartPointRect;
    private Vector2 textPos1 = new Vector2(-57f, 10f);
    private Vector2 textPos2 = new Vector2(-70f, 10f);
    
    
	// Use this for initialization
	void Start () {
		init();
	}
	
	// Update is called once per frame
	void Update () {
		showTextboxCheck();
		printLoop();

		if (Input.GetKeyUp(KeyCode.Space))
		{
			progressConv();
		}

        if (showDebug) { Debug.DrawRay(textStartPoint.transform.position, textStartPoint.transform.right *singleCharLength *lineLength, Color.white, .01f, false); }
	}

    //The initialization function for when our program has started running.
    public void init()
    {
        player = (playerChar)GameObject.FindObjectOfType(typeof(playerChar));//DYNAMICALLY GET OUR CHARACTER ON INIT!
        sceneEventTracker = (eventTracker)GameObject.FindObjectOfType(typeof(eventTracker));//Dynamically get our eventTracker on INIT.
        //We set these next two so that the timers will know what number to reset to.
        resetTimer = tinyTimer;
        resetTextSpeed = textSpeed;
        isFinishedDisplaying = false;
        conversationPoint = 0;//So that we start at the BEGINNING of the convo

        if (currentNode !=null)
        {
            if (currentNode.myType != nodeType.question) {//Make sure to check whether or not we should even be bothering to set up.
                convArray = currentNode.convoTextArray;//Load up our text to sidplay from our node.
                text.text = "";//Make sure our text is empty before we start.
                charArray = new char[convArray[conversationPoint].Length];//We get the length of the string in our current conversationPoint.
                resetNPCFace();  
                
                prepStrings();
            }
            else//If it's just a question, immediately activate our Ask Box buddy.
            {
                disableBox();
                askQuestion(currentNode);
            }
        }
	}
    
    //Set the face to the current NPC's face, or disabled if null
    public void resetNPCFace() {
        if (currentNode.NPCFace) 
        {
            //Vector2 tempPos = textStartPoint.getComponent<RectTransform>();
            //tempPos = textPos1;
            textStartPointRect.anchoredPosition = textPos1;
            NPCFace.sprite = currentNode.NPCFace;
            NPCFace.enabled = true;
        }
        else 
        {
            NPCFace.enabled = false;
            //Vector2 tempPos = textStartPoint.getComponent<RectTransform>();
            //tempPos = textPos2;
            textStartPointRect.anchoredPosition = textPos2;
        }
    }

	//Initialize after the program has started
	public void reInit()
	{
		text.text = "";
        resetNPCFace();
		tinyTimer = resetTimer;
		textSpeed = resetTextSpeed;
		conversationPoint = 0;
		charArray = new char[convArray[conversationPoint].Length];//We get the length of the string in our current conversationPoint.
		isFinishedDisplaying = false;
        showEndLine = false;
        prepStrings();
		horiIndex = 0;
		vertIndex = 0;
	}

	//Makes the box show after resetting it.
	public void startConvo()
	{
        text.fontStyle = FontStyle.Normal;
        if (askBoxPartner.showBox == false)
        {
            if (currentNode.myType != nodeType.question)
            {
                showBox = true;
                textbox.enabled = true;
                text.enabled = true;
                //NPCFace.enabled = true;
                reInit();
            }
            else
            {
                askQuestion(currentNode);
            }
        }
	}


	//Overloaded enable that allows for new conversations to be loaded in.
	public void startConvo(convoNode newNode)
	{
        text.fontStyle = FontStyle.Normal;
        currentNode = newNode;
        if (askBoxPartner.showBox == false)
        {
            if (currentNode.myType == nodeType.question)
            {
                askQuestion(currentNode);

            }
            else
            {

                
                showBox = true;
                convArray = currentNode.convoTextArray;
                reInit();
                enableBox();
            }
        }
	}


	public void endConvo()//Ends the conversation! Use this at the end of EVERYTHING.
	{
        reInit();
		showBox = false;
 
		disableBox();
	}
	public void enableBox()//This enables the textbox so you can see it.
	{
		textbox.enabled = true;
		text.enabled = true;
        //NPCFace.enabled = true;
        showBox = true;
	}

	public void disableBox()//This disables the textbox so that you can't see it.
	{
		textbox.enabled = false;
		text.enabled = false;
        NPCFace.enabled = false;
        showBox = false;
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

    //This runs constantly to decide whether or not the graphics should be displayed or not.
	public void showTextboxCheck()
	{
		if (!showBox || askBoxPartner.showBox)
		{
			disableBox();
		}
		else if (showBox)
		{
			enableBox();
		}
	}

	//Go 1 step forward in the conversation
	public void progressConv()
	{
		if (showBox) {//If we're actually active....

            /*
            Okay, so if we're actually active, at this point we wanna check what type of node we're handling.
            At this point, there's two cases. Either [it has text] or [it has no text]
                Case 1 happens when we're either [both] or [textOnly]
                Case 2 happens when we're [question]
            */

            //Case 1. WE DEFINITELY HAVE TEXT. We'll check if there's a question at the end later.
            if (currentNode.myType == nodeType.textOnly || currentNode.myType == nodeType.both)
            {
                if (horiIndex == charArray.Length)
                { //If our line is finished displaying...
                    if (conversationPoint < convArray.Length - 1)
                    {//...And we aren't at the end of our conversation, then do this.
                        resetDisplay();//This moves us to the next text display.

                    }
                    else//If we ARE at the end of our conversation, close the box. OR move to the question at the end.
                    {
                        if (currentNode.myType == nodeType.both) //THIS MEANS WE HAVE A QUESTION TO ANSWER. Activate the Askbox.
                        {
                            
                            askQuestion(currentNode);
                        }
                        else //This means we have no questions, and can safely end the conversation once and for all.
                        {
                            if (currentNode.hasEndEvent == true)//If our TextOnly node has an End event, we should activate it here.
                            {
                                if (currentNode.endEvent != null) { 
                                    sceneEventTracker.loadEvent(currentNode.endEvent);
                                }
                            }
                                endConvo();
                        }
                    }
                }
                else//Otherwise, MAKE THE LINE FINISH DISPLAYING.
                {
                    isSkipping = true;
                    displaySpeedSkip();
                }
            }
            else//This means the currentNode's type is "question" and we can turn on the AskBox.
            {
                askQuestion(currentNode);
            }


        }
	}

    //If the player is impatient and wants the text to display instantly isntead of being shown one character at a time, use this to force the whole line to display at once.
    public void displaySpeedSkip()
    {
        horiIndex = 0;
        vertIndex = 0;
        text.text = "";
        while (horiIndex < charArray.Length)
        {
            textSpeed = resetTextSpeed;
            //Create a character at the horizontal length
            switch (charArray[horiIndex])
            {
                case '◙':
                    break;

                case '♂':
                    break;

                case '♀':
                    break;

                case '♪':
                    break;
                default:
                    text.text = text.text + charArray[horiIndex];
                    break;
            }
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
        isSkipping = false;
        isFinishedDisplaying = true;
        textSpeed = endLineBlinkSpeed;
        showEndLine = true;
        text.text = text.text + " ■";
    }

	public void askQuestion(convoNode q)
	{

        askBoxPartner.activate(q);
        disableBox();
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
        /*
        Note:
        
        Normal:         ◙
        Italic ASCII:   ♂
        Bold ASCII:     ♀
        Bold&Italic:    ♪


        */

		if (showBox && !isSkipping) {//If we're even active...
            
			tinyTimer -= Time.deltaTime * textSpeed;
            
            if (horiIndex < charArray.Length)
            {
                switch (charArray[horiIndex])
                {
                    case '◙':
                        text.fontStyle = FontStyle.Normal;
                      
                        break;

                    case '♂':
                        text.fontStyle = FontStyle.Italic;
                       
                        break;

                    case '♀':
                        text.fontStyle = FontStyle.Bold;
                      
                        break;

                    case '♪':
                        text.fontStyle = FontStyle.BoldAndItalic;
                       
                        break;
                    default:
                        break;
                }
            }

            //Every time we time down to zero, do the thing.
            if (tinyTimer <= 0)
			{
                if (horiIndex < charArray.Length)
                {
                    
                 
                    textSpeed = resetTextSpeed;
                    //Create a character at the horizontal length
                    switch (charArray[horiIndex])
                    {
                        case '◙':
                            text.fontStyle = FontStyle.Normal;
                            break;

                        case '♂':
                            text.fontStyle = FontStyle.Italic;
                            break;

                        case '♀':
                            text.fontStyle = FontStyle.Bold;
                            break;

                        case '♪':
                            text.fontStyle = FontStyle.BoldAndItalic;
                            break;
                        default:
                            text.text = text.text + charArray[horiIndex];
                            break;
                    }
                    
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
