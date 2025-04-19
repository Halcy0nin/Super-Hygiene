using UnityEngine;
using UnityEngine.UI;

public class ImageChangerWithSwipe : MonoBehaviour
{
    // Public properties to assign in the inspector
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;

    // Reference to the Image component of the prefab
    private Image imageComponent;

    // Variables to track touch/swipe position
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    // Threshold for detecting swipe (change as needed)
    public float swipeThreshold = 100f;

    // Flags to track if images 1-4 have been shown
    private bool image1Shown = false;
    private bool image2Shown = false;
    private bool image3Shown = false;
    private bool image4Shown = false;

    void Start()
    {
        // Get the Image component attached to this GameObject
        imageComponent = GetComponent<Image>();

        // Only set the sprite to Image1 if the Image component is using the default sprite
        if (imageComponent.sprite == null)
        {
            imageComponent.sprite = image1;  // Set Image 1 as default if no sprite is assigned in the Inspector
        }
    }

    void Update()
    {
        // Detect tap
        if (Input.GetMouseButtonDown(0)) // Left-click or tap on screen
        {
            touchStartPos = Input.mousePosition; // Store starting touch position
            HandleTap(); // Handle tap
        }

        // Detect swipe or drag
        if (Input.GetMouseButtonUp(0)) // When mouse button is released
        {
            touchEndPos = Input.mousePosition; // Store ending touch position

            // Calculate swipe distance
            float swipeDistance = touchEndPos.x - touchStartPos.x;
            float swipeVerticalDistance = touchEndPos.y - touchStartPos.y;

            // Check for swipe
            if (Mathf.Abs(swipeDistance) > swipeThreshold)
            {
                if (swipeDistance > 0 && image1Shown) // Right swipe and Image 1 has been shown
                {
                    ShowImage2(); // Show Image 2 on right swipe
                }
                else if (swipeDistance < 0 && image2Shown) // Left swipe and Image 2 has been shown
                {
                    ShowImage3(); // Show Image 3 on left swipe
                }
            }
            else if (Mathf.Abs(swipeVerticalDistance) > swipeThreshold) // Vertical swipe
            {
                if (swipeVerticalDistance > 0 && image3Shown) // Up swipe and Image 3 has been shown
                {
                    ShowImage4(); // Show Image 4 on up swipe
                }
            }
        }
    }

    // Handle tap logic
    void HandleTap()
    {
        if (!image1Shown) // If Image 1 hasn't been shown yet
        {
            imageComponent.sprite = image1;
            image1Shown = true;
        }
        else if (image1Shown && image2Shown && image3Shown && image4Shown) // If all 4 steps are done
        {
            imageComponent.sprite = image5;
        }
    }

    // Show Image 2 on right swipe
    void ShowImage2()
    {
        if (image1Shown && !image2Shown) // Only show Image 2 if Image 1 has been shown
        {
            imageComponent.sprite = image2;
            image2Shown = true;
        }
    }

    // Show Image 3 on left swipe
    void ShowImage3()
    {
        if (image2Shown && !image3Shown) // Only show Image 3 if Image 2 has been shown
        {
            imageComponent.sprite = image3;
            image3Shown = true;
        }
    }

    // Show Image 4 on up swipe
    void ShowImage4()
    {
        if (image3Shown && !image4Shown) // Only show Image 4 if Image 3 has been shown
        {
            imageComponent.sprite = image4;
            image4Shown = true;
        }
    }
}
