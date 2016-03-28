using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour, interactable {

    public convoNode myConvo;// What we say to the player
    public string name = "defaultName";
    public playerChar player;
    private Animator myAnim;
    private SpriteRenderer mySprite;
    private Rigidbody2D myRigidbody2D;

    public float walkRange = 5f; //How far away from our origin we can get. Used to calculate a few private variables.
    [SerializeField] private Vector2 originPoint;//Where we originally started. (Only the X Value of this transform matters.)
    [SerializeField] private Vector2 leftBoundary;//How far left of our origin we can go
    [SerializeField] private Vector2 rightBoundary;//How far right of our origiin we can go.

    public bool isFacingRight = true;//Tells us where we're facing.
    public bool activatedTimer = true;

    public float timer = 1.5f;
    private float resetTimer;
    //ENUMS Definitions
    /// <summary>
    /// 
    /// </summary>
    public enum NPCState { Idle, Walking };

    /// <summary>
    ///THIS tells us whether or not our origin boundaries should treat the origin point as the left side, center, or right side of the boundary.
    /// </summary>
    public enum BoundaryPositionType {LeftSide, Center, RightSide};

    public NPCState state;
    public BoundaryPositionType bPos;

    public virtual void interact()
    {
        player.startConvo(myConvo);   
    }

    // Use this for initialization
    void Start () {
        resetTimer = timer;
        myAnim = GetComponent<Animator>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        setBoundaries();
    }
	
	// Update is called once per frame
	void Update () {
        timerLoop();
        behaviorLoop();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<playerChar>();
            player.touchedPerson = this;
            player.isTouchingPerson = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { print("EXIT"); }

        //player.touchedPerson = null;
        player.isTouchingPerson = false;
        player = null;


    }

    public void timerLoop()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            randomizeBehavior();
            timer = resetTimer;
        }
    }

    //Sets how far we can go left or right, based on our boundary type.
    public void setBoundaries()
    {
        originPoint = new Vector2(transform.position.x, 0);
        leftBoundary = new Vector2(originPoint.x - walkRange, 0);
        rightBoundary = new Vector2(originPoint.x + walkRange, 0);
    }

    public void behaviorLoop()
    {
            switch (state)
            {
                case NPC.NPCState.Idle:
                    myAnim.SetInteger("AnimState", (int)state);
                    break;
                case NPC.NPCState.Walking:
                if (true == true) ///if Our player can move we move.
                {
                    myAnim.SetInteger("AnimState", (int)state);

                    /*
                        We use the following section to determine if we are moveing left or right, along with whether or not we should turn around.
                    */
                    if (isFacingRight)
                    {
                        myRigidbody2D.velocity = new Vector2(1, myRigidbody2D.velocity.y);
                        mySprite.flipX = false;
                        if (this.transform.position.x >= rightBoundary.x)
                        {
                            myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y);
                            this.transform.position = new Vector2(rightBoundary.x - .01f, this.transform.position.y);
                            isFacingRight = false;
                        }
                    }
                    else
                    {
                        myRigidbody2D.velocity = new Vector2(-1, myRigidbody2D.velocity.y);
                        mySprite.flipX = true;
                        if (this.transform.position.x <= leftBoundary.x)
                        {
                            myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y);
                            this.transform.position = new Vector2(leftBoundary.x + .01f, this.transform.position.y);
                            isFacingRight = true;
                        }
                    }
                }
                /*else//Else we don't move.
                {
                    state = NPCState.Idle;
                }*/
                    break;
                default:
                    break;
                }
        }
       
    
    public void randomizeBehavior()
    {
        state = (NPCState)Random.Range(0, 2);
        print(state);
        myAnim.SetInteger("AnimState", (int)state);
    }
}
