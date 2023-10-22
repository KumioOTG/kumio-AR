using UnityEngine;
using System.Collections;

public class JigsawManagerV2 : MonoBehaviour
{
    public GameObject wellDoneMessage;
    public GameObject firstAdditionalObject;  // The first additional GameObject to activate
    public GameObject secondAdditionalObject; // The second additional GameObject to activate
    public GameObject puzzleContainer;  // This will be the parent GameObject that contains all the puzzle pieces

    public float popUpDelay = 1f; // Delay before the message pops up
    public float puzzleDeactivationDelay = 3f; // Delay before deactivating the puzzle pieces

    public float fadeInDuration = 0.5f; // Duration of the fade-in animation

    public static JigsawManagerV2 Instance { get; private set; }

    private PuzzlePieceV2[] puzzlePieces;

    private void Awake()
    {
        Instance = this;
        puzzlePieces = FindObjectsOfType<PuzzlePieceV2>();
    }

    public void CheckPuzzleCompletion()
    {
        bool allPiecesCorrect = true;

        foreach (PuzzlePieceV2 piece in puzzlePieces)
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

        // Activate the additional objects
        firstAdditionalObject.SetActive(true);
        secondAdditionalObject.SetActive(true);

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

        // Wait for the specified delay and then deactivate the puzzle
        yield return new WaitForSeconds(puzzleDeactivationDelay);
        puzzleContainer.SetActive(false);
    }
}
