using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;//cesar nazario puso esto
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    [SerializeField] public SpriteRenderer sprite;

    //Movement
    [SerializeField] private float MovementSpeed = 1;
    [SerializeField] private float JumpForce = 1;
    [HideInInspector] public Vector3 currentposition = new Vector3(0, 0, 0);
    //[SerializeField] public float TimeBetweenSounds = 1;

    //[SerializeField] private Transform player;
    [SerializeField] private ScoreManager ScriptScore;
    private bool IsFinished;
    [HideInInspector] public float Move, movement;
    private Vector3 RespawnPoint;

    public Animator animator;
    [SerializeField] private CharactersSwap whichplayer;
    [HideInInspector] public bool threeD = false;

    //Add float called Coyote Time and Coyote Time Counter ******
    private float coyoteTime = .75f;
    private float coyoteTimeCounter;

    //***********************************************************
    public ParticleSystem dust;
    [SerializeField] private LevelTransition LevelTransition;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        AudioManager.Instance.Play("BackgroundMusic");
        AudioManager.Instance.Stop("CreditsMusic");
        AudioManager.Instance.Stop("MainMenuMusic");
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        IsFinished = ScriptScore.FinishedGame;
        Move = movement;
        MovementFunc();
        animator.SetFloat("speed", Mathf.Abs(Move));
        currentposition = transform.position;
        FlipCharacter();
        
        
        //Detects if player is starting to fall and if the player is not falling the coyote time Counter will assign the Coyote Time, if not it will reduce Coyote Timer Counter each delta time 
        if (Mathf.Abs(_rigidBody.velocity.y) < 0.001f)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        //**************************************************************************************************************************************************************************************

        //Detects if "Spacebar" , "W" or "UpArrow" is pressed and the Coyote Timer Counter is higher than 0
        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0 || Input.GetKeyDown(KeyCode.UpArrow) && coyoteTimeCounter > 0 || Input.GetKeyDown(KeyCode.W) && coyoteTimeCounter > 0)
        {
            Jump();
            coyoteTimeCounter = 0;
            RunningDust();
        }
        //*************************************************************************************************

        //Detects if "Spacebar" , "W" or "UpArrow" is pressed and is not falling then calls the Jump function
        /*if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidBody.velocity.y) < 0.001f || Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(_rigidBody.velocity.y) < 0.001f || Input.GetKeyDown(KeyCode.W) && Mathf.Abs(_rigidBody.velocity.y) < 0.001f)
        {
            Jump();
        }*/

        //Rotation

        if (whichplayer.whichCharacter == 2)
        {
            Vector3 movementDirection = new Vector3(-movement, 0, movement);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
        
        if(Mathf.Abs(Move) >0 && Mathf.Abs(_rigidBody.velocity.y) <0.1)
        {
            RunningDust();
        }
    }
    private void MovementFunc()
    {
        //Detects if "A" and "D" or "LeftArrow" and "RightArrow" is pressed giving it a value of 1 or -1

        //New Movement.
        movement = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(movement * MovementSpeed, _rigidBody.velocity.y);

        //Old Movement.

        //movement = Input.GetAxis("Horizontal"); //Does what the if functions uptop do.
        //ransform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed; //Moves the player
        
    }
    private void Jump()
    {
        _rigidBody.AddForce(new Vector3(0, JumpForce, 0), ForceMode2D.Impulse);
        animator.SetBool("IsJumping", true);
        //playerjump.PlayDelayed(0f);//player jump sound plays.
        AudioManager.Instance.Play("Jump");
        StartCoroutine(Landing());
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    //This Coroutine will count down a certain amount of seconds before playing the landing sound
    private IEnumerator Landing()
    {
        yield return new WaitForSeconds(0.5f);
        //AudioManager.Instance.Play("Landing");
        animator.SetBool("IsJumping", false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))//Checks if we collided with an object with the tag "Coins"
        {
            ScoreManager.instance.ChangeScore();// Gives the player a point
            Destroy(other.gameObject); //Destroys the object with the tag "Coins"
            AudioManager.Instance.Play("CoinCollected");
        }

        if (other.gameObject.CompareTag("3DCoins"))//Checks if we collided with an object with the tag "Coins"
        {
            ScoreManager.instance.Change3DScore();// Gives the player a point
            Destroy(other.gameObject); //Destroys the object with the tag "Coins"
            AudioManager.Instance.Play("CoinCollected");
        }

        if (other.CompareTag("Death")) //Checks if we collided with an object with the tag "Death" 
        {
            transform.position = RespawnPoint; //Changes position of player to the checkpoint
        }

        if (other.CompareTag("Checkpoint"))//Checks if we collided with an object with the tag "Checkpoint"
        {
            RespawnPoint = other.transform.position; //changes the respawnpoint to the checkpoint
        }

        if (other.CompareTag("Level 2"))
        {
            Destroy(other.gameObject);
            StartCoroutine(TurnMessageOff());
        }
        if (other.CompareTag("Level 2 Tutorial"))
        {
            LevelTransition.FirstTransition();
        }

        if (other.CompareTag("Level 3"))
        {
            Destroy(other.gameObject);
            StartCoroutine(TurnMessageOff());

            LevelTransition.SecondTransition();
        }

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Collided");
            Debug.Log(IsFinished);
            if (IsFinished)
            {
                Debug.Log("FinishedGame");
                SceneManager.LoadScene("Credits");
            }
        }
    }
    private IEnumerator TurnMessageOff()
    {
        yield return new WaitForSeconds(2.5f);
    }

    //This Function will flip the character towards the location it is walking 
    void FlipCharacter()
    {
        if (Move < 0)
        {
            sprite.flipX = true;
        }
        if (Move > 0)
        {
            sprite.flipX = false;
        }
    }

    //When player collides with the box it will start the pushing animation until player stops colliding
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box") && Mathf.Abs(_rigidBody.velocity.x) > 0.1 && Mathf.Abs(_rigidBody.velocity.y) <0.1)
        {
            animator.SetBool("IsPushing", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {
            animator.SetBool("IsPushing", false);
        }
    }
    //**************************************************************************************************

    //Creating dust when player moves
    public void RunningDust()
    {
        //dust.Play();
    }
}

