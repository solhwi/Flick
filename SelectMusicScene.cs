using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMusicScene : MonoBehaviour
{
    public static SelectMusicScene Instance = null;

    public Song selectedSong;
    public float musicSpeed = 1.0f;

    void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // instance를 유일 오브젝트로 만든다
        Instance = this;

        // Scene 이동 시 삭제 되지 않도록 처리
        DontDestroyOnLoad(this.gameObject);
        // if (Instance == null)
        // {
        //     DontDestroyOnLoad(gameObject);
        //     Instance = this;
        // }
    }

    public void TouchTitle()
    {
        SceneLoader.Instance.LoadScene("DesignTest");
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
        //BGMMgr.Instance.FadeOutBGM();
    }

    public void BackToSelectScene()
    {
        SceneLoader.Instance.LoadScene("SelectMusicScene");
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
    }

    public void BackToTitle()
    {
        SceneLoader.Instance.LoadScene("MainTitle");
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
        //BGMMgr.Instance.FadeOutBGM();
    }

    public void SetSelectedSong(Song song)
    {
        selectedSong = song;
        Debug.Log($"song name : {song.MusicName}");

        //곡 재킷 이미지 변경
        var jacket = GameObject.Find("Jacket");
        jacket.GetComponent<Image>().sprite = selectedSong.JacketImage;

        //좌측 하단 레벨/랭크 UI 변경
        var ui = GameObject.Find("Rectangle").GetComponent<RankLevelUI>();
        ui.UpdateLevelText();
        ui.UpdateRank();

        SFXMgr.Instance.SetSFXbyIndex(5);
        SFXMgr.Instance.PlaySFX();
    }

    public Song GetSelectedSong()
    {
        return selectedSong;
    }

}
