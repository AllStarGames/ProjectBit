using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    private float timer;

    void RandomizeDirection()
    {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance >= 0.5f)
        {
            xSpeed = -xSpeed;
        }
        chance = Random.Range(0.0f, 1.0f);
        if (chance >= 0.5f)
        {
            ySpeed = -ySpeed;
        }
        chance = Random.Range(0.0f, 1.0f);
        if (chance >= 0.5f)
        {
            zSpeed = -zSpeed;
        }
    }
    void Start()
    {
        RandomizeDirection();
        timer = 10.0f;
    }
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            //Randomize direction
            RandomizeDirection();

            //Reset the timer
            timer = 10.0f;
        }

        //Rotate along the x-axis
        transform.Rotate(Vector3.right * xSpeed, Space.World);
        transform.Rotate(Vector3.up * ySpeed, Space.World);
        transform.Rotate(Vector3.forward * zSpeed, Space.World);
	}
}
