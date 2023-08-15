using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Diagnostics;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;

[DebuggerDisplay("Collectable Object: {gameObject.name}")]
public class CollectableObjectTouchV2 : MonoBehaviour, IMixedRealityTouchHandler
{
    [SerializeField]
    private GameObject objectToCollect;
    [SerializeField]
    private AudioClip collectSound;

    private AudioSource audioSource;
    private bool isCollected = false;
    private static int numCollected = 0;

    private void Start()
    {
        numCollected = 0;
        audioSource = gameObject.AddComponent<AudioSource>();

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

                // Play collect sound
                if (collectSound != null)
                {
                    audioSource.PlayOneShot(collectSound);
                }

                // Add collect action here
                UnityEngine.Debug.Log("Collecting " + objectToCollect.name);
                isCollected = true;
                numCollected++;
                UnityEngine.Debug.Log(numCollected + " objects collected");

                // Notify parent about collection
                OnCollect?.Invoke();

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

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
