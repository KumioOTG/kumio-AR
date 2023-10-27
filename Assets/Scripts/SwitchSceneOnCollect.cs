using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;

public class SwitchSceneOnCollect : MonoBehaviour
{
    [SerializeField] private string triggerItemId; // The itemId of the object that, when collected, triggers the scene switch
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float delayInSeconds = 0f;

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
