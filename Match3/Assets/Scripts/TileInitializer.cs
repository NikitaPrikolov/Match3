using UnityEngine;
using UnityEngine.UI;

public class TileInitializer : MonoBehaviour
{
    public GameObject suggesterTile;
    public Image[] tiles; // ������ ��� ���� ������ �� �����
    public TileData[] tileDataOptions; // ��������� ������
    private const string FirstLaunchKey = "FirstLaunch";

    void Start()
    {

        InitializeTiles();
        if(PlayerPrefs.HasKey(FirstLaunchKey))
        {
            DisableAnimation();
        }
        else
        {
            PlayerPrefs.SetInt(FirstLaunchKey, 1);
            PlayerPrefs.Save();
        }

    }

    public void DisableAnimation()
    {
        Animation animation = suggesterTile.GetComponent<Animation>();
        if (animation != null)
        {
            animation.enabled = false;
        }
    }
    void InitializeTiles()
    {
        // ��������� ����������� �����:
        int width = 5;
        int height = 5;

        // ������ �������� ������
        SetHardcodedTiles();

        // �������� �� ������� ����� �� ����� � ��������� ������
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // ���������� ��� �������� ������
                if (!PlayerPrefs.HasKey(FirstLaunchKey) && IsHardcodedTile(x, y))
                    continue;

                Image currentTile = tiles[y * width + x];
                TileData newTileData = GetValidTileData(x, y);
                currentTile.sprite = newTileData.sprite; // ��������� ������
                currentTile.tag = newTileData.tag; // ��������� ���, ���� �����
            }
        }
    }

    void SetHardcodedTiles()
    {
        tiles[13].sprite = tileDataOptions[2].sprite;
        tiles[12].sprite = tileDataOptions[3].sprite;
        tiles[14].sprite = tileDataOptions[3].sprite;
        tiles[18].sprite = tileDataOptions[3].sprite;
    }

    bool IsHardcodedTile(int x, int y)
    {
        int index = y * 5 + x;
        return index == 12 || index == 13 || index == 14 || index == 18;
    }

    TileData GetValidTileData(int x, int y)
    {
        TileData chosenTileData;

        do
        {
            // ����� ���������� TileData �� ������� ��������� ������
            chosenTileData = tileDataOptions[Random.Range(0, tileDataOptions.Length)];

        } while (IsTileDataInvalid(chosenTileData, x, y));

        return chosenTileData;
    }

    bool IsTileDataInvalid(TileData tileData, int x, int y)
    {
        // �������� �� �������� ������ �� ����������� � ���������
        if (x >= 2)
        {
            if (tiles[y * 5 + (x - 1)].sprite == tileData.sprite && tiles[y * 5 + (x - 2)].sprite == tileData.sprite)
                return true;
        }

        if (y >= 2)
        {
            if (tiles[(y - 1) * 5 + x].sprite == tileData.sprite && tiles[(y - 2) * 5 + x].sprite == tileData.sprite)
                return true;
        }

        if (x >= 1)
        {
            if (tiles[y * 5 + (x - 1)].sprite == tileData.sprite) return true; // �����
        }

        if (y >= 1)
        {
            if (tiles[(y - 1) * 5 + x].sprite == tileData.sprite) return true; // �����
        }

        return false;
    }
    //private void OnDestroy()
    //{
    //    PlayerPrefs.DeleteAll();
    //    PlayerPrefs.Save();
    //}
}
