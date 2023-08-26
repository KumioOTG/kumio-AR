using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DetectionManager : MonoBehaviour
{
    // List of all detectors in the scene
    [SerializeField]
    private List<PuzzleDetectorV2> detectors = new List<PuzzleDetectorV2>();

    [SerializeField]
    private GameObject objectToActivate; // The object you want to activate when all are detected.

    [SerializeField]
    private GameObject objectToDeactivate; // The object you want to deactivate when all are detected.


    private int detectedCount = 0; // Count of objects that have been detected

    private void Start()
    {
        foreach (var detector in detectors)
        {
            detector.OnDetection += HandleDetection; // Subscribe to detection events from each detector
        }
    }

    private void HandleDetection(bool isDetected)
    {
        if (isDetected)
        {
            detectedCount++;
            UnityEngine.Debug.Log($"Detected objects: {detectedCount}/{detectors.Count}");
        }
        else
        {
            detectedCount--;
            UnityEngine.Debug.Log($"Detected objects reduced: {detectedCount}/{detectors.Count}");
        }

        // Check if all objects are detected
        CheckAllObjectsDetected(); // This line was missing in your original code
    }


    private void OnDestroy()
    {
        // Unsubscribe from all events to prevent memory leaks
        foreach (var detector in detectors)
        {
            detector.OnDetection -= HandleDetection;
        }
    }

    private void CheckAllObjectsDetected()
    {
        if (detectedCount == detectors.Count)
        {
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
    }
}
