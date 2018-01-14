using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float swapTime;
    public int maxWeaponsAllowed;

    private float timer;
    private int currentWeapon;
    private Weapon[] weapons;
    
    public Weapon CurrentWeapon()
    {
        return weapons[currentWeapon];
    }

	// Use this for initialization
	void Start ()
    {
        timer = swapTime;
        currentWeapon = 0;

        weapons = GetComponentsInChildren<Weapon>();
        for(int w = 0; w < weapons.Length; ++w)
        {
            if(w == currentWeapon)
            {
                weapons[w].gameObject.SetActive(true);
            }
            else
            {
                weapons[w].gameObject.SetActive(false);
            }
        }
	}
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            //Next weapon
            weapons[currentWeapon].gameObject.SetActive(false);
            ++currentWeapon;
            if(currentWeapon > (weapons.Length - 1))
            {
                currentWeapon = 0;
            }
            weapons[currentWeapon].gameObject.SetActive(true);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            //Previous weapon
            weapons[currentWeapon].gameObject.SetActive(false);
            --currentWeapon;
            if (currentWeapon < 0)
            {
                currentWeapon = (weapons.Length - 1);
            }
            weapons[currentWeapon].gameObject.SetActive(true);
        }
	}
}
