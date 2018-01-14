using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public int direction;
    public int offset;
    private Transform playerTransform;

    void OnGUI()
    {
        if (offset > -85 && offset < 90)
        {
            GUI.Label(new Rect((Screen.width / 2) - offset * 2, (Screen.height) - 50, 180, 25), "N");
        }

        if (offset > 5 && offset < 180)
        {
            GUI.Label(new Rect((Screen.width / 2) - offset * 2 + 180, (Screen.height) - 50, 180, 25), "E");
        }

        if ((direction > 95 && offset > 95) || (direction < 276 && offset < -90))
        {
            GUI.Label(new Rect((Screen.width / 2) - direction * 2 + 360, (Screen.height) - 50, 180, 25), "S");
        }

        if ((direction > 186 && offset < -5))
        {
            GUI.Label(new Rect((Screen.width / 2) - direction * 2 + 540, (Screen.height) - 50, 180, 25), "W");
        }

        GUI.Box(new Rect((Screen.width / 2) - 180, (Screen.height) - 65, 360, 35), "Heading");
    }
	// Use this for initialization
	void Start ()
    {
        playerTransform = GetComponentInParent<Player>().transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        direction = (int)Mathf.Abs(playerTransform.eulerAngles.y);
        if (direction > 360)
        {
            direction = direction % 360;
        }

        offset = direction;
        if (offset > 180)
        {
            offset = offset - 360;
        }
    }
}