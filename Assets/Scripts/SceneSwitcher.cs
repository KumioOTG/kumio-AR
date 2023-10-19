using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;



public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // The name of the scene to load
    [SerializeField] private Interactable buttonToActivate; // The MRTK button that will trigger the scene change
    [SerializeField] private float delayInSeconds = 0f; // Delay before scene change

    private void Awake()
    { 
      // Unregister to ensure we don't add it multiple times
    buttonToActivate.OnClick.RemoveListener(OnClick);
    // Register our OnClick function
    buttonToActivate.OnClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (MixedRealityPlayspace.Transform != null)
        {
            PlayerData.LastPosition = MixedRealityPlayspace.Transform.position;
            PlayerData.LastRotation = MixedRealityPlayspace.Transform.rotation;
        }
        
        UnityEngine.Debug.Log("Button Pressed! Loading scene after delay...");

        // Start the scene load with delay
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Load the specified scene
        UnityEngine.Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
