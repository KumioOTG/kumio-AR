using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;


public class PuzzleDetectorV2 : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetLayers;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onObjectEnter; // This will be triggered when an object enters
    [SerializeField]
    private UnityEvent onObjectExit;  // This will be triggered when an object exits

    private Transform targetTransform;
    public event Action<bool> OnDetection; // Event to notify detection status

    private void OnTriggerEnter(Collider other)
    {
        if (IsTargetLayer(other.gameObject.layer))
        {
            targetTransform = other.transform;
            OnDetection?.Invoke(true); // Target detected
            onObjectEnter.Invoke(); // Trigger the UnityEvent for entering
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsTargetLayer(other.gameObject.layer))
        {
            targetTransform = null;
            OnDetection?.Invoke(false); // Target lost
            onObjectExit.Invoke(); // Trigger the UnityEvent for exiting
        }
    }

    private bool IsTargetLayer(int objectLayer)
    {
        return (targetLayers.value & (1 << objectLayer)) != 0;
    }


}

