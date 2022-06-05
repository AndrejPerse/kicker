using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Text
    public GameObject titleScreen;
    public Button restartButton;
    public Text scoreText, countText, gameOverText;
    private int score;
    private int count;

    // Game Manager stuff
    public bool isGameActive;

    // Ball properties
    public GameObject soccerBall;
    private Vector3 ballSpawnLocation = new Vector3(-9.01f, 0.12f, 0);

    // Obstacle properties
    public GameObject obstacle;
    private float obstacleUpper = 1.19f;
    private float obstacleLower = -2.21f;

    // Sound
    private AudioSource goalAudio;
    public AudioClip fansSound;
    private float soundVolume = 1.0f;

    // Start is called before the first frame update
    public void StartGame()
    {
        goalAudio = GetComponent<AudioSource>();
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        score = 0;
        count = 10;
        ShowGUI();

        SpawnObstacle();
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBall()
    {
        Instantiate(soccerBall, ballSpawnLocation, Quaternion.identity);
        GameObject.Find("Obstacle(Clone)").transform.position = new Vector3(-5, Random.Range(obstacleLower, obstacleUpper), 0);
    }
    public void SpawnObstacle()
    {
        Vector3 obstacleLocation = new Vector3(-5, Random.Range(obstacleLower, obstacleUpper), 0);
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
        gameOverText.text = "Game Over\nYou scored " + score + " goals.";
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGUI()
    {
       scoreText.gameObject.SetActive(true);
       countText.gameObject.SetActive(true);
    }
}
