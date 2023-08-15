using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject childObject;

    private Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();

        if (interactable != null)
        {
            interactable.OnClick.AddListener(ToggleChild);
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.OnClick.RemoveListener(ToggleChild);
        }
    }

    private void ToggleChild()
    {
        if (childObject != null)
        {
            childObject.SetActive(!childObject.activeSelf);
        }
    }
}
