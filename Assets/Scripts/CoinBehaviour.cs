using CodeMonkey.Utils;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour, IMixedRealityPointerHandler
{


    public CoinType type;

    [SerializeField] private Manager gameManager;
    [SerializeField] private Transform player;
    [SerializeField] private NarratorManager narrator;
    [SerializeField] private AudioClip narration;
    [SerializeField] private List<AudioClip> followingNarrations;
    [SerializeField] private GameObject soundPrefab; 

    [SerializeField] private float clicked = 0;
    [SerializeField] private float clickdelay = 0.5f;
    [SerializeField] private float clicktime = 0;
    [SerializeField] private float spawnDistance = 1;

    private bool isCollected = false;
    private bool isTouched = false;
    private static int numCollected = 0;

    private int airTapCount = 0;
    private float tapTimeLimit = 0.5f;
    private int originalLayer;

    void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        narrator = FindObjectOfType<NarratorManager>();
        originalLayer = gameObject.layer;
    }

    void Update()
    {

    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (!isCollected)
        {
            airTapCount++;

            if (airTapCount == 1)
            {
                StartCoroutine(ResetAirTapCount());
            }
            else if (airTapCount == 2)
            {
                UnityEngine.Debug.Log("Collecting " + gameObject.name);
                isCollected = true;
                numCollected++;
                UnityEngine.Debug.Log(numCollected + " objects collected");

                // Play collect sound
                if (soundPrefab != null)
                {
                    GameObject soundInstance = Instantiate(soundPrefab, transform.position, Quaternion.identity);
                    AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
                    audioSource.Play();
                    Destroy(soundInstance, audioSource.clip.length);
                }

                // Collect
                Collect();
            }
        }
    }

    private IEnumerator ResetAirTapCount()
    {
        yield return new WaitForSeconds(tapTimeLimit);
        airTapCount = 0;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void Activate()
    {
        transform.position = player.position + player.forward * spawnDistance;
        transform.rotation = player.rotation;
        isCollected = false; // reset the collected state
        gameObject.layer = originalLayer;
        GetComponent<Renderer>().enabled = true;
    }


    private void Collect()
    {
        ResetClicks();
        gameManager.coins[(int)type] = this;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        GetComponent<Renderer>().enabled = false;
    }

    private void ResetClicks()
    {
        clicked = 0;
        clicktime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHand" && !isTouched)
        {
            isTouched = true;
            FunctionTimer.Create(ResetTouchStatus, 1f, "ResetTouchStatus");
            Vector3 colliderDirection = other.transform.forward;
            float dotProduct = Vector3.Dot(colliderDirection, transform.forward);

            if (dotProduct < 0)
            {     
                narrator.ChangeNarration(narration, followingNarrations);
            }
        }
    }

    private void ResetTouchStatus()
    {
        isTouched = false;
    }

    public static int GetNumCollected()
    {
        return numCollected;
    }
}

