
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetChoicesScript : MonoBehaviour {
	public convoNode QuestionList;
	public Text firstone;
	public Text secondOne;
	public Text thirdOne;
	// Use this for initialization
	void Start () {
		int ourFirstPick = (PlayerPrefs.GetInt ("What should I do?"));
		if (ourFirstPick == 0) {
			firstone.text = ("Discovered Becky was lying about results");
		} else if (ourFirstPick == 1) {
			firstone.text =("Pointlessly Rummaged Through Becky's Desk");
		} else if (ourFirstPick == 2) {
			firstone.text = ("Stole Becky's Notes");
		}
		int ourSecondPick = (PlayerPrefs.GetInt ("Response?"));
		if (ourSecondPick == 0) {
			secondOne.text = ("Ignored Boss");
		}
		else if (ourSecondPick == 1) {
			secondOne.text = ("Turned Becky in");
		}
		else if (ourSecondPick == 2) {
			secondOne.text = ("Covered for Becky");
		}
		int ourThirdPick = (PlayerPrefs.GetInt ("What should I say?"));
		if (ourThirdPick == 0) {
			thirdOne.text = ("Asked Becky Directly");
		} else if (ourThirdPick == 1) {
			thirdOne.text = ("Asked Becky Casually");
		}else if (ourThirdPick == 2) {
			thirdOne.text = ("Lied to Becky");
		}else if (ourThirdPick == 3) {
			thirdOne.text = ("Blackmailed Becky");
		}else if (ourThirdPick == 4) {
			thirdOne.text = ("Tried to help Becky");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
