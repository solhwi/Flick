using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public static Combo Instance;
    public Sprite[] Numbers;
    public Image[] Combos;
    private int nowCombo;
    private int[] TypeCombo = new int[4];
    private int MaxCombo;
    private int MissCount = 0;
    private bool Lock = false;

    void Start()
    {
        nowCombo = 0;
        MaxCombo = 0;
        Instance = this;
        for (int i = 0; i < 4; i++)
            TypeCombo[i] = 0;
    }

    private void NowComboToString()
    {
        var str = nowCombo.ToString();
        int nowLength = str.Length;
        for (int i = 0; i < 3 - nowLength; i++)
        {
            str = "0" + str;
        }

        for (int i = 0; i < str.Length; i++)
        {
            Combos[i].sprite = Numbers[int.Parse(str[i].ToString())];
        }
    }

    public void AddCombo(int type)
    {
        nowCombo += 1;
        if (nowCombo > MaxCombo) MaxCombo = nowCombo; //베스트 콤보 갱신

        TypeCombo[type] += 1;
        NowComboToString();
    }

    public int GetMaxCombo()
    {
        return MaxCombo;
    }

    public int[] GetTypeCombo()
    {
        return TypeCombo;
    }
    public void GetMiss()
    {
        nowCombo = 0;
        TypeCombo[3] += 1;
        NowComboToString();
        MissCount++;
        if (MissCount >= 25 && Lock == false)
        {
            Lock = true;
            NoteGenerator.Instance.GetComponent<AudioSource>().Stop();
            ResultSlave.Instance.TouchTitle();
        }
    }
}
