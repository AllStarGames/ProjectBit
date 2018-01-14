using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[System.Serializable]
	public struct HealthVariables
	{
		private bool decaying;
		private float currentHealth;
		private float maxHealth;

		public bool Decaying()
		{
			return decaying;
		}
		public float CurrentHealth()
		{
			return currentHealth;
		}
		public float MaxHealth()
		{
			return maxHealth;
		}
		public void Decay(bool value)
		{
			decaying = value;
		}
		public void Initialize()
		{
			decaying = false;

			maxHealth = 100;
			currentHealth = maxHealth;
		}
		public void SetCurrentHealth(float value)
		{
			currentHealth = value;
		}
		public void SetMaxHealth(float value)
		{
			maxHealth = value;
		}
	}
	[System.Serializable]
	public struct ShieldVarialbes
	{
		public bool canRegen;
		public float regenDelay;
		public float regenRate;

		private bool decaying;
		private float currentShields;
		private float delayTimer;
		private float maxShields;

		public bool CanRegen()
		{
			return canRegen;
		}
		public bool Decaying()
		{
			return decaying;
		}
		public float CurrentShields()
		{
			return currentShields;
		}
		public float DelayTimer()
		{
			return delayTimer;
		}
		public float MaxShields()
		{
			return maxShields;
		}
		public float RegenDelay()
		{
			return regenDelay;
		}
		public float RegenRate()
		{
			return regenRate;
		}
		public void Decay(bool value)
		{
			decaying = value;
		}
		public void Initialize()
		{
			decaying = false;

			maxShields = 100.0f;
			currentShields = maxShields;

			delayTimer = regenDelay;
		}
		public void Regen(bool value)
		{
			canRegen = value;
		}
		public void Run()
		{
			if(canRegen)
			{
				if(currentShields < maxShields)
				{
					delayTimer -= Time.deltaTime;

					if(delayTimer <= 0.0f)
					{
						// Regen the shields til full or hit
						currentShields += regenRate;
						if(currentShields >= maxShields)
						{
							currentShields = maxShields;
						}
					}
				}
			}
		}
		public void SetCurrentShields(float value)
		{
			currentShields = value;
		}
		public void SetDelayTimer(float value)
		{
			delayTimer = value;
		}
		public void SetMaxShields(float value)
		{
			maxShields = value;
		}
		public void SetRegenDelay(float value)
		{
			regenDelay = value;
		}
		public void SetRegenRate(float value)
		{
			regenRate = value;
		}
	}

	public ShieldVarialbes shields;

	private bool dead;
	private bool debugMode;
	private HealthVariables health;

	// Method for retrieving the death flag externally
	public bool Dead()
	{
		return dead;
	}
	// Method for retrieving the health variables externally
	public HealthVariables HealthVars()
	{
		return health;
	}
	// Method for retrieving the shield variables externally
	public ShieldVarialbes Shields()
	{
		return shields;
	}
	// Method for setting the death flag externally
	public void IsDead(bool value)
	{
		dead = value;
	}
	public void TakeDamage(Weapon.DamageVariables damage)
	{
		shields.SetDelayTimer(shields.RegenDelay());
		if(shields.CurrentShields() - damage.emp <= 0.0f)
		{
			//float remainder = Mathf.Abs(0.0f - (shields.CurrentShields() - damage.emp));
            shields.SetCurrentShields(0.0f);
			health.SetCurrentHealth(health.CurrentHealth() - damage.physical);
		}
		else
		{
			shields.SetCurrentShields(shields.CurrentShields() - damage.emp);
		}
	}

	// Method for handling the death (or destruction) of this object
	void Death()
	{
#if UNITY_EDITOR
		if(debugMode)
		{
			Debug.Log("[" + gameObject.name + "->Health.cs] " + gameObject.name + " has died or been destroyed!");
		}
#endif
		dead = true;
        gameObject.SetActive(false);
	}
	// Unity initialization method
	void Start ()
	{
		dead = false;
		debugMode = false;

		health.Initialize();
		shields.Initialize();
	}
	// Unity update method
	void Update ()
	{
#if UNITY_EDITOR
		if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.F12))
		{
			debugMode = !debugMode;
			switch(debugMode)
			{
				case true:
					Debug.Log("[" + gameObject.name + "->Health.cs] Debug mode enabled");
					break;
				case false:
					Debug.Log("[" + gameObject.name + "->Health.cs] Debug mode disabled");
					break;
			}
			
		}

		if(debugMode)
		{
			if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.F1))
			{
                Weapon.DamageVariables testDamage;
                testDamage.physical = 10.0f;
                testDamage.emp = 100.0f;
				TakeDamage(testDamage);
			}
		}
#endif
		// Check if the player is already dead
		if(!dead)
		{
			//Check for death
			if(health.CurrentHealth() <= 0.0f)
			{
				//This object is dead (or destroyed)
				Death();
			}

			shields.Run();
		}
		else
		{
			// Run respawning logic
			Canvas[] canvases = GetComponentsInChildren<Canvas>();
			foreach(Canvas canvas in canvases)
			{
				if(canvas.gameObject.name == "HUD_Canvas")
				{
					canvas.enabled = false;
				}
				else if(canvas.gameObject.name == "Respawn_Canvas")
				{
					canvas.enabled = true;
				}
			}

			Cursor.lockState = CursorLockMode.None;
		}
	}
}
