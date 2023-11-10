using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] public GameObject WalkingTutorial;
    [SerializeField] public float WalkingCountdown;
    [SerializeField] public GameObject JumpingTutorial;
    [SerializeField] public float JumpingCountdown;
    [SerializeField] public GameObject Level_1Prompt;
    [SerializeField] public GameObject Level_1Tutorial;
    [SerializeField] public GameObject Level_2Prompt;
    [SerializeField] public GameObject Level_2Tutorial;
    [SerializeField] public GameObject Level_3Prompt;
    [SerializeField] public GameObject Level_3Tutorial;
    [SerializeField] public float LevelCountdown;

    public PauseMenu pauseMenu;
    
    private float currentTime = 0f;
    private bool StartTimer = false;
    private bool StartLevel1 = false;
    private bool StartLevel2 = false;
    private bool StartLevel3 = false;
    private bool ActivatedTutorial = false;
    [HideInInspector]
    public int whichCharacter;
    [HideInInspector]
    public int whichMovement;
    [HideInInspector]
    public bool changedPosition = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= 0)
        {
            //Debug.Log(currentTime);
        }
        currentTime -= 1 * Time.deltaTime;

        if (StartTimer && currentTime <= 0)
        {

            JumpingTutorial.SetActive(false);
            WalkingTutorial.SetActive(false);
            StartTimer = false;
        }
        if (StartTimer && currentTime <= 2 && StartLevel1)
        {
            Debug.Log("Entered Level 1 ");
            Level_1Prompt.SetActive(false);


            Level_1Tutorial.SetActive(true);
            ActivatedTutorial = true;
            Debug.Log("Activated Tutorial 1");
            StartLevel1 = false;
        }
        if (StartTimer && currentTime <= 2 && StartLevel2)
        {
            Debug.Log("Entered Level 2 ");
            Level_2Prompt.SetActive(false);


            Level_2Tutorial.SetActive(true);
            ActivatedTutorial = true;
            Debug.Log("Activated Tutorial 2");
            StartLevel2 = false;
        }
        if (StartTimer && currentTime <= 2 && StartLevel3)
        {
            Debug.Log("Entered Level 3 ");
            Level_3Prompt.SetActive(false);


            Level_3Tutorial.SetActive(true);
            ActivatedTutorial = true;
            Debug.Log("Activated Tutorial 3");
            StartLevel3 = false;
        }
        if (currentTime <= 0 && ActivatedTutorial)
        {
            Debug.Log("Deactivated Tutorial");
            Level_1Tutorial.SetActive(false);
            Level_2Tutorial.SetActive(false);
            Level_3Tutorial.SetActive(false);

            ActivatedTutorial = false;
            //StartTimer = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Walking"))
        {
            WalkingTutorial.SetActive(true);
        }
        if (other.CompareTag("Jumping"))
        {
            JumpingTutorial.SetActive(true);
        }
        //Changed the element of the level tags from OnTriggerExit2d to OntriggerEnter2d so that the sprite change on caontact instead of when exiting the trigger
        if (other.CompareTag("Level 1 Tutorial"))
        {
            Level_1Prompt.SetActive(true);
            Destroy(other.gameObject);
            StartTimer = true;
            currentTime = LevelCountdown;
            StartLevel1 = true;
        }
        if (other.CompareTag("Level 2 Tutorial"))
        {
            StartCoroutine(WaitforLoadScreen());
            
            Destroy(other.gameObject);
            
            //changeposition();
        }
        if (other.CompareTag("Level 3 Tutorial"))
        {
            //CharacterSwap.Instance.changeCharacter(1);
            whichMovement = 2;
            Level_3Prompt.SetActive(true);
            Destroy(other.gameObject);
            StartTimer = true;
            currentTime = LevelCountdown;
            StartLevel3 = true;
            changedPosition = true;
            changeposition();
            CharactersSwap.Instance.changeCharacter(1);
            pauseMenu.Resume();
            //changeposition();
        }
        //********************************************************************************************************************************************************
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Walking"))
        {
            Destroy(other.gameObject);
            StartTimer = true;
            currentTime = WalkingCountdown;
        }
        if (other.CompareTag("Jumping"))
        {
            Destroy(other.gameObject);
            StartTimer = true;
            currentTime = JumpingCountdown;
        }
        
    }
    private IEnumerator changeposition()
    {
        
        yield return new WaitForSeconds(0.2f);
        changedPosition = false;
    }

    private IEnumerator WaitforLoadScreen()
    {
        yield return new WaitForSeconds(1f);
        whichMovement = 1;
        Level_2Prompt.SetActive(true);
        StartTimer = true;
        currentTime = LevelCountdown;
        StartLevel2 = true;
        changedPosition = true;
        CharactersSwap.Instance.changeCharacter(1);
        changeposition();
    }
}
