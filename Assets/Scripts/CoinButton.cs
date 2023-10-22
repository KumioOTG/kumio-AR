using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CoinButton : MonoBehaviour
{
    public string associatedCoinID;
    public Image targetImage;
    public Sprite iconOpened;
    public Sprite iconCollected;
    public Sprite disableIcon;

    void Start()
    {
        if (targetImage == null)
        {
            Debug.LogWarning("No Target Image assigned!");
        }
    }

    void Update()
    {
        // Check if this coin has already been collected
        if (InventoryManager2.Instance.IsObjectCollected(associatedCoinID))
        {
            targetImage.sprite = iconCollected;
        }
        else
        {
            targetImage.sprite = disableIcon;
        }
    }
}
