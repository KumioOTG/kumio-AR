using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;

public class SceneManagementAR : MonoBehaviour
{
    // Optional Singleton implementation
    public static SceneManagementAR Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the player's position and rotation
        if (MixedRealityPlayspace.Transform != null) // Ensure MRTK is set up in the scene
        {
            MixedRealityPlayspace.Transform.position = Vector3.zero;
            MixedRealityPlayspace.Transform.rotation = Quaternion.identity;
        }
    }
}

