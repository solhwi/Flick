using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongProgress : MonoBehaviour
{
    private Image progress;
    private float maxTime = 15f; // 나중에 적당히 곡 길이 받아와서 처리해주면 됩니다.

    void Start()
    {
        progress = GetComponent<Image>();
        maxTime = GameObject.Find("ReadCSV").GetComponent<ReadCSV>().currentSong.clip.length;
    }
    void Update()
    {
        progress.fillAmount = HighSpeed.Instance.CurrentTime / maxTime;
    }
}
