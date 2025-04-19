using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShirtSpawner : MonoBehaviour
{
    public GameObject shirtPrefab;
    public Transform spawnParent;
    public List<Color> shirtColors;
    public Button continueButton;

    public Sprite imageDefault;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;

    public RectTransform shirtStackParent; // Assign this in the Inspector
    private float stackOffsetY = 20f;       // Adjust spacing between stacked shirts
    private int stackedShirtCount = 0;


    private int currentShirtIndex = 0;
    private GameObject activeShirt;

    void Start()
    {
        SpawnNextShirt();
    }


   public void SpawnNextShirt()
{
    // Move the last active shirt aside
    if (activeShirt != null)
    {
        ShirtFolding lastLogic = activeShirt.GetComponent<ShirtFolding>();
        if (lastLogic != null)
        {
            lastLogic.MoveShirtToStack(shirtStackParent, stackedShirtCount * stackOffsetY);
            stackedShirtCount++;

        }
    }

    if (currentShirtIndex >= shirtColors.Count)
    {
        Debug.Log("All shirts completed!");
        continueButton.gameObject.SetActive(true);
        return;
    }

    // Spawn shirt
    activeShirt = Instantiate(shirtPrefab, spawnParent);

    // Setup RectTransform
    RectTransform rt = activeShirt.GetComponent<RectTransform>();
    rt.anchorMin = new Vector2(0.5f, 0.5f);
    rt.anchorMax = new Vector2(0.5f, 0.5f);
    rt.pivot = new Vector2(0.5f, 0.5f);
    rt.anchoredPosition = Vector2.zero;
    rt.sizeDelta = new Vector2(259, 200); // Adjust as needed

    // Set color and default sprite
    Image img = activeShirt.GetComponent<Image>();
    img.color = shirtColors[currentShirtIndex];
    img.sprite = imageDefault;

    // Attach folding logic
    ShirtFolding logic = activeShirt.AddComponent<ShirtFolding>();
    logic.image1 = image1;
    logic.image2 = image2;
    logic.image3 = image3;
    logic.image4 = image4;
    logic.image5 = image5;
    logic.onStepsComplete = SpawnNextShirt;

    currentShirtIndex++;
}

}  