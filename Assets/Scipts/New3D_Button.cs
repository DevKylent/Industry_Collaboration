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
        Button_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (bIsOn == true)
        {
            MoveUpDown();
        }
    }
    private void MoveUpDown()
    {
        if (bSwitch == false)
        {
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, Pos1.position, 2.5f * Time.deltaTime);
        }
        else if (bSwitch == true)
        {
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, Pos2.position, 2.5f * Time.deltaTime);
        }

        if (Platform.transform.position == Pos1.position)
        {
            bSwitch = true;
        }
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
            Button_anim.SetBool("Pressed", true);
            bIsOn = true;
        }
    }

    // When the player steps the button then an animation of the button will play going up.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Button_anim.SetBool("Pressed", false);
        }
    }
}
