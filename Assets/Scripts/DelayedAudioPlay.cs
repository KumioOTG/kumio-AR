using System.Collections;
using UnityEngine;


public class DelayedAudioPlay : MonoBehaviour
{

    [SerializeField] private float delayInSeconds = 1f; // Set the desired delay in seconds.

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogError("AudioSource component not found.");
        }
    }

    private void Start()
    {
        if (audioSource != null)
        {
            Invoke("PlayAudio", delayInSeconds);
        }
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }
}


