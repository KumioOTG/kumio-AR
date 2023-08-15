using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UDebug = UnityEngine.Debug;

public class ToggleGameObjectsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToActivate;

    [SerializeField]
    private GameObject objectToDeactivate;

    private Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        if (interactable == null)
        {
            Debug.LogError("ToggleGameObjectsButton requires an Interactable component on the same GameObject.");
            return;
        }

        interactable.OnClick.AddListener(ToggleGameObjects);
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.OnClick.RemoveListener(ToggleGameObjects);
        }
    }

    private void ToggleGameObjects()
    {
        objectToActivate.SetActive(true);
        objectToDeactivate.SetActive(false);
    }
}
