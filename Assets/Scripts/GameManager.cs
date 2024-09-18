using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject gameOverMenu;
    public bool isGameOn;

    // Flappy Bird
    [SerializeField] private Transform birdSpawningPosition;
    [SerializeField] private GameObject flappyBirdObject;
    private Transform flappyBird;

    // Points
    public int points;
    private float timer;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Sounds
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private UnityEngine.UI.Button muteButton;
    [SerializeField] private Sprite mutedImage;
    [SerializeField] private Sprite unmutedImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isGameOn = true;
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
        isGameOn = false;

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
        flappyBird.rotation = Quaternion.identity;

        // Points
        timer = 0;
        points = 0;
        UpdatePointsText(0);

        isGameOn = true;
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

    public void MuteButtonClicked()
    {
        float masterVolume;
        audioMixer.GetFloat("MasterVolume", out masterVolume);

        if (masterVolume == 0f)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
            muteButton.image.sprite = mutedImage; 
        } else
        {
            audioMixer.SetFloat("MasterVolume", 0f);
            muteButton.image.sprite = unmutedImage;
        }
    }

}
