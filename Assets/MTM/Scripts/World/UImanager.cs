using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public Text[] statsList;
    public Text coinText;
    public Text coinText2;
    public StatsSaved st;
    public GameStats gm;

    public GameDiffucultChange forestDiff;
    public GameDiffucultChange caveDiff;
    public GameDiffucultChange dungeonDiff;
    public AdsManager adsManager;
    

    public int indexScene;

    public void Start()
    {
        st = FindObjectOfType<StatsSaved>();
        adsManager = FindAnyObjectByType<AdsManager>();
    }

    public void Update()
    {
        if (indexScene == 0)
        {
            if (coinText != null)
                coinText.text = st.coins.ToString();
            if (coinText2 != null)
                coinText2.text = st.coins.ToString();
        }
    }

    public void OpenGame()
    {
        if (st.characterName.Length > 2 && st.gunSelected.Length > 2)
        {
            forestDiff.ApplyDiffucult();
            indexScene = 1;
            st.indexScene = 1;
            AudioManager.Instance.StopMusic();

            adsManager.ShowAdAndLoadSceneByIndex(1);
        }
    }

    public void OpenInfGame()
    {
        if (st.characterName.Length > 2 && st.gunSelected.Length > 2)
        {
            forestDiff.ApplyDiffucult();
            indexScene = 4;
            st.indexScene = 4;
            AudioManager.Instance.StopMusic();

            adsManager.ShowAdAndLoadSceneByIndex(4);
        }
    }

    public void OpenGameGave()
    {
        if (st.characterName.Length > 2 && st.gunSelected.Length > 2)
        {
            caveDiff.ApplyDiffucult();
            indexScene = 2;
            st.indexScene = 2;
            AudioManager.Instance.StopMusic();

            adsManager.ShowAdAndLoadSceneByIndex(2);
        }
    }

    public void OpenGameCastle()
    {
        if (st.characterName.Length > 2 && st.gunSelected.Length > 2)
        {
            dungeonDiff.ApplyDiffucult();
            indexScene = 3;
            st.indexScene = 3;
            AudioManager.Instance.StopMusic();

            adsManager.ShowAdAndLoadSceneByIndex(3);
        }
    }

    public void OpenMenu()
    {
        indexScene = 0;
        AudioManager.Instance.StopMusic();

        adsManager.ShowAdAndLoadSceneByIndex(0);
    }

    public bool coinSpend(int spend)
    {
        bool spended = false;
        if (st.coins >= spend)
        {
            st.coins -= spend;
            spended = true;
        }
        return spended;
    }

    public void EndGame()
    {
        gm = FindObjectOfType<GameStats>();
        gm.GiveAwards();
    }
}