using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class playerChar : MonoBehaviour {

	public bool isFacingRight = true;
	public bool isTouchingDoor = false;
	public bool isTouchingElevator = true;
	public Door touchedDoor = null;//Tells if we are touching a door or not.
	public lerpChain chain;

	public float moveSpeed = 5f;
	public float stopSpeedDiv = 2f; 
	public Animator myAnim;
	public Rigidbody2D myRigidbody2D;
	public SpriteRenderer mySprite;

	public int State = 1;

	public GameObject groundCheck;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
		mySprite = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		moveCheck();
		interactCheck();
		directionCheck();
	}

	//Checks whether or not we are moving left or right. Or moving at all.
	public void moveCheck()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			myAnim.SetInteger("AnimState", 2);
			State = 2;
			isFacingRight = true;
			myRigidbody2D.velocity = new Vector2(1*moveSpeed, myRigidbody2D.velocity.y);
		}

		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			myAnim.SetInteger("AnimState", 2);
			State = 2;
			isFacingRight = false;
			myRigidbody2D.velocity = new Vector2(-1 * moveSpeed, myRigidbody2D.velocity.y);
		}

		else
		{
			myAnim.SetInteger("AnimState", 1);
			State = 1;
			myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / stopSpeedDiv, myRigidbody2D.velocity.y);

		}
	}

	//Checks what direction we are facing, and flips sprite appropriately
	public void directionCheck()
	{
		if (isFacingRight)
		{
			mySprite.flipX = false;
		}

		else
		{
			mySprite.flipX = true;
		}
	}


	//Checks if we are in range of a door, and if we are in range of a door, transports us using that door.
	public void interactCheck()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow)){
			if (isTouchingElevator) 
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
			}
			if (isTouchingDoor)
			{
				this.transform.position = new Vector2(touchedDoor.partner.transPoint.transform.position.x, touchedDoor.partner.transPoint.transform.position.y);
				chain.resetLerp();
				RaycastHit2D hit;
				float distanceToground = 0;
				if(Physics2D.Raycast(groundCheck.transform.position, Vector2.up, 200.0f))
				{
					hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, 200.0f);
                    distanceToground = hit.distance;
					print(hit.distance);
				}
				

			}
		}
	}



}
