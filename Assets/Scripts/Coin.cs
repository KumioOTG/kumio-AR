using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToCollect;

    [SerializeField]
    private GameObject soundPrefab; // Set this prefab in the inspector

    private bool isCollected = false;
    private int tapCount = 0;
    private float tapTimeLimit = 0.5f;
    private float lastTapTime = 0f;

    public string coinID;

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
            InventoryManager2.Instance.AddCollectableObject(coinID, objectToCollect);

            // Play collect sound
            if (soundPrefab != null)
            {
                GameObject soundInstance = Instantiate(soundPrefab, objectToCollect.transform.position, Quaternion.identity);
                AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
                audioSource.Play();
                Destroy(soundInstance, audioSource.clip.length);
            }

            // Deactivate the game object
            objectToCollect.SetActive(false);
        }
    }
}