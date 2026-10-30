using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpIdent : MonoBehaviour
{
    public PowerUpIdent nextPU;
    public bool canBeApleed;
    public bool isBossDrop;
    
    public void nextPuCanBeSpawn()
    {
        nextPU.canBeApleed = true;
    }
}
