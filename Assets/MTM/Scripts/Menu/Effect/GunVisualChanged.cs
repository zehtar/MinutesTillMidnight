using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GunVisualChanged : MonoBehaviour
{
    public List<GameObject> guns;
    public string gunID;
    public Text gunText;

    public StatsSaved statsSaved;
    private string currentLang;

    void Start()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
    }

    void Update()
    {
        gunID = statsSaved.gunSelected;
        currentLang = YG2.lang;

        switch (gunID)
        {
            case "Gun1":
                GunsReset();
                gunText.text = GetGunName("Gun1");
                if (statsSaved.isSuperWeapon) { guns[4].SetActive(true); }
                else { guns[0].SetActive(true); }
                break;

            case "Gun2":
                GunsReset();
                gunText.text = GetGunName("Gun2");
                if (statsSaved.isSuperWeapon) { guns[5].SetActive(true); }
                else { guns[1].SetActive(true); }
                break;

            case "Gun3":
                GunsReset();
                gunText.text = GetGunName("Gun3");
                if (statsSaved.isSuperWeapon) { guns[6].SetActive(true); }
                else { guns[2].SetActive(true); }
                break;

            case "Gun4":
                GunsReset();
                gunText.text = GetGunName("Gun4");
                if (statsSaved.isSuperWeapon) { guns[7].SetActive(true); }
                else { guns[3].SetActive(true); }
                break;
            
            case "Gun5":
                GunsReset();
                gunText.text = GetGunName("Gun5");
                if (statsSaved.isSuperWeapon) { guns[9].SetActive(true); }
                else { guns[8].SetActive(true); }
                break;
            
            case "Gun6":
                GunsReset();
                gunText.text = GetGunName("Gun6");
                if (statsSaved.isSuperWeapon) { guns[11].SetActive(true); }
                else { guns[10].SetActive(true); }
                break;

            default:
                GunsReset();
                break;
        }
    }

    // Метод, который возвращает название оружия на нужном языке
    private string GetGunName(string gunKey)
    {
        if (currentLang == "ru")
        {
            switch (gunKey)
            {
                case "Gun1": return "Пистолет";
                case "Gun2": return "Дробовик";
                case "Gun3": return "Автомат";
                case "Gun4": return "Винтовка";
                case "Gun5": return "Пулемет";
                case "Gun6": return "Базука";
                default: return "???";
            }
        }
        else // "en" или любой другой — fallback на английский
        {
            switch (gunKey)
            {
                case "Gun1": return "Pistol";
                case "Gun2": return "Shotgun";
                case "Gun3": return "Assault Rifle";
                case "Gun4": return "Sniper Rifle";
                case "Gun5": return "machine gun";
                case "Gun6": return "bazooka";
                default: return "???";
            }
        }
    }

    public void GunsReset()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }
}