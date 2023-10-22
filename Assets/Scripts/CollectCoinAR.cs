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
    [SerializeField]
    private AudioClip collectSound; // The audio clip of the collection sound
    [SerializeField]
    private string itemId; // unique identifier for each collectible across all scenes


    private bool isCollected = false;
    private float lastTapTime = 0f;
    private float tapSpeed = 0.5f;
    private AudioSource audioSource;

    public bool IsCollected { get { return isCollected; } }

    private void Start()
    {
        UpdateButtonImage();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSound;

        // Listen to the event
        CollectorManager.Instance.OnItemCollected += HandleItemCollected;

        // Subscribe to the event
        CollectorManager.Instance.OnSceneChanged += UpdateButtonImage;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        if (CollectorManager.Instance != null)
            CollectorManager.Instance.OnItemCollected -= HandleItemCollected;

        // Unsubscribe from the new event
        if (CollectorManager.Instance != null)
            CollectorManager.Instance.OnSceneChanged -= UpdateButtonImage;
    }

    private void UpdateButtonImage()
    {
        if (CollectorManager.Instance.IsItemCollected(itemId))
        {
            relatedButton.GetComponent<Image>().sprite = collectedSprite;
        }
        else
        {
            relatedButton.GetComponent<Image>().sprite = notCollectedSprite;
        }
    }

    private void HandleItemCollected(string collectedItemId)
    {
        if (collectedItemId == itemId)
        {
            UpdateButtonImage();
        }
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
        if (Time.time - lastTapTime < tapSpeed)
        {
            Collect();
        }
        lastTapTime = Time.time;
    }

    private void Collect()
    {
        isCollected = true;
        relatedButton.GetComponent<Image>().sprite = collectedSprite;
        audioSource.Play();
        CollectorManager.Instance.CollectItem(itemId);
        StartCoroutine(DeactivateAfterSound());
    }

    private IEnumerator DeactivateAfterSound()
    {
        yield return new WaitForSeconds(collectSound.length); // Wait for the duration of the sound
        gameObject.SetActive(false);
    }

    public void ResetCollectible()
    {
        isCollected = false;
        relatedButton.GetComponent<Image>().sprite = notCollectedSprite;
    }
}
