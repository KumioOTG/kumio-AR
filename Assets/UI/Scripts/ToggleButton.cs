using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Image targetImage;     // Assign the child Image in inspector
    public Sprite iconOpened;          // Assign in inspector
    public Sprite iconCollected;          // Assign in inspector
    public Sprite disableIcon;    // This will be the sprite used when the controlState is set to 0

    [Header("Control State")]
    public int controlState = 1;  // 0 = disabled, 1 = normal

    private bool isClickedBefore = false;
    private bool isInitialized = false; // To track if initialization is complete
    private Button myButton;

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

        // Set initial icon
        if (targetImage != null)
        {
            targetImage.sprite = iconCollected;
            isInitialized = true; // Set this flag once everything is ready
        }
        else
        {
            Debug.LogWarning("Target Image is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (!isInitialized) return; // Skip the update if we aren't fully initialized

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
        if (!isClickedBefore && controlState == 1)
        {
            targetImage.sprite = iconOpened;
            isClickedBefore = true;
        }
    }
}
