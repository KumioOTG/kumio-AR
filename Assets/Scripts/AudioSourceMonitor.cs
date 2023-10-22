using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceMonitor : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToActivate; // The objects that should be activated when audio stops.
    private AudioSource audioSource; // The AudioSource attached to this GameObject

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;  // Ensuring it doesn't play immediately when enabled
    }

    private void OnEnable()
    {
        // Start the coroutine that waits for the audio to end
        StartCoroutine(WaitForAudioToEnd());
    }

    private IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        Debug.Log("Audio has stopped playing. Activating objects.");

        // Activate each object in the list if they are not active
        foreach (var obj in objectsToActivate)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                Debug.Log($"Activating {obj.name} at time {Time.time}");
                obj.SetActive(true);
            }
        }
    }

    public void DeactivatePuzzlePieces()
    {
        foreach (var obj in objectsToActivate)
        {
            if (obj != null && obj.activeInHierarchy)
            {
                Debug.Log($"Deactivating {obj.name} at time {Time.time}");
                obj.SetActive(false);
            }
        }
    }
}
