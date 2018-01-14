using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    private Color defaultEmision;
    private Color warningEmision;
    private float timer;
    private Material material;
    private Text ammotext;
    private Weapon playerWeapon;

	// Use this for initialization
	void Start ()
    {
        defaultEmision = new Color(0.079f, 0.2f, 0.24f);
        warningEmision = new Color(0.42f, 0.05f, 0.0f);

        timer = 0.5f;

        material = GetComponent<MeshRenderer>().material;
        playerWeapon = transform.parent.GetComponent<Weapon>();
        ammotext = GetComponentInChildren<Text>();

        material.EnableKeyword("_EMISSION");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Update the indicator's colour based on the amount of ammo left in the magazine
        /*if(playerWeapon.CurrentAmmo() == 0)
        {
            //Set it to the warning colour
            material.SetColor("_Emission", warningEmision);
        }
        else if(playerWeapon.CurrentAmmo() <= Mathf.RoundToInt(playerWeapon.MaxAmmo() * 0.3f) && playerWeapon.CurrentAmmo() > 0)
        {
            //Flash the indicator
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                if(material.GetColor("_Emission") == defaultEmision)
                {
                    material.SetColor("_Emission", warningEmision);
                }
                else
                {
                    material.SetColor("_Emission", defaultEmision);
                }
                timer = 0.5f;
            }
        }
        else
        {
            //Keep the default colour
            material.SetColor("_Emission", defaultEmision);
        }
        
        //Display the amount of ammo remaining
        if(playerWeapon.CurrentAmmo() < 10)
        {
            ammotext.text = "0" + playerWeapon.CurrentAmmo().ToString();
        }
        else
        {
            ammotext.text = playerWeapon.CurrentAmmo().ToString();
        }*/
	}
}