using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public Text killLine;
    public ScoreManager scoreMN;
    public GameTimer gameTimer;
    public InfGameTimer gameInfTimer;
    public Text timeLine;
    public Text levelLine;
    public Level lvMN;
    public Text resultText;

    public StatsSaved st;
    public SaveStatsInMenu mainMenu;
    public Point gameEnemySpawner;

    public double result = 0;
    public bool gameOver = false;
    public bool isRunning = true;
    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameInfTimer = FindAnyObjectByType<InfGameTimer>();
        scoreMN = FindObjectOfType<ScoreManager>();
        lvMN = FindObjectOfType<Level>();
        st = FindObjectOfType<StatsSaved>();
        mainMenu= FindObjectOfType<SaveStatsInMenu>();
        gameEnemySpawner = FindObjectOfType<Point>();
    }

    public void CoutingAwards()
    {
        killLine.text = ((scoreMN.score * st.CoinMyltiply) / 5).ToString();
        
        if (gameTimer == null)
        {
            timeLine.text = (gameInfTimer.second * st.CoinMyltiply ).ToString();
        }
        else
        {
            timeLine.text = ((gameTimer.second * st.CoinMyltiply )* 2).ToString();
        }

        levelLine.text = ((lvMN.level * st.CoinMyltiply) * 20).ToString();
        //можно строчку для мультипле койн
        if (gameTimer == null)
        {
            result = Math.Round(((scoreMN.score / 5) + gameInfTimer.second + (lvMN.level * 20)) * st.CoinMyltiply);
        }
        else
        {
            result = Math.Round(((scoreMN.score / 5) + gameTimer.second + (lvMN.level * 20)) * st.CoinMyltiply);
        }
        resultText.text = result.ToString();
        if (gameEnemySpawner != null)
        {
            EnemyAllDestroy();
        }
    }
    
    public void EnemyAllDestroy()
    {
        if (gameEnemySpawner == null)
        {
            gameEnemySpawner = FindObjectOfType<Point>();
        }
        
        for (int i = 0; i < gameEnemySpawner.enemyAtGameList.Count; i++)
        {
            if (gameEnemySpawner.enemyAtGameList[i] != null)
            {
                gameEnemySpawner.enemyAtGameList[i].GetComponent<Enemy>().TakeDamage(1000);
            }
            gameEnemySpawner.enemyAtGameList.RemoveAt(i);
        }
    }

    public void GiveAwards()
    {
        CoutingAwards();
        st.coins += (int)result;
        mainMenu.SaveCoins();
        gameOver = false;
        if (gameTimer != null)
        {
            if (gameTimer.gameIsWinOver)
            {
                switch (st.indexScene)
                {
                    case 1: mainMenu.levelFirstComplite = true; break;
                    case 2: mainMenu.levelSecondComplite = true; break;
                    case 3: mainMenu.levelThirdComplite = true; break;
                    default: break;
                }
                mainMenu.SaveDiffDate();
            }
        }
    }
}
