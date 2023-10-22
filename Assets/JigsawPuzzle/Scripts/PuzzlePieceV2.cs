using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceV2 : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector2 initialPosition;
    private PuzzleTargetV2 correctPosition;
    private float snapThreshold = 50f;
    private bool isCorrectlyPlaced = false;

    public Transform linked3DModel;  // Drag the 3D model here in the editor
    private Vector3 initial3DModelPosition;  // To store the initial position of the 3D model

    private void Start()
    {
        initialPosition = transform.position;
        int siblingIndex = transform.GetSiblingIndex();
        correctPosition = GameObject.Find("Piece" + (siblingIndex + 1) + "_Target")?.GetComponent<PuzzleTargetV2>();

        // Save the 3D model's initial position
        if (linked3DModel != null)
        {
            initial3DModelPosition = linked3DModel.position;
        }
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
            JigsawManagerV2.Instance.CheckPuzzleCompletion();
        }
        else
        {
            transform.position = initialPosition;

            // Reset the 3D model's position
            if (linked3DModel != null)
            {
                linked3DModel.position = initial3DModelPosition;
            }
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
