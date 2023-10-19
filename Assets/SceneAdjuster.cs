using UnityEngine;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;


public class SceneAdjuster : MonoBehaviour
{
    [System.Serializable]
    public class SceneObjectData
    {
        public Transform sceneObject;
        public Vector3 initialPosition;
        public Quaternion initialRotation;
    }

    public List<SceneObjectData> sceneObjects = new List<SceneObjectData>();

    private void Awake()
    {
        // Capture the initial position and rotation for each object in the list
        foreach (SceneObjectData objData in sceneObjects)
        {
            if (objData.sceneObject != null)
            {
                objData.initialPosition = objData.sceneObject.position;
                objData.initialRotation = objData.sceneObject.rotation;
            }
        }
    }

    public void ResetToInitialPosition()
    {
        foreach (SceneObjectData objData in sceneObjects)
        {
            if (objData.sceneObject != null)
            {
                // Set the object back to its initial position and rotation
                objData.sceneObject.position = objData.initialPosition;
                objData.sceneObject.rotation = objData.initialRotation;

                Debug.Log("Reset object: " + objData.sceneObject.name);
            }
        }
    }
}
