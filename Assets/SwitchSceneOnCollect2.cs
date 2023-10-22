using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

public class SwitchSceneOnCollect2 : MonoBehaviour
{
    [SerializeField] private string triggerItemId; // The itemId of the object that, when collected, triggers the scene switch
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float delayInSeconds = 0f;
    [SerializeField] private AudioSource soundToWaitFor; // The sound to wait for completion

    private void Start()
    {
        // Subscribe to the OnItemCollected event from CollectorManager
        CollectorManager.Instance.OnItemCollected += HandleItemCollected;
    }

    private void OnDestroy()
    {
        if (CollectorManager.Instance != null)
        {
            CollectorManager.Instance.OnItemCollected -= HandleItemCollected;
        }
    }

    private void HandleItemCollected(string collectedItemId)
    {
        if (collectedItemId == triggerItemId)
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        if (soundToWaitFor != null && soundToWaitFor.isPlaying)
        {
            // If the sound is playing, wait for it to finish, then switch scene
            StartCoroutine(WaitForSoundToEnd());
        }
        else
        {
            // If there's no sound playing or if no AudioSource is set, immediately switch the scene
            DoSwitchScene();
        }
    }

    private IEnumerator WaitForSoundToEnd()
    {
        // Wait until the audio source has finished playing
        while (soundToWaitFor.isPlaying)
        {
            yield return null;
        }

        DoSwitchScene();
    }

    private void DoSwitchScene()
    {
        if (MixedRealityPlayspace.Transform != null)
        {
            PlayerData.LastPosition = MixedRealityPlayspace.Transform.position;
            PlayerData.LastRotation = MixedRealityPlayspace.Transform.rotation;
        }

        UnityEngine.Debug.Log("Switching scene after delay...");
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        UnityEngine.Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
