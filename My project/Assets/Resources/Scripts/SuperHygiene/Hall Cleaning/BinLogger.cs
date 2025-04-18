using UnityEngine;
using UnityEngine.EventSystems;

public class BinLogger : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer entered bin: {gameObject.name} | Tag: {gameObject.tag}");
    }
    void OnDrawGizmos()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(rt.position, rt.rect.size);
    }
}