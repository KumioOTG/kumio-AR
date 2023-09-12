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

    private CollectCoinAR collectibleScript;

    private void Start()
    {
        spawnButton.onClick.AddListener(Spawn);
        collectibleScript = collectibleObject.GetComponent<CollectCoinAR>();
    }

    public void Spawn()
    {
        if (collectibleScript.IsCollected)
        {
            collectibleObject.transform.position = spawnLocation.position; // Set the collectible's position to the spawn location
            collectibleObject.transform.rotation = spawnLocation.rotation; // Set the collectible's rotation to the spawn location's rotation
            collectibleObject.SetActive(true); // Reactivate the collectible
            collectibleScript.ResetCollectible(); // Reset its state to not collected
        }
    }
}
