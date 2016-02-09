using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Door partner;
	public GameObject transPoint;
	public playerChar player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			player = other.gameObject.GetComponent<playerChar>();
			player.touchedDoor = this;
			player.isTouchingDoor = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.GetType() == typeof(playerChar))
		{
			
			player.touchedDoor = null;
			player.isTouchingDoor = false;
			player = null;
		}

	}
}
