using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1.0f;
    public float rotationSpeed = 180f; // Speed for rotation in degrees per second
    private int currentWaypoint = 0;
    private bool isReturning = false;
    private bool isRotating = false;

    void Update()
    {
        if (waypoints.Length == 0 || isRotating) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, step);

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            if (!isReturning)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    isReturning = true;
                    currentWaypoint--;
                    StartCoroutine(RotateObject());
                }
            }
            else
            {
                currentWaypoint--;
                if (currentWaypoint < 0)
                {
                    isReturning = false;
                    currentWaypoint = 0;
                    StartCoroutine(RotateObject());
                }
            }
        }
    }

    private IEnumerator RotateObject()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180, 0); // Rotate 180 degrees on Y axis
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * (rotationSpeed / 180);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}