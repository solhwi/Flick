using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMgr : MonoBehaviour
{
    public GameObject FullCombo;
    public ResultScore ResultScore;
    public Rank rank;
    public GameObject NewBest;
    public GameObject AllPerfectBonus;
    public GameObject FullComboBonus;
    public CanvasGroup Next;
    public CanvasGroup Retry;
    public CanvasGroup Back;

    void Start()
    {
        //BGMMgr.Instance.FadeBGMbyIndex(1);
        Invoke("SetActiveRank", 2f);
        Invoke("SetBonusScore", 3.5f); // 올 퍼펙트이면 +10000
        Invoke("SetActiveFullCombo", 5f);
        Invoke("SetActiveNewBest", 6f);
        Invoke("SetButton", 7f);
    }
    public void SetActiveFullCombo()
    {
        Debug.Log("SetActiveFullCombo");
        FullCombo.SetActive(ResultSlave.Instance.IsFullCombo);
        SFXMgr.Instance.SetSFXbyIndex(2);
        SFXMgr.Instance.PlaySFX();
    }
    public void SetBonusScore()
    {
        if (ResultSlave.Instance.IsFullCombo)
        {
            Debug.Log("Full Combo Set Active");
            FullComboBonus.SetActive(true);
            ResultScore.Score += 10000;
        }
        if (ResultSlave.Instance.IsAllPerfect)
        {
            Debug.Log("All Perfect Set Active");
            AllPerfectBonus.SetActive(true);
            ResultScore.Score += 10000;
        }
        ResultScore.ScoreToString();
        SFXMgr.Instance.SetSFXbyIndex(1);
        SFXMgr.Instance.PlaySFX();
    }
    public void SetActiveRank()
    {
        Debug.Log("SetActiveRank");
        SFXMgr.Instance.SetSFXbyIndex(0);
        SFXMgr.Instance.PlaySFX();
        rank.SetActiveRank();
    }
    public void SetActiveNewBest()
    {
        // 과거 점수와 비교, 더 높은 경우에만 실제로 저장
        Debug.Log("SetActiveNewBest");
        int MusicIdx = PlayerDataMgr.playerData_SO.GetIdxByName(SelectMusicScene.Instance.selectedSong.MusicName);
        int pastScore = PlayerDataMgr.playerData_SO.SongList[MusicIdx].SongScore;
        int nowScore = ResultScore.Score;
        if (pastScore < nowScore)
        {
            PlayerDataMgr.playerData_SO.SongList[MusicIdx].SongScore = nowScore;
            PlayerDataMgr.playerData_SO.SetRankBynum(MusicIdx, ResultSlave.Instance.Rank);
            PlayerDataMgr.Sync_Cache_To_Persis(); //기기 내에 저장

            NewBest.SetActive(true);
            SFXMgr.Instance.SetSFXbyIndex(3);
            SFXMgr.Instance.PlaySFX();
        }
    }
    public void SetButton()
    {
        bool IsNowProgressSong = false;
        if (PlayerDataMgr.playerData_SO.SongProgress < 4) IsNowProgressSong = SelectMusicScene.Instance.selectedSong.MusicName == PlayerDataMgr.playerData_SO.SongList[PlayerDataMgr.playerData_SO.SongProgress].SongName;
        if (ResultSlave.Instance.Rank < 3 && IsNowProgressSong) // a랭크 이상이고, 현재 곡이 스토리를 안본 곡인 경우
        {
            Debug.Log(ResultSlave.Instance.Rank);
            PlayerDataMgr.Sync_Cache_To_Persis();
            Next.gameObject.SetActive(true);
            StartCoroutine(FadeBackground(Next));
        }
        else
        {
            Back.gameObject.SetActive(true);
            Retry.gameObject.SetActive(true);
            StartCoroutine(FadeBackground(Back));
            StartCoroutine(FadeBackground(Retry));
        }
    }
    public void GoToStory()
    {
        SceneLoader.Instance.LoadScene("Story");
        SFXMgr.Instance.SetSFXbyIndex(6);
        SFXMgr.Instance.PlaySFX();
    }
    public void GoToSelect()
    {
        SceneLoader.Instance.LoadScene("SelectMusicScene");
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
    }
    public void GoToDesign()
    {
        SceneLoader.Instance.LoadScene("DesignTest");
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
    }
    IEnumerator FadeBackground(CanvasGroup fadeIn)
    {
        float timeElapsed = 0f;
        float fadeTime = 0.8f;
        while (timeElapsed < fadeTime)
        {
            fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeTime);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 1f;
    }

}
