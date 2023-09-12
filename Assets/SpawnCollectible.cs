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
            collectibleObject.SetActive(true); // Reactivate the collectible
            collectibleScript.ResetCollectible(); // Reset its state to not collected
        }
    }
}
