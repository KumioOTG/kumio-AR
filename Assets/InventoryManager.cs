using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public static event System.Action<GameObject> OnCollect;

    private List<GameObject> collectedObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            collectedObjects = new List<GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddCollectableObject(GameObject collectableObject)
    {
        collectedObjects.Add(collectableObject);
        OnCollect?.Invoke(collectableObject);
        UnityEngine.Debug.Log("Collected " + collectableObject.name + ". Total collected: " + collectedObjects.Count);
    }

    public int GetCollectedCount()
    {
        return collectedObjects.Count;
    }
}