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
    [SerializeField]
    private string[] itemIds; // You need an array of itemIds corresponding to your buttons

    private void Start()
    {
        // Initialize buttons based on CollectorManager's state
        for (int i = 0; i < collectibleButtons.Length; i++)
        {
            if (CollectorManager.Instance.IsItemCollected(itemIds[i]))
            {
                collectibleButtons[i].GetComponent<Image>().sprite = collectedSprite;
            }
            else
            {
                collectibleButtons[i].GetComponent<Image>().sprite = notCollectedSprite;
            }
        }
    }

    // Call this method when a collectible is collected
    public void UpdateCollectibleButton(int index)
    {
        if (CollectorManager.Instance.IsItemCollected(itemIds[index]))
        {
            collectibleButtons[index].GetComponent<Image>().sprite = collectedSprite;
        }
    }

    // Call this method to check if a collectible has been collected
    public bool IsCollectibleCollected(int index)
    {
        return CollectorManager.Instance.IsItemCollected(itemIds[index]);
    }
}
