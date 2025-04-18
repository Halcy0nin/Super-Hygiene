using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject phase1Group;
    public GameObject phase2Group;
    public GameObject sortingPanel;
    public GameObject trashUIPrefab;

    void Start()
    {
        BootTracer.Log("UIManager Start()");
        phase1Group.SetActive(true);
        phase2Group.SetActive(false);
    }

    public void ProceedToSorting()
    {
        phase1Group.SetActive(false);
        phase2Group.SetActive(true);

        foreach (var trash in GameManager.Instance.collectedTrash)
        {
            GameObject obj = Instantiate(trashUIPrefab, sortingPanel.transform);
            obj.GetComponent<Image>().sprite = trash.trashSprite;

            var sorter = obj.GetComponent<TrashSorter>();
            sorter.correctBinTag = trash.category.ToString();

            // ✅ Assign the tag here based on correctBinTag
            obj.tag = sorter.correctBinTag;

        }
    }

}
