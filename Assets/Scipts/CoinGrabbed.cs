using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinGrabbed : MonoBehaviour
{
    [SerializeField] GameObject coin;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play("GrabbedCoin");
        Destroy(coin,2.21f);
        
    }

    

}
