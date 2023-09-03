using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonAR2 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioClip buttonClickSound; // Assign this from the editor

    private AudioSource audioSource;
    private bool hasBeenClicked = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource on this GameObject, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // This function will be called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasBeenClicked)
        {
            PlaySound();
            hasBeenClicked = true;
        }
    }

    private void PlaySound()
    {
        if (buttonClickSound && audioSource)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
