using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;
using System.Collections;




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

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);  // Wait for half a second
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
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
        Debug.Log("Scene loaded: " + scene.name);
    }
}

