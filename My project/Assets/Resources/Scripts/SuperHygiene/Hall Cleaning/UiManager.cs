using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject phase1Group;
    public GameObject phase2Group;
    public GameObject sortingPanel;
    public GameObject trashUIPrefab;
    public Transform trashContainer;

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
            GameObject obj = Instantiate(trashUIPrefab, trashContainer); // or appropriate parent
            obj.transform.localScale = Vector3.one; // 🔥 Important
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 
            obj.GetComponent<Image>().sprite = trash.trashSprite;

            var sorter = obj.GetComponent<TrashSorter>();
            sorter.enabled = true;
            obj.GetComponent<TrashTapHandler>().enabled = false;
            sorter.correctBinTag = trash.category.ToString();

            // ✅ Assign the tag here based on correctBinTag
            obj.tag = sorter.correctBinTag;

        }
    }

}
