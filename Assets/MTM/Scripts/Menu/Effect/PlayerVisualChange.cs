using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerVisualChange : MonoBehaviour
{
    public List<GameObject> players;
    public string playerID;
    public Text playerText;

    public StatsSaved statsSaved;
    private string currentLang;

    void Start()
    {
        statsSaved = FindObjectOfType<StatsSaved>();
    }

    void Update()
    {
        playerID = statsSaved.characterName;
        currentLang = YG2.lang;

        switch (playerID)
        {
            case "Player1":
                PlayersReset();
                playerText.text = GetPlayerName("Player1");
                players[0].SetActive(true);
                break;

            case "Player2":
                PlayersReset();
                playerText.text = GetPlayerName("Player2");
                players[1].SetActive(true);
                break;

            case "Player3":
                PlayersReset();
                playerText.text = GetPlayerName("Player3");
                players[2].SetActive(true);
                break;

            default:
                PlayersReset();
                break;
        }
    }

    // Метод для получения имени персонажа на нужном языке
    private string GetPlayerName(string playerKey)
    {
        if (currentLang == "ru")
        {
            switch (playerKey)
            {
                case "Player1": return "Эфф";
                case "Player2": return "Лами";
                case "Player3": return "Польер";
                default: return "???";
            }
        }
        else // "en"
        {
            switch (playerKey)
            {
                case "Player1": return "Eff";
                case "Player2": return "Lami";
                case "Player3": return "Polyer";
                default: return "???";
            }
        }
    }

    public void PlayersReset()
    {
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
    }
}