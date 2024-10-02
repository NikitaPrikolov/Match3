using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


    public class GameManager : MonoBehaviour
    {
    public GameObject[] tiles;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private bool isPaused = false;
    private bool isOver = false;
    public ScoreManager scoreManager;

    public void PauseGame()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        public void ResumeGame()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        public void ExitToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
        }
    public void UnclickableTiles()
    {
        foreach (GameObject tile in tiles)
        {
            Tile tileScript = tile.GetComponent<Tile>();
            EventTrigger eventTrigger = tile.GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                tileScript.Unclicable();
                eventTrigger.enabled = false;
            }
        }
        isOver = true; // ������������� ���� ��������� ����
    }

    public IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isOver)
        {
            bool newRecord = scoreManager.CheckScore();
            if (newRecord)
            {
                GameState.SourceScene = "GameScene";
                SceneManager.LoadScene("RecordsScene"); // ������� �� ����� ��������
            }
            else
            {
                gameOverMenu.SetActive(true); // ���������� ���� "Game Over"
            }
        }
    }

    public void ClickableTiles()
    {
        if (!isOver) return; // ���� ���� �� ��������, ������ �� ������

        foreach (GameObject tile in tiles)
        {
            Tile tileScript = tile.GetComponent<Tile>();
            EventTrigger eventTrigger = tile.GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                tileScript.Clickable();
                eventTrigger.enabled = true;
            }
        }

        isOver = false; // ���������� ���� ��������� ����
    }
}
public static class GameState
{
    public static string SourceScene
    { get; set; }
}
