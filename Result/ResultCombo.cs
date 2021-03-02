using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCombo : MonoBehaviour
{
    private int ComboCount;
    public Sprite[] Numbers;
    public Image[] Combos;

    void Awake()
    {
        ComboCount = ResultSlave.Instance.ComboCount;
    }
    void Start()
    {
        ComboToString(); // MaxScore띄우기
    }
    private void ComboToString()
    {
        var str = ComboCount.ToString();
        int nowLength = str.Length;
        for (int i = 0; i < 4 - nowLength; i++)
        {
            str = "0" + str;
        }
        for (int i = 0; i < str.Length; i++)
        {
            Combos[i].sprite = Numbers[int.Parse(str[i].ToString())];
        }
    }
}
