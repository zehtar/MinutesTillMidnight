using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButtonViev : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public void SetData(Sprite iconSprite, string text)
    {
        var myImage = GetComponent<Image>();
        myImage.sprite = iconSprite;
        tmp.text = text;
    }
}
