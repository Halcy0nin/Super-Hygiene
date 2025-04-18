using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashSorter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string correctBinTag;
    private Vector3 startPosition;
    private Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(transform.root); // Bring it to the front

        Debug.Log($"🟡 Begin drag: {gameObject.name} | Expected bin tag: {correctBinTag}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject hitObj = eventData.pointerEnter;
        Transform target = hitObj?.transform;

        Debug.Log($"🟡 Initial pointerEnter: {hitObj?.name} | Tag: {hitObj?.tag}");

        // Traverse up the hierarchy until we find a tag that matches the correctBinTag
        while (target != null && !target.CompareTag(correctBinTag))
        {
            Debug.Log($"🔍 Checking parent: {target.name} | Tag: {target.tag}");
            target = target.parent;
        }

        if (target != null && target.CompareTag(correctBinTag))
        {
            Debug.Log($"✅ Correct bin! {gameObject.name} dropped on {target.name} (Tag: {target.tag})");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"❌ Wrong bin! {gameObject.name} expected {correctBinTag}, but got {hitObj?.tag ?? "null"}");
            transform.position = startPosition;
        }

        transform.SetParent(originalParent);
    }



}
