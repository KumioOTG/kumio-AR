
using System.Collections;
using UnityEngine;

public class SwitchObjectsButton2 : MonoBehaviour
{
    [Tooltip("The parent object to deactivate.")]
    public GameObject objectToDeactivate;

    [Tooltip("The first parent object to activate.")]
    public GameObject objectToActivate1;  // renamed for clarity

    [Tooltip("The second parent object to activate.")]
    public GameObject objectToActivate2;  // new object to activate

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

        if (objectToActivate1 != null)
        {
            objectToActivate1.SetActive(true);
        }

        if (objectToActivate2 != null)
        {
            objectToActivate2.SetActive(true);
        }
    }
}
