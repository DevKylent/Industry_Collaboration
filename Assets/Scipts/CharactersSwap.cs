using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static UnityEditor.Experimental.GraphView.GraphView;

// The script inherits from the Tutorial script in order to have access to the variables needed.
public class CharactersSwap : MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2D;
    [SerializeField] private GameObject Player3D;

    public static CharactersSwap Instance;
    public Transform character;
    public List<Transform> possibleCharacters;
    public CinemachineVirtualCamera cam;

    public int whichCharacter;

    private bool alreadychanged = false;
    private bool alreadychangedLevel3 = false;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void changeCharacter(int value)
    {
        whichCharacter += value;
    }
    public void Swap()
    {
        character = possibleCharacters[whichCharacter];
        if (whichCharacter == 1 && !alreadychanged)
        {
            Player1.SetActive(false);
            Player2D.SetActive(true);

            alreadychanged = true;
        }
        if (whichCharacter == 2 && !alreadychangedLevel3)
        {
            Player2D.SetActive(false);
            Player3D.SetActive(true);

            alreadychangedLevel3 = true;
        }
        character.GetComponent<Rigidbody2D>().gravityScale = 5;
       
        cam.LookAt = character;
        cam.Follow = character;
    }
}

