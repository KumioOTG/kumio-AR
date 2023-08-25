using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector2 initialPosition;
    private PuzzleTarget correctPosition;
    private float snapThreshold = 50f;
    private bool isCorrectlyPlaced = false;

    private void Start()
    {
        initialPosition = transform.position;
        int siblingIndex = transform.GetSiblingIndex();
        correctPosition = GameObject.Find("Piece" + (siblingIndex + 1) + "_Target")?.GetComponent<PuzzleTarget>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float distance = Vector2.Distance(transform.position, correctPosition.transform.position);

        if (distance < snapThreshold && IsCorrectlyPlaced())
        {
            transform.position = correctPosition.transform.position;
            isCorrectlyPlaced = true;
            JigsawManager.Instance.CheckPuzzleCompletion();
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    // Check if the piece is correctly placed
    public bool IsCorrectlyPlaced()
    {
        float distance = Vector2.Distance(transform.position, correctPosition.transform.position);
        float angleDifference = Quaternion.Angle(transform.rotation, correctPosition.transform.rotation);

        // Modify these threshold values to match your game's requirements
        float positionThreshold = 20f;
        float rotationThreshold = 10f;

        return distance < positionThreshold && angleDifference < rotationThreshold;
    }

}
