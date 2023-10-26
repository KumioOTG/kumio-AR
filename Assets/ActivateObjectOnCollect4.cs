using System.Collections;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

public class ActivateObjectOnCollect4 : MonoBehaviour
{
    [SerializeField] private string triggerItemId1; // The first itemId
    [SerializeField] private string triggerItemId2; // The second itemId
    [SerializeField] private string triggerItemId3; // The third itemId
    [SerializeField] private GameObject objectToActivate; // Object to activate
    [SerializeField] private GameObject objectToDeactivate; // Object to deactivate
    [SerializeField] private float delayInSeconds = 0f;
    [SerializeField] private AudioSource soundToWaitFor;

    private bool isItem1Collected = false;
    private bool isItem2Collected = false;
    private bool isItem3Collected = false;

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
        if (collectedItemId == triggerItemId1)
        {
            isItem1Collected = true;
        }
        else if (collectedItemId == triggerItemId2)
        {
            isItem2Collected = true;
        }
        else if (collectedItemId == triggerItemId3)
        {
            isItem3Collected = true;
        }

        // Check if all items have been collected
        if (isItem1Collected && isItem2Collected && isItem3Collected)
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
        UnityEngine.Debug.Log("Activating and deactivating objects after delay...");
        StartCoroutine(ActivateAndDeactivateObjectsAfterDelay());
    }

    private IEnumerator ActivateAndDeactivateObjectsAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objectToDeactivate != null)
        {
            UnityEngine.Debug.Log("Deactivating object: " + objectToDeactivate.name);
            objectToDeactivate.SetActive(false);
        }

        if (objectToActivate != null)
        {
            UnityEngine.Debug.Log("Activating object: " + objectToActivate.name);
            objectToActivate.SetActive(true);
        }
    }
}
