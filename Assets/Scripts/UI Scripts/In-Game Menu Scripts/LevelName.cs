using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelName : MonoBehaviour
{
	private Text levelName;
	private Text modeName;

	// Unity initialization method
	void Start ()
	{
		levelName = GetComponent<Text>();
		if(!levelName)
		{
			Debug.LogError("[" + gameObject.name + "-->LevelName.cs] Cannot find the text component!");
		}

		levelName.text = SceneManager.GetActiveScene().name + " | Mode";
	}
}
