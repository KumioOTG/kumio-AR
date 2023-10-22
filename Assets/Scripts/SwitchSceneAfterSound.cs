using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class SwitchSceneAfterSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private string sceneToLoad;
    [SerializeField] private float delayInSeconds = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Play the AudioSource and when it's done, change the scene.
        audioSource.Play();
        StartCoroutine(WaitForAudioToEnd());
    }

    private IEnumerator WaitForAudioToEnd()
    {
        // Wait for the duration of the audio clip
        yield return new WaitForSeconds(audioSource.clip.length);

        // Once audio ends, proceed to scene switch
        SwitchScene();
    }

    private void SwitchScene()
    {
        if (MixedRealityPlayspace.Transform != null)
        {
            PlayerData.LastPosition = MixedRealityPlayspace.Transform.position;
            PlayerData.LastRotation = MixedRealityPlayspace.Transform.rotation;
        }

        UnityEngine.Debug.Log("Audio completed! Loading scene after delay...");
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        UnityEngine.Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}

