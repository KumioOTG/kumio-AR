using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceMonitor : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // The object that should be activated when audio stops.
    private AudioSource audioSource; // The AudioSource attached to this GameObject

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if audio is not playing and the specified object is not active
        if (!audioSource.isPlaying && !objectToActivate.activeInHierarchy)
        {
            // Activate the object
            objectToActivate.SetActive(true);
        }
    }
}
