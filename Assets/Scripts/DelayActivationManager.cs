using UnityEngine;
using System.Collections;

public class DelayActivationManager : MonoBehaviour
{
    public static DelayActivationManager Instance;

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

    public void ActivateObjectWithDelay(GameObject obj, float delayInSeconds)
    {
        StartCoroutine(DoActivateObjectWithDelay(obj, delayInSeconds));
    }

    private IEnumerator DoActivateObjectWithDelay(GameObject obj, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (obj != null)
        {
            obj.SetActive(true);
        }
    }
}
