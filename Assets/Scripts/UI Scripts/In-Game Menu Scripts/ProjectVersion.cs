using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectVersion : MonoBehaviour
{
	private Text version;
	
	// Unity initialization method
	void Start ()
	{
		version = GetComponent<Text>();
		if(!version)
		{
			Debug.LogError("[" + gameObject.name + "-->ProjectVersion.cs] Cannot find the text component!");
		}

		version.text = Application.version;
	}
}
