using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ShowRecords()
    {
        GameState.SourceScene = "MainMenu";
        SceneManager.LoadScene("RecordsScene");
    }
    public void ShowAbout()
    {
        SceneManager.LoadScene("AboutScene");
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
