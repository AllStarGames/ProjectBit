using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public Color fullHealthColour;
    public Color lowHealthColour;
    public float maxHealth;
    public float respawnTime;

    private bool dead;
    private Color emissionColour;
    private Collider collider;
    private float currentHealth;
    private float timer;
    private MeshRenderer meshRenderer;
    private Renderer[] renderers;

    public void TakeDamage(Weapon.DamageVariables damage)
    {
        currentHealth -= damage.physical;
        if(currentHealth <= 0.0f)
        {
            Death();
        }
    }

    void Death()
    {
        dead = true;

        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
        collider.enabled = false;
    }
	// Use this for initialization
	void Start ()
    {
        dead = false;
        currentHealth = maxHealth;
        timer = respawnTime;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        if(!meshRenderer)
        {
            Debug.LogError("[ObjectHealth.cs] Cannont find the mesh renderer on " + transform.name + "!");
        }
        collider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();

        emissionColour = fullHealthColour;
        meshRenderer.materials[1].SetColor("_EmissionColor", emissionColour);
	}
	// Update is called once per frame
	void Update ()
    {
        if(dead)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }
                collider.enabled = true;

                dead = false;
                timer = respawnTime;
                currentHealth = maxHealth;
            }
        }

        emissionColour = Color.Lerp(lowHealthColour, fullHealthColour, (currentHealth / maxHealth));
        meshRenderer.materials[1].SetColor("_EmissionColor", emissionColour);

        if (Input.GetKeyDown(KeyCode.K))
        {
            Weapon.DamageVariables damage = new Weapon.DamageVariables();
            damage.physical = 1.0f;
            TakeDamage(damage);
        }
    }
}