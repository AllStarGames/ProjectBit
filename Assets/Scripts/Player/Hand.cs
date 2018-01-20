using UnityEngine;
using UnityEngine.Networking;

public class Hand : NetworkBehaviour
{
    private Camera playerCamera;
    private int currentWeapon;
    private Weapon[] weapons;

    public Weapon CurrrentWeapon()
    {
        return weapons[currentWeapon];
    }

    [Command]
    void CmdRegisterDamage(string playerID)
    {
        Debug.Log(playerID + " has taken damage!");
    }

    [Client]
    void FireWeapon()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, weapons[currentWeapon].range, weapons[currentWeapon].mask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                CmdRegisterDamage(hit.collider.name);
            }
        }

    //weapons[currentWeapon].SetFireTimer(weapons[currentWeapon].FireTimer() - Time.deltaTime);
    //if(Input.GetMouseButton(0) && weapons[currentWeapon].FireTimer() <= 0.0f)
    //{
    //    weapons[currentWeapon].Fire();
    //}
    }
	// Use this for initialization
	void Start ()
    {
        //Get reference to the player's camera
        playerCamera = GetComponentInChildren<Camera>();
        if(!playerCamera)
        {
            Debug.LogError("[Hand.cs] Cannot find the player's camera!");
        }

        //Get reference to current weapon
        currentWeapon = 0;
        weapons = GetComponentsInChildren<Weapon>();
        foreach(Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        weapons[currentWeapon].gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            FireWeapon();
        }
        VentWeapon();
	}
    void VentWeapon()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(weapons[currentWeapon].heatSystem.CurrentHeatLevel() > 0.0f)
            {
                weapons[currentWeapon].IsVenting(true);
            }
        }
    }
}