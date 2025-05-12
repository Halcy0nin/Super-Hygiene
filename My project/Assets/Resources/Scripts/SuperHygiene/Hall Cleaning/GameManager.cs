using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public List<TrashData> collectedTrash = new List<TrashData>();

    public bool BedroomCleaned = false;
    public bool HallCleaned = false;
    public bool ClassroomClean = false;
    public GameObject NextGame;


    void Update()
    {
        if (AreAllRoomsCleaned())
        {
            NextGame.SetActive(true);
        }
    }
    void Awake()
    {
        BootTracer.Log("GameManager Awake()");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClearCollectedTrash()
    {
        collectedTrash.Clear();
    }
    public void CheckBedroomCleaned()
    {
        BedroomCleaned = true;
    }
    public void CheckHallCleaned()
    {
        HallCleaned = true;
    }
    public void CheckClassroomCleaned()
    {
        ClassroomClean = true;
    }
    bool AreAllRoomsCleaned()
    {
        return BedroomCleaned && HallCleaned && ClassroomClean;
    }
}
