using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public player player;
    
    public void Start()
    {
        player = FindObjectOfType<player>();
    }
    
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y , 0f);
    }   
}
