using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int highscore;

    public PlayerData(MainManager data)
    {
        this.name = data.playerName;
        this.highscore = data.highscore;
    }

    public PlayerData()
    {
        name = "Player";
        highscore = 0;
    }
}
