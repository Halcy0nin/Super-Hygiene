using UnityEngine;
using UnityEngine.UI;

public class SortingInitializer : MonoBehaviour
{
    public GameObject trashUIPrefab;
    public Transform collectionPanel;
    public GameManager gameManager;

    void Start()
    {
        BootTracer.Log("SortingInitializer Start()");
        foreach (var trash in gameManager.collectedTrash)
        {
            GameObject trashUI = Instantiate(trashUIPrefab, collectionPanel);
            trashUI.GetComponent<Image>().sprite = trash.trashSprite;

            var sorter = trashUI.GetComponent<TrashSorter>();
            sorter.correctBinTag = trash.category.ToString();
        }
    }
}
