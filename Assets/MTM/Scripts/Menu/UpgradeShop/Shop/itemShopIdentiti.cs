using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class itemShopIdentiti : MonoBehaviour
{
    public List<Image> images;
    public TextMeshProUGUI upgradeCost;
    public int upgradeLevel;
    public int maxUpgradeLevel;
    public int[] upgradeCosts = { 100, 200, 300, 400, 500 };
}
