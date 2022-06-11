using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerListData
{
    public List<PlayerData> playerList;

    public PlayerListData(MainManager data)
    {
        playerList = data.loadedPlayerList;
    }

    public PlayerListData()
    {
        playerList = new List<PlayerData>();
    }
}
