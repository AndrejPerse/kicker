using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string playerName;
    public int highscore;
    public List<PlayerData> loadedPlayerList;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLastPlayer();
        LoadPlayerList();
    }

    public void UpdatePlayerList(int score)
    {
        bool playerExists = false;
        // Check if player already exists
        foreach (PlayerData player in loadedPlayerList)
        {
            if (player.name == playerName)
            {
                // check if player beat his previous score
                if (player.highscore < score)
                {
                    player.highscore = score;
                    highscore = score;
                }
                playerExists = true;
            }
        }
        if (!playerExists)
        {
            highscore = score;
            PlayerData newPlayer = new PlayerData(this);
            loadedPlayerList.Add(newPlayer);
        }
    }

    public void SaveLastPlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadLastPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        playerName = data.name;
        highscore = data.highscore;
    }

    public void SavePlayerList()
    {
        SaveSystem.SavePlayerList(this);
    }

    public void LoadPlayerList()
    {
        PlayerListData data = SaveSystem.LoadPlayerList();
        loadedPlayerList = data.playerList;
    }

    public void SortPlayerList()
    {
        for (int i = 0; i < loadedPlayerList.Count; i++)
        {
            for (int j = i + 1; j < loadedPlayerList.Count; j++)
            {
                if (loadedPlayerList[j].highscore > loadedPlayerList[i].highscore)
                {
                    // Swap
                    PlayerData tmp = loadedPlayerList[i];
                    loadedPlayerList[i] = loadedPlayerList[j];
                    loadedPlayerList[j] = tmp;
                }
            }
        }
    }
}
