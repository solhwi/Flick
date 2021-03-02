using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData_SO", fileName = "PlayerData_SO", order = 0)]
public class PlayerData_SO : ScriptableObject
{
    public List<SongData> SongList;
    public int SongProgress;

    public int GetScoreByIdx(string name)
    {
        for (int i = 0; i < SongList.Count; i++)
        {
            if (SongList[i].SongName == name)
            {
                return SongList[i].SongScore;
            }
        }
        return -1;
    }
    public char GetRankByName(string name)
    {
        for (int i = 0; i < SongList.Count; i++)
        {
            if (SongList[i].SongName == name)
            {
                Debug.Log("·©Å©°ª: " + SongList[i].Rank);
                return SongList[i].Rank;
            }
        }
        return ' ';
    }
    public int GetIdxByName(string name)
    {
        for (int i = 0; i < SongList.Count; i++)
        {
            if (SongList[i].SongName == name)
            {
                return i;
            }
        }
        return -1;
    }
    public void SetRankBynum(int SongIdx, int num)
    {
        if (num == 0) SongList[SongIdx].Rank = 'V';
        else if (num == 1) SongList[SongIdx].Rank = 'S';
        else if (num == 2) SongList[SongIdx].Rank = 'A';
        else if (num == 3) SongList[SongIdx].Rank = 'B';
        else if (num == 4) SongList[SongIdx].Rank = 'C';
        else if (num == 5) SongList[SongIdx].Rank = 'D';
    }
}

