using UnityEngine;
using System.Collections;

public class JigsawManager : MonoBehaviour
{
    public GameObject wellDoneMessage;

    public float popUpDelay = 1f; // Delay before the message pops up
    public float fadeInDuration = 0.5f; // Duration of the fade-in animation

    [SerializeField]
    private AudioClip successSound; // Success sound when the puzzle is completed
    [SerializeField]
    private AudioClip piecePlacedSound; // Sound when a puzzle piece is correctly placed

    private AudioSource audioSource;

    public static JigsawManager Instance { get; private set; }

    private PuzzlePiece[] puzzlePieces;

    private void Awake()
    {
        Instance = this;
        puzzlePieces = FindObjectsOfType<PuzzlePiece>();
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        audioSource.clip = piecePlacedSound;
    }

    public void PlayPiecePlacedSound()
    {
        audioSource.PlayOneShot(piecePlacedSound);
    }

    public void CheckPuzzleCompletion()
    {
        bool allPiecesCorrect = true;

        foreach (PuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrectlyPlaced())
            {
                allPiecesCorrect = false;
                break;
            }
        }

        if (allPiecesCorrect)
        {
            audioSource.PlayOneShot(successSound); // Play success sound
            StartCoroutine(PopUpMessageWithDelay());
        }
    }

    private IEnumerator PopUpMessageWithDelay()
    {
        yield return new WaitForSeconds(popUpDelay);

        CanvasGroup canvasGroup = wellDoneMessage.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // Ensure it's fully transparent at the beginning

        wellDoneMessage.SetActive(true);

        float currentTime = 0;

        // Fade-in animation
        while (currentTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, currentTime / fadeInDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1; // Make sure it stays fully visible
    }
}
