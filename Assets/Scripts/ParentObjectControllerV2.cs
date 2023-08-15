using UnityEngine;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;

public class ParentObjectControllerV2 : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField]
    private GameObject nextParent;
    [SerializeField]
    private GameObject currentParent;
    [SerializeField]
    private float delayInSeconds = 0.0f;

    [SerializeField]
    private ObjectDetectorV2 objectDetector;

    private bool isNextParentActivated = false;
    private bool isPointerReleased = false;


    private void Start()
    {
        isNextParentActivated = false;
    }

    private void Update()
    {
        if (objectDetector != null && !isNextParentActivated)
        {
            if (objectDetector.IsTargetInsideMiddle() && isPointerReleased)
            {
                isNextParentActivated = true;

                if (delayInSeconds > 0.0f)
                {
                    StartCoroutine(ActivateNextParentWithDelay());
                }
                else
                {
                    ActivateNextParent();
                }
            }
        }
    }

    private IEnumerator ActivateNextParentWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        ActivateNextParent();
        DeactivateCurrentParent();
    }

    public void ActivateNextParent()
    {
        if (nextParent != null)
        {
            nextParent.SetActive(true);
        }
    }

    public void DeactivateCurrentParent()
    {
        if (currentParent != null)
        {
            currentParent.SetActive(false);
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // Do nothing on pointer down
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        isPointerReleased = true;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Do nothing on pointer clicked
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // Do nothing on pointer dragged
    }
}
