using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWithDelay : MonoBehaviour
{
    [SerializeField]
    private float timeToDeactivate = 5f; // Time (in seconds) after which the object will be deactivated. Set this value from the editor.

    private void OnEnable() // This function is called when the object is activated.
    {
        Invoke("DeactivateObject", timeToDeactivate); // Schedule the deactivation.
    }

    private void OnDisable() // This function is called when the object is deactivated.
    {
        CancelInvoke("DeactivateObject"); // Cancel the scheduled deactivation to avoid any unwanted behavior if the object is deactivated manually before the timer runs out.
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false); // Deactivate the object.
    }
}
