using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    [SerializeField] private NarratorManager narrator;
    [SerializeField] private AudioClip narration;
    [SerializeField] private List<AudioClip> followingNarrations;

    void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
        narrator = FindObjectOfType<NarratorManager>();
    }

    void Update()
    {
        // No action needed.
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHand")
        {
            narrator.ChangeNarration(narration, followingNarrations);
        }
    }

    public void ChangeNarration()
    {
        narrator.ChangeNarration(narration, followingNarrations);
    }
}
