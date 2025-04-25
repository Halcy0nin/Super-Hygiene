using UnityEngine;
using UnityEngine.UI;

public class ShirtAspectRatio : MonoBehaviour
{
    public Image imageComponent; // Reference to the Image component
    public float maxWidth = 300f; // Max width of the image
    public float maxHeight = 300f; // Max height of the image

    void Start()
    {
        if (imageComponent != null)
        {
            // Get the original aspect ratio of the image
            float originalWidth = imageComponent.sprite.texture.width;
            float originalHeight = imageComponent.sprite.texture.height;

            // Calculate the scale factor for the image to fit within the max dimensions
            float widthRatio = maxWidth / originalWidth;
            float heightRatio = maxHeight / originalHeight;

            // Use the smaller of the two ratios to ensure the image fits within the max dimensions
            float scaleFactor = Mathf.Min(widthRatio, heightRatio);

            // Set the scale based on the calculated ratio
            RectTransform rectTransform = imageComponent.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(originalWidth * scaleFactor, originalHeight * scaleFactor);
        }
    }
}
