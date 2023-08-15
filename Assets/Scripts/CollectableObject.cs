using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Diagnostics;
using UDebug = UnityEngine.Debug;

[DebuggerDisplay("Collectable Object: {gameObject.name}")]
public class CollectableObject : MonoBehaviour, IMixedRealityPointerHandler
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

    public event System.Action OnCollect;

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (!isCollected)
        {
            if (eventData.Pointer == null || eventData.Pointer.Result.CurrentPointerTarget != objectToCollect)
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

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // No action needed
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // No action needed
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
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
