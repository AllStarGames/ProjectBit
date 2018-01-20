using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encoder : Weapon
{
    public override void Aim()
    {
        base.Aim();
    }
    public override void Fire()
    {
        base.Fire();

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, mask))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Player player = GameManager.GetPlayer(hit.transform.name);
                if(player.GetComponent<Health>())
                {
                    player.GetComponent<Health>().TakeDamage(damage);
                    if(player.GetComponent<Rigidbody>())
                    {
                        player.GetComponent<Rigidbody>().AddForce(-hit.normal * force);
                    }
                    else
                    {
                        Debug.LogError("[] Could not find the Rigidbody component on " + hit.transform.name + "!");
                    }
                }
                else
                {
                    Debug.LogError("[Encoder.cs] Could not find the Health component on " + hit.transform.name + "!");
                }
            }

            //Debug.Log("[Weapon.cs]" + transform.name + " hit " + hit.transform.name);
            //if (hit.transform.GetComponent<Health>())
            //{
            //    hit.transform.GetComponent<Health>().TakeDamage(damage);
            //    if (hit.transform.GetComponent<Rigidbody>())
            //    {
            //        hit.transform.GetComponent<Rigidbody>().AddForce(-hit.normal * force);
            //    }
            //}
            //else if (hit.transform.GetComponent<ObjectHealth>())
            //{
            //    hit.transform.GetComponent<ObjectHealth>().TakeDamage(damage);
            //}
        }

        heatSystem.SetHeatLevel(heatSystem.CurrentHeatLevel() + heatSystem.heatRate);
        if (heatSystem.CurrentHeatLevel() >= 100.0f)
        {
            heatSystem.SetHeatLevel(100.0f);
            overheated = true;
        }
    }
}