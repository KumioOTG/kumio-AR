using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectCoinAR : MonoBehaviour
{
    [SerializeField]
    private Button relatedButton;
    [SerializeField]
    private Sprite notCollectedSprite;
    [SerializeField]
    private Sprite collectedSprite;

    public bool IsCollected { get; private set; } = false;

    private float lastTapTime = 0f;
    private float tapSpeed = 0.5f;

    private void Start()
    {
        relatedButton.GetComponent<Image>().sprite = notCollectedSprite;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !IsCollected)
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
        if (Time.time - lastTapTime < tapSpeed)
        {
            Collect();
        }
        lastTapTime = Time.time;
    }

    private void Collect()
    {
        IsCollected = true;
        relatedButton.GetComponent<Image>().sprite = collectedSprite;
        gameObject.SetActive(false); // Deactivate the coin when collected
    }

    public void ResetCollectible()
    {
        IsCollected = false;
        relatedButton.GetComponent<Image>().sprite = notCollectedSprite;
    }
}
