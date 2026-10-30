using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSpawn : MonoBehaviour
{
    public StatsSaved statsSaved;
    public List<GameObject> guns;

    public List<GameObject> targetGunImages;      // Тот самый Image у каждого оружия для замены спрайта
    public List<Sprite> classicGunSkins;   // Список спрайтов для классического оружия
    public List<Sprite> superGunSkins;     // Список спрайтов для супероружия

    void Start()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
        ActivateWeapon(statsSaved.gunSelected);
    }

    public void ActivateWeapon(string weaponName)
    {
        // Выключаем все оружия
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }

        // Ищем нужное оружие по имени и включаем
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i].name == weaponName)
            {
                guns[i].SetActive(true);

                // Меняем скин (ищем Image в оружии)
                if (statsSaved.isSuperWeapon)
                { 
                    if (i < superGunSkins.Count)
                    {
                        targetGunImages[i].GetComponent<SpriteRenderer>().sprite = superGunSkins[i];
                    }
                }
                else
                {
                    if (i < classicGunSkins.Count)
                    {
                        targetGunImages[i].GetComponent<SpriteRenderer>().sprite  = classicGunSkins[i];
                    }
                }
                
                break;
            }
        }
    }
}