using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] localComponents;
    [SerializeField]
    string remotePlayerLayerName;

    private Camera sceneCamera;
    private int score;
    private JointDrive drive;

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameManager.RegisterPlayer(ID(), GetComponent<Player>());
    }

    public int Score()
    {
        return score;
    }
    public string ID()
    {
        return GetComponent<NetworkIdentity>().netId.ToString();
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remotePlayerLayerName);
    }
    void DisableLocalComponents()
    {
        foreach (Behaviour component in localComponents)
        {
            component.enabled = false;
        }
    }
    void DisableRespawnUI()
    {
        GameManager.NetworkManagerHUD().enabled = false;
        sceneCamera.gameObject.SetActive(false);
    }
	// Use this for initialization
	void Start ()
    {
        sceneCamera = Camera.main;
        score = 0;

        drive = GetComponent<ConfigurableJoint>().yDrive;
        drive.mode = JointDriveMode.Position;

		if(isLocalPlayer)
        {
            if(sceneCamera)
            {
                DisableRespawnUI();

                //Lock the cursor
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            AssignRemoteLayer();
            DisableLocalComponents();
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