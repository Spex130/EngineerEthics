using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textBoxScript : MonoBehaviour {


	public GameObject textStartPoint; //Tells where our text will begin generating.

	public bool isPrepped = false; //Tells whether or not we've converted the Strings to arrays of Chars

	public string convArray; //Holds the strings we want to display in the textbox.
	public char[] charArray; //Holds our strings, converted to characters

	public int lineLength = 25; //Number of characters per line
	public float kerning = 1f;//Space between characters
	public float vSpace = 1f;//Space between lines

	public float textSpeed = 15f;
	public float endLineBlinkSpeed = 1f;
	private float resetTextSpeed;


	private float tinyTimer = 1f;
	private float resetTimer;

	private int horiIndex = 0;
	private int vertIndex = 0;

	public Text text;

	public bool showEndLine = false;

	// Use this for initialization
	void Start () {
		text.text = "";
		resetTimer = tinyTimer;
		resetTextSpeed = textSpeed;
		charArray = new char[convArray.Length];
		prepStrings();
	}
	
	// Update is called once per frame
	void Update () {
		tinyTimer -= Time.deltaTime * textSpeed;

		if (tinyTimer <= 0)
		{
			
			if (horiIndex < charArray.Length)
			{
				textSpeed = resetTextSpeed;
				//Create a character at the horizontal length
				text.text = text.text + charArray[horiIndex];
				horiIndex++;
				if (horiIndex % lineLength == 0)//Check if we've hit the line length by checking if your horiIndex is divisible by our linelength
				{
					switch (charArray[horiIndex]) {

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

	public void prepStrings()
	{
		//Convert all of the strings to characters
			charArray = convArray.ToCharArray();

		isPrepped = true;
	}
}
