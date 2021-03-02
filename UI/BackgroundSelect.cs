using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSelect : MonoBehaviour
{
    SpriteRenderer background;
    public Sprite[] sprites;
    void Awake()
    {
        background = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (SelectMusicScene.Instance.selectedSong.name == "Faded") background.sprite = sprites[0];
        else if (SelectMusicScene.Instance.selectedSong.name == "Invincible") background.sprite = sprites[1];
        else if (SelectMusicScene.Instance.selectedSong.name == "LightItUp") background.sprite = sprites[2];
        else if (SelectMusicScene.Instance.selectedSong.name == "Seasons") background.sprite = sprites[3];
    }
}
