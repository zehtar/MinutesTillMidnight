using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackDelete : MonoBehaviour
{
    public ToCanvasEffectScript canvas;
    void Start()
    {
        //Destroy(gameObject, 0.1f);
        canvas = FindObjectOfType<ToCanvasEffectScript>();
        transform.SetParent(canvas.gameObject.transform);
    }

    public void EffectOff()
    {
        Destroy(gameObject);
    }
}
