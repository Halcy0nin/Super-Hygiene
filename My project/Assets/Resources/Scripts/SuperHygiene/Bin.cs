using UnityEngine;

public class Bin : MonoBehaviour
{
    public TrashCleaning.TrashType acceptsType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrashCleaning trash = collision.GetComponent<TrashCleaning>();
        if (trash != null)
        {
            if (trash.type == acceptsType)
            {
                Destroy(trash.gameObject); // Correct bin
            }
            else
            {
                trash.ReturnToOriginalPosition(); // Wrong bin
            }
        }
    }
}