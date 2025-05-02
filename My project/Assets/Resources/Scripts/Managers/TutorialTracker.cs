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

    private void Start()
    {
        // Add listeners to all buttons in the array
        foreach (Button button in tutorialButtons)
        {
            button.onClick.AddListener(OnTutorialButtonPressed);
        }

        // Hide final tutorial UI initially
        finalTutorialUI.SetActive(false);
    }

    private void OnTutorialButtonPressed()
    {
        // Increment tutorial progress when a button is pressed
        tutorialProgress++;

        // Debugging progress
        Debug.Log("Tutorial Progress: " + tutorialProgress);

        // Check if progress reaches 3 and gameSelectUI is active
        if (tutorialProgress >= 3)
        {
            Debug.Log("Tutorial Progress Reached 3!");

            if (gameSelectUI.activeSelf)
            {
                Debug.Log("GameSelectUI is active. Showing final tutorial UI.");
                ShowFinalTutorialUI();
            }
            else
            {
                Debug.Log("GameSelectUI is not active. Wait for it to be active.");
            }
        }
    }

    private void ShowFinalTutorialUI()
    {
        // Show the final tutorial UI and the button to proceed to the next scene
        finalTutorialUI.SetActive(true);

        // Add listener to the next scene button
        nextSceneButton.onClick.AddListener(LoadNextScene);
    }

    private void LoadNextScene()
    {
        // Load the next scene (replace with your scene loading logic)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
