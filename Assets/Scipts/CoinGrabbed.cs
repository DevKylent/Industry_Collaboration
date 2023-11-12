using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinGrabbed : MonoBehaviour
{
    [SerializeField] MeshFilter coin;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coin.GetInstanceID();
        if (coin.GetInstanceID() == null)
        {
            animator.SetBool("IsDestroyed", true);
        }
        else
        {
            animator.SetBool("IsDestroyed", false);
        }
    }
    
    
}
