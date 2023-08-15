using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float delayInSeconds = 2.0f; // 2 seconds delay, change as needed

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAudioAfterDelay(delayInSeconds));
    }

    IEnumerator PlayAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
