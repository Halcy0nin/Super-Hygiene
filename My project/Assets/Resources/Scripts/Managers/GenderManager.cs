using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GenderManager : MonoBehaviour
{

    public void MakeMale()
    {
        GameDataManager.heroGender = "M";
        SceneManager.LoadScene("Main");
    }

    public void MakeFem()
    {
        GameDataManager.heroGender = "F";
        SceneManager.LoadScene("Main");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
