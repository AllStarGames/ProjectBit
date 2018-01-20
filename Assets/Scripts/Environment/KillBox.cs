using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public bool softKillBox;

    private bool runTimer;
    private Collider obj;
    private float timer;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("[KillBox.cs]" + other.name + " is within " + name + "'s bounds!");
        obj = other;

        if(softKillBox)
        {
            runTimer = true;
        }
        else
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Local") || other.gameObject.layer == LayerMask.NameToLayer("Remote"))
            {
                //Kill the object
                if (other.CompareTag("Player") || other.CompareTag("Bot"))
                {
                    other.GetComponent<Health>().HealthVars().SetCurrentHealth(0.0f);
                }
                else if (other.CompareTag("Environment"))
                {
                    other.GetComponent<ObjectHealth>().SetCurrentHealth(0.0f);
                }
            }
        }

        
    }
    void Start()
    {
        runTimer = false;
        timer = 10.0f;
    }
    void Update()
    {
        if(runTimer)
        {
            Debug.Log("[KillBox.cs] OUT OF BOUNDS: " + timer.ToString());
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                //Stop and reset the timer
                runTimer = false;
                timer = 10.0f;

                //Kill object
                if (obj.gameObject.layer == LayerMask.NameToLayer("Local") || obj.gameObject.layer == LayerMask.NameToLayer("Remote"))
                {
                    //Kill the object
                    if (obj.CompareTag("Player") || obj.CompareTag("Bot"))
                    {
                        obj.GetComponent<Health>().HealthVars().SetCurrentHealth(0.0f);
                    }
                    else if (obj.CompareTag("Environment"))
                    {
                        obj.GetComponent<ObjectHealth>().SetCurrentHealth(0.0f);
                    }
                }
            }
        }
    }
}