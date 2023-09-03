using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleButton : MonoBehaviour
{
    public static List<ToggleButton> allButtons = new List<ToggleButton>();

    public Image targetImage;
    public Sprite iconOpened;
    public Sprite iconCollected;
    public Sprite disableIcon;

    [Header("Control State")]
    public int controlState = 1;

    private bool isClickedBefore = false;
    private bool isInitialized = false;
    private Button myButton;

    void Awake()
    {
        allButtons.Add(this);
    }

    void OnDestroy()
    {
        allButtons.Remove(this);
    }

    void Start()
    {
        if (GetComponent<Button>() != null)
        {
            myButton = GetComponent<Button>();
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("No Button component found on this GameObject!");
        }

        if (targetImage != null)
        {
            targetImage.sprite = iconCollected;
            isInitialized = true;
        }
        else
        {
            Debug.LogWarning("Target Image is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (!isInitialized) return;

        if (iconOpened == null)
        {
            Debug.LogWarning("Icon A sprite is not assigned in the inspector!");
            return;
        }

        if (iconCollected == null)
        {
            Debug.LogWarning("Icon B sprite is not assigned in the inspector!");
            return;
        }

        if (disableIcon == null)
        {
            Debug.LogWarning("Disable Icon sprite is not assigned in the inspector!");
            return;
        }

        switch (controlState)
        {
            case 0:
                targetImage.sprite = disableIcon;
                if (myButton != null) myButton.interactable = false;
                break;
            case 1:
                if (!isClickedBefore) targetImage.sprite = iconCollected;
                if (myButton != null) myButton.interactable = true;
                break;
            default:
                Debug.LogWarning("Invalid controlState value");
                break;
        }
    }

    void OnButtonClick()
    {
        if (controlState == 1)
        {
            ResetAllButtonsExcept(this);

            if (!isClickedBefore)
            {
                targetImage.sprite = iconOpened;
                isClickedBefore = true;
            }
            else
            {
                targetImage.sprite = iconCollected;
                isClickedBefore = false;
            }
        }
    }

    void ResetAllButtonsExcept(ToggleButton exception)
    {
        foreach (var button in allButtons)
        {
            if (button != exception)
            {
                button.targetImage.sprite = button.iconCollected;
                button.isClickedBefore = false;
            }
        }
    }
}