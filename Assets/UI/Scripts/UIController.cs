using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    #region Private Variables

    // A flag to indicate if the second rectangle should move out
    private bool shouldMoveSecondRectangleOut = true;

    // Variables to store the current and initial states of various UI elements
    private Vector2 buttonCurrentTargetPosition;
    private bool isButtonRotated = false;
    private Quaternion initialButtonRotation;
    private Vector2 initialRectanglePosition;
    private Vector2 initialSecondRectanglePosition;
    private Vector2 initialButtonPosition;
    private Vector2 startButtonPosition;

    // Array of second-level rectangles
    private RectTransform[] secondRectangles;

    // Currently opened second-level rectangle
    private RectTransform currentOpenRectangle;

    private float rectangleMovePixels = 100f;
    private float secondRectangleMovePixels = 100f;

    #endregion

    #region Public Variables

    // Configuration variables to determine how far the rectangles should move
    //[SerializeField] private float rectangleMovePixels = 100f;
    //[SerializeField] private float secondRectangleMovePixels = 200f;

    // References to UI elements
    public RectTransform button;

    public RectTransform rectangleContainer;
    public RectTransform panelInsideRectangleContainer;

    public RectTransform secondRectangleContainer;
    public RectTransform panelInsideSecondRectangleContainer;

    public RectTransform secondRectangleContainer2;
    public RectTransform secondRectangleContainer3;

    public RectTransform secondRectangleContainer4;

    #endregion

    #region Constants

    // Animation durations
    private const float slideInDuration = 1.0f;
    private const float slideOutDuration = 0.5f;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        // Store initial positions and rotations
        startButtonPosition = button.anchoredPosition;
        initialButtonPosition = button.anchoredPosition;
        initialButtonRotation = button.rotation;
        initialRectanglePosition = rectangleContainer.anchoredPosition;
        initialSecondRectanglePosition = secondRectangleContainer.anchoredPosition;

        // Set rectangleMovePixels to the height of the panel inside rectangleContainer
        rectangleMovePixels = panelInsideRectangleContainer.rect.height;

        // Set secondRectangleMovePixels to the sum of the heights of the panels inside rectangleContainer and secondRectangleContainer
        secondRectangleMovePixels = panelInsideRectangleContainer.rect.height + panelInsideSecondRectangleContainer.rect.height;

        // Initialize array of second-level rectangles
        secondRectangles = new RectTransform[] { secondRectangleContainer, secondRectangleContainer2, secondRectangleContainer3, secondRectangleContainer4 };
    }

    #endregion

    #region Public Methods

    public void OnButtonClick()
    {
        // Handle button click based on whether the button is already rotated
        if (!isButtonRotated)
        {
            RotateButton();
            MoveRectangleIn();
        }
        else
        {
            MoveAllBack();
            RotateButtonBack();
        }
        if (shouldMoveSecondRectangleOut && currentOpenRectangle != null)
        {
            MoveSecondRectangleOut(currentOpenRectangle);
            currentOpenRectangle = null;
        }
    }

    public void OnImageClick(int imageIndex)
    {
        // Handle image click, open or close second-level rectangles
        if (currentOpenRectangle != null)
        {
            if (currentOpenRectangle == secondRectangles[imageIndex])
            {
                // If the clicked rectangle is already open, close it.
                MoveSecondRectangleOut(currentOpenRectangle);
                currentOpenRectangle = null;
                return;
            }
            else
            {
                // If another rectangle is open, close it.
                MoveSecondRectangleOut(currentOpenRectangle, true);
            }
        }

        if (imageIndex >= 0 && imageIndex < secondRectangles.Length)
        {
            currentOpenRectangle = secondRectangles[imageIndex];
            MoveSecondRectangleIn(currentOpenRectangle);
        }
    }


    #endregion

    #region Private Methods

    private void RotateButton()
    {
        // Rotate the button by 180 degrees
        button.rotation *= Quaternion.Euler(0, 0, -180f);
        isButtonRotated = true;
    }

    private void RotateButtonBack()
    {
        // Reset the button rotation to its initial state
        button.rotation = initialButtonRotation;
        isButtonRotated = false;
    }

    private void MoveRectangleIn()
    {
        // Move the main rectangle in
        Vector2 targetPosition = new Vector2(initialRectanglePosition.x, initialRectanglePosition.y - rectangleMovePixels);
        Vector2 buttonTargetPosition = new Vector2(initialButtonPosition.x, initialButtonPosition.y - rectangleMovePixels);
        StartCoroutine(MoveToPosition(rectangleContainer, targetPosition, button, buttonTargetPosition, slideInDuration));
    }

    private void MoveAllBack()
    {
        // Move all rectangles back to their initial positions
        shouldMoveSecondRectangleOut = false;
        Vector2 buttonTargetPosition = startButtonPosition;
        StartCoroutine(MoveButtonToStartPosition(slideOutDuration));
        StartCoroutine(MoveToPosition(rectangleContainer, initialRectanglePosition, null, Vector2.zero, slideOutDuration));
        foreach (RectTransform rect in secondRectangles)
        {
            StartCoroutine(MoveToPosition(rect, initialSecondRectanglePosition, null, Vector2.zero, slideOutDuration));
        }
    }

    private IEnumerator MoveButtonToStartPosition(float duration)
    {
        // Coroutine to animate the button moving to its start position
        Vector2 startPosition = button.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            button.anchoredPosition = Vector2.Lerp(startPosition, startButtonPosition, t);
            yield return null;
        }

        button.anchoredPosition = startButtonPosition;
    }

    private IEnumerator MoveButtonToPosition(RectTransform buttonRect, Vector2 targetPosition, float duration)
    {
        // Coroutine to animate the button moving to a target position
        Vector2 startPosition = buttonRect.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            buttonRect.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        buttonRect.anchoredPosition = targetPosition;
    }

    private void MoveSecondRectangleIn(RectTransform targetRectangle)
    {
        // Move a second-level rectangle in
        targetRectangle.gameObject.SetActive(true);
        Vector2 targetPosition = new Vector2(initialSecondRectanglePosition.x, initialSecondRectanglePosition.y - secondRectangleMovePixels);
        Vector2 buttonTargetPosition = new Vector2(initialButtonPosition.x, initialButtonPosition.y - rectangleMovePixels + (rectangleMovePixels - secondRectangleMovePixels));
        StartCoroutine(MoveToPosition(targetRectangle, targetPosition, button, buttonTargetPosition, slideInDuration));
    }

    private void MoveSecondRectangleOut(RectTransform targetRectangle, bool samePosition = false)
    {
        // Move a second-level rectangle out
        Vector2 targetPosition = initialSecondRectanglePosition;
        Vector2 buttonTargetPosition = new Vector2(initialButtonPosition.x, initialButtonPosition.y - rectangleMovePixels - (rectangleMovePixels - secondRectangleMovePixels));
        if (!samePosition)
        {
            buttonTargetPosition += new Vector2(0, (rectangleMovePixels - secondRectangleMovePixels));
        }
        StartCoroutine(MoveToPosition(targetRectangle, targetPosition, button, buttonTargetPosition, slideOutDuration));
    }

    private IEnumerator MoveToPosition(RectTransform rectTransform, Vector2 targetPosition, RectTransform buttonRect, Vector2 buttonTargetPosition, float duration)
    {
        // Coroutine to animate the moving of rectangles and the button
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 buttonStartPosition = buttonRect != null ? buttonRect.anchoredPosition : Vector2.zero;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            if (buttonRect != null)
            {
                buttonRect.anchoredPosition = Vector2.Lerp(buttonStartPosition, buttonTargetPosition, t);
            }

            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;

        if (buttonRect != null)
        {
            buttonRect.anchoredPosition = buttonTargetPosition;
        }
    }

    #endregion
}
