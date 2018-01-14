using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	public GameObject mainObjects;
	public GameObject settingsObjects;

	private Canvas canvas;

	public Canvas GetCanvas()
	{
		return canvas;
	}
	public void Reset()
	{
		mainObjects.SetActive(true);
		settingsObjects.SetActive(false);
	}
	public void ToggleMenu()
	{	
		// Reset the menu
		Reset();

		switch (Cursor.lockState)
		{
			case CursorLockMode.None:
			    Cursor.lockState = CursorLockMode.Locked;
				break;
			case CursorLockMode.Locked:
				Cursor.lockState = CursorLockMode.None;
				break;
		}
		canvas.enabled = !canvas.enabled;
	}

	// Use this for initialization
	void Start ()
	{
		canvas = GetComponent<Canvas>();
	}
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleMenu();
		}
	}
}
