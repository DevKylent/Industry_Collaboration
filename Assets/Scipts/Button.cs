using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Button : MonoBehaviour
{
    //This Script needs to be added to every single button, each button has one door that it opens if we want it to open multiple then we would need to create another reference for it

    //Reference for the door we will open
    public GameObject OpenWall;

    //Boolians used to know what object is opening the door
    //private bool IsOpenedbyPlayer = false;
    //private bool IsOpenedbyBox = false;

    //References for the colliders of all parties involved in opening a door the box, player and the button itself.
    [SerializeField] Collider2D boxCollider;
    [SerializeField] Collider2D buttonCollider;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] float BridgeOrWall = 0f;
    private bool activated = false;


    //private bool wallOpened = true;
    //private bool BridgeOpened = false;
    private void Update()
    {
        //IsTouching() checks if two colliders are touching or are not
        bool ItIstouching = Physics2D.IsTouching(collider1: boxCollider, collider2: buttonCollider);
        bool ItIstouching2 = Physics2D.IsTouching(collider1: playerCollider, collider2: buttonCollider);



        /*could replace the OnTrigger Functions but decided not to use it because if a puzzle were to have multiple boxes that can activate the same button we would have to create another reference for that box 
         * If when the level designs are complete and we verify that it is impossible to get a different box to the same button we could use the IsTouching functions only
        */
        if (ItIstouching || ItIstouching2)
        {
            //PlayAudio();
            if (BridgeOrWall == 1)
            {
                //Door
                OpenWall.SetActive(false);
                activated = true;
            }
            else
            {
                OpenWall.SetActive(true);
                activated = true;
            }


        }
        else
        {
            if (BridgeOrWall == 1)
            {
                //Door
                OpenWall.SetActive(true);
                activated = false;
            }
            else
            {
                OpenWall.SetActive(false);
                //PlayAudio();
                activated = false;
            }

        }

    }

    //Add Ontrigger enter to play Audio
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!activated)
                PlayAudio();
        }
        if (other.CompareTag("Box"))
        {
            if (!activated)
                PlayAudio();
        }
    }
    private void PlayAudio()
    {
        //OpenWall.SetActive(false);
        AudioManager.Instance.Play("OpenDoor");
    }
}
