using UnityEngine;

public class QuizController : MonoBehaviour
{
    public CanvasGroup[] quizCanvases; // Drag all your quiz canvases here.

    // Function to activate a specific quiz canvas
    public void ActivateQuiz(int quizIndex)
    {
        if (quizIndex >= 0 && quizIndex < quizCanvases.Length)
        {
            // Ensure all other quizzes are deactivated
            foreach (CanvasGroup canvas in quizCanvases)
            {
                canvas.gameObject.SetActive(false);
            }

            // Activate the selected quiz
            quizCanvases[quizIndex].gameObject.SetActive(true);
            quizCanvases[quizIndex].alpha = 0; // Set initial transparency

            // If your quizzes also have the fade-in effect, you can call the fadeIn function from the QuizButtonManager of the activated quiz here
            // For example: quizCanvases[quizIndex].GetComponent<QuizButtonManager>().StartQuiz();
        }
    }
}
