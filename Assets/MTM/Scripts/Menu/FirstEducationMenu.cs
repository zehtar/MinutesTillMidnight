using System.Collections;
using UnityEngine;

public class FirstEducationMenu : MonoBehaviour
{
    public GameObject[] texts;
    public int index;
    public bool storyEnd;
    public GameObject storyPanel;
    public SaveStatsInMenu stm;

    public void Start()
    {
        stm = FindObjectOfType<SaveStatsInMenu>();
        
        if (stm.storyEnd)
        {
            storyPanel.SetActive(false);
        }
    }

    public void MoveStory()
    {
        if (stm.storyEnd)
        {
            StartCoroutine(FadeOut(storyPanel));
            stm.SaveStory(stm.storyEnd);
            return;
        }

        if (index < texts.Length)
        {
            texts[index].SetActive(true);
            StartCoroutine(FadeIn(texts[index]));
            index++;

            if (index >= texts.Length)
            {
                stm.storyEnd = true;
                stm.SaveStory(stm.storyEnd);
            }
        }
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
        }

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsed / duration));
            yield return null;
        }

        canvasGroup.alpha = 0f;
        obj.SetActive(false);
    }
}