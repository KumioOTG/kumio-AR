using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    // Reference to the ARSessionResetter script
    public ARSessionResetter arSessionResetter;

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Reset the AR session when the button is clicked
        arSessionResetter.ResetARSession();
    }
}
