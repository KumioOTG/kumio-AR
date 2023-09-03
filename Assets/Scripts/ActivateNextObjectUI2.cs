using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateNextObjectUI2 : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToActivate;  // The object you want to activate

    [SerializeField]
    private GameObject objectToDeactivate;  // The object you want to deactivate

    [SerializeField]
    private float delayTime = 1.0f; // Default delay of 1 second, but you can set this from the editor

    private bool hasBeenClicked = false;

    void Start()
    {
        // Add a listener to the button component, which triggers when the button is clicked
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
        }
        else
        {
            // Use StartCoroutine to implement the delay
            StartCoroutine(DelayedSwapActivation());
        }
    }

    System.Collections.IEnumerator DelayedSwapActivation()
    {
        yield return new WaitForSeconds(delayTime);

        // Deactivate the specified object
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        // Activate the specified object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
