

using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using System.Collections;
using UDebug = UnityEngine.Debug;


public class ParentObjectControllerV4 : MonoBehaviour
{
    [SerializeField] private GameObject childObject;
    [SerializeField] private GameObject nextParent;
    [SerializeField] private float delayInSeconds = 0.0f;
    [SerializeField] private AudioClip resizeCompleteSound;
    [SerializeField] private AudioClip volumeIncreasedSound;

    private Vector3 prevScale = Vector3.zero;
    private float prevVolume = 0.0f;
    private bool hasIncreasedSize = false;
    private bool hasDecreasedSize = false;
    private bool hasPlayedVolumeIncreasedSound = false;
    private AudioSource audioSource;
    [SerializeField] AudioSource volumeIncrease;

    private float initializationDelay = 0.5f;
    private float startTime;
    private bool isInitialized = false;

    private void Start()
    {
        prevScale = childObject.transform.localScale;
        prevVolume = CalculateVolume(childObject);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        startTime = Time.time;
    }

    private void Update()
    {
        if (!isInitialized && Time.time - startTime >= initializationDelay)
        {
            isInitialized = true;
            prevScale = childObject.transform.localScale;
            prevVolume = CalculateVolume(childObject);
        }

        if (!isInitialized)
        {
            return;
        }

        Vector3 currScale = childObject.transform.localScale;
        float currVolume = CalculateVolume(childObject);
        float epsilon = 1e-4f;

        if (currVolume > prevVolume + epsilon)
        {
            hasIncreasedSize = true;
            prevVolume = currVolume;
            Debug.Log("Increased size!");

            if (!hasPlayedVolumeIncreasedSound && volumeIncreasedSound != null)
            {
                volumeIncrease.Stop();
                audioSource.clip = volumeIncreasedSound;
                audioSource.Play();
                hasPlayedVolumeIncreasedSound = true;
            }
        }
        else if (currVolume < prevVolume - epsilon)
        {
            hasDecreasedSize = true;
            prevVolume = currVolume;
            Debug.Log("Decreased size!");
        }

        if (hasIncreasedSize && hasDecreasedSize)
        {
            Debug.Log("Parent activated!");
            ActivateNextParentIfNeeded();
            hasIncreasedSize = false;
            hasDecreasedSize = false;
            if (resizeCompleteSound != null)
            {
                audioSource.PlayOneShot(resizeCompleteSound);
            }
        }
    }

    public void ActivateNextParentIfNeeded()
    {
        if (delayInSeconds > 0.0f)
        {
            StartCoroutine(ActivateNextParentWithDelay());
        }
        else
        {
            ActivateNextParent();
        }
    }

    private float CalculateVolume(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;

        Vector3 size = bounds.size;
        return size.x * size.y * size.z;
    }

    private IEnumerator ActivateNextParentWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        ActivateNextParent();

        gameObject.SetActive(false);
    }

    private void ActivateNextParent()
    {
        if (nextParent != null)
        {
            nextParent.SetActive(true);
            Debug.Log("Next parent activated!");
        }
    }
}