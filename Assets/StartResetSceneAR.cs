using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartResetSceneAR : MonoBehaviour
{
    [Tooltip("The parent object to deactivate.")]
    public GameObject objectToDeactivate;

    [Tooltip("The parent object to activate.")]
    public GameObject objectToActivate;

    [Tooltip("The delay in seconds before the object switch.")]
    public float delay = 1.0f;

    // Reference to the ARSessionResetter script
    public ARSessionResetter arSessionResetter;

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // Reset the AR session
        arSessionResetter.ResetARSession();

        StartCoroutine(SwitchObjectsAfterDelay());
    }

    public IEnumerator SwitchObjectsAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Deactivate the button itself
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
