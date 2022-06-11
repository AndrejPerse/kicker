using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Text
    public Button restartButton, menuButton;
    public Text playerNameText, scoreText, countText, highScoreText, gameOverText;
    // Game Manager stuff
    public bool isGameActive;
    private int score;
    private int count;
    // Ball properties
    public GameObject soccerBall;
    private Vector3 ballSpawnLocation = new Vector3(-9.01f, 0.12f, 0);
    // Obstacle properties
    public GameObject obstacle;
    private float obstacleX = -5.0f;
    private float obstacleZ = 0;
    private float obstacleUpper = 1.19f;
    private float obstacleLower = -2.21f;
    // Sound
    private AudioSource goalAudio;
    public AudioClip fansSound;
    private float soundVolume = 1.0f;

    public void Awake()
    {
        goalAudio = GetComponent<AudioSource>();
        isGameActive = true;

        playerNameText.text = MainManager.Instance.playerName;
        highScoreText.text = "Your high score: " + MainManager.Instance.highscore;
        score = 0;
        count = 3;

        SpawnObstacle();
        SpawnBall();
    }

    public void SpawnBall()
    {
        Instantiate(soccerBall, ballSpawnLocation, Quaternion.identity);
        GameObject.Find("Obstacle(Clone)").transform.position = new Vector3(obstacleX, Random.Range(obstacleLower, obstacleUpper), obstacleZ);
    }
    public void SpawnObstacle()
    {
        Vector3 obstacleLocation = new Vector3(obstacleX, Random.Range(obstacleLower, obstacleUpper), obstacleZ);
        Instantiate(obstacle, obstacleLocation, Quaternion.identity);
    }

    public void UpdateCount()
    {
        count--;
        countText.text = "Shots remaining: " + count;
        if(count == 0)
        {
            GameOver();
        }
    }

    public void UpdateScore()
    {
        goalAudio.PlayOneShot(fansSound, soundVolume);
        score++;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        // Display Game over
        gameOverText.text = "Game Over\nYou scored " + score + " goals.";
        // Display buttons
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        // Save scores
        MainManager.Instance.UpdatePlayerList(score);
        MainManager.Instance.SaveLastPlayer();
        MainManager.Instance.SavePlayerList();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
