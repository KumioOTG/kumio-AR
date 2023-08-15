using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;

public class SwitchSceneButton : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName = "NextSceneName";

    private void Start()
    {
        var button = GetComponent<Interactable>();
        button.OnClick.AddListener(() => SwitchScene());
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
