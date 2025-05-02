using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ShirtFoldingTutorial : MonoBehaviour
{
    [Header("Shirt Sprites")]
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;

    private Image imageComponent;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    [Header("Swipe Settings")]
    public float swipeThreshold = 100f;

    private bool image1Shown = false;
    private bool image2Shown = false;
    private bool image3Shown = false;
    private bool image4Shown = false;
    private bool image5Shown = false;
    private bool isComplete = false;

    public UnityAction onStepsComplete;

    private ShirtControls controls;

    [Header("Swipe Arrow UI")]
    public Image arrowImage;
    public Sprite arrowRight;
    public Sprite arrowLeft;
    public Sprite arrowUp;
    public Sprite tapStartIcon;
    public Sprite tapFinishIcon;

    private void Awake()
    {
        controls = new ShirtControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Tap.started += OnTapStarted;
        controls.Player.Tap.canceled += OnTapCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Tap.started -= OnTapStarted;
        controls.Player.Tap.canceled -= OnTapCanceled;
        controls.Disable();
    }

    private void Start()
    {
        imageComponent = GetComponent<Image>();

        // Show the default shirt image (from prefab)
        image1Shown = false; // Wait for first tap

        ShowNextArrow(); // Should display tap icon
    }

    private void OnTapStarted(InputAction.CallbackContext ctx)
    {
        touchStartPos = controls.Player.Swipe.ReadValue<Vector2>();
    }

    private void OnTapCanceled(InputAction.CallbackContext ctx)
    {
        touchEndPos = controls.Player.Swipe.ReadValue<Vector2>();

        float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

        if (swipeDistance < swipeThreshold)
        {
            HandleTap();
        }
        else
        {
            DetectSwipe();
        }
    }

    private void HandleTap()
    {
        if (!image1Shown)
        {
            imageComponent.sprite = image1;
            image1Shown = true;
            ShowNextArrow();
            return;
        }

        if (image4Shown && !image5Shown)
        {
            imageComponent.sprite = image5;
            image5Shown = true;
            isComplete = true;
            HideArrow();
            onStepsComplete?.Invoke();
        }
    }

    private void DetectSwipe()
    {
        if (isComplete) return;

        Vector2 swipeVector = touchEndPos - touchStartPos;

        if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
        {
            if (Mathf.Abs(swipeVector.x) > swipeThreshold)
            {
                if (swipeVector.x > 0 && image1Shown && !image2Shown)
                {
                    ShowImage2();
                }
                else if (swipeVector.x < 0 && image2Shown && !image3Shown)
                {
                    ShowImage3();
                }
            }
        }
        else
        {
            if (Mathf.Abs(swipeVector.y) > swipeThreshold)
            {
                if (swipeVector.y > 0 && image3Shown && !image4Shown)
                {
                    ShowImage4();
                }
            }
        }
    }

    private void ShowImage2()
    {
        imageComponent.sprite = image2;
        image2Shown = true;
        ShowNextArrow();
    }

    private void ShowImage3()
    {
        imageComponent.sprite = image3;
        image3Shown = true;
        ShowNextArrow();
    }

    private void ShowImage4()
    {
        imageComponent.sprite = image4;
        image4Shown = true;
        ShowNextArrow();
    }

    private void ShowNextArrow()
    {
        if (arrowImage == null) return;

        if (!image1Shown)
        {
            arrowImage.sprite = tapStartIcon;
            arrowImage.enabled = true;
        }
        else if (image1Shown && !image2Shown)
        {
            arrowImage.sprite = arrowRight;
            arrowImage.enabled = true;
        }
        else if (image2Shown && !image3Shown)
        {
            arrowImage.sprite = arrowLeft;
            arrowImage.enabled = true;
        }
        else if (image3Shown && !image4Shown)
        {
            arrowImage.sprite = arrowUp;
            arrowImage.enabled = true;
        }
        else if (image4Shown && !image5Shown)
        {
            arrowImage.sprite = tapFinishIcon;
            arrowImage.enabled = true;
        }
        else
        {
            arrowImage.enabled = false;
        }
    }

    private void HideArrow()
    {
        if (arrowImage != null)
        {
            arrowImage.enabled = false;
        }
    }

    public void ResetSteps()
    {
        image1Shown = false;
        image2Shown = false;
        image3Shown = false;
        image4Shown = false;
        image5Shown = false;
        isComplete = false;

        if (imageComponent != null)
        {
            // Reset to the prefab’s original image
            imageComponent.sprite = null;
        }

        ShowNextArrow();
    }

    public void MoveShirtAside()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(-250f, 0f);
    }

    public void MoveShirtToStack(Transform stackParent, float offsetY)
    {
        transform.SetParent(stackParent, worldPositionStays: false);

        RectTransform rt = GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);
        rt.anchoredPosition = new Vector2(0f, offsetY);
    }
}
