using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBoxSystem : MonoBehaviour
{

    private Vector3 RespawnPoint;
    [SerializeField] private Transform boxPos;

    // Start is called before the first frame update
    void Start()
    {
        RespawnPoint = boxPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death")) //Checks if we collided with an object with the tag "Death" 
        {
            transform.position = RespawnPoint; //Changes position of player to the box original position.
        }
    }
}
