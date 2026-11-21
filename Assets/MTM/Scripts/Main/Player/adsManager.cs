
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AdsManager : MonoBehaviour
{
    private int sceneIndexToLoad;
    private bool waitingForSceneLoad;
    private bool adOpened;

    [SerializeField] private float adOpenTimeout = 2f;

    private Coroutine adOpenCoroutine;

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
        if (waitingForSceneLoad)
            return;

        sceneIndexToLoad = sceneIndex;
        waitingForSceneLoad = true;
        adOpened = false;

        adOpenCoroutine = StartCoroutine(CheckAdOpened());

        YG2.InterstitialAdvShow();
    }

    private IEnumerator CheckAdOpened()
    {
        yield return new WaitForSecondsRealtime(adOpenTimeout);

        adOpenCoroutine = null;

        if (waitingForSceneLoad && !adOpened)
        {
            LoadScene();
        }
    }

    private void OnOpenAd()
    {
        if (!waitingForSceneLoad)
            return;

        adOpened = true;

        if (adOpenCoroutine != null)
        {
            StopCoroutine(adOpenCoroutine);
            adOpenCoroutine = null;
        }
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
        if (!waitingForSceneLoad)
            return;

        waitingForSceneLoad = false;

        if (adOpenCoroutine != null)
        {
            StopCoroutine(adOpenCoroutine);
            adOpenCoroutine = null;
        }

        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
