using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectByTags : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] List<string> tagsToSnap;
    [SerializeField] Transform objectToSnap = null;

    // Property with public getter and private setter
    public bool Snapped { get; private set; } = false;

    public Transform GetObjectToSnap() // Public getter for objectToSnap
    {
        return objectToSnap;
    }

    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityPointerHandler>(this);
    }

    public void Update()
    {
        if (Snapped)
        {
            if (Vector3.Distance(objectToSnap.position, transform.position) > 0.01f)
            {
                objectToSnap = null;
                Snapped = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!Snapped && objectToSnap == null && tagsToSnap.Contains(other.tag))
        {
            objectToSnap = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsToSnap.Contains(other.tag))
        {
            objectToSnap = null;
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (!Snapped && objectToSnap != null)
        {
            objectToSnap.position = transform.position;
            objectToSnap.rotation = transform.rotation;
            Snapped = true;
        }
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }
}
