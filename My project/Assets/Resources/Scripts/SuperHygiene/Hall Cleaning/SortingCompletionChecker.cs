using UnityEngine;
using UnityEngine.UI;

public class SortingCompletionChecker : MonoBehaviour
{
    public static SortingCompletionChecker Instance;

    public Button finishSortingButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckIfAllSorted()
    {
        int activeCount = 0;
        TrashSorter[] sorters = FindObjectsByType<TrashSorter>(FindObjectsSortMode.None);
        foreach (var sorter in sorters)
        {
            if (sorter.gameObject.activeInHierarchy)
                activeCount++;
        }

        Debug.Log($"🧮 Trash remaining: {activeCount}");

        if (activeCount == 0 && finishSortingButton != null)
        {
            finishSortingButton.gameObject.SetActive(true);
            Debug.Log("🎉 All trash sorted! Button enabled.");
        }
    }
}
