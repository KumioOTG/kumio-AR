using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class InventoryManager2 : MonoBehaviour
{
    public static InventoryManager2 Instance { get; private set; }

    private HashSet<string> collectedObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collectedObjects = new HashSet<string>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCollectableObject(string objectID, GameObject objectToCollect)
    {
        if (!collectedObjects.Contains(objectID))
        {
            collectedObjects.Add(objectID);
            // Add other code to handle the collected object if necessary
        }
    }

    public bool IsObjectCollected(string objectID)
    {
        return collectedObjects.Contains(objectID);
    }
}
