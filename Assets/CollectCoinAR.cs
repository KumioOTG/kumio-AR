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
        // Check for touch input
        if (Input.touchCount > 0 && !isCollected)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Check if touch position intersects with collectible
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                if (GetComponent<Collider>().bounds.Contains(touchPosition))
                {
                    HandleTap();
                }
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
