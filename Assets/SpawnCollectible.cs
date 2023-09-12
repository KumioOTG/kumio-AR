using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCollectible : MonoBehaviour
{
    [SerializeField]
    private Button spawnButton;
    [SerializeField]
    private GameObject collectibleObject; // Direct reference to the object in the scene
    [SerializeField]
    private Transform spawnLocation; // Where the collectible should be reactivated
    [SerializeField]
    private string itemId; // should match the itemId of the related collectible

    [Header("Sounds")]
    [SerializeField]
    private AudioClip notCollectedSound; // Sound clip to play if the item hasn't been collected yet

    private AudioSource audioSource;


    private CollectCoinAR collectibleScript;

    private void Start()
    {
        spawnButton.onClick.AddListener(Spawn);
        collectibleScript = collectibleObject.GetComponent<CollectCoinAR>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = notCollectedSound;
    }

    public void Spawn()
    {
        if (CollectorManager.Instance.IsItemCollected(itemId))
        {
            collectibleObject.transform.position = spawnLocation.position; // Set the collectible's position to the spawn location
            collectibleObject.transform.rotation = spawnLocation.rotation; // Set the collectible's rotation to the spawn location's rotation
            collectibleObject.SetActive(true); // Reactivate the collectible
            collectibleScript.ResetCollectible(); // Reset its state to not collected
        }

        else
        {
            // Play the not collected sound
            audioSource.Play();
        }
    }
}
