using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    // public float minSpeed = 0.01f;
    // public float maxSpeed = 0.05f;
    public int minSpeed = 0;
    public int maxSpeed = 20;
    public float endPositionX = 20f; // x pos to where the cloud will loop back
    private Vector3 startPosition;

    void Start()
    {
        // set initial pos (based on pos of cloud sprite)
        startPosition = transform.position;
    }

    void Update()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        // move cloud horizontally by small "speed" each frame
        transform.position += new Vector3(randomSpeed, 0, 0) * Time.deltaTime;

        // reset back to starting point if cloud goes beyond end pos 
        if (transform.position.x > endPositionX)
        {
            transform.position = new Vector3(startPosition.x, transform.position.y, transform.position.z);
        }
    }
}