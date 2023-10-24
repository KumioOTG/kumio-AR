using UnityEngine;
using System.Collections.Generic;

public class TimedActivation : MonoBehaviour
{
    [Header("Activation Settings")]
    public List<GameObject> targetObjects = new List<GameObject>(); // List of objects to control visibility.
    public Material[] activationMaterials; // Materials to be applied when objects are activated.
    public float activationTime = 5f;      // Time in seconds before the objects activate.
    public float duration = 10f;           // Duration in seconds the objects remain active.

    private bool hasActivated = false;
    private float startTime;
    private List<Renderer> objectRenderers = new List<Renderer>();
    private List<Material[]> originalMaterials = new List<Material[]>(); // To store the original materials.

    void OnEnable()
    {
        // This method is called when the parent object becomes active.
        // Set the start time.
        startTime = Time.time;

        // For each target object, get its renderer, store its original materials and disable it.
        foreach (var target in targetObjects)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer)
            {
                // Store the original materials.
                originalMaterials.Add(renderer.materials);

                renderer.enabled = false;
                objectRenderers.Add(renderer);
            }
        }
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= activationTime && !hasActivated)
        {
            Activate();
            hasActivated = true;
        }

        if (hasActivated && elapsedTime >= activationTime + duration)
        {
            Deactivate();
        }
    }

    void Activate()
    {
        for (int i = 0; i < objectRenderers.Count; i++)
        {
            objectRenderers[i].enabled = true;

            // If there are enough activation materials provided, apply them.
            if (i < activationMaterials.Length)
            {
                objectRenderers[i].material = activationMaterials[i];
            }
        }
    }

    void Deactivate()
    {
        for (int i = 0; i < objectRenderers.Count; i++)
        {
            objectRenderers[i].enabled = false;

            // Restore original materials.
            objectRenderers[i].materials = originalMaterials[i];
        }
    }
}
