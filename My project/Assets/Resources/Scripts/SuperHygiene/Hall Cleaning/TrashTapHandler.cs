using UnityEngine;
using UnityEngine.EventSystems;

public class TrashTapHandler : MonoBehaviour, IPointerClickHandler
{
    public TrashManager manager;
    public TrashDataHolder dataHolder;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dataHolder != null && dataHolder.trashData != null)
        {
            GameManager.Instance.collectedTrash.Add(dataHolder.trashData);
            gameObject.SetActive(false);
            manager?.TrashPicked();
        }
    }


}
