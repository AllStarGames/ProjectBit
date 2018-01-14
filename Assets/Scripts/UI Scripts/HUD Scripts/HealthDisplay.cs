using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
	private Health playerHealth;
	private Text healthText;

	// Use this for initialization
	void Start ()
	{
		playerHealth = GetComponentInParent<Health>();
		healthText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		healthText.text = Mathf.RoundToInt(playerHealth.HealthVars().CurrentHealth()).ToString();
	}
}