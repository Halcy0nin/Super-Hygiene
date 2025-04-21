using UnityEngine;

public enum TrashCategory
{
    Biodegradable,
    Nonbiodegradable,
    Recyclable
}

[System.Serializable]
public class TrashData
{
    public string trashName;
    public Sprite trashSprite;
    public TrashCategory category;
}
