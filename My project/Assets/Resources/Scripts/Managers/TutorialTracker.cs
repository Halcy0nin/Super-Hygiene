using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialTracker : MonoBehaviour
{
    // Tutorial progress tracking
    public static int tutorialProgress = 0;

    // Array of tutorial buttons
    public Button[] tutorialButtons;

    // Final tutorial UI and button to move to the next scene
    public GameObject finalTutorialUI;
    public Button nextSceneButton;

    // Reference to the GameSelectUI
    public GameObject gameSelectUI;

    // Internal flag to prevent multiple UI triggers
    private bool finalUIShown = false;

    private void Start()
    {
        // Add listeners to all tutorial buttons
        foreach (Button button in tutorialButtons)
        {
            button.onClick.AddListener(OnTutorialButtonPressed);
        }

        // Ensure the final UI is hidden at the start
        finalTutorialUI.SetActive(false);
    }

    private void OnTutorialButtonPressed()
    {
        // Increment tutorial progress
        tutorialProgress++;
        Debug.Log("Tutorial Progress: " + tutorialProgress);
    }

    private void Update()
    {
        // Continuously check if conditions are met to show final tutorial UI
        if (!finalUIShown && tutorialProgress >= 3 && gameSelectUI.activeSelf)
        {
            Debug.Log("Tutorial complete and GameSelectUI is active. Showing final tutorial UI.");
            ShowFinalTutorialUI();
            finalUIShown = true;
        }
    }

    private void ShowFinalTutorialUI()
    {
        // Display final tutorial screen
        finalTutorialUI.SetActive(true);

        // Assign scene load to next button
        nextSceneButton.onClick.AddListener(LoadNextScene);
    }

    private void LoadNextScene()
    {
        // Load your next scene here
        SceneManager.LoadScene("Main"); // Replace "Main" with your actual scene name
    }
}
