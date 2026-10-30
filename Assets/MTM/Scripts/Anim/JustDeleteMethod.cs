using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDeleteMethod : MonoBehaviour
{
    public List<GameObject> list;
    // Start is called before the first frame update
    public void DeleteThis()
    {
        Destroy(gameObject);
    }

    public void DisableThis()
    {
        gameObject.SetActive(false);
    }

    public void DeactivateAllLevelObjects()
    {
        foreach (var l in list)
        {
            if (l != null)
            {
                l.SetActive(false);
                Debug.Log($"Отключен объект: {l.name}");
            }
            else
            {
                Debug.Log("пизда");
            }
        }
    }

}
