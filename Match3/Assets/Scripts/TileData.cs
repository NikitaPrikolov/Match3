using UnityEngine;

[System.Serializable]
public class TileData
{
    public string tag; // Тег плитки
    public Sprite sprite; // Спрайт плитки

    public TileData(string tag, Sprite sprite)
    {
        this.tag = tag;
        this.sprite = sprite;
    }
}
