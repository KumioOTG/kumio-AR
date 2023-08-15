using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObjectManagerAR1 : MonoBehaviour
{
    [SerializeField]
    private GameObject currentParentObject; // Set this in the inspector

    [SerializeField]
    private GameObject nextParentObject; // Set this in the inspector

    private int collectibleCoinCount = 0;

    private void Start()
    {
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
                // Deactivate current parent object and activate next parent object
                if (currentParentObject != null) currentParentObject.SetActive(false);
                if (nextParentObject != null) nextParentObject.SetActive(true);
            }
        }
    }
}
