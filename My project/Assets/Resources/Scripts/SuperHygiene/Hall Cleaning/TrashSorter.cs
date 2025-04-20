using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class TrashSorter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string correctBinTag;
    private Vector3 startPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(transform.root); // Bring to front

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        Debug.Log($"🟡 Begin drag: {gameObject.name} | Expected bin tag: {correctBinTag}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        GameObject hitBin = null;

        foreach (var result in results)
        {
            if (result.gameObject == gameObject) continue; // Skip self

            Debug.Log($"🔎 Raycast hit: {result.gameObject.name} | Tag: {result.gameObject.tag}");

            if (result.gameObject.CompareTag(correctBinTag) && result.gameObject.GetComponent<TrashSorter>() == null)
            {
                hitBin = result.gameObject;
                break;
            }
        }

        if (hitBin != null)
        {
            Debug.Log($"✅ Correct bin! {gameObject.name} dropped on {hitBin.name} (Tag: {hitBin.tag})");
            gameObject.SetActive(false);
            SortingCompletionChecker.Instance.CheckIfAllSorted();
        }
        else
        {
            Debug.Log($"❌ Wrong bin! {gameObject.name} expected {correctBinTag}");
            transform.position = startPosition;
        }

        transform.SetParent(originalParent);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
