using UnityEngine;
using System.Collections;

public class ImageController : MonoBehaviour
{
    public GameObject[] infoPanels; // Reference to the information panels.
    public float fadeSpeed = 0.5f; // Speed of the fade-in and fade-out.

    private int currentActivePanel = -1; // Keep track of the currently active panel.
    private bool isFading = false;

    public void ShowInfo(int panelIndex)
    {
        if (isFading) return;

        // If the same button is clicked, fade out the current panel and reset the currentActivePanel.
        if (currentActivePanel == panelIndex)
        {
            StartCoroutine(FadeOut(infoPanels[currentActivePanel].GetComponent<CanvasGroup>()));
            currentActivePanel = -1;
            return;
        }

        // Start fading out the currently active panel.
        if (currentActivePanel >= 0)
        {
            StartCoroutine(FadeOut(infoPanels[currentActivePanel].GetComponent<CanvasGroup>()));
        }

        // Start fading in the selected panel.
        StartCoroutine(FadeIn(infoPanels[panelIndex].GetComponent<CanvasGroup>()));

        currentActivePanel = panelIndex;
    }


    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        isFading = true;
        float progress = 0f;

        // Set the starting state.
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(true);

        // Fade in over time.
        while (progress < 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);
            progress += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        isFading = false;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        isFading = true;
        float progress = 0f;

        // Fade out over time.
        while (progress < 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);
            progress += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
        isFading = false;
    }

    public void CloseAllPanels()
    {
        if (currentActivePanel >= 0)
        {
            StartCoroutine(FadeOut(infoPanels[currentActivePanel].GetComponent<CanvasGroup>()));
            currentActivePanel = -1;
        }
    }
}
