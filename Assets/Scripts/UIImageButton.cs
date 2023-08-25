using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Locate or create the audio source
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = clickSound;
        audioSource.playOnAwake = false;
    }

    public void PlaySound()
    {
        if (audioSource && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
