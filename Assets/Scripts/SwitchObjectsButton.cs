
using System.Collections;
using UnityEngine;

public class SwitchObjectsButton : MonoBehaviour
{
    [Tooltip("The parent object to deactivate.")]
    public GameObject objectToDeactivate;

    [Tooltip("The parent object to activate.")]
    public GameObject objectToActivate;

    [Tooltip("The delay in seconds before the object switch.")]
    public float delay = 1.0f;

    public void SwitchObjectsDelayed()
    {
        StartCoroutine(SwitchObjectsAfterDelay());
    }

    private IEnumerator SwitchObjectsAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
