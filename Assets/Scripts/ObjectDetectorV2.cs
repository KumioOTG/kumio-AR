using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UDebug = UnityEngine.Debug;

public class ObjectDetectorV2 : MonoBehaviour
{
    [SerializeField] private string targetTag = "Target";
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private UnityEvent onObjectEnter;
    [SerializeField] private UnityEvent onObjectExit;

    private Transform targetTransform;
    private bool isTargetInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetTransform = other.transform;
            StartCoroutine(CheckTargetPosition());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetTransform = null;
            StopCoroutine(CheckTargetPosition());
            if (isTargetInside)
            {
                isTargetInside = false;
                Debug.Log("Target object exited the container.");
                onObjectExit.Invoke();
            }
        }
    }

    private IEnumerator CheckTargetPosition()
    {
        while (targetTransform != null)
        {
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if (distance <= detectionRadius && !isTargetInside)
            {
                isTargetInside = true;
                Debug.Log("Target object entered the middle of the container.");
                onObjectEnter.Invoke();
            }
            else if (distance > detectionRadius && isTargetInside)
            {
                isTargetInside = false;
                Debug.Log("Target object moved out of the middle of the container.");
                onObjectExit.Invoke();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public bool IsTargetInsideMiddle()
    {
        return isTargetInside && Vector3.Distance(transform.position, targetTransform.position) <= detectionRadius;
    }
}
