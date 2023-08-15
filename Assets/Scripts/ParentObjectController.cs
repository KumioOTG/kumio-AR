using UnityEngine;
using System.Collections;

public class ParentObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] collectableObjects;
    [SerializeField]
    private GameObject nextParent;
    [SerializeField]
    private float delayInSeconds = 0.0f;

    private int totalCollectableObjects;
    private int collectedObjectsCount;

    private void Start()
    {
        totalCollectableObjects = collectableObjects.Length;
        collectedObjectsCount = 0;
    }

    public void CollectableObjectCollected()
    {
        collectedObjectsCount++;

        if (collectedObjectsCount >= totalCollectableObjects)
        {
            if (delayInSeconds > 0.0f)
            {
                StartCoroutine(ActivateNextParentWithDelay());
            }
            else
            {
                ActivateNextParent();
            }
        }
    }

    private IEnumerator ActivateNextParentWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        ActivateNextParent();

        Destroy(gameObject);
    }

    public void ActivateNextParent()
    {
        if (nextParent != null)
        {
            nextParent.SetActive(true);
        }
    }
}
