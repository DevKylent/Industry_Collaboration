using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private Animator transition;

    [SerializeField] private GameObject Player2D;
    [SerializeField] private GameObject Player3D;

    [SerializeField] private GameObject CollisionWallL2;
    [SerializeField] private GameObject CollisionWallL3;

    [SerializeField] private GameObject Level_2_Background_Art;

    public void FirstTransition()
    {
        StartCoroutine(BeginTransition());
        CollisionWallL2.SetActive(true);
    }

    public void SecondTransition()
    {
        StartCoroutine(BeginTransition());
        CollisionWallL3.SetActive(true);
    }

    IEnumerator BeginTransition()
    {

        transition.SetBool("Active", true);

        yield return new WaitForSeconds(1.5f);

        Level_2_Background_Art.SetActive(true);

        transition.SetBool("Active", false);
    }
}
