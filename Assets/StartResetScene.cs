using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartResetScene : MonoBehaviour
{
    [Tooltip("The parent object to deactivate.")]
    public GameObject objectToDeactivate;

    [Tooltip("The parent object to activate.")]
    public GameObject objectToActivate;

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

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
