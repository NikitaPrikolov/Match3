using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    private string tileTag;
    private Image tileImage;
    public TileManager tileManager;
    public ScoreManager scoreManager;
    private bool isClicked = false;
    public bool isTeleporting = false; // Новая переменная состояния
    private bool isClickable = true;
    // Инициализация плитки с использованием TileData
    public void Initialize(TileData data)
    {
        tileTag = data.tag;
        tileImage = GetComponent<Image>();
        tileImage.sprite = data.sprite; // Установка спрайта
        gameObject.tag = tileTag; // Установка тега
    }
    // Метод для телепортации плитки
    public void Teleport(float deltaY)
    {
        if (isTeleporting) return; // Если уже выполняется телепортация, ничего не делаем

        isTeleporting = true; // Устанавливаем состояние телепортации
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 initialSize = rectTransform.sizeDelta;
        Vector2 targetSize = initialSize - new Vector2(1.5f, 1.5f);
        Vector2 returnSize = initialSize;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(rectTransform.DOSizeDelta(targetSize, 0.15f))
            .OnComplete(() =>
            {
                transform.position += new Vector3(0, deltaY, 0);
                TileData newData = tileManager.GetRandomTileData(0, 0);
                ChangeTile(newData);
                rectTransform.DOSizeDelta(returnSize, 0.15f).OnComplete(() =>
                {
                    isTeleporting = false; // Восстанавливаем состояние после завершения анимации
                });
                isClicked = false;
            });
    }
    public void Clickable()
    {
        isClickable = true;
    }    
    public void Unclicable()
    {
        isClickable = false;
    }
    public void Clicked()
    {
        if (!isClicked && isClickable && !isTeleporting) // Проверяем состояние teleport
        {
            isClicked = true;
            scoreManager.MinusMove();
            Teleport(5);
        }
    }
// Метод для изменения плитки
public void ChangeTile(TileData newData)
    {
        Initialize(newData);
    }
    public string GetTag() => tileTag;

    public void SetTag(string newTag)
    {
        tileTag = newTag;
        gameObject.tag = newTag; // Обновляем тег игры
    }
}
