using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : Weapon
{
    public override void Fire()
    {
        if(!overheated && !venting)
        {
            base.Fire();

            heatSystem.SetHeatLevel(heatSystem.CurrentHeatLevel() + heatSystem.heatRate);
            if (heatSystem.CurrentHeatLevel() >= 100.0f)
            {
                heatSystem.SetHeatLevel(100.0f);
                overheated = true;
            }
        }
    }
}