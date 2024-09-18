using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject gameOverMenu;

    // Flappy Bird
    [SerializeField] private Transform birdSpawningPosition;
    [SerializeField] private GameObject flappyBirdObject;
    private Transform flappyBird;

    // Points
    public int points;
    private float timer;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
        timer = 0;
        BuildingSpawner.Instance.StartSpawningBuildings();
        flappyBird = Instantiate(flappyBirdObject, birdSpawningPosition.position, Quaternion.identity).GetComponent<Transform>();

        UpdatePointsText(0);
        UpdateHighScoreText();
    }

    private void Update()
    {
        if (timer > 1)
        {
            points += 1;
            timer = 0;
            UpdatePointsText(points);
        }

        timer += Time.deltaTime;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;

        SaveScore();
    }

    public void RestartGame()
    {
        // Turn off game over menu
        gameOverMenu.SetActive(false);

        // Reseting game objects
        BuildingSpawner.Instance.DestroyBuildings();
        BuildingSpawner.Instance.StartSpawningBuildings();
        flappyBird.position = birdSpawningPosition.position;

        // Points
        timer = 0;
        points = 0;
        UpdatePointsText(0);

        Time.timeScale = 1f;
    }

    private void UpdatePointsText(int value)
    {
        pointsText.text = value.ToString();
    }

    private void UpdateHighScoreText(int value)
    {
        highScoreText.text = "Highscore: " + value.ToString();
    }

    private void UpdateHighScoreText()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Highscore: " + highScore.ToString();
    }

    private void SaveScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (points > highScore) 
        {
            PlayerPrefs.SetInt("HighScore", points);
            PlayerPrefs.Save();
            UpdateHighScoreText(points);
        }

    }

}
