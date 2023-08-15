using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UDebug = UnityEngine.Debug;

public class ObjectDetectorV4 : MonoBehaviour, IMixedRealityPointerHandler
{

    [SerializeField]
    private GameObject targetGameObject;
    private SphereCollider sphereCollider;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            UDebug.LogError("Sphere Collider is missing");
        }

        if (targetGameObject == null)
        {
            UDebug.LogError("Target Game Object is not assigned");
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Vector3 pointerPosition = eventData.Pointer.Result.Details.Point;
        if (IsInsideSphere(pointerPosition))
        {
            UDebug.Log("Target Game Object detected inside the sphere.");
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }

    private bool IsInsideSphere(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) <= sphereCollider.radius * transform.localScale.x;
    }
}
