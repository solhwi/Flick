using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    public Sprite[] Numbers;
    public Image[] Scores;
    public int Score;

    void Awake()
    {
        Score = ResultSlave.Instance.score;
    }
    void Start()
    {
        ScoreToString();
    }
    public void ScoreToString()
    {
        var str = Score.ToString();
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
}
