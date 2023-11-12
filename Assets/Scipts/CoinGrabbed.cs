using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinGrabbed : MonoBehaviour
{
    private Animator animator;
    private AudioSource au_Coin;

    private void Start()
    {
        au_Coin = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("GrabbedCoin", true);
            au_Coin.Play();
        }
    }
}
