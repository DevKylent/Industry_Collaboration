using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinGrabbed : MonoBehaviour
{
    [SerializeField] MeshRenderer coin;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabCoin()
    {
        animator.SetBool("GrabbedCoin", true);
       
        
    }

    

}
