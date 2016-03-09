using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, interactable {

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
		if (other.gameObject.CompareTag("Player"))
		{ print("EXIT"); }

		player = other.gameObject.GetComponent<playerChar>();
		//player.touchedDoor = null;
		player.isTouchingDoor = false;
			player = null;
		

	}

    public virtual void interact()
    {
        player.transform.position = new Vector2(player.touchedDoor.partner.transPoint.transform.position.x, player.touchedDoor.partner.transPoint.transform.position.y);
        player.chain.resetLerp();
        player.touchedDoor = player.touchedDoor.partner;
        RaycastHit2D hit;
        float distanceToground = 0;
        if (Physics2D.Raycast(player.groundCheck.transform.position, Vector2.up, 200.0f))
        {
            hit = Physics2D.Raycast(player.groundCheck.transform.position, Vector2.down, 200.0f);
            distanceToground = hit.distance;
            print(hit.distance);
        }
    }
}
