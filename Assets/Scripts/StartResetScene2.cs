using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartResetScene2 : MonoBehaviour
{
    [Tooltip("The first parent object to deactivate.")]
    public GameObject firstObjectToDeactivate;

    [Tooltip("The second parent object to deactivate.")]
    public GameObject secondObjectToDeactivate;

    [Tooltip("The first parent object to activate.")]
    public GameObject firstObjectToActivate;

    [Tooltip("The second parent object to activate.")]
    public GameObject secondObjectToActivate;

    [Tooltip("The delay in seconds before the object switch.")]
    public float delay = 1.0f;

    // Reference to the ARSessionResetter script
    public ARSessionResetter arSessionResetter;

    public void OnInteractableClick()
    {
        // Reset the AR session
        arSessionResetter.ResetARSession();

        StartCoroutine(SwitchObjectsAfterDelay());
    }

    public IEnumerator SwitchObjectsAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Deactivate the button itself
        gameObject.SetActive(false);

        // Deactivating objects
        if (firstObjectToDeactivate != null)
        {
            firstObjectToDeactivate.SetActive(false);
        }

        if (secondObjectToDeactivate != null)
        {
            secondObjectToDeactivate.SetActive(false);
        }

        // Activating objects
        if (firstObjectToActivate != null)
        {
            firstObjectToActivate.SetActive(true);
        }

        if (secondObjectToActivate != null)
        {
            secondObjectToActivate.SetActive(true);
        }
    }
}
