using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedCtrl : MonoBehaviour
{
    float speed = 1.0f;
    public void IncreaseSpeed()
    {
        if (speed < 2.0f)
            speed += 0.1f;
        UpdateSpeedUI();
    }
    public void DecreaseSpeed()
    {
        if (0.5f < speed)
            speed -= 0.1f;
        UpdateSpeedUI();
    }
    void UpdateSpeedUI()
    {
        var ui = GameObject.Find("SpeedTxt").GetComponent<Text>();
        ui.text = speed.ToString("N1");

        var component = GameObject.Find("SelectMusicScene").GetComponent<SelectMusicScene>();
        component.musicSpeed = this.speed;
    }
}