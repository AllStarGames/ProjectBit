using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDisplay : MonoBehaviour
{
	private Health playerHealth;
	private Text shieldText;

	// Use this for initialization
	void Start ()
	{
		playerHealth = GetComponentInParent<Health>();
		shieldText = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float percentage = (playerHealth.Shields().CurrentShields() / playerHealth.Shields().MaxShields()) * 100.0f;
		shieldText.text = Mathf.RoundToInt(percentage).ToString() + "%";
	}
}
