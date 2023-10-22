using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectorManager : MonoBehaviour
{
    public static CollectorManager Instance;

    public event Action<string> OnItemCollected;

    public event Action OnSceneChanged;

    private HashSet<string> collectedItems = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Destroying CollectorManager due to duplicate instance.");

        Debug.Log("CollectorManager Awake called in scene: " + SceneManager.GetActiveScene().name);


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Broadcast an event or a method call that informs all CollectCoinAR scripts to update their button images.
        OnSceneChanged?.Invoke(); // This is a new event you need to declare
    }

    public void CollectItem(string itemId)
    {
        collectedItems.Add(itemId);
        OnItemCollected?.Invoke(itemId);
    }

    public bool IsItemCollected(string itemId)
    {
        return collectedItems.Contains(itemId);
    }
}
