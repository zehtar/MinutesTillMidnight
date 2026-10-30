using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTowerEffector : MonoBehaviour
{
    public BladeEffect bladeTower;
    public VenomEffect venomTower;

    public void BladeEnable()
    {
        bladeTower.enabled = true;
    }

    public void VenomEnable()
    {
        venomTower.enabled = true;
        venomTower.SummonVenomAura();
    }
    public void VenomUpdate()
    {
        venomTower.UpdateVenomAura();
    }
}
