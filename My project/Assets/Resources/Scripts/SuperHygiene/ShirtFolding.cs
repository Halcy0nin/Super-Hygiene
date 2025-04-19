using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShirtFolding : MonoBehaviour
{
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;

    private Image imageComponent;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    public float swipeThreshold = 100f;

    private bool image1Shown = false;
    private bool image2Shown = false;
    private bool image3Shown = false;
    private bool image4Shown = false;
    private bool image5Shown = false;

    // 🔄 Callback to notify when all steps are complete
    public UnityAction onStepsComplete;

    void Start()
    {
        imageComponent = GetComponent<Image>();

        // Only set Image1 if nothing is assigned (respect default Inspector sprite)
        if (imageComponent.sprite == null)
        {
            imageComponent.sprite = image1;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            HandleTap();
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;

            float swipeDistance = touchEndPos.x - touchStartPos.x;
            float swipeVerticalDistance = touchEndPos.y - touchStartPos.y;

            if (Mathf.Abs(swipeDistance) > swipeThreshold)
            {
                if (swipeDistance > 0 && image1Shown)
                {
                    ShowImage2();
                }
                else if (swipeDistance < 0 && image2Shown)
                {
                    ShowImage3();
                }
            }
            else if (Mathf.Abs(swipeVerticalDistance) > swipeThreshold)
            {
                if (swipeVerticalDistance > 0 && image3Shown)
                {
                    ShowImage4();
                }
            }
        }
    }

    void HandleTap()
    {
        if (!image1Shown)
        {
            imageComponent.sprite = image1;
            image1Shown = true;
        }
        else if (image1Shown && image2Shown && image3Shown && image4Shown && !image5Shown)
        {
            imageComponent.sprite = image5;
            image5Shown = true;

            // ✅ Notify the spawner that this shirt is finished
            onStepsComplete?.Invoke();
        }
    }

    void ShowImage2()
    {
        if (image1Shown && !image2Shown)
        {
            imageComponent.sprite = image2;
            image2Shown = true;
        }
    }

    void ShowImage3()
    {
        if (image2Shown && !image3Shown)
        {
            imageComponent.sprite = image3;
            image3Shown = true;
        }
    }

    void ShowImage4()
    {
        if (image3Shown && !image4Shown)
        {
            imageComponent.sprite = image4;
            image4Shown = true;
        }
    }

    // 🔄 Optional reset method (if reusing the same shirt object)
    public void ResetSteps()
    {
        image1Shown = false;
        image2Shown = false;
        image3Shown = false;
        image4Shown = false;
        image5Shown = false;

        imageComponent.sprite = null; // or set to a default
    }

    public void MoveShirtAside()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(-250, 0); // Move to the right side (adjust value as needed)
    }

    public void MoveShirtToStack(Transform stackParent, float offsetY)
    {
        transform.SetParent(stackParent); // Move shirt under stack area
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);
        rt.anchoredPosition = new Vector2(0f, offsetY);
    }

}
