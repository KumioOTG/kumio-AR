using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenuButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    [SerializeField] private NarratorManager narrator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private AudioClip busyAudioClip;

    [SerializeField] private int narrationId;

    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private List<Sprite> icons;

    void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
        narrator = FindAnyObjectByType<NarratorManager>();
    }

    void Update()
    {
        if (narrator.isBusy)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        if (narrationId < gameManager.listenedNarrations.Count && gameManager.listenedNarrations[narrationId] != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;

            if (audioSource.clip == gameManager.listenedNarrations[narrationId])
            {
                icon.sprite = icons[2];
            }
            else
            {
                icon.sprite = icons[0];
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            icon.sprite = icons[1];
        }
    }

    public void ToggleNarration()
    {
        if (!narrator.isBusy)
        {
            audioSource.Stop();
            if (audioSource.clip == gameManager.listenedNarrations[narrationId])
            {
                audioSource.clip = null;
            }
            else
            {
                audioSource.clip = gameManager.listenedNarrations[narrationId];
                audioSource.Play();
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(busyAudioClip, transform.position);
        }
    }
}
