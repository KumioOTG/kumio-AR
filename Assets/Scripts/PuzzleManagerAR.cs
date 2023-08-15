using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerAR : MonoBehaviour
{
    [SerializeField] private CoinBehaviour coin;
    [SerializeField] private List<SnapObjectByTags> controlPoints;
    [SerializeField] private GameObject finalImage;
    [SerializeField] private GameObject objectToActivate;  // Game object to activate
    [SerializeField] private AudioClip completionSound;    // Sound to play on completion
    private AudioSource audioSource;                       // AudioSource to play the sound
    [SerializeField] private bool isCompleted = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        bool completionCheck = true;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            if (controlPoints[i].GetObjectToSnap() != null)
            {
                bool snapped = controlPoints[i].Snapped;
                completionCheck &= snapped;
                if (snapped)
                {
                    controlPoints[i].GetObjectToSnap().gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }

        if (completionCheck && !isCompleted)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.gameObject.name != "BackPlate")
                {
                    child.gameObject.SetActive(false);
                }
            }

            finalImage.SetActive(true);
            coin.Activate();
            isCompleted = true;

            // Activate the game object
            if (objectToActivate)
            {
                objectToActivate.SetActive(true);
            }

            // Play the completion sound
            if (completionSound)
            {
                audioSource.PlayOneShot(completionSound);
            }
        }
    }
}
