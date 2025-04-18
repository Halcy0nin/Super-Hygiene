using UnityEngine;
using ScratchCard;
using UnityEngine.UI;

public class SuperHygieneUIManager : MonoBehaviour
{

    public ScratchCardMaskUGUI dirtMask;
    public Button continueButton; 
    private void CheckBathProgress()
    {
        float progress = dirtMask.GetRevealProgress() * 100f;
        if (progress >= 93f && !continueButton.gameObject.activeSelf)
            {
                continueButton.gameObject.SetActive(true);
            }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckBathProgress();
    }
}
