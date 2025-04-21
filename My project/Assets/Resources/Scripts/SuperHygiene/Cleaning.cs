using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Cleaning : MonoBehaviour
{
    // The container (e.g., a Canvas panel) where buttons will be spawned
    public RectTransform parentContainer;

    // List of sprites to randomly assign to each button
    public List<Sprite> buttonSprites;

    // Represents a rectangular area using min/max X and Y coordinates
    [System.Serializable]
    public class PositionArea
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
    }

    // Defines a spawn region with a name, a position area, and how many buttons to spawn there
    [System.Serializable]
    public class SpawnRegion
    {
        public string name;
        public PositionArea area;
        public int spawnCount;
    }

    // A list of regions where buttons will be spawned
    public List<SpawnRegion> spawnRegions = new List<SpawnRegion>();

    // Keep track of all spawned buttons to easily destroy/reset them
    private List<GameObject> spawnedButtons = new List<GameObject>();

    // Called on game start
    void Start()
    {
        SpawnButtons(); // Start by spawning the dirt/germ buttons
    }

    // Spawns buttons randomly within each defined spawn region
    public void SpawnButtons()
    {
        ClearExistingButtons(); // Clean up any previous buttons

        foreach (var region in spawnRegions)
        {
            for (int i = 0; i < region.spawnCount; i++)
            {
                GameObject newButton = CreateButton(); // Create a new button
                RectTransform rt = newButton.GetComponent<RectTransform>();

                // Randomize the position within the region boundaries
                float x = Random.Range(region.area.minX, region.area.maxX);
                float y = Random.Range(region.area.minY, region.area.maxY);
                rt.anchoredPosition = new Vector2(x, y);

                newButton.SetActive(true); // Make sure button is visible
                AddClickToHide(newButton); // Add functionality: hide on click
                spawnedButtons.Add(newButton); // Track this button
            }
        }
    }

    // Creates a new button GameObject with an image and a random sprite
    private GameObject CreateButton()
    {
        // Create a new button with required components
        GameObject button = new GameObject("Button", typeof(RectTransform), typeof(Button), typeof(Image));
        button.transform.SetParent(parentContainer, false); // Attach it to the parent container

        RectTransform rt = button.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(118, 110); // Set button size

        Image img = button.GetComponent<Image>();
        if (buttonSprites != null && buttonSprites.Count > 0)
        {
            // Assign a random sprite (dirt/germ image)
            img.sprite = buttonSprites[Random.Range(0, buttonSprites.Count)];
        }

        return button;
    }

    // Adds a click listener to a button to hide it when clicked
    private void AddClickToHide(GameObject btn)
    {
        btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            btn.SetActive(false); // Simulates "cleaning" the dirt/germ
        });
    }

    // Removes all previously spawned buttons
    private void ClearExistingButtons()
    {
        foreach (var btn in spawnedButtons)
        {
            Destroy(btn); // Destroy the GameObject
        }
        spawnedButtons.Clear(); // Clear the list
    }
}
