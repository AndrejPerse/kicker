using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(MainManager data)
    {
        PlayerData playerData = new PlayerData(data);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/lastplayer.json", json);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/lastplayer.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        else
        {
            PlayerData data = new PlayerData();
            return data;
        }
    }

    public static void SavePlayerList(MainManager data)
    {
        PlayerListData playerListData = new PlayerListData(data);
        string json = JsonUtility.ToJson(playerListData);
        File.WriteAllText(Application.persistentDataPath + "/playerlist.json", json);
    }

    public static PlayerListData LoadPlayerList()
    {
        string path = Application.persistentDataPath + "/playerlist.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerListData data = JsonUtility.FromJson<PlayerListData>(json);
            return data;
        }
        else
        {
            PlayerListData data = new PlayerListData();
            return data;
        }
    }
}
