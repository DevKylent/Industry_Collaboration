using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// The script inherits from the Tutorial script in order to have access to the variables needed.
public class CharactersSwap : MonoBehaviour
{
    public static CharactersSwap Instance;
    public Transform character;
    public List<Transform> possibleCharacters;
    public CinemachineVirtualCamera cam;
    private Vector3 currentposition;
    //[SerializeField] public GameObject Level_2Tutorial;

    public int whichCharacter;
    // public int whichMovement;
    //[SerializeField] private List<Movement> position;

    private bool alreadychanged = false;
    private bool alreadychangedLevel3 = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Swap();
    }
    public void changeCharacter(int value)
    {
        whichCharacter += value;
    }
    public void Swap()
    {
        Debug.Log(whichCharacter);

        character = possibleCharacters[whichCharacter];
        if (whichCharacter == 1 && !alreadychanged)
        {
            character.transform.position = new Vector3(118.52f, -2.2f, 0f);
            character.GetComponent<Movement>().enabled = false;
            alreadychanged = true;
        }
        if (whichCharacter == 2 && !alreadychangedLevel3)
        {
            character.transform.position = new Vector3(374.4f, -7f, 1.64f);
            character.GetComponent<Movement>().enabled = false;
            alreadychangedLevel3 = true;
        }
        character.GetComponent<Movement>().enabled = true;
        character.GetComponent<Rigidbody2D>().gravityScale = 5;
        //character.position = currentposition;
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] != character)
            {
                possibleCharacters[i].transform.position = new Vector3(-75, -20, 0);
                possibleCharacters[i].GetComponent<Movement>().enabled = false;
            }
        }
        cam.LookAt = character;
        cam.Follow = character;


    }
}

