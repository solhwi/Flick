using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public List<SongData> SongList;
    public int SongProgress;

    public PlayerData()
    {
        SongList = new List<SongData>();
        SongList.Add(new SongData("Faded"));
        SongList.Add(new SongData("Invincible"));
        SongList.Add(new SongData("LightItUp"));
        SongList.Add(new SongData("Seasons"));
        SongProgress = 0;
    }
}

[System.Serializable]
public class SongData
{
    public string SongName;
    public int SongScore;
    public char Rank;

    public SongData()
    {
        SongName = null;
        SongScore = 0;
        Rank = 'D';
    }

    public SongData(string name)
    {
        SongName = name;
        SongScore = 0;
        Rank = 'D';
    }
}
