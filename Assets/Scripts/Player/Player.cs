using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    private Camera sceneCamera;
    private int id;
    private int score;

    public int ID()
    {
        return id;
    }
    public int Score()
    {
        return score;
    }

	// Use this for initialization
	void Start ()
    {
        sceneCamera = Camera.main;
        score = 0;

		if(isLocalPlayer)
        {
            if(sceneCamera)
            {
                //Disable the respawn UI
                GameManager.NetworkManagerHUD().enabled = false;
                sceneCamera.gameObject.SetActive(false);

                //Lock the cursor
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            foreach(Behaviour component in componentsToDisable)
            {
                component.enabled = false;
            }
        }
	}
    void OnDisable()
    {
        if(sceneCamera)
        {
            //Unlock the cursor
            Cursor.lockState = CursorLockMode.None;

            //Enable the respawn UI
            sceneCamera.gameObject.SetActive(true);
            GameManager.NetworkManagerHUD().enabled = true;
        }
    }
}