using UnityEngine;

public class KeepOnTop : MonoBehaviour
{
    void LateUpdate()
    {
        transform.SetAsLastSibling(); // Keeps this object rendered on top
    }
}
