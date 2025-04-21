using UnityEngine;

public class ShowerSpriteController : MonoBehaviour
{
    public Sprite[] showerCharacter; // 0 = sleeping, 1 = awake
    public SpriteRenderer showerSpriteRenderer; // SpriteRenderer component to update

    public void ChangeShowerSprite()
    {
        if (GameDataManager.heroGender == "M")
        {
            showerSpriteRenderer.sprite = showerCharacter[0]; 
        }
        else 
        {
            showerSpriteRenderer.sprite = showerCharacter[1]; 
        }
    }

    void Start()
    {
        ChangeShowerSprite();
    }

}
