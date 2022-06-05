using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Goal").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(gameManager.StartGame);
    }
}
