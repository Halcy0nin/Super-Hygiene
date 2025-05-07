using UnityEngine;

public class ChangeSceneFromButton : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        LevelManager.Instance.LoadSceneFromButton(sceneName);
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
