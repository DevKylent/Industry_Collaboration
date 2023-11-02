using System.Collections;
using System.Collections.Generic;
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
    //Coins
    public int coinValue = 1;
    //UI


    [SerializeField] private GameObject Level2;
    [SerializeField] public GameObject Level_2_Background_Art;
    [SerializeField] private GameObject Level3;

    //[SerializeField] private Transform player;
    [SerializeField] private ScoreManager ScriptScore;
    private bool IsFinished;
    private Collision collision;
    [HideInInspector] public float Move, movement;
    private Vector3 RespawnPoint;
    private bool Level1Active = true;
    private bool Level2Active = true;
    private bool Level3Active = true;

    public Animator animator;
    [SerializeField] private CharactersSwap whichplayer;
    [HideInInspector] public bool threeD = false;




    private Rigidbody2D _rigidBody;

    private void Start()
    {
        AudioManager.Instance.Play("BackgroundMusic");
        AudioManager.Instance.Stop("CreditsMusic");
        AudioManager.Instance.Stop("MainMenuMusic");
        _rigidBody = GetComponent<Rigidbody2D>();
        //Level2.SetActive(false);
        //Level3.SetActive(false);

    }

    // Update is called once per frame
    private void Update()
    {

        IsFinished = ScriptScore.FinishedGame;
        //Detects if "A" and "D" or "LeftArrow" and "RightArrow" is pressed giving it a value of 1 or -1
        movement = Input.GetAxis("Horizontal"); //Does what the if functions uptop do.
        Move = movement;
        MovementFunc();
        animator.SetFloat("speed", Mathf.Abs(Move));
        currentposition = transform.position;
        FlipCharacter();

        //Detects if "Spacebar" , "W" or "UpArrow" is pressed and is not falling then calls the Jump function
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidBody.velocity.y) < 0.001f || Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(_rigidBody.velocity.y) < 0.001f || Input.GetKeyDown(KeyCode.W) && Mathf.Abs(_rigidBody.velocity.y) < 0.001f)
        {
            Jump();
        }

        //Rotation




        if (whichplayer.whichCharacter == 2)
        {
            Vector3 movementDirection = new Vector3(-movement, 0, movement);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);

        }
    }

    private void MovementFunc()
    {
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed; //Moves the player
        //movement = 0;
    }

    public float GetMove()
    {
        return Move;
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
            ScoreManager.instance.ChangeScore(coinValue); // Gives the player a point
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
            Level2.SetActive(true);
            Destroy(other.gameObject);

            StartCoroutine(TurnMessageOff());
            Time.timeScale = 0.01f;
        }
        if (other.CompareTag("Level 2 Tutorial"))
        {

            Level_2_Background_Art.SetActive(true);
        }
        if (other.CompareTag("Level 3"))
        {
            Level3.SetActive(true);
            Destroy(other.gameObject);
            Time.timeScale = 0.01f;
            StartCoroutine(TurnMessageOff());

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

        yield return new WaitForSeconds(0.03f);
        Level2.SetActive(false);
        Level3.SetActive(false);
        Time.timeScale = 1f;

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



}

