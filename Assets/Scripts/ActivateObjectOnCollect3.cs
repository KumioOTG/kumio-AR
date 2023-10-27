using System.Collections;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

public class ActivateObjectOnCollect3 : MonoBehaviour
{
    [SerializeField] private string triggerItemId1; // The first itemId
    [SerializeField] private string triggerItemId2; // The second itemId
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private float delayInSeconds = 0f;
    [SerializeField] private AudioSource soundToWaitFor;

    private void Start()
    {
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
        // Check against both item IDs
        if (collectedItemId == triggerItemId1 || collectedItemId == triggerItemId2)
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (soundToWaitFor != null && soundToWaitFor.isPlaying)
        {
            StartCoroutine(WaitForSoundToEnd());
        }
        else
        {
            DoActivateObject();
        }
    }

    private IEnumerator WaitForSoundToEnd()
    {
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
