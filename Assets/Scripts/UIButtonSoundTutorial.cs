using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSoundTutorial : MonoBehaviour
{
    [SerializeField]
    private AudioClip firstClickSound;
    [SerializeField]
    private AudioClip secondClickSound;
    private AudioSource audioSource;
    private int clickCount = 0;

    private void Start()
    {
        // Locate or create the audio source
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void HandleButtonClick()
    {
        clickCount++;

        if (clickCount % 2 == 1)
        {
            PlaySound(firstClickSound);
        }
        else
        {
            PlaySound(secondClickSound);
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if (audioSource && sound)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}
