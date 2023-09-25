using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuizButtonManager : MonoBehaviour
{
    public Button[] buttons; // Drag your 3 buttons into this array in the Inspector.
    public int correctAnswerIndex; // Set the index (0, 1, or 2) of the correct button in the Inspector.

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip popupSound; // Sound for both fade in and fade out.

    public CanvasGroup quizCanvasGroup; // Drag the CanvasGroup component here in the Inspector.
    public float popupDelay = 2.0f; // Delay for both fade in at the beginning and fade out for correct answer.
    public float fadeDuration = 1.0f; // Duration for both fade in and fade out effects.

    private AudioSource audioSource;
    private Color[] initialButtonColors; // To store initial button colors

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialButtonColors = new Color[buttons.Length];

        // Initiate the delayed fade-in. No need to set the canvas group active here.
        StartCoroutine(DelayedFadeInQuiz());

        // Store initial colors and add listeners to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            initialButtonColors[i] = buttons[i].image.color;
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private IEnumerator DelayedFadeInQuiz()
    {
        yield return new WaitForSeconds(popupDelay);

        quizCanvasGroup.gameObject.SetActive(true); // Activate the canvas group here.
        PlaySound(popupSound); // Play fade-in sound

        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            quizCanvasGroup.alpha = (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        quizCanvasGroup.alpha = 1;
    }


    public void OnButtonClick(int index)
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        if (index == correctAnswerIndex)
        {
            buttons[index].image.color = Color.green;
            PlaySound(correctSound);
            StartCoroutine(DelayedFadeOutQuiz());
        }
        else
        {
            buttons[index].image.color = Color.red;
            PlaySound(wrongSound);
            StartCoroutine(RevertButtonAfterDelay(index));
        }
    }

    private IEnumerator DelayedFadeOutQuiz()
    {
        yield return new WaitForSeconds(popupDelay);
        PlaySound(popupSound); // Play fade-out sound

        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            quizCanvasGroup.alpha = 1 - ((Time.time - startTime) / fadeDuration);
            yield return null;
        }

        quizCanvasGroup.alpha = 0;
        quizCanvasGroup.gameObject.SetActive(false);
    }

    private IEnumerator RevertButtonAfterDelay(int index)
    {
        yield return new WaitForSeconds(fadeDuration);
        buttons[index].image.color = initialButtonColors[index];

        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource && clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
