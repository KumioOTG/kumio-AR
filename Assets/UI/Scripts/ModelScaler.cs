using UnityEngine;
using System.Collections;

public enum RotationAxis
{
    X,
    Y,
    Z
}

public class ModelScaler : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleDuration = 1.0f;
    public int numberOfFullRotations = 5;
    public float rotationTime = 0.5f;
    public float waitTime = 3.0f;
    public Vector3 endingRotationAngles = Vector3.zero;
    public RotationAxis rotationAxis = RotationAxis.Y;
    public float clickRotationAngle = 350.0f;
    public bool rotateBackAfterClick = true;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Start()
    {
        if (gameObject.activeSelf)
        {
            transform.localScale = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }

    private IEnumerator ScaleModel()
    {
        float elapsedTime = 0;

        while (elapsedTime < scaleDuration)
        {
            float progress = elapsedTime / scaleDuration;

            // Scale model
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, progress);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame.
        }

        // Ensure final values are set
        transform.localScale = originalScale;
    }

    private IEnumerator RotateModel()
    {
        float elapsedTime = 0;

        while (elapsedTime < scaleDuration)
        {
            float rotationSpeed = 360f * numberOfFullRotations / scaleDuration;

            // Determine the appropriate rotation axis based on the enum value
            Vector3 currentRotationAxis = GetRotationAxisVector(rotationAxis);
            transform.Rotate(currentRotationAxis * rotationSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame.
        }

        // Set the ending rotation from the public variable
        Vector3 endingRotation = GetRotationAxisVector(rotationAxis) * endingRotationAngles.x;
        transform.rotation = Quaternion.Euler(endingRotation);
    }

    private IEnumerator RotateOnClick()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(GetRotationAxisVector(rotationAxis) * clickRotationAngle);

        float totalRotation = 0.0f;
        float rotationIncrement = clickRotationAngle / rotationTime;

        while (totalRotation < clickRotationAngle)
        {
            float currentRotation = Mathf.Min(rotationIncrement * Time.deltaTime, clickRotationAngle - totalRotation);
            transform.rotation *= Quaternion.Euler(GetRotationAxisVector(rotationAxis) * currentRotation);
            totalRotation += currentRotation;
            yield return null;
        }

        if (rotateBackAfterClick)
        {
            yield return new WaitForSeconds(waitTime);  // Wait before rotating back

            Quaternion startBackRotation = transform.rotation;
            Quaternion endBackRotation = startBackRotation * Quaternion.Euler(GetRotationAxisVector(rotationAxis) * -clickRotationAngle);

            totalRotation = 0.0f;
            while (totalRotation < clickRotationAngle)
            {
                float currentRotation = Mathf.Min(rotationIncrement * Time.deltaTime, clickRotationAngle - totalRotation);
                transform.rotation *= Quaternion.Euler(GetRotationAxisVector(rotationAxis) * -currentRotation);  // Notice the negative sign here
                totalRotation += currentRotation;
                yield return null;
            }
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(RotateOnClick());
    }

    private void OnEnable()
    {
        StartCoroutine(ScaleModel());
        StartCoroutine(RotateModel());
    }

    private Vector3 GetRotationAxisVector(RotationAxis axis)
    {
        switch (axis)
        {
            case RotationAxis.X:
                return Vector3.right;
            case RotationAxis.Y:
                return Vector3.up;
            case RotationAxis.Z:
                return Vector3.forward;
            default:
                return Vector3.up;
        }
    }
}
