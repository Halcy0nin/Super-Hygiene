using UnityEngine;

public class ShowerSpriteController : MonoBehaviour
{
    public Sprite[] showerCharacter; 
    public SpriteRenderer showerSpriteRenderer; 

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
