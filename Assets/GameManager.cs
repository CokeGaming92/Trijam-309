using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int lives = 1;
    float score;
    public TextMeshProUGUI scoreText;
    public GameObject GameOverPanel;
    public bool hasGameStarted = false;
    public Ball ball;

    private void Start()
    {
        hasGameStarted = false;  // Initially, game is not started
    }

    private void Update()
    {
        if (!hasGameStarted) return;

        UpdateUI();
    }

    public void StartGame()
    {
        hasGameStarted = true;
        ball.canMove = true;
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        hasGameStarted = false;
        ball.canMove = false;
    }

    public void LoseLife(int newLife)
    {
        lives -= newLife;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
    }

    public void AddScore(float newScore)
    {
        score += newScore;
        UpdateUI();
    }

    public void RemoveScore(float newScore)
    {
        score -= newScore;
        if (score < 0)
        {
            score = 0;
            LoseLife(1);
        }
        UpdateUI();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString("00000");
    }
}
