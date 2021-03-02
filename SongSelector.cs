using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SongSelector : MonoBehaviour
{
    public Song Song;
    public AudioClip previewMusic;          //미리듣기 음악

    private Text songName;
    private Text composerName;

    public bool hasBtnMoved;

    private void Start()
    {

        hasBtnMoved = false;

        SetMusicBtnText();
        GetComponent<Button>().onClick.AddListener(SetMusic);
    }

    private void SetMusicBtnText() //여기서 곡 선택버튼의 텍스트가 결정됨
    {
        songName = transform.Find("MusicName").GetComponent<Text>();
        composerName = transform.Find("Composer").GetComponent<Text>();

        songName.text = Song.MusicName;
        composerName.text = Song.composer;
    }

    private void SetMusic()
    {
        transform.parent.GetComponent<SongBtnAni>().MoveBtnToLeft(gameObject);
        transform.parent.GetComponent<SongBtnAni>().SelectBarAnimation(gameObject);

        SelectMusicScene.Instance.SetSelectedSong(Song);

        BGMMgr.Instance.StopBGMImmediate();
        BGMMgr.Instance.FadeInBGMbyClip(previewMusic);
    }

}
