using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPassives : MonoBehaviour
{
    public StatsSaved st;
    public List<GameObject> gunsList;
    public List<GameObject> gunsStatList;
    public string lastgunSelected;
    void Start()
    {
        st = FindObjectOfType<StatsSaved>();
    }

    void Update()
    {
        if (st.gunSelected == lastgunSelected) return;

        lastgunSelected = st.gunSelected;

        DeselectiveAll();
        
        switch(st.gunSelected)
        {
            case "Gun1": gunsList[0].SetActive(true); break;
            case "Gun2": gunsList[1].SetActive(true); break;
            case "Gun3": gunsList[2].SetActive(true); break;
            case "Gun4": gunsList[3].SetActive(true); break;
            case "Gun5": gunsList[4].SetActive(true); break;
            case "Gun6": gunsList[5].SetActive(true); break;
            default: Debug.Log("пусто тута"); break;
        }
    }

    public void DeselectiveAll()
    {
        for(int i = 0; i < 6; i++)
        {
            gunsList[i].SetActive(false);
        }
    }
}
