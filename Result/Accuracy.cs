using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accuracy : MonoBehaviour
{
    public Text AccuracyText;

    void Awake()
    {
        AccuracyText.text = ResultSlave.Instance.accuracy.ToString("N2") + " " + "%";
    }
}
