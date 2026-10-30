using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public StatsSaved statsSaved;
    public GameObject subCircle;
    void Start()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
       // transform.localScale = new Vector3(transform.localScale.x * statsSaved.collectRange, transform.localScale.y* statsSaved.collectRange, 1f);
        subCircle.transform.localScale = new Vector3(subCircle.transform.localScale.x * statsSaved.collectRange, subCircle.transform.localScale.y* statsSaved.collectRange, 1f);
    }
}
