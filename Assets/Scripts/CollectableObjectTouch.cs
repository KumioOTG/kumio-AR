using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UDebug = UnityEngine.Debug;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Diagnostics;

public class CollectableObjectTouch : MonoBehaviour, IMixedRealityTouchHandler
{
    [SerializeField]
    private GameObject objectToCollect;
    [SerializeField]
    private GameObject soundPrefab; // Set this prefab in the inspector

    private bool isCollected = false;
    private static int numCollected = 0;

    private void Start()
    {
        numCollected = 0;

        // Register for parent notification
        ParentObjectController parentController = GetComponentInParent<ParentObjectController>();
        if (parentController != null)
        {
            OnCollect += parentController.CollectableObjectCollected;
        }
    }

    public event Action OnCollect;

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        if (!isCollected)
        {
            // Calculate the ray from the input pointer
            Transform cameraTransform = CameraCache.Main.transform;
            Vector3 pointerPosition = eventData.InputSource.Pointers[0].Position;
            Vector3 pointerDirection = cameraTransform.TransformDirection(eventData.InputSource.Pointers[0].Rotation * Vector3.forward);
            Ray pointerRay = new Ray(pointerPosition, pointerDirection);
            // Check if the ray hits the object to collect
            RaycastHit hit;
            if (Physics.Raycast(pointerRay, out hit))
            {
                if (hit.collider.gameObject != objectToCollect)
                {
                    return;
                }
                // Add collect action here
                UnityEngine.Debug.Log("Collecting " + objectToCollect.name);
                isCollected = true;
                numCollected++;
                UnityEngine.Debug.Log(numCollected + " objects collected");

                // Notify parent about collection
                OnCollect?.Invoke();

                // Play collect sound
                if (soundPrefab != null)
                {
                    GameObject soundInstance = Instantiate(soundPrefab, objectToCollect.transform.position, Quaternion.identity);
                    AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
                    audioSource.Play();
                    Destroy(soundInstance, audioSource.clip.length);
                }

                // Destroy the game object
                Destroy(objectToCollect);
                UnityEngine.Debug.Log("Destroyed " + objectToCollect.name);
            }
        }
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        // No action needed
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        // No action needed
    }

    public static int GetNumCollected()
    {
        return numCollected;
    }
}