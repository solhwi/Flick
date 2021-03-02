using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public Sprite[] Numbers;
    public Image[] Scores;
    private int nowScore;

    void Start()
    {
        nowScore = 0;
        Instance = this;
    }

    private void NowScoreToString()
    {
        var str = nowScore.ToString();
        int nowLength = str.Length;
        for (int i = 0; i < 7 - nowLength; i++)
        {
            str = "0" + str;
        }

        for (int i = 0; i < str.Length; i++)
        {
            Scores[i].sprite = Numbers[int.Parse(str[i].ToString())];
        }
    }

    public void AddScore(int score)
    {
        nowScore += score; // judge된 스코어 더해주기
        NowScoreToString();
    }

    public int GetScore()
    {
        return nowScore;
    }
}
