using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public struct DamageVariables
    {
        [Tooltip("Amount of damage dealt to shields")]
        public float emp;
        [Tooltip("Amount of damage dealt to health")]
        public float physical;
    }
    [System.Serializable]
    public struct HeatVariables
    {
        [Tooltip("The rate at which this weapon will cool down when its not firing")]
        public float coolRate;
        [Tooltip("How long after firing this weapon will begin to cool down")]
        public float coolDelay;
        [Tooltip("The rate at which this weapon will heat up")]
        public float heatRate;
        [Tooltip("The rate at which this weapon will cool down after overheating")]
        public float overheatTime;
        [Tooltip("The rate at which this weapon will cool down when venting (reloading)")]
        public float ventRate;

        private float currentHeatLevel;

        public float CurrentHeatLevel()
        {
            return currentHeatLevel;
        }
        public void SetHeatLevel(float value)
        {
            currentHeatLevel = value;
        }
    }

    public Color lowHeatColour;
    public Color highHeatColour;
    public DamageVariables damage;
    public float fireRate;
    public float force;
    public float range;
    public HeatVariables heatSystem;
    public LayerMask mask;
    public Sprite icon;
    public string name;

    protected bool overheated;
    protected bool venting;
    protected Camera camera;
    protected Color emissionColour;
    protected float coolTimer;
    protected float fireTimer;
    protected List<ParticleSystem> muzzleFlash;
    protected List<ParticleSystem> steam;
    protected MeshRenderer meshRenderer;
    
    public virtual void Aim()
    {    }
    public virtual void Cool()
    {
        emissionColour = Color.Lerp(lowHeatColour, highHeatColour, heatSystem.CurrentHeatLevel() / 100.0f);
        meshRenderer.material.SetColor("_EmissionColor", emissionColour);

        coolTimer -= Time.deltaTime;
        if (coolTimer <= 0)
        {
            if (heatSystem.CurrentHeatLevel() > 0.0f)
            {
                heatSystem.SetHeatLevel(heatSystem.CurrentHeatLevel() - heatSystem.coolRate);
                if (heatSystem.CurrentHeatLevel() <= 0.0f)
                {
                    heatSystem.SetHeatLevel(0.0f);
                }
            }
        }
    }
    public virtual void Fire()
    {
        emissionColour = Color.Lerp(lowHeatColour, highHeatColour, heatSystem.CurrentHeatLevel() / 100.0f);
        meshRenderer.material.SetColor("_EmissionColor", emissionColour);

        coolTimer = heatSystem.coolDelay;
        fireTimer = fireRate;

        foreach(ParticleSystem flash in muzzleFlash)
        {
            flash.Play();
        }
    }

    public bool Overheated()
    {
        return overheated;
    }
    public bool Venting()
    {
        return venting;
    }
    public Camera CameraObject()
    {
        return camera;
    }
    public Color EmissionColour()
    {
        return emissionColour;
    }
    public float CoolTimer()
    {
        return coolTimer;
    }
    public float FireTimer()
    {
        return fireTimer;
    }
    public List<ParticleSystem> MuzzleFlashEffect()
    {
        return muzzleFlash;
    }
    public List<ParticleSystem> SteamEffect()
    {
        return steam;
    }
    public MeshRenderer MeshRendererObject()
    {
        return meshRenderer;
    }
    public void IsOverheated(bool value)
    {
        overheated = value;
    }
    public void IsVenting(bool value)
    {
        venting = value;
    }
    public void SetCamera(Camera cam)
    {
        camera = cam;
    }
    public void SetEmissionColour(Color colour)
    {
        emissionColour = colour;
    }
    public void SetCoolTimer(float value)
    {
        coolTimer = value;
    }
    public void SetFireTimer(float value)
    {
        fireTimer = value;
    }
    public void SetMeshRenderer(MeshRenderer mr)
    {
        meshRenderer = mr;
    }
    public void SetMuzzleFlash(List<ParticleSystem> effects)
    {
        muzzleFlash = effects;
    }
    public void SetSteam(List<ParticleSystem> effects)
    {
        steam = effects;
    }

    void Overheat()
    {
        Debug.Log("[" + GetType() + ".cs] Has been overheated!");
        
        foreach(ParticleSystem effect in steam)
        {
            if (effect.isStopped)
            {
                effect.Play();
            }
        }
        heatSystem.SetHeatLevel(heatSystem.CurrentHeatLevel() - (heatSystem.overheatTime * Time.deltaTime));
        if(heatSystem.CurrentHeatLevel() <= 0.0f)
        {
            foreach (ParticleSystem effect in steam)
            {
                if (effect.isPlaying)
                {
                    effect.Stop();
                }
            }
            heatSystem.SetHeatLevel(0.0f);
            overheated = false;
        }
    }
    void Start()
    {
        overheated = false;
        venting = false;

        heatSystem.SetHeatLevel(0.0f);
        emissionColour = lowHeatColour;

        muzzleFlash = new List<ParticleSystem>();
        steam = new List<ParticleSystem>();

        ParticleSystem[] pSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pSystem in pSystems)
        {
            if(pSystem.name == "MuzzleFlash")
            {
                muzzleFlash.Add(pSystem);
            }
            else if (pSystem.name == "Steam")
            {
                pSystem.Stop();
                steam.Add(pSystem);
            }
        }

        camera = transform.parent.parent.GetComponentInChildren<Camera>();
        if (!camera)
        {
            Debug.LogError("[Weapon.cs] " + transform.name + " cannot find " + transform.parent.name + "'s camera!");
        }

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        if(!meshRenderer)
        {
            Debug.LogError("[Weapon.cs] Could not find the mesh renderer on " + transform.name + "!");
        }
        meshRenderer.material.SetColor("_EmissionColor", emissionColour);
    }
    void Update()
    {
        if(overheated)
        {
            Overheat();
        }
        else if(venting)
        {
            Vent();
        }
    }
    void Vent()
    {
        Debug.Log("[" + GetType() + ".cs] Now being vented!");

        emissionColour = Color.Lerp(lowHeatColour, highHeatColour, heatSystem.CurrentHeatLevel() / 100.0f);
        meshRenderer.material.SetColor("_EmissionColor", emissionColour);
        foreach (ParticleSystem effect in steam)
        {
            if (effect.isStopped)
            {
                effect.Play();
            }
        }

        heatSystem.SetHeatLevel(heatSystem.CurrentHeatLevel() - (heatSystem.ventRate * Time.deltaTime));
        if(heatSystem.CurrentHeatLevel() <= 0.0f)
        {
            heatSystem.SetHeatLevel(0.0f);
            venting = false;
            foreach (ParticleSystem effect in steam)
            {
                if (effect.isPlaying)
                {
                    effect.Stop();
                }
            }
        }
    }
}