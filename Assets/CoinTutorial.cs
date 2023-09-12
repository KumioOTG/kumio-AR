using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToCollect;

    [SerializeField]
    private GameObject soundPrefab1; // Set the first sound prefab in the inspector

    [SerializeField]
    private GameObject soundPrefab2; // Set the second sound prefab in the inspector

    private bool isCollected = false;
    private int tapCount = 0;
    private float tapTimeLimit = 0.5f;
    private float lastTapTime = 0f;

    private void Start()
    {
        // Other initialization code if needed
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == objectToCollect.transform)
            {
                float currentTime = Time.time;
                if (currentTime - lastTapTime < tapTimeLimit)
                {
                    tapCount++;
                    if (tapCount == 2)
                    {
                        CollectObject();
                        tapCount = 0;
                    }
                }
                else
                {
                    tapCount = 1;
                }
                lastTapTime = currentTime;
            }
        }
    }

    private void CollectObject()
    {
        if (!isCollected)
        {
            UnityEngine.Debug.Log("Collecting " + objectToCollect.name);
            isCollected = true;

            // Add object to inventory
            InventoryManager.Instance.AddCollectableObject(objectToCollect);

            // Play the first sound
            PlaySound(soundPrefab1);

            // Play the second sound
            PlaySound(soundPrefab2);

            // Optionally, you can destroy the game object or make it inactive
            Destroy(objectToCollect);
            UnityEngine.Debug.Log("Destroyed " + objectToCollect.name);
        }
    }

    private void PlaySound(GameObject soundPrefab)
    {
        if (soundPrefab != null)
        {
            GameObject soundInstance = Instantiate(soundPrefab, objectToCollect.transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
        }
    }
}
