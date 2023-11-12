using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] public float WalkingCountdown;
    [SerializeField] public float JumpingCountdown;
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

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if (StartTimer && currentTime <= 0)
        {
            StartTimer = false;
        }
        if (StartTimer && currentTime <= 2 && StartLevel1)
        {
            Debug.Log("Entered Level 1 ");

            ActivatedTutorial = true;
            Debug.Log("Activated Tutorial 1");
            StartLevel1 = false;
        }
        if (StartTimer && currentTime <= 2 && StartLevel2)
        {
            Debug.Log("Entered Level 2 ");

            ActivatedTutorial = true;
            Debug.Log("Activated Tutorial 2");
            StartLevel2 = false;
        }
        if (StartTimer && currentTime <= 2 && StartLevel3)
        {
            Debug.Log("Entered Level 3 ");

            ActivatedTutorial = true;
            Debug.Log("Activated Tutorial 3");
            StartLevel3 = false;
        }
        if (currentTime <= 0 && ActivatedTutorial)
        {
            Debug.Log("Deactivated Tutorial");

            ActivatedTutorial = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Changed the element of the level tags from OnTriggerExit2d to OntriggerEnter2d so that the sprite change on caontact instead of when exiting the trigger
        if (other.CompareTag("Level 1 Tutorial"))
        {
            Destroy(other.gameObject);
            StartTimer = true;
            currentTime = LevelCountdown;
            StartLevel1 = true;
        }
        if (other.CompareTag("Level 2 Tutorial"))
        {
            StartCoroutine(WaitforLoadScreen());
            
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Level 3 Tutorial"))
        {
            //CharacterSwap.Instance.changeCharacter(1);
            whichMovement = 2;
            Destroy(other.gameObject);
            StartTimer = true;
            currentTime = LevelCountdown;
            StartLevel3 = true;
            changedPosition = true;
            changeposition();
            CharactersSwap.Instance.changeCharacter(1);
            //pauseMenu.Resume();
            //changeposition();
        }
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
        StartTimer = true;
        currentTime = LevelCountdown;
        StartLevel2 = true;
        changedPosition = true;
        CharactersSwap.Instance.changeCharacter(1);
        changeposition();
    }
}
