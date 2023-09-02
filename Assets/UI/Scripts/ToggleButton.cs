using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



public class ToggleButton : MonoBehaviour
{
    public Image targetImage;
    public Sprite iconCollected;
    public Sprite iconNotCollected;

    public string associatedCoinID;

    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("No Button component found on this GameObject!");
        }

        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (InventoryManager2.Instance.IsObjectCollected(associatedCoinID))
        {
            targetImage.sprite = iconCollected;
        }
        else
        {
            targetImage.sprite = iconNotCollected;
        }
    }

    public void OnButtonClick()
    {
        GameObject coin = GameObject.Find(associatedCoinID);
        if (coin != null)
        {
            if (!InventoryManager2.Instance.IsObjectCollected(associatedCoinID))
            {
                coin.SetActive(true);
            }
            else
            {
                Debug.Log("Coin already collected!");
            }
        }
        else
        {
            Debug.LogWarning("Coin not found!");
        }
    }
}
