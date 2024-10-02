using UnityEngine;

[System.Serializable]
public class TileData
{
    public string tag; // ��� ������
    public Sprite sprite; // ������ ������

    public TileData(string tag, Sprite sprite)
    {
        this.tag = tag;
        this.sprite = sprite;
    }
}
