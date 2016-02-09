using UnityEngine;
using System.Collections;

public class playerChar : MonoBehaviour {

	public bool isFacingRight = true;
	public bool isTouchingDoor = false;
	public Door touchedDoor = null;


	public float moveSpeed = 5f;
	public float stopSpeedDiv = 2f; 
	public Animator myAnim;
	public Rigidbody2D myRigidbody2D;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		moveCheck();
		interactCheck();
	}

	public void moveCheck()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			isFacingRight = true;
			myRigidbody2D.velocity = new Vector2(1*moveSpeed, myRigidbody2D.velocity.y);
		}

		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			isFacingRight = false;
			myRigidbody2D.velocity = new Vector2(-1 * moveSpeed, myRigidbody2D.velocity.y);
		}

		else
		{
			myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / stopSpeedDiv, myRigidbody2D.velocity.y);

		}
	}

	public void interactCheck()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow)){
			if (isTouchingDoor)
			{
				RaycastHit2D hit;
				float distanceToground = 0;
				print(Physics2D.Raycast(transform.position, Vector2.down, 200.0f));
				if(Physics2D.Raycast(transform.position, Vector2.down, 200.0f))
				{
					hit = Physics2D.Raycast(transform.position, Vector2.down, 200.0f);
                    distanceToground = hit.distance;
					print(distanceToground);
				}
				this.transform.position = touchedDoor.partner.transform.position;
				this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + distanceToground);

			}
		}
	}



}
