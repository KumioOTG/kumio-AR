using UnityEngine;

public class PuzzleTarget : MonoBehaviour
{
    private PuzzlePiece assignedPiece;

    public void AssignPiece(PuzzlePiece piece)
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
