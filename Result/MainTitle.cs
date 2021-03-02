using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitle : MonoBehaviour
{
    void Start()
    {
        //BGMMgr.Instance.FadeInBGM(0);
        if (!PlayerDataMgr.isPlayerDataExist()) PlayerDataMgr.Init_PlayerData(); // 처음인 경우, 초기화
        else PlayerDataMgr.Sync_Persis_To_Cache(); // 아닌 경우 기존 정보 load
    }
    public void TouchTitle()
    {
        PlayerDataMgr.Sync_Persis_To_Cache();
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
        SceneLoader.Instance.LoadScene("SelectMusicScene");
    }

    public void Init_Start()
    {
        PlayerDataMgr.Init_PlayerData();
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
        SceneLoader.Instance.LoadScene("SelectMusicScene");
    }
}
