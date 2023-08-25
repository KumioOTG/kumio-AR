using UnityEngine;
using System.Collections;

public class JigsawManager : MonoBehaviour
{
    public GameObject wellDoneMessage;

    public float popUpDelay = 1f; // Delay before the message pops up

    public float fadeInDuration = 0.5f; // Duration of the fade-in animation

    public static JigsawManager Instance { get; private set; }

    private PuzzlePiece[] puzzlePieces;

    private void Awake()
    {
        Instance = this;
        puzzlePieces = FindObjectsOfType<PuzzlePiece>();
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
