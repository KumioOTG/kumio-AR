using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivateNextObjectUI3 : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToActivate;  // The object you want to activate

    [SerializeField]
    private GameObject objectToDeactivate1;  // The first object you want to deactivate

    [SerializeField]
    private GameObject objectToDeactivate2;  // The second object you want to deactivate

    [SerializeField]
    private float delayTime = 1.0f; // Default delay of 1 second, but you can set this from the editor

    private int clickCount = 0;

    void Start()
    {
        // Add a listener to the button component, which triggers when the button is clicked
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        clickCount++;

        if (clickCount == 2)
        {
            // Use StartCoroutine to implement the delay
            StartCoroutine(DelayedSwapActivation());
        }
    }

    IEnumerator DelayedSwapActivation()
    {
        yield return new WaitForSeconds(delayTime);

        // Deactivate the specified objects
        if (objectToDeactivate1 != null)
        {
            objectToDeactivate1.SetActive(false);
        }

        if (objectToDeactivate2 != null)
        {
            objectToDeactivate2.SetActive(false);
        }

        // Activate the specified object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
