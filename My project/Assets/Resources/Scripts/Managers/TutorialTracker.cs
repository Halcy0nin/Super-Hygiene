using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialTracker : MonoBehaviour
{
    public static int tutorialProgress = 0;

    public Button[] tutorialButtons;
    public GameObject finalTutorialUI;
    public Button nextSceneButton;
    public GameObject gameSelectUI;

    private bool finalUIShown = false;

    private void Start()
    {
        // Attach a separate listener to each button that disables itself
        foreach (Button button in tutorialButtons)
        {
            Button capturedButton = button; // Prevent closure issue
            capturedButton.onClick.AddListener(() =>
            {
                capturedButton.interactable = false; // Disable the button
                OnTutorialButtonPressed();
            });
        }

        finalTutorialUI.SetActive(false);
    }

    private void OnTutorialButtonPressed()
    {
        tutorialProgress++;
        Debug.Log("Tutorial Progress: " + tutorialProgress);
    }

    private void Update()
    {
        if (!finalUIShown && tutorialProgress >= 3 && gameSelectUI.activeSelf)
        {
            Debug.Log("Tutorial complete and GameSelectUI is active. Showing final tutorial UI.");
            ShowFinalTutorialUI();
            finalUIShown = true;
        }
    }

    private void ShowFinalTutorialUI()
    {
        finalTutorialUI.SetActive(true);
        nextSceneButton.onClick.AddListener(LoadNextScene);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Main"); // Replace with your target scene name
    }
}
