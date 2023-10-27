using System.Collections;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

public class ActivateObjectOnCollect : MonoBehaviour
{
    [SerializeField] private string triggerItemId; // The itemId of the object that, when collected, activates the target GameObject
    [SerializeField] private GameObject objectToActivate; // The GameObject to activate when the item is collected
    [SerializeField] private float delayInSeconds = 0f;
    [SerializeField] private AudioSource soundToWaitFor; // The sound to wait for completion

    private void Start()
    {
        // Subscribe to the OnItemCollected event from CollectorManager
        CollectorManager.Instance.OnItemCollected += HandleItemCollected;
    }

    private void OnDestroy()
    {
        if (CollectorManager.Instance != null)
        {
            CollectorManager.Instance.OnItemCollected -= HandleItemCollected;
        }
    }

    private void HandleItemCollected(string collectedItemId)
    {
        if (collectedItemId == triggerItemId)
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (soundToWaitFor != null && soundToWaitFor.isPlaying)
        {
            // If the sound is playing, wait for it to finish, then activate the object
            StartCoroutine(WaitForSoundToEnd());
        }
        else
        {
            // If there's no sound playing or if no AudioSource is set, immediately activate the object
            DoActivateObject();
        }
    }

    private IEnumerator WaitForSoundToEnd()
    {
        // Wait until the audio source has finished playing
        while (soundToWaitFor.isPlaying)
        {
            yield return null;
        }

        DoActivateObject();
    }

    private void DoActivateObject()
    {
        UnityEngine.Debug.Log("Activating object after delay...");
        StartCoroutine(ActivateObjectAfterDelay());
    }

    private IEnumerator ActivateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        UnityEngine.Debug.Log("Activating object: " + objectToActivate.name);
        objectToActivate.SetActive(true);
    }
}
