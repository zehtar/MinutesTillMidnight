using UnityEngine;
using YG;

public class LanguageInit : MonoBehaviour
{
    void Start()
    {
        string lang = YG2.lang;  // Берём язык напрямую
        Debug.Log("Язык пользователя: " + lang);

        switch (lang)
        {
            case "ru":
                SetLanguage("Russian");
                break;
            case "en":
                SetLanguage("English");
                break;
            default:
                SetLanguage("Russian");
                break;
        }
    }

    void SetLanguage(string language)
    {
        Debug.Log("Установлен язык: " + language);
        // Здесь подключаем твою систему локализации
    }
}