using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCollectible : MonoBehaviour
{
    [SerializeField]
    private Button spawnButton; // Button that will spawn the collectible
    [SerializeField]
    private GameObject collectiblePrefab; // Prefab of the collectible to spawn
    [SerializeField]
    private Transform spawnPoint; // Point where the collectible will be spawned

    private void Start()
    {
        spawnButton.onClick.AddListener(Spawn);
    }

    public void Spawn()
    {
        Instantiate(collectiblePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
