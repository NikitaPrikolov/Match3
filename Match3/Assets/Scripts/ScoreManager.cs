using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // ������ �� ��������� ������, ������������ ����
    public Text movesText; // ������ �� ��������� ������, ������������ ���������� �����
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
            StartCoroutine(gameManager.GameOverAfterDelay(2f)); // �������� � 2 �������
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
            scoreText.text = "����: " + score.ToString();
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
                // ��������� ����� ������ ���������
                PlayerPrefs.SetInt(bestScoreKey, score);
                PlayerPrefs.Save();
                SaveScore(score);
                return true; // ����� ������ ����������
            }
        }
        return false; // ����� ������ �� ����������
    }

    public void SaveScore(int score)
    {
        string date = DateTime.Now.ToString("yyyy.MM.dd");
        string newLine = $"{score}            {date}";

        // ������ ������������ ������
        List<string> lines = new List<string>();
        if (File.Exists(filePath))
        {
            lines = File.ReadAllLines(filePath).ToList();
        }

        // ���������� ����� ������ �� ������ ������
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine(newLine); // ���������� ����� ������
            foreach (string line in lines) // ���������� ������ ������
            {
                writer.WriteLine(line);
            }
        }
    }
    private void UpdateMovesUI()
    {
        if (movesText != null)
        {
            movesText.text = "�������� �����: " + moves.ToString();
        }
    }
}
