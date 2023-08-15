using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UDebug = UnityEngine.Debug;

public class ObjectDetectorV3 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string targetTag = "Target";
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private UnityEvent onObjectEnter;
    [SerializeField] private UnityEvent onObjectExit;

    private Transform targetTransform;
    private bool isTargetInside;

    public void OnPointerDown(PointerEventData eventData)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag))
            {
                targetTransform = hitCollider.transform;
                StartCoroutine(CheckTargetPosition());
                break;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (targetTransform != null)
        {
            StopCoroutine(CheckTargetPosition());
            if (isTargetInside)
            {
                isTargetInside = false;
                UDebug.Log("Target object exited the container.");
                onObjectExit.Invoke();
            }
            targetTransform = null;
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
                UDebug.Log("Target object entered the middle of the container.");
                onObjectEnter.Invoke();
            }
            else if (distance > detectionRadius && isTargetInside)
            {
                isTargetInside = false;
                UDebug.Log("Target object moved out of the middle of the container.");
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