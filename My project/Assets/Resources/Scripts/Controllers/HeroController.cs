using UnityEngine;

public class HeroController : MonoBehaviour
{

   public string heroGender;
   public Sprite heroSprite;
    public Sprite[] heroSprites;

    public void SetSprite()
    {
        if(heroGender=="M")
        {
            GetComponent<SpriteRenderer>().sprite = heroSprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = heroSprites[1];
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heroGender = GameDataManager.heroGender;
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
