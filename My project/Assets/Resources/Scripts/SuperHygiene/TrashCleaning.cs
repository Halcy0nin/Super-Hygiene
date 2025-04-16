using UnityEngine;

public class TrashCleaning : MonoBehaviour
{
    public enum TrashType { NonBiodegradable, Biodegradable, Recyclable }
    public TrashType type;

    private Vector3 originalPosition;
    private bool isDragging = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
    }
}

