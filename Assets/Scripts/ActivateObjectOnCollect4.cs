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
    [SerializeField] private AudioSource audioSource; // AudioSource component to play sound
    [SerializeField] private AudioClip collectionCompleteSound; // Sound to play when all items are collected

    private bool isItem1Collected = false;
    private bool isItem2Collected = false;
    private bool isItem3Collected = false;

    private void Start()
    {
        // Assuming CollectorManager.Instance.OnItemCollected is an event that's invoked when an item is collected
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
            PlayCollectionCompleteSound();
        }
    }

    private void PlayCollectionCompleteSound()
    {
        if (audioSource != null && collectionCompleteSound != null)
        {
            audioSource.clip = collectionCompleteSound;
            audioSource.Play();
            StartCoroutine(WaitForSoundToEnd());
        }
        else
        {
            ActivateDeactivateObjects();
        }
    }

    private IEnumerator WaitForSoundToEnd()
    {
        while (audioSource != null && audioSource.isPlaying)
        {
            yield return null;
        }
        ActivateDeactivateObjects();
    }

    private void ActivateDeactivateObjects()
    {
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
