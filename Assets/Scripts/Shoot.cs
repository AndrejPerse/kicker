using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Ball properties
    private Rigidbody ballRb;
    public float speed = 50;
    private Vector3 direction;
    public Transform ballCenter;
    private int tries = 0;

    // Indicator properties
    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;

    public float time = 3.0f;
    private GameManager gameManager;

    // Sounds
    private AudioSource ballAudio;
    public AudioClip kickSound;
    private float soundVolume = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        ballAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Goal").GetComponent<GameManager>();

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, ballCenter.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show direction indicator
        Vector3 ballPosition = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - ballPosition;

        if (tries < 1)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }

        // Shoot the ball
        if (Input.GetMouseButtonDown(0))
        {
            if(tries < 1)
            {
                tries++;
                ballAudio.PlayOneShot(kickSound, soundVolume);
                ballRb.velocity = direction.normalized * speed;
                StartCoroutine(RemoveObjectRoutine());
                //Destroy indicator
                for (int i = 0; i < numberOfPoints; i++)
                {
                    Destroy(points[i]);
                }
            }
        }
    }

    // Calculate position of every indicator point
    Vector3 PointPosition(float t)
    {
        Vector3 position = (Vector3)ballCenter.position + (direction.normalized * speed * t) + 0.5f * Physics.gravity * (t * t);
        return position;
    }

    IEnumerator RemoveObjectRoutine()
    {
        yield return new WaitForSeconds(time);
        DestroyAndSpawn();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            gameManager.UpdateScore();
            DestroyAndSpawn();
        }
    }

    public void DestroyAndSpawn()
    {
        Destroy(gameObject);
        gameManager.UpdateCount();
        if(gameManager.isGameActive)
        {
            gameManager.SpawnBall();
        }
    }


}
