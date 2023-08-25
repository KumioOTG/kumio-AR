using UnityEngine;
using UnityEngine.UI;

public class UIImageButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonClickSound; // Assign this from the editor

    private AudioSource audioSource;
    private Button uiButton;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource on this GameObject, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        uiButton = GetComponent<Button>();
        if (uiButton)
        {
            uiButton.onClick.AddListener(PlaySound);
        }
    }

    public void PlaySound()
    {
        if (buttonClickSound && audioSource)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    private void OnDestroy()
    {
        if (uiButton)
        {
            uiButton.onClick.RemoveListener(PlaySound);
        }
    }
}
