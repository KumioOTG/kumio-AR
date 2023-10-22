using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionResetter : MonoBehaviour
{
    private ARSession arSession;

    private void Awake()
    {
        arSession = FindObjectOfType<ARSession>();
    }

    public void ResetARSession()
    {
        if (arSession)
        {
            arSession.Reset();
        }
    }
}
