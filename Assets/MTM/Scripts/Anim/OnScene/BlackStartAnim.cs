using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStartAnim : MonoBehaviour
{
     public void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}
