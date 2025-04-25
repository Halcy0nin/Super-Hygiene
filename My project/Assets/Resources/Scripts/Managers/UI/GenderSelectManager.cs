using TMPro;
using UnityEngine;

public class GenderSelectManager : MonoBehaviour
{
    [Header("Assign your TMP InputField and TMP Text")]
    public TMP_InputField inputField;
    public TMP_Text outputText;

    void Start()
    {
        if (inputField != null && outputText != null)
        {
            // Listen for value changes in the input field
            inputField.onValueChanged.AddListener(UpdateText);
        }
    }

    void UpdateText(string newText)
    {
        outputText.text = newText;
    }

    private void OnDestroy()
    {
        // Clean up listener to avoid memory leaks
        if (inputField != null)
            inputField.onValueChanged.RemoveListener(UpdateText);
    }
}
