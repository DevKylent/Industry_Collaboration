using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Programmer: Carlos A. Rodriguez
 * Purpose: This code is meant to have the camera follow the player after the player reaches a predetermined threshhold
 */

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject followObject; //Target or player
    [SerializeField] private Vector2 followOffset; //Size of threshhold
    [SerializeField] private float speed = 20f; //Camera movement speed Intentionally set to be higher than the players movement speed
    [SerializeField] private Vector2 threshholdPosition;
    private Vector2 threshhold; //Camera threshhold
    private Rigidbody2D rb; //Player rigidbody

    //Variables that record original Y and Z position of the camera. Used to reposition the camera when player respawns
    private float originalYPosition = 0;
    private float originalZPosition = 0;

    private void Start()
    {

        threshhold = CalculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>(); //Player RigidBody
        originalYPosition = transform.position.y;
        originalZPosition = transform.position.z;

    }

    private void Update()
    {

        //Calculates the difrence between the player position and the threshhold 
        Vector2 follow = followObject.transform.position; //player position
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        //If the player position is outside the threshhold, set the position towards which the camera will begin to move
        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshhold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshhold.y)
        {
            newPosition.y = follow.y;
        }

        //If the player is VERY far away from the camera, then it has respawned and the camera will snap to its position instead of mooving normaly
        if (Mathf.Abs(yDifference) >= threshhold.y && Mathf.Abs(yDifference) >= 100f || Mathf.Abs(xDifference) >= threshhold.x && Mathf.Abs(xDifference) >= 100f)
        {
            transform.position = new Vector3(newPosition.x, originalYPosition, originalZPosition);
        }

        //Move the camera towards the previously set position
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);


    }

    //Calculate the threshold size relative to the set camera size (Changing the aspect ratio will affect the threshold size) 
    private Vector3 CalculateThreshold()
    {

        Rect aspect = Camera.main.pixelRect;
        aspect.position = new Vector2(aspect.position.x + threshholdPosition.x, aspect.position.y + threshholdPosition.x);
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;

        return t;
    }

    //Draws a visual representation of the camera threshold only visible in the editor
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Vector2 wireCubePosition = new Vector2(transform.position.x + threshholdPosition.x, transform.position.y + threshholdPosition.y);
        Gizmos.DrawWireCube(wireCubePosition, new Vector3(border.x * 2, border.y * 2, 1));

    }

}
