using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFloating : MonoBehaviour, IMixedRealityPointerHandler
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if(gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
