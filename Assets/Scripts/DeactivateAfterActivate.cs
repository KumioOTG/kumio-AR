using System;
using System.Linq;
using System.Collections;
using UnityEngine;


public class DeactivateAfterActivate : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private float activationDelayInSeconds; // Renamed for clarity
    [SerializeField] private float deactivationDelayInSeconds; // New variable for deactivation delay

    private void Start()
    {
        if (targetObjects != null && targetObjects.Length > 0) // Check if there are target objects
        {
            foreach (GameObject target in targetObjects)
            {
                target.SetActive(false);
            }
            Invoke("ActivateTargetObject", activationDelayInSeconds);
        }
        else
        {
            Debug.LogError("Target Object not assigned or array is empty.");
        }
    }

    private void ActivateTargetObject()
    {
        foreach (GameObject target in targetObjects)
        {
            target.SetActive(true);
        }

        // Invoke the deactivation after the specified delay
        Invoke("DeactivateTargetObject", deactivationDelayInSeconds);
    }

    private void DeactivateTargetObject()
    {
        foreach (GameObject target in targetObjects)
        {
            target.SetActive(false);
        }
    }
}
