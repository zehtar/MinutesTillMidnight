using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDiffSpawn : MonoBehaviour
{
    public List<GameObject> diffs;
    public List<GameObject> images;
    public List<GameObject> buttons;
    
    public int levelID;
    public void OnEnable()
    {
        DeSelectAll();
        Select();
    }

    public void SetLevelID(int levelID)
    {
        this.levelID = levelID;
    }

    public void Select()
    {
        switch (levelID)
        {
            case 1:
                diffs[0].SetActive(true);
                images[0].SetActive(true);
                buttons[0].SetActive(true);
                break;
            case 2:
                diffs[1].SetActive(true);
                images[1].SetActive(true);
                buttons[1].SetActive(true);
                break;
            case 3:
                diffs[2].SetActive(true);
                images[2].SetActive(true);
                buttons[2].SetActive(true);
                break;
            default: 
                diffs[0].SetActive(true);
                images[0].SetActive(true);
                buttons[0].SetActive(true);
                break;
        }
    }

    public void DeSelectAll()
    {
        foreach (GameObject g in buttons)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in images)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in diffs)
        {
            g.SetActive(false);
        }
    }
}
