using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPlayerUpgrade : MonoBehaviour
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
        switch(st.characterName)
        {
            case "Player1": infoPanels[0].SetActive(true); break;
            case "Player2": infoPanels[1].SetActive(true); break;
            case "Player3": infoPanels[2].SetActive(true); break;
            default: infoPanels[0].SetActive(true); break;
        }
    }
}
