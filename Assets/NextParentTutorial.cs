using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using System.Collections;
using UDebug = UnityEngine.Debug;
using System.Diagnostics;

public class NextParentTutorial : MonoBehaviour
{
    [SerializeField] private GameObject childObject;
    [SerializeField] public GameObject nextParent1; // First parent
    [SerializeField] public GameObject nextParent2; // Second parent
    [SerializeField] private float delayInSeconds = 0.0f;
    [SerializeField] private float minAngle = 0.0f;
    [SerializeField] private float maxAngle = 360.0f;
    [SerializeField] private AudioClip rotationCompleteSound;

    private Quaternion prevRotation = Quaternion.identity;
    private bool isParentActivated = false;
    private bool isRotating = false;
    private bool isPointerDown = false;
    private AudioSource audioSource;

    private void Start()
    {
        prevRotation = childObject.transform.rotation;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        ObjectManipulator objectManipulator = childObject.GetComponent<ObjectManipulator>();
        if (objectManipulator != null)
        {
            objectManipulator.OnManipulationStarted.AddListener(OnManipulationStarted);
            objectManipulator.OnManipulationEnded.AddListener(OnManipulationEnded);
        }
        else
        {
            UDebug.LogError("ObjectManipulator component not found on child object.");
        }
    }

    private void Update()
    {
        UDebug.Log($"isRotating: {isRotating}, isPointerDown: {isPointerDown}");

        Quaternion currRotation = childObject.transform.rotation;

        float angleDifference = Quaternion.Angle(prevRotation, currRotation);
        if (angleDifference > 0.0f)
        {
            float currentAngle = childObject.transform.localEulerAngles.y;
            if (currentAngle >= minAngle && currentAngle <= maxAngle)
            {
                isRotating = true;
            }
        }
        else if (isRotating && !isPointerDown)
        {
            isRotating = false;
            isParentActivated = true;

            UDebug.Log("Parent activated!");

            if (rotationCompleteSound != null)
            {
                audioSource.PlayOneShot(rotationCompleteSound);
            }

            if (delayInSeconds > 0.0f)
            {
                StartCoroutine(ActivateNextParentWithDelay());
            }
            else
            {
                ActivateNextParent();
            }
        }

        prevRotation = currRotation;
    }

    private IEnumerator ActivateNextParentWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        ActivateNextParent();

        gameObject.SetActive(false);
    }

    public void ActivateNextParent()
    {
        if (nextParent1 != null)
        {
            nextParent1.SetActive(true);
            UDebug.Log("Next parent 1 activated!");
        }

        if (nextParent2 != null)
        {
            nextParent2.SetActive(true);
            UDebug.Log("Next parent 2 activated!");
        }
    }

    private void OnManipulationStarted(ManipulationEventData eventData)
    {
        UDebug.Log("OnManipulationStarted");
        isRotating = true;
    }

    private void OnManipulationEnded(ManipulationEventData eventData)
    {
        UDebug.Log("OnManipulationEnded");
        isRotating = false;
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        UDebug.Log("OnPointerDown");
        isPointerDown = true;
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        UDebug.Log("OnPointerUp");
        isPointerDown = false;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
