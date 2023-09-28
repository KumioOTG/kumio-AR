using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceDrag : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Transform linked3DModel;  // Drag your 3D model here in the editor
    public Camera camera3D;  // Drag your 3D camera here

    private Vector2 lastScreenPosition;  // Last known position of the puzzle piece in screen space

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastScreenPosition = eventData.position;  // Store initial position
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 screenDelta = eventData.position - lastScreenPosition;  // Calculate movement delta in screen space
        Vector3 worldDelta = ScreenDeltaToWorldDelta(screenDelta);  // Convert to world space
        linked3DModel.position += worldDelta;  // Apply the delta to the 3D model
        lastScreenPosition = eventData.position;  // Update last known position
    }

    private Vector3 ScreenDeltaToWorldDelta(Vector2 screenDelta)
    {
        // Calculate depth based on distance from the 3D camera to the linked 3D model
        float depth = Vector3.Distance(camera3D.transform.position, linked3DModel.position);

        // Convert both the previous and current screen positions to world space
        Vector3 previousWorldPos = camera3D.ScreenToWorldPoint(new Vector3(lastScreenPosition.x, lastScreenPosition.y, depth));
        Vector3 currentWorldPos = camera3D.ScreenToWorldPoint(new Vector3(lastScreenPosition.x + screenDelta.x, lastScreenPosition.y + screenDelta.y, depth));

        // Return the difference between these positions
        return currentWorldPos - previousWorldPos;
    }
}
