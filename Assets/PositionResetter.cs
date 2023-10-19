using System.Collections.Generic;
using UnityEngine;

public class PositionResetter : MonoBehaviour
{
    // Create a dictionary to store the original positions and rotations of objects.
    private Dictionary<Transform, (Vector3 position, Quaternion rotation)> originalTransforms
        = new Dictionary<Transform, (Vector3 position, Quaternion rotation)>();

    void Start()
    {
        // At the start of the scene, store the initial position and rotation of every child object.
        foreach (Transform child in transform)
        {
            originalTransforms[child] = (child.position, child.rotation);
        }
    }

    public void ResetPositions()
    {
        // When called, this function will reset every child object to its original position and rotation.
        foreach (var entry in originalTransforms)
        {
            entry.Key.position = entry.Value.position;
            entry.Key.rotation = entry.Value.rotation;
        }
    }
}
