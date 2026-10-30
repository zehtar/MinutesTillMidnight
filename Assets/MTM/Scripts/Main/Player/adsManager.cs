using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AdsManager : MonoBehaviour
{
    private int sceneIndexToLoad;
    private bool waitingForSceneLoad;
    private bool adOpened;

    [SerializeField] private float adOpenTimeout = 1f;

    private void OnEnable()
    {
        YG2.onOpenInterAdv += OnOpenAd;
        YG2.onCloseInterAdv += OnCloseAd;
        YG2.onErrorInterAdv += OnErrorAd;
    }

    private void OnDisable()
    {
        YG2.onOpenInterAdv -= OnOpenAd;
        YG2.onCloseInterAdv -= OnCloseAd;
        YG2.onErrorInterAdv -= OnErrorAd;
    }

    public void ShowInterstitial()
    {
        YG2.InterstitialAdvShow();
    }

    public void ShowAdAndLoadSceneByIndex(int sceneIndex)
    {
        sceneIndexToLoad = sceneIndex;
        waitingForSceneLoad = true;
        adOpened = false;

        YG2.InterstitialAdvShow();
        StartCoroutine(CheckAdOpened());
    }

    private IEnumerator CheckAdOpened()
    {
        yield return new WaitForSecondsRealtime(adOpenTimeout);

        if (waitingForSceneLoad && !adOpened)
        {
            LoadScene();
        }
    }

    private void OnOpenAd()
    {
        adOpened = true;
    }

    private void OnCloseAd()
    {
        LoadScene();
    }

    private void OnErrorAd()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        if (!waitingForSceneLoad) return;

        waitingForSceneLoad = false;
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}