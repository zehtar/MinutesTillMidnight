using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpAnim : MonoBehaviour
{
    public Level lv;
  
    public void Start()
    {
        lv = FindObjectOfType<Level>();
    }
    
    public void OnAnimationOver()
    {
        Destroy(gameObject);
        lv.LevelUp();
    }
}
