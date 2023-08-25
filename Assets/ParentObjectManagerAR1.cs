using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObjectManagerAR1 : MonoBehaviour
{
    [SerializeField]
    private GameObject currentParentObject; // Set this in the inspector

    [SerializeField]
    private GameObject nextParentObject; // Set this in the inspector

    [SerializeField]
    private AudioClip collectionSound; // The sound clip to play. Set this in the inspector.

    private AudioSource audioSource; // This will play the sound

    private int collectibleCoinCount = 0;

    private void Start()
    {
        // Initialize the audio source component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Register for notifications when a coin is collected
        InventoryManager.OnCollect += HandleCoinCollected;
    }

    private void OnDestroy()
    {
        // Unregister to prevent memory leaks
        InventoryManager.OnCollect -= HandleCoinCollected;
    }

    private void HandleCoinCollected(GameObject collectedObject)
    {
        // Check if the collected object has the "CollectibleCoin" tag
        if (collectedObject.CompareTag("Collectible"))
        {
            collectibleCoinCount++;
            if (collectibleCoinCount % 3 == 0)
            {
                // Play the collection sound
                if (collectionSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(collectionSound);
                }

                // Deactivate current parent object and activate next parent object
                if (currentParentObject != null) currentParentObject.SetActive(false);
                if (nextParentObject != null) nextParentObject.SetActive(true);
            }
        }
    }
}
