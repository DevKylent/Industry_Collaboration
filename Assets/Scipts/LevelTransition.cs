using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    // We get the animator from the canvas called 'TransitionScreen'.
    [SerializeField] private Animator transition;

    // Our collisions before going to 2D.
    [SerializeField] private GameObject CollisionWallL2;
    [SerializeField] private GameObject SecondCollisionL2;

    // Our collision before that will stop the player form going back.
    [SerializeField] private GameObject CollisionWallL3;

    // The background of the second level.
    [SerializeField] private GameObject Level_2_Background_Art;

    public void FirstTransition()
    {
        // This function will be in charge of starting a Coroutine.
        StartCoroutine(BeginTransition());

        // It will set active (true) to our first Gameobject to not let the player go back to the previous level.
        CollisionWallL2.SetActive(true);

        // This Gameobject is active this helps us not let the player miss the transition.
        // It will set active (false) to our second gameobject, that will let the player continue the game. 
        SecondCollisionL2.SetActive(false);
    }

    public void SecondTransition()
    {
        // This function will be in charge of making the second transition. From 2D to 3D.
        StartCoroutine(BeginTransition());

        // It will set active (true) to our first Gameobject to not let the player go back to the previous level.
        CollisionWallL3.SetActive(true);
    }

    IEnumerator BeginTransition()
    {
        // This will set the bool, 'Active', on our animator to true. This will play the animation to begin the transition.
        transition.SetBool("Active", true);

        // This will make us wait for 1.5 seconds.
        yield return new WaitForSeconds(1.5f);

        // Will activate the background.
        Level_2_Background_Art.SetActive(true);

        // This will set the bool, 'Active', on our animator to false. This will play the animation to end the transition.
        transition.SetBool("Active", false);
    }
}
