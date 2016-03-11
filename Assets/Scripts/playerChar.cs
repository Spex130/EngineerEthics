using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class playerChar : MonoBehaviour {

	public bool isFacingRight = true;

	public bool isTouchingElevator = false;

	public bool isTouchingDoor = false;//Tells if we are touching a door or not.
	public bool isTouchingPerson = false;
	public bool canMove = false;//Whether or not we should be capable of moving.


	public Door touchedDoor = null;//If a door is touching us, it will set this variable true for us.
	public lerpChain chain; //We have a reference to our lerpChain so we can reset the camera position when we enter a door.
	public textBoxScript textBox;//This is a reference to the textbox so we can activate dialogue.
    public askBoxScript askBox;//A public reference to the askBox so we can figure out if the askbox is active

	public float moveSpeed = 5f;//This is how fast we run.
	public float stopSpeedDiv = 2f;//This is our stopping friction.
	private Animator myAnim;
	private Rigidbody2D myRigidbody2D;
	private SpriteRenderer mySprite;//This is our character.

	public int State = 1;
	/// <summary>
	/// States overview
	/// 1:		Idle
	/// 2:		Moving
	/// </summary>

	public GameObject groundCheck;//A simple gameobject with a collider meant to help us check what's under us.
	public SpriteRenderer interactIndicator;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
		mySprite = gameObject.GetComponent<SpriteRenderer>();
		interactIndicator.enabled = false;
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
        if (textBox.showBox || askBox.showBox)
        {
            canMove = false;
        }
        else{
            canMove = true;
        }

        if (canMove) {
			if (Input.GetKey(KeyCode.RightArrow))
			{
				State = 2;
				myAnim.SetInteger("AnimState", State);
				isFacingRight = true;
				myRigidbody2D.velocity = new Vector2(1 * moveSpeed, myRigidbody2D.velocity.y);
			}

			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				State = 2;
				myAnim.SetInteger("AnimState", State);
				isFacingRight = false;
				myRigidbody2D.velocity = new Vector2(-1 * moveSpeed, myRigidbody2D.velocity.y);
			}

			else
			{
				State = 1;
				myAnim.SetInteger("AnimState", State);
				myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / stopSpeedDiv, myRigidbody2D.velocity.y);

			}
		}
		else
		{
			State = 1;
			myAnim.SetInteger("AnimState", State);
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
    //If we are near a person, it talkes to the person.
    //It also handles whether or not our INTERACTION INDICATOR should show.
    public void interactCheck()
    {

        if (isTouchingDoor || isTouchingPerson || isTouchingElevator)
        {
            interactIndicator.enabled = true;
        }
        else
        {
            interactIndicator.enabled = false;
            touchedDoor = null;
        }

        if (canMove) {
            if (Input.GetKeyUp(KeyCode.UpArrow)) {
                if (isTouchingDoor)
                {
                    touchedDoor.interact();

                }

                else if (isTouchingPerson)
                {

                }



            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                textBox.startConvo();
            }
        }
	}



}
