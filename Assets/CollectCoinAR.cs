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

    private bool isCollected = false;
    private float lastTapTime = 0f;
    private float tapSpeed = 0.5f;
    private AudioSource audioSource;

    public bool IsCollected { get { return isCollected; } }

    private void Start()
    {
        relatedButton.GetComponent<Image>().sprite = notCollectedSprite;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSound;
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
