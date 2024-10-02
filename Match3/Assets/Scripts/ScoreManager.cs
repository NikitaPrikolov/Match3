using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Ссылка на текстовый объект, отображающий очки
    public Text movesText; // Ссылка на текстовый объект, отображающий количество ходов
    public GameObject loosePanel;
    public GameManager gameManager;
    private int score;
    private int moves;
    private string filePath;

    private void Start()
    {
        score = 0;
        moves = 3;
        UpdateScoreUI();
        UpdateMovesUI();
        filePath = Path.Combine(Application.persistentDataPath, "scores.csv");
    }
    public void AddPoints(int count)
    {
        score += count;
        UpdateScoreUI();
    }

    public void AddMoves(int count)
    {
        moves += count;
        UpdateMovesUI();
        CheckMoves();
    }

    public void MinusMove()
    {
        if (moves > 0)
        {
            moves--;
            UpdateMovesUI();
            CheckMoves();
        }
    }
    private void CheckMoves()
    {
        if (moves <= 0)
        {
            gameManager.UnclickableTiles();
            StartCoroutine(gameManager.GameOverAfterDelay(2f)); // Задержка в 2 секунды
        }
        else
        {
            gameManager.ClickableTiles();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Очки: " + score.ToString();
        }
    }
    public bool CheckScore()
    {
        if (score > 0)
        {
            string bestScoreKey = "BestScore";
            int bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);
            if (score > bestScore)
            {
                // Сохраняем новый лучший результат
                PlayerPrefs.SetInt(bestScoreKey, score);
                PlayerPrefs.Save();
                SaveScore(score);
                return true; // Новый рекорд установлен
            }
        }
        return false; // Новый рекорд не установлен
    }

    public void SaveScore(int score)
    {
        string date = DateTime.Now.ToString("yyyy.MM.dd");
        string newLine = $"{score}            {date}";

        // Читаем существующие записи
        List<string> lines = new List<string>();
        if (File.Exists(filePath))
        {
            lines = File.ReadAllLines(filePath).ToList();
        }

        // Записываем новый рекорд на первую строку
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine(newLine); // Записываем новый рекорд
            foreach (string line in lines) // Записываем старые записи
            {
                writer.WriteLine(line);
            }
        }
    }
    private void UpdateMovesUI()
    {
        if (movesText != null)
        {
            movesText.text = "Осталось ходов: " + moves.ToString();
        }
    }
}
