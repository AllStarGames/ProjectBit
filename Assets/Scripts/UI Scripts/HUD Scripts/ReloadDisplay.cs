using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadDisplay : MonoBehaviour
{
    private Image progressBar;
    private Text reloadText;
    private Weapon weapon;

	// Use this for initialization
	void Start ()
    {
        progressBar = GetComponentInChildren<Image>();
        reloadText = GetComponentInChildren<Text>();
        //weapon = transform.parent.parent.parent.parent.GetComponentInChildren<Hand>().CurrentWeapon();
	}
	
	// Update is called once per frame
	void Update ()
    {
		/*if(weapon.CurrentAmmo() == 0)
        {
            //Enable the UI
            reloadText.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(true);

            float xScale = weapon.ReloadTimer() * 0.25f;
            Vector3 scale = new Vector3(xScale, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
            progressBar.transform.localScale = scale;
        }
        else
        {
            //Disable the UI
            reloadText.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(false);
        }*/
	}
}
