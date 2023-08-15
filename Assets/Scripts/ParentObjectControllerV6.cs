using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UDebug = UnityEngine.Debug;

public class ParentObjectControllerV6: MonoBehaviour
{
    [SerializeField]
    private GameObject nextParent;
    [SerializeField]
    private PressableButtonHoloLens2 button;
    [SerializeField]
    private float delayInSeconds = 0.0f;

    private void Start()
    {
        if (button != null)
        {
            button.ButtonPressed.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("PressableButtonHoloLens2 not assigned in the inspector.");
        }
    }

    public void OnButtonPressed()
    {
        if (delayInSeconds > 0.0f)
        {
            Invoke(nameof(ActivateNextParent), delayInSeconds);
        }
        else
        {
            ActivateNextParent();
        }
    }

    public void ActivateNextParent()
    {
        if (nextParent != null)
        {
            nextParent.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
