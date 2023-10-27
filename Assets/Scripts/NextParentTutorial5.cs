using UnityEngine;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;


public class NextParentTutorial5 : MonoBehaviour
{
    [Header("Object Detection")]
    [SerializeField] private PuzzleDetectorV3 puzzleDetector;

    [Header("Object Management")]
    [SerializeField] private GameObject currentParent;
    [SerializeField] private GameObject nextParent;
    [SerializeField] private GameObject nextParent2;

    [Header("Settings")]
    [SerializeField] private float delayInSeconds = 1.0f; // Delay in seconds before switching

    private bool isNextParentActivated = false;

    void Start()
    {
        if (puzzleDetector != null)
        {
            puzzleDetector.OnDetection += HandleObjectDetection;
        }
    }

    void OnDestroy()
    {
        if (puzzleDetector != null)
        {
            puzzleDetector.OnDetection -= HandleObjectDetection;
        }
    }

    private void HandleObjectDetection(bool isDetected)
    {
        if (isDetected && !isNextParentActivated)
        {
            StartCoroutine(DelayedActivation());
            isNextParentActivated = true;
        }
    }

    private IEnumerator DelayedActivation()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Now activate/deactivate objects
        if (currentParent != null)
            currentParent.SetActive(false);
        if (nextParent != null)
            nextParent.SetActive(true);
        if (nextParent2 != null)
            nextParent2.SetActive(true);
    }
}