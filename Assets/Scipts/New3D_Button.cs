using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New3D_Button : MonoBehaviour
{
    // We can move whatever gameobject we want.
    [SerializeField] private GameObject Platform;

    // This variable will be hanlded in the inspector only.
    private bool bIsOn;
    private bool bSwitch;

    [SerializeField] private Transform Pos1;
    [SerializeField] private Transform Pos2;

    // We get the animator.
    private Animator Button_anim;
    void Start()
    {
        // We get our Animator component of our object.
        Button_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // If bIsOn is true then it will call the function.
        if (bIsOn == true)
        {
            MoveUpDown();
        }
    }
    private void MoveUpDown()
    {
        // If bSwitch is equal to false, then the platform will go directly to our Pos1.
        if (bSwitch == false)
        {
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, Pos1.position, 2.5f * Time.deltaTime);
        }
        // If bSwitch is equal to true, then the platform will go directly to our Pos2.
        else if (bSwitch == true)
        {
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, Pos2.position, 2.5f * Time.deltaTime);
        }

        // We check if our platform is equal to Pos1. 
        // If it is then bSwitch will be true.
        if (Platform.transform.position == Pos1.position)
        {
            bSwitch = true;
        }
        // We check if our platform is equal to Pos2. 
        // If it is then bSwitch will be false.
        else if (Platform.transform.position == Pos2.position)
        {
            bSwitch = false;
        }
    }

    // When the player steps the button then an animation of the button will play going down.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // We set our bIsOn to true.
            // And we play a little animation.
            Button_anim.SetBool("Pressed", true);
            bIsOn = true;
        }
    }

    // When the player steps the button then an animation of the button will play going up.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // We play our animation of Pressed, false, when the player is not on the trigger.
        if (collision.gameObject.tag == "Player")
        {
            Button_anim.SetBool("Pressed", false);
        }
    }
}
