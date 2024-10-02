using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText; // ���������, ��� � ��� ���� UI Text ������� �� �����
    private string filePath;
    public GameObject light;

    void Start()
    {
        if(GameState.SourceScene == "GameScene")
        {
            light.SetActive(true);
        }
        filePath = Path.Combine(Application.persistentDataPath, "scores.csv");
        DisplayScores();
    }

    private void DisplayScores()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            string displayText = "";

            for (int i = 0; i < Mathf.Min(10, lines.Length); i++)
            {
                displayText += lines[i] + "\n";
            }

            scoreText.text = displayText;
        }
        else
        {
            scoreText.text = "No scores available.";
        }
    }
}
