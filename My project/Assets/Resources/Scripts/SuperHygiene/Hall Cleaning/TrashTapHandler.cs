using UnityEngine;
using UnityEngine.EventSystems;

public class TrashTapHandler : MonoBehaviour, IPointerClickHandler
{
    public TrashManager manager;
    public TrashDataHolder dataHolder;
    public GameManager gameManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dataHolder != null && dataHolder.trashData != null)
        {
            gameManager.collectedTrash.Add(dataHolder.trashData);
            gameObject.SetActive(false);
            manager?.TrashPicked();
        }
    }


}
