using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectCoinAR : MonoBehaviour
{
    [SerializeField]
    private Button relatedButton; // The UI button related to this collectible
    [SerializeField]
    private Sprite notCollectedSprite; // The sprite when not collected
    [SerializeField]
    private Sprite collectedSprite; // The sprite when collected

    private bool isCollected = false;
    private float lastTapTime = 0f;
    private float tapSpeed = 0.5f; // Time window for double-tap in seconds

    private void Start()
    {
        // Initialize UI button with not collected sprite
        relatedButton.GetComponent<Image>().sprite = notCollectedSprite;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isCollected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == this.transform)
            {
                HandleTap();
            }
        }
    }

    private void HandleTap()
    {
        // Check if time since last tap is within double-tap window
        if (Time.time - lastTapTime < tapSpeed)
        {
            Collect();
        }
        lastTapTime = Time.time;
    }

    private void Collect()
    {
        isCollected = true;
        // Update UI button with collected sprite
        relatedButton.GetComponent<Image>().sprite = collectedSprite;
        // Disable or destroy collectible object in the scene
        gameObject.SetActive(false);
    }
}
