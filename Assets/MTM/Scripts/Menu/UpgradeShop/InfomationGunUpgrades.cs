using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfomationGunUpgrades : MonoBehaviour
{
    public StatsSaved st;
    public GameObject[] infoPanels;

    void Awake()
    {
        st = FindObjectOfType<StatsSaved>();
    }

    private void DeSelectAll()
    {
        for(int i = 0; i< infoPanels.Length;i++)
        {
            infoPanels[i].SetActive(false);
        }
    }

    public void SelectInfoPanel()
    {
        DeSelectAll();
        switch(st.gunSelected)
        {
            case "Gun1": infoPanels[0].SetActive(true); break;
            case "Gun2": infoPanels[1].SetActive(true); break;
            case "Gun3": infoPanels[2].SetActive(true); break;
            case "Gun4": infoPanels[3].SetActive(true); break;
            case "Gun5": infoPanels[4].SetActive(true); break;
            case "Gun6": infoPanels[5].SetActive(true); break;
            default: infoPanels[0].SetActive(true); break;
        }
    }
}
