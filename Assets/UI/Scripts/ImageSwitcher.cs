using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageSwitcher : MonoBehaviour
{
    public Sprite[] imageVariations; // Drag your 6 images here in the inspector
    [Range(0, 10)] // Assuming you have 6 images
    public int currentImageIndex = 0; // Change this in the inspector to switch image

    private Image _imageComponent;

    private void Start()
    {
        _imageComponent = GetComponent<Image>();
        UpdateImage();
    }

    private void OnValidate()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        // Initialize _imageComponent if it's null
        if (_imageComponent == null)
        {
            _imageComponent = GetComponent<Image>();

            // If it's still null after attempting to get the component, log an error and exit the method.
            if (_imageComponent == null)
            {
                Debug.LogError("No Image component found on this GameObject!");
                return;
            }
        }

        if (imageVariations != null && imageVariations.Length > 0 && currentImageIndex >= 0 && currentImageIndex < imageVariations.Length)
        {
            _imageComponent.sprite = imageVariations[currentImageIndex];
        }
    }


}
