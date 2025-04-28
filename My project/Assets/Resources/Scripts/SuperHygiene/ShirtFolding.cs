using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ShirtFolding : MonoBehaviour
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

    public void OnDisable()
    {
        controls.Player.Tap.started -= OnTapStarted;
        controls.Player.Tap.canceled -= OnTapCanceled;

        controls.Disable();
    }

    private void Start()
    {
        imageComponent = GetComponent<Image>();

        if (imageComponent.sprite == null)
        {
            imageComponent.sprite = image1;
        }
    }

    private void OnTapStarted(InputAction.CallbackContext ctx)
    {
        touchStartPos = controls.Player.Swipe.ReadValue<Vector2>();
        Debug.Log("[TapStarted] Touch start at: " + touchStartPos);
    }

    private void OnTapCanceled(InputAction.CallbackContext ctx)
    {
        touchEndPos = controls.Player.Swipe.ReadValue<Vector2>();
        Debug.Log("[TapCanceled] Touch end at: " + touchEndPos);

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
        Debug.Log("[Tap] Simple tap detected");

        if (!image1Shown)
        {
            imageComponent.sprite = image1;
            image1Shown = true;
        }
        else if (image1Shown && image2Shown && image3Shown && image4Shown && !image5Shown)
        {
            imageComponent.sprite = image5;
            image5Shown = true;
            isComplete = true;
            if (onStepsComplete != null)
                onStepsComplete.Invoke(); // Safe call
        }
    }

    private void DetectSwipe()
    {
        if (isComplete) return;

        Vector2 swipeVector = touchEndPos - touchStartPos;
        Debug.Log($"[Swipe] Delta: {swipeVector}");

        if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
        {
            // Horizontal swipe
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
            // Vertical swipe
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
        Debug.Log("[Image2] Shown");
    }

    private void ShowImage3()
    {
        imageComponent.sprite = image3;
        image3Shown = true;
        Debug.Log("[Image3] Shown");
    }

    private void ShowImage4()
    {
        imageComponent.sprite = image4;
        image4Shown = true;
        Debug.Log("[Image4] Shown");
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
            imageComponent.sprite = null;
        }
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
