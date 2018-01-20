using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
	public static GameManager instance {get; private set;}

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

	public GameObject bulletPrefab;
	public int scoreToWin;

    private const string IDPrefix = "Player_";
	private GameObject[] bulletPool;

    public static GameObject GetBullet()
    {
        for(int b = 0; b < instance.bulletPool.Length; ++b)
        {
            if(!instance.bulletPool[b].activeSelf)
            {
                return instance.bulletPool[b];
            }
        }
        return null;
    }
	public static GameObject[] BulletPool()
	{
		return instance.bulletPool;
	}
    public static NetworkManager NetworkManager()
    {
        return instance.GetComponent<NetworkManager>();
    }
    public static NetworkManagerHUD NetworkManagerHUD()
    {
        return instance.GetComponent<NetworkManagerHUD>();
    }
	public static int ScoreToWin()
	{
		return instance.scoreToWin;
	}
    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }
    public static void RegisterPlayer(string playerID, Player playerObject)
    {
        string ID = IDPrefix + playerID;
        players.Add(ID, playerObject);

        playerObject.transform.name = ID;
    }
    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	// Use this for initialization
	void Awake()
	{
		if(instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(gameObject);
			return;
		}

		Player[] playersInGame = Object.FindObjectsOfType<Player>();
		foreach(Player player in playersInGame)
		{
            RegisterPlayer(player.ID(), player);
		}
	}
	void Start ()
	{
		//int numBullets = 0;
		//Weapon[] weapons = Object.FindObjectsOfType<Weapon>();
		//foreach(Weapon weapon in weapons)
		//{
		//	numBullets += (weapon.MagazineSize() * weapon.MaxNumMagazines());
		//}
		//GameObject parent = new GameObject();
		//parent.name = "BulletPool";
		//bulletPool = new GameObject[numBullets];
		//for(int b = 0; b < numBullets; ++b)
		//{
		//	bulletPool[b] = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
		//	bulletPool[b].SetActive(false);
		//	bulletPool[b].transform.parent = parent.transform;
		//}
	}
	// Update is called once per frame
	void Update ()
	{
		
	}
}
