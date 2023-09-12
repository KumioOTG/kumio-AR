using UnityEngine;
using System.Collections;

public class SikkeImageController : MonoBehaviour
{
    public GameObject[] infoPanels; // Reference to the information panels.
    public float fadeSpeed = 0.5f; // Speed of the fade-in and fade-out.

    private bool isFading = false;

    public void ShowInfo(int panelIndex)
    {
        if (isFading) return;

        CanvasGroup currentCanvasGroup = infoPanels[panelIndex].GetComponent<CanvasGroup>();

        // If the image is not already visible, fade it in.
        if (!currentCanvasGroup.gameObject.activeInHierarchy || currentCanvasGroup.alpha == 0f)
        {
            StartCoroutine(FadeIn(currentCanvasGroup));
        }
        // Otherwise, do nothing, even if clicked again.
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
        for (int i = 0; i < infoPanels.Length; i++)
        {
            CanvasGroup currentCanvasGroup = infoPanels[i].GetComponent<CanvasGroup>();
            if (currentCanvasGroup.alpha == 1f)
            {
                StartCoroutine(FadeOut(currentCanvasGroup));
            }
        }
    }
}
