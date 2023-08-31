using UnityEngine;
using System.Collections;

public class ModelController : MonoBehaviour
{
    public GameObject[] infoPanels; // Reference to the information panels.
    public float fadeSpeed = 0.5f; // Speed of the fade-in.

    private bool isFading = false;

    public void ShowInfo(int panelIndex)
    {
        if (isFading) return;

        // Start fading in the selected panel.
        StartCoroutine(FadeIn(infoPanels[panelIndex].GetComponent<CanvasGroup>()));
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

    public void CloseAllPanels()
    {
        for (int i = 0; i < infoPanels.Length; i++)
        {
            infoPanels[i].GetComponent<CanvasGroup>().alpha = 0f;
            infoPanels[i].SetActive(false);
        }
    }
}
