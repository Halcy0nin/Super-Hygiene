using UnityEngine;
using UnityEngine.UI;
using ScratchCard;

public class CharacterMaskController : MonoBehaviour
{
    public Texture2D maleMainTex;
    public Texture2D femaleMainTex;

    public Material baseMaterial;

    public Material characterMaterial;
    public RawImage rawImage;
    private ScratchCardMaskUGUI scratchCard;

    void Start()
    {
        ApplyMaskBasedOnGender();
    }


    public void ApplyMaskBasedOnGender()
    {
        rawImage = GetComponent<RawImage>();
        scratchCard = GetComponent<ScratchCardMaskUGUI>();
        if (baseMaterial != null)
        {
            // Duplicate the material to avoid modifying the original
            characterMaterial = new Material(baseMaterial);
            rawImage.material = characterMaterial;
        }
        else
        {
            Debug.LogWarning("Base material is not assigned in the inspector!");
        }
        Debug.Log("ApplyMaskBasedOnGender() called");
        characterMaterial.SetTexture("_MaskTex", scratchCard.TargetTexture);

        if (characterMaterial == null) return;

        Texture2D chosenMainTex = (GameDataManager.heroGender == "M") ? maleMainTex : femaleMainTex;

        if (chosenMainTex != null)
        {
            characterMaterial.SetTexture("_MainTex", chosenMainTex);
            rawImage.texture = chosenMainTex;
            Debug.Log("Assigned main texture: " + chosenMainTex.name);
        }
        else
        {
            Debug.LogWarning("Chosen main texture is null!");
        }

        if (scratchCard.TargetTexture != null)
        {
            characterMaterial.SetTexture("_MaskTex", scratchCard.TargetTexture);
            Debug.Log("Assigned RenderTexture mask");
        }
        else
        {
            Debug.LogWarning("ScratchCardMaskUGUI.TargetTexture is null!");
        }
    }
}
