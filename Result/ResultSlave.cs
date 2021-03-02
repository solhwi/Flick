using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSlave : MonoBehaviour
{
    public static ResultSlave Instance;
    public int score;
    public int ComboCount;
    public int[] TypeComboCount;
    public int NoteCount;
    public float accuracy;
    public bool IsFullCombo = false;
    public bool IsAllPerfect = false;
    public int Rank;
    public string SongName;

    // 0. 맥스콤보와 판정수(퍼펙 굿 배드 미스 수)는 미리 출력되어있음
    // 1. 정확도 숫자 드르르륵 하면서 올라감
    // 2. 올퍼펙트 체크 애니메이션
    // 3. 풀콤보 체크 애니메이션
    // 4. 점수 숫자 드르르륵 하면서 올라감
    // 5. 랭크 스탬프 찍듯이 쾅 하면서 등장
    // 6. 뉴 베스트 왼쪽에서 오른쪽으로 출력하듯 생성

    public void Awake()
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
    }

    public void TouchTitle()
    {
        score = Score.Instance.GetScore(); // 최종 스코어
        ComboCount = Combo.Instance.GetMaxCombo(); // max combo 수
        TypeComboCount = Combo.Instance.GetTypeCombo(); // 각 타입에 따른 판정 횟수
        NoteCount = ReadCSV.Instance.GetNoteCount(); // 총 노트 수
        accuracy = 100.0f * (float)(TypeComboCount[0] + TypeComboCount[1] + TypeComboCount[2]) / (float)NoteCount; // 정확도
        SongName = ReadCSV.Instance.currentSong.MusicName; // 곡 이름
        IsFullCombo = false;
        IsAllPerfect = false;

        //int nowScore = 0;
        if (score >= 79999)
        {
            IsAllPerfect = true;
            //nowScore += 10000; // 보너스 점수
        } // 올퍼펙트인가 여부
        if (NoteCount == ComboCount)
        {
            IsFullCombo = true;
            //nowScore += 10000;
        }
        JudgeRank();
        SceneLoader.Instance.LoadScene("EndingTest");
    }
    public void JudgeRank()
    {
        if (IsAllPerfect) Rank = 0;
        else if (score >= 75000) Rank = 1;
        else if (score >= 70000) Rank = 2;
        else if (score >= 60000) Rank = 3;
        else if (TypeComboCount[3] < 25) Rank = 4;
        else Rank = 5;
    }
}
