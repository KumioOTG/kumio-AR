using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectCoinARTutorial2 : MonoBehaviour
{

    [SerializeField]
    private AudioClip collectSound; // Assign this via the Inspector

    [SerializeField]
    private GameObject objectToActivate; // GameObject to activate on collection

    [SerializeField]
    private string itemId; // Unique identifier for each collectible across all scenes

    private bool isCollected = false;
    private float lastTapTime = 0f;
    private float tapSpeed = 0.5f;
    private AudioSource audioSource;

    public bool IsCollected { get { return isCollected; } }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (collectSound != null)
        {
            audioSource.clip = collectSound;
        }

        // Listen to the event
        if (CollectorManager.Instance != null)
        {
            CollectorManager.Instance.OnItemCollected += HandleItemCollected;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        if (CollectorManager.Instance != null)
        {
            CollectorManager.Instance.OnItemCollected -= HandleItemCollected;
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isCollected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
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
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Activate the assigned GameObject
        }

        if (CollectorManager.Instance != null)
        {
            CollectorManager.Instance.CollectItem(itemId);
        }
        StartCoroutine(DeactivateAfterSound());
    }

    private IEnumerator DeactivateAfterSound()
    {
        if (collectSound != null)
        {
            yield return new WaitForSeconds(collectSound.length);
        }
        gameObject.SetActive(false);
    }

    public void ResetCollectible()
    {
        isCollected = false;
        gameObject.SetActive(true); // Reactivating the GameObject if needed
    }

    private void HandleItemCollected(string collectedItemId)
    {
        // Handle the item collected event if needed
    }
}
