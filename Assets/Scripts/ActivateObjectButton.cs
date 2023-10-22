using UnityEngine;
using UnityEngine.UI; // For the Button
using System.Collections; // For the IEnumerator

public class ActivateObjectButton : MonoBehaviour
{
    [SerializeField]
    private GameObject firstObjectToActivate;

    [SerializeField]
    private GameObject secondObjectToActivate;

    [SerializeField]
    private float delayInSeconds = 0f;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TryActivateObjectsWithDelay);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(TryActivateObjectsWithDelay);
    }

    private void TryActivateObjectsWithDelay()
    {
        DelayActivationManager.Instance.ActivateObjectWithDelay(firstObjectToActivate, delayInSeconds);
        DelayActivationManager.Instance.ActivateObjectWithDelay(secondObjectToActivate, delayInSeconds);
    }
}
