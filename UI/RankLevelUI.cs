using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankLevelUI : MonoBehaviour
{
    public Text LevelText;
    public Image RankImg;

    public Sprite[] ranks;
    public SelectMusicScene selectMusicScene;

    void Awake()
    {
        selectMusicScene = FindObjectOfType<SelectMusicScene>();
    }

    public void UpdateRank()
    {
        var currSongName = selectMusicScene.selectedSong.name;
        var rank = PlayerDataMgr.playerData_SO.GetRankByName(currSongName);
        Debug.Log("노래 이름 : " + currSongName + "랭크 : " + rank);
        switch (rank)
        {
            case 'S':
                RankImg.sprite = ranks[0];
                break;
            case 'A':
                RankImg.sprite = ranks[1];
                break;
            case 'B':
                RankImg.sprite = ranks[2];
                break;
            case 'C':
                RankImg.sprite = ranks[3];
                break;
            case 'D':
                RankImg.sprite = ranks[4];
                break;
            case 'V':
                RankImg.sprite = ranks[5];
                break;
        }
    }

    public void UpdateLevelText()
    {
        var currSongLevel = selectMusicScene.selectedSong.Level;
        LevelText.text = currSongLevel.ToString();
    }
}
