using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSelectedPLAYER : MonoBehaviour
{
    
    void Start()
    {
        CharacterSelection charaSelection = GetComponent<CharacterSelection>();
        if (charaSelection != null)
        {
            charaSelection.SelectCharacter();
        }
    }
}
