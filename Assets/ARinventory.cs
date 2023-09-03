using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ARinventory : MonoBehaviour
{
    [SerializeField]
    private Button[] collectibleButtons; // Buttons for each collectible (coins and objects)
    [SerializeField]
    private Sprite notCollectedSprite; // Sprite to show when not collected
    [SerializeField]
    private Sprite collectedSprite; // Sprite to show when collected

    private bool[] collectiblesCollected;

    private void Start()
    {
        collectiblesCollected = new bool[collectibleButtons.Length];

        // Initialize buttons with not collected sprite
        for (int i = 0; i < collectibleButtons.Length; i++)
        {
            collectibleButtons[i].GetComponent<Image>().sprite = notCollectedSprite;
        }
    }

    // Call this method when a collectible is collected
    public void UpdateCollectibleButton(int index)
    {
        collectiblesCollected[index] = true;
        collectibleButtons[index].GetComponent<Image>().sprite = collectedSprite;
    }

    // Call this method to check if a collectible has been collected
    public bool IsCollectibleCollected(int index)
    {
        return collectiblesCollected[index];
    }
}
