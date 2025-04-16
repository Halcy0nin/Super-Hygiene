using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Cleaning : MonoBehaviour
{
    public RectTransform parentContainer;
    public List<Sprite> buttonSprites;

    [System.Serializable]
    public class PositionArea
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
    }

    [System.Serializable]
    public class SpawnRegion
    {
        public string name;
        public PositionArea area;
        public int spawnCount;
    }

    public List<SpawnRegion> spawnRegions = new List<SpawnRegion>();

    private List<GameObject> spawnedButtons = new List<GameObject>();

    void Start()
    {
        SpawnButtons();
    }

    public void SpawnButtons()
    {
        ClearExistingButtons();

        foreach (var region in spawnRegions)
        {
            for (int i = 0; i < region.spawnCount; i++)
            {
                GameObject newButton = CreateButton();
                RectTransform rt = newButton.GetComponent<RectTransform>();

                float x = Random.Range(region.area.minX, region.area.maxX);
                float y = Random.Range(region.area.minY, region.area.maxY);
                rt.anchoredPosition = new Vector2(x, y);

                newButton.SetActive(true);
                AddClickToHide(newButton);
                spawnedButtons.Add(newButton);
            }
        }
    }

    private GameObject CreateButton()
    {
        GameObject button = new GameObject("Button", typeof(RectTransform), typeof(Button), typeof(Image));
        button.transform.SetParent(parentContainer, false);

        RectTransform rt = button.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(118, 110);

        Image img = button.GetComponent<Image>();
        if (buttonSprites != null && buttonSprites.Count > 0)
        {
            img.sprite = buttonSprites[Random.Range(0, buttonSprites.Count)];
        }

        return button;
    }

    private void AddClickToHide(GameObject btn)
    {
        btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            btn.SetActive(false);
        });
    }

    private void ClearExistingButtons()
    {
        foreach (var btn in spawnedButtons)
        {
            Destroy(btn);
        }
        spawnedButtons.Clear();
    }
}
