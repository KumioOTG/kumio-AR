using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private List<SnapObjectByTags> controlPoints;
    [SerializeField] private GameObject finalImage;
    [SerializeField] private GameObject anlati2;
    [SerializeField] private GameObject sound;
    [SerializeField] private bool isCompleted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool completionCheck = true;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            bool snapped = controlPoints[i].Snapped;
            completionCheck &= snapped;
            if (snapped)
            {
                controlPoints[i].GetObjectToSnap().gameObject.GetComponent<Collider>().enabled = false;
            }
        }

        if (completionCheck && !isCompleted)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.gameObject.name != "BackPlate")
                {
                    child.gameObject.SetActive(false);
                }
            }
            finalImage.SetActive(true);
            coin.SetActive(true);
            anlati2.SetActive(true);
            sound.SetActive(true);
            isCompleted = true;
        }
    }
}
