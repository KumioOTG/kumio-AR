using UnityEngine;

public class PuzzleTargetV2 : MonoBehaviour
{
    private PuzzlePieceV2 assignedPiece;

    public void AssignPiece(PuzzlePieceV2 piece)
    {
        assignedPiece = piece;
    }

    public bool IsCorrectlyPlaced()
    {
        if (assignedPiece == null)
        {
            Debug.LogWarning("No assigned piece in target.");
            return false;
        }

        return Vector2.Distance(assignedPiece.transform.position, transform.position) < 20f;
    }
}
