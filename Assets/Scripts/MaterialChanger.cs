using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material material1;
    [SerializeField] private Material material2;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public void ChangeToMaterial1()
    {
        objectRenderer.material = material1;
    }

    public void ChangeToMaterial2()
    {
        objectRenderer.material = material2;
    }
}
