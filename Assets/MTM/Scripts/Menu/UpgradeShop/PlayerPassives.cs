using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPassives : MonoBehaviour
{
    public StatsSaved st;
    public List<GameObject> playersList;
    string lastCharacter;
    
    void Start()
    {
        st = FindObjectOfType<StatsSaved>();
    }
    
    void Update()
    {
        if (st.characterName == lastCharacter) return;

        lastCharacter = st.characterName;

        DeselectiveAll();

        switch(st.characterName)
        {
            case "Player1": playersList[0].SetActive(true); break;
            case "Player2": playersList[1].SetActive(true); break;
            case "Player3": playersList[2].SetActive(true); break;
        }
    }

    public void DeselectiveAll()
    {
        for(int i = 0; i < playersList.Count; i++)
        {
            playersList[i].SetActive(false);
        }
    }
}
