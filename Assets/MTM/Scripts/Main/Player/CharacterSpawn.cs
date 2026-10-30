using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{    
    public StatsSaved statsSaved;
    public List<GameObject> characters;
    public List<GameObject> charactersActivesPowers;
    public List<GameObject> charactersIcons;
    public Entrance entrance;

    void Awake()
    {
        entrance = FindObjectOfType<Entrance>();
        string selectedCharacter = FindObjectOfType<StatsSaved>().characterName;
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].name == selectedCharacter)
            {
                characters[i].SetActive(true);
                charactersActivesPowers[i].SetActive(true);
                charactersIcons[i].SetActive(true);
                entrance.player = characters[i].GetComponent<player>();
                entrance.playerActive = true;
            }            
        }
    }    
}
