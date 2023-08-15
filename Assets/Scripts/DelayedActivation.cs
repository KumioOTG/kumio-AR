using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class DelayedActivation : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private float delayInSeconds;

    private void Start()
    {
        if (targetObjects != null)
        {
            foreach(GameObject target in targetObjects)
            {
                target.SetActive(false);
            }
            Invoke("ActivateTargetObject", delayInSeconds);
        }
        else
        {
            Debug.LogError("Target Object not assigned.");
        }
    }

    private void ActivateTargetObject()
    {
        foreach (GameObject target in targetObjects)
        {
            target.SetActive(true);
        }
    }
}
