using UnityEngine;
using UnityEngine.UI; // Needed for RawImage

public class CharacterMaskController : MonoBehaviour
{
    public Texture2D maleMask;
    public Texture2D femaleMask;
    public Material baseMaterial;

    private Material characterMaterial;
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        if (rawImage == null)
        {
            Debug.LogError("RawImage component not found on this GameObject.");
            return;
        }

        if (baseMaterial != null)
        {
            // Duplicate the material
            characterMaterial = new Material(baseMaterial);
            rawImage.material = characterMaterial;

            ApplyMaskBasedOnGender();
        }
        else
        {
            Debug.LogWarning("Base material is not assigned in the inspector!");
        }
    }

    public void ApplyMaskBasedOnGender()
    {
        Debug.Log("ApplyMaskBasedOnGender() called");

        if (characterMaterial == null) return;

        Texture2D chosenMask = (GameDataManager.heroGender == "M") ? maleMask : femaleMask;

        if (chosenMask != null)
        {
            characterMaterial.SetTexture("_MaskTex", chosenMask);
            Debug.Log("Assigned mask: " + chosenMask.name);
        }
        else
        {
            Debug.LogWarning("Chosen mask texture is null!");
        }
    }
}
