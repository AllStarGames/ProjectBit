using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Old : MonoBehaviour
{
    [System.Serializable]
    public struct DamageVariables
    {
        public float emp;
        public float physical;
    }

    public DamageVariables damage;
    //public float damage;
    public float fireRate;
    public float force;
    public float range;
    public float reloadTime;
    public GameObject impactEffect;
    public int maxAmmo;
    private ParticleSystem muzzleFlash;

    private Camera camera;
    private float fireTimer;
    private float reloadTimer;
    private GameObject[] impactEffectPool;
    private int currentAmmo;

    public DamageVariables Damage()
    {
        return damage;
    }
    public float FireRate()
    {
        return fireRate;
    }
    public float Force()
    {
        return force;
    }
    public float Range()
    {
        return range;
    }
    public float ReloadTime()
    {
        return reloadTime;
    }
    public float ReloadTimer()
    {
        return reloadTimer;
    }
    public int CurrentAmmo()
    {
        return currentAmmo;
    }
    public int MaxAmmo()
    {
        return maxAmmo;
    }

    GameObject GetImpactEffect()
    {
        for(int i = 0; i < maxAmmo; ++i)
        {
            if(!impactEffectPool[i].activeSelf)
            {
                return impactEffectPool[i];
            }
        }

        Debug.LogError("[Weapon.cs] All impact effects in pool for " + transform.name + " are in use!");
        return null;
    }
    void Fire()
    {
        --currentAmmo;
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, LayerMask.NameToLayer("Target")))
        {
            Debug.Log("[Weapon.cs]" + transform.name + " hit " + hit.transform.name);
            if(hit.transform.GetComponent<Health>())
            {
                //hit.transform.GetComponent<Health>().TakeDamage(damage);

                GameObject impact = GetImpactEffect();
                impact.transform.position = hit.point;
                impact.transform.rotation = Quaternion.LookRotation(hit.normal);
                impact.SetActive(true);
                impact.GetComponent<ParticleSystem>().Play();
            }
            if(hit.transform.GetComponent<Rigidbody>())
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(-hit.normal * force);
            }
        }
    }
    void Reload()
    {
        //Run the following code when an animation is available;
        /*Animator animator = transform.parent.parent.parent.GetComponent<Animator>();
        animator.SetBool("reload", true);
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            currentAmmo = maxAmmo;
        }*/

        reloadTimer -= Time.deltaTime;
        if(reloadTimer <= 0.0f)
        {
            currentAmmo = maxAmmo;
            reloadTimer = reloadTime;
        }
    }
    // Use this for initialization
	void Start ()
	{
        camera = transform.parent.parent.GetComponentInChildren<Camera>();
        if(!camera)
        {
            Debug.LogError("[Weapon.cs] " + transform.name + " cannot find " + transform.parent.name + "'s camera!");
        }
        fireTimer = 0.0f;
        reloadTimer = reloadTime;
        currentAmmo = maxAmmo;

        GameObject parent = new GameObject();
        parent.name = "Impact Effect Pool";
        impactEffectPool = new GameObject[maxAmmo];
        for(int i = 0; i < maxAmmo; ++i)
        {
            impactEffectPool[i] = Instantiate(impactEffect, Vector3.zero, Quaternion.identity);
            impactEffectPool[i].GetComponent<ParticleSystem>().Clear();
            impactEffectPool[i].GetComponent<ParticleSystem>().Stop();
            impactEffectPool[i].SetActive(false);
            impactEffectPool[i].transform.parent = parent.transform;
        }

        muzzleFlash = GetComponentInChildren<ParticleSystem>();
	}
	// Update is called once per frame
	void Update ()
	{
        fireTimer -= Time.deltaTime;
		if(Input.GetMouseButton(0) && fireTimer <= 0.0f && currentAmmo > 0)
        {
            fireTimer = fireRate;
            Fire();
        }

        if(currentAmmo == 0)
        {
            Reload();
            return;
        }
	}
}
