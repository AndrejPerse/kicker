using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public InputField inputField;

    // Start is called before the first frame update
    private void Start()
    {
        EnterInput();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ReadPlayerName()
    {
        MainManager.Instance.playerName = inputField.text;

        bool playerExists = false;
        // Check if player already exists
        foreach (PlayerData player in MainManager.Instance.loadedPlayerList)
        {
            if(player.name == inputField.text)
            {
                MainManager.Instance.highscore = player.highscore;
                playerExists = true;
            }
        }
        if (!playerExists)
        {
            MainManager.Instance.highscore = 0;
        }
    }

    public void EnterInput()
    {
        if(MainManager.Instance.playerName != "Player")
        {
            inputField.text = MainManager.Instance.playerName;
        }
    }
}
