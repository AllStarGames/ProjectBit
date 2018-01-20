using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    public float xForce;
    public float yForce;
    public float zForce;

    private Vector3 force;

	// Use this for initialization
	void Start ()
    {
        force = new Vector3(xForce, yForce, zForce);
	}
	
	void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            //other.GetComponent<Controller>().flags.IsDisabled(true);
            other.GetComponent<Rigidbody>().velocity = force;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            //other.GetComponent<Controller>().flags.IsDisabled(false);
        }
    }
}