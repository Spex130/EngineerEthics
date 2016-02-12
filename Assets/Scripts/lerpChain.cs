using UnityEngine;
using System.Collections;

/*
	Camera system using multiple Lerp points
	Produces a slightly lagged camera effect
*/

public class lerpChain : MonoBehaviour {

	public Camera myCamera;
	public playerChar player;
	public GameObject[] pointArray;
	public Transform[] locArray;
	public int arraySize = 3;
	public float lerpSpeed = .1f;

	// Use this for initialization
	void Start () {
		init();
	}

	public void init()
	{
		//Set an array that holds gameObjects
		pointArray = new GameObject[arraySize];

		//Set the initial array object to US.
		pointArray[0] = this.gameObject;
		for (int i = 1; i < arraySize; i++)
		{
			//Create a new gameObject, and set its parent to the previous gameobject
			pointArray[i] = (GameObject)Instantiate(new GameObject(), this.transform.position, this.transform.rotation);
			//pointArray[i].transform.SetParent(pointArray[i - 1].transform);
		}

		//To finish it off, set the camera to the last point.
		myCamera.transform.SetParent(pointArray[pointArray.Length - 1].transform);
	}
	
	//Go through the array and lerp each point to [lerpSpeed]
	public void lerpLoop()
	{
		pointArray[0].transform.position = Vector2.Lerp(pointArray[0].transform.position, player.transform.position, lerpSpeed);
		for (int i = 1; i < arraySize; i++)
		{
			pointArray[i].transform.position = Vector2.Lerp(pointArray[i].transform.position, pointArray[i-1].transform.position, lerpSpeed);
		}
	}

	public void resetLerp()
	{
		pointArray[0].transform.position = player.transform.position;
		for (int i = 1; i < arraySize; i++)
		{
			pointArray[i].transform.position = player.transform.position;
		}
	}

	// Update is called once per frame
	void Update () {
		lerpLoop();
	}
}
