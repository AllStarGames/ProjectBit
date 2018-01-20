using UnityEngine;
using UnityEngine.Networking;

//public class Hand : MonoBehaviour
//{
//    public float swapTime;
//    public int maxWeaponsAllowed;
//
//    private float timer;
//    private int currentWeapon;
//    private Weapon[] weapons;
//    
//    public Weapon CurrentWeapon()
//    {
//        return weapons[currentWeapon];
//    }
//
//     void FireWeapon()
//    {
//        if(weapons.Length > 0)
//        {
//            weapons[currentWeapon].SetFireTimer(weapons[currentWeapon].FireTimer() - Time.deltaTime);
//            if(!weapons[currentWeapon].Overheated())
//            {
//                if(Input.GetMouseButton(0) && weapons[currentWeapon].FireTimer() <= 0.0f)
//                {
//                    weapons[currentWeapon].CmdFire();
//                }
//                else
//                {
//                    weapons[currentWeapon].CmdCool();
//                }
//            }
//        }
//    }
//    void PickUpWeapon()
//    {
//
//    }
//	// Use this for initialization
//	void Start ()
//    {
//        timer = swapTime;
//        currentWeapon = 0;
//
//        weapons = GetComponentsInChildren<Weapon>();
//        for(int w = 0; w < weapons.Length; ++w)
//        {
//            if(w == currentWeapon)
//            {
//                weapons[w].gameObject.SetActive(true);
//            }
//            else
//            {
//                weapons[w].gameObject.SetActive(false);
//            }
//        }
//	}
//    void SwapWeapons()
//    {
//        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
//        {
//            //Next weapon
//            weapons[currentWeapon].gameObject.SetActive(false);
//            ++currentWeapon;
//            if (currentWeapon > (weapons.Length - 1))
//            {
//                currentWeapon = 0;
//            }
//            weapons[currentWeapon].gameObject.SetActive(true);
//        }
//        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
//        {
//            //Previous weapon
//            weapons[currentWeapon].gameObject.SetActive(false);
//            --currentWeapon;
//            if (currentWeapon < 0)
//            {
//                currentWeapon = (weapons.Length - 1);
//            }
//            weapons[currentWeapon].gameObject.SetActive(true);
//        }
//    }
//	// Update is called once per frame
//	void Update ()
//    {
//        FireWeapon();
//        PickUpWeapon();
//        SwapWeapons();
//        VentWeapon();
//	}
//    void VentWeapon()
//    {
//        if(Input.GetKeyDown(KeyCode.R) && weapons[currentWeapon].heatSystem.CurrentHeatLevel() > 0.0f)
//        {
//            weapons[currentWeapon].IsVenting(true);
//        }
//    }
//}