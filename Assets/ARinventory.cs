using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ARinventory : MonoBehaviour
{
    [SerializeField]
    private Button[] collectibleButtons; // Buttons for each collectible (coins and objects)

    [SerializeField]
    private Sprite[] notCollectedSprites; // Sprites to show when not collected for each type of object

    [SerializeField]
    private Sprite[] collectedSprites; // Sprites to show when collected for each type of object

    [SerializeField]
    private string[] itemIds; // Array of itemIds corresponding to your buttons

    private void Start()
    {
        // Initialize buttons based on CollectorManager's state
        for (int i = 0; i < collectibleButtons.Length; i++)
        {
            if (CollectorManager.Instance.IsItemCollected(itemIds[i]))
            {
                collectibleButtons[i].GetComponent<Image>().sprite = collectedSprites[i];
            }
            else
            {
                collectibleButtons[i].GetComponent<Image>().sprite = notCollectedSprites[i];
            }
        }
    }

    // Call this method when a collectible is collected
    public void UpdateCollectibleButton(int index)
    {
        if (CollectorManager.Instance.IsItemCollected(itemIds[index]))
        {
            collectibleButtons[index].GetComponent<Image>().sprite = collectedSprites[index];
        }
    }

    // Call this method to check if a collectible has been collected
    public bool IsCollectibleCollected(int index)
    {
        return CollectorManager.Instance.IsItemCollected(itemIds[index]);
    }
}
