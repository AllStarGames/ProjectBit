using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    public float lifeSpan;

    private float timer;
    private ParticleSystem system;

	// Use this for initialization
	void Start ()
    {
        timer = lifeSpan;
        system = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            system.Clear();
            system.Stop();
            gameObject.SetActive(false);
        }
	}
}
