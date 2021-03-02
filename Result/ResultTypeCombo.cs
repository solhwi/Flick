using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTypeCombo : MonoBehaviour
{
    private int[] TypeComboCount = new int[4];
    public Sprite[] Numbers;
    public Image[] Combos;

    void Awake()
    {
        TypeComboCount = ResultSlave.Instance.TypeComboCount;
    }
    void Start()
    {
        TypeComboToString(); // 타입에 따른 스코어 띄우기
    }
    private void TypeComboToString()
    {
        int type = -1;
        if (gameObject.name == "PerfectCombo") type = 0;
        else if (gameObject.name == "GoodCombo") type = 1;
        else if (gameObject.name == "BadCombo") type = 2;
        else if (gameObject.name == "MissCombo") type = 3;

        var str = TypeComboCount[type].ToString();
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
