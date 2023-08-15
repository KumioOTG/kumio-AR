using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class NarratorManager : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip busyAudioClip;

    public bool isBusy;
    public float waitTime;

    [SerializeField] private AudioClip currentNarration;
    [SerializeField] private List<AudioClip> followingNarrations;

    void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
        isBusy = false;
        currentNarration = null;
        followingNarrations = null;
    }

    void Update()
    {
        if(isBusy)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            waitTime = 0;
        }
    }

    public void ChangeNarration(AudioClip narration, List<AudioClip> nextNarrations = null)
    {
        if (!isBusy)
        {
            isBusy = true;
            currentNarration = narration;
            followingNarrations = nextNarrations;

            PlayNarration();
        }
        else
        {
            AudioSource.PlayClipAtPoint(busyAudioClip, transform.position);
        }
    }

    private void PlayNarration()
    {
        audioSource.clip = currentNarration;
        audioSource.Play();
        waitTime = currentNarration.length;

        gameManager.CheckAndAddToListenedNarrations(currentNarration);

        if (followingNarrations == null || followingNarrations.Count == 0)
        {
            FunctionTimer.Create(ReleaseAudioSource, waitTime, "ReleaseAudioSource");
        }
        else
        {
            FunctionTimer.Create(MoveToNextAudioClip, waitTime, "MoveToNextAudioSource");
        }
    }

    private void MoveToNextAudioClip()
    {
        currentNarration = followingNarrations[0];
        followingNarrations.RemoveAt(0);

        PlayNarration();
    }

    private void ReleaseAudioSource()
    {
        waitTime = 0;

        currentNarration = null;
        followingNarrations = null;

        isBusy = false;
    }
}
