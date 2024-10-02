using UnityEngine;

public class TileManager : MonoBehaviour
{
    public TileData[] availableTiles; // ������ � �������
    private Tile[,] tiles = new Tile[5, 5]; // ������� ������

    public TileData GetRandomTileData(int x, int y)
    {
        TileData selectedData;
        bool isValid;

        do
        {
            isValid = true;
            int randomIndex = Random.Range(0, availableTiles.Length);
            selectedData = availableTiles[randomIndex];

            // �������� �� ������� ���� ���������� ������
            if (y > 1 && tiles[x, y - 1].GetTag() == selectedData.tag && tiles[x, y - 2].GetTag() == selectedData.tag)
                isValid = false;

            if (x > 1 && tiles[x - 1, y].GetTag() == selectedData.tag && tiles[x - 2, y].GetTag() == selectedData.tag)
                isValid = false;

        } while (!isValid);

        return selectedData;
    }
}

