using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private NarratorManager narrator;
    [SerializeField] private GameObject scene;
    [SerializeField] private AudioClip narration;
    [SerializeField] private List<AudioClip> followingNarrations;

    void Start()
    {
        narrator = FindObjectOfType<NarratorManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            scene.SetActive(true);

            if (narration != null)
            {
                SetNarration();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void SetNarration()
    {
        if (!narrator.isBusy)
        {
            narrator.ChangeNarration(narration, followingNarrations);
        }
        else
        {
            FunctionTimer.Create(SetNarration, narrator.waitTime + 0.2f, "SceneNarrationWait");
        }
    }
}
