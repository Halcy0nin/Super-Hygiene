using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NameValidator : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject nameError;

    public void TryLoadNextScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(nameInputField.text))
        {
            nameError.SetActive(true); // show error message
            return; // ⛔ prevent scene change
        }

        nameError.SetActive(false); // hide error if previously shown
        LevelManager.Instance.LoadSceneFromButton(sceneName);
    }
}
