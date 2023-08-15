using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Diagnostics;
using CodeMonkey.Utils;

public class CollectibleObjectBehaviour : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] private Manager gameManager;
    [SerializeField] private Transform player;
    [SerializeField] private float clicked = 0;
    [SerializeField] private float clickdelay = 0.5f;
    [SerializeField] private float clicktime = 0;
    [SerializeField] private float moveDistance = 1;

    private void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if(clicked == 0)
        {
            clicktime = Time.time;
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            FunctionTimer.Create(CheckOneClick, clickdelay + 0.01f, "CheckOneClick");
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            ResetClicks();
            Collect();

        }
        else if (clicked > 2 || Time.time - clicktime > clickdelay + 0.01f)
        {
            clicked = 1;
            ResetClicks();
        }
    }

    private void ResetClicks()
    {
        clicked = 0;
        clicktime = 0;
    }

    private void CheckOneClick()
    {
        if (clicked == 1)
        {
            ResetClicks();
            MoveToPlayer();
        }
    }
    public void Activate()
    {
        MoveToPlayer();
        gameObject.SetActive(true);
    }

    private void MoveToPlayer()
    {
        transform.position = player.position + player.forward * moveDistance;
        transform.rotation = player.rotation;
    }

    private void Collect()
    {
        gameManager.CheckAndAddToCollectedObjects(this);
        gameObject.SetActive(false);
    }
}
