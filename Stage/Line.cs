using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour
{
    private double now = 0;

    public GameObject[] spawnPoints;
    public NoteDecisionEffectGenerator noteDecisionEffectGenerator;

    public bool isMouseHold = false;
    //꾹 누르고 있는가
    public bool isMouseClicked = false;
    //한번 누른 상태인가

    private Note.ScoreType LongScoreType = Note.ScoreType.MISS;
    private bool isStateCheckedLong = false;

    public int NoteCount;

    public int[] ScoreOfType = new int[4];

    void Start()
    {
        StartCoroutine(SetScoreOfType());
    }

    IEnumerator SetScoreOfType()
    {
        NoteCount = ReadCSV.Instance.GetNoteCount();
        if (NoteCount != 0)
        {
            ScoreOfType[0] = 80000 / NoteCount; //퍼펙트 1개의 점수
            ScoreOfType[1] = 65000 / NoteCount;
            ScoreOfType[2] = 27000 / NoteCount;
            ScoreOfType[3] = 0;
        }
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note.noteType != Note.NoteType.LONG) // 롱노트가 아니라면 롱노트 세팅 초기화
        {
            LongScoreType = Note.ScoreType.MISS;
            isStateCheckedLong = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Note note = other.gameObject.GetComponent<Note>();

        if (note.noteType == Note.NoteType.NORMAL)
        {
            if (note.isStateChecked == false)
            {
                Combo.Instance.GetMiss();
                note.scoreType = Note.ScoreType.MISS;
                //note.scoreType = Note.ScoreType.PERFECT; //디버깅용
                Judge(note.trackNum);
            }
        }

        if (note.noteType == Note.NoteType.LONG)
        {
            if (note.isStateChecked == false)
            {
                Combo.Instance.GetMiss();
                note.scoreType = Note.ScoreType.MISS;
                Judge(note.trackNum);
            }
        }

        if (note.noteType == Note.NoteType.FLICK_DOWN)
        {
            //플릭 노트인 경우
            if (note.isStateChecked == false)
            {
                Combo.Instance.GetMiss();
                note.scoreType = Note.ScoreType.MISS;
                Judge(note.trackNum);
            }
        }
    }

    void ScoreProcess(Note note)
    {
        ComboEffect.Instance.GetJudgement((int)note.scoreType); // 현재 state에 맞는 점수 처리
        if (note.scoreType != Note.ScoreType.MISS)
        {
            Score.Instance.AddScore(ScoreOfType[(int)note.scoreType]);
            Combo.Instance.AddCombo((int)note.scoreType);

        }
        else
        {
            //Combo.Instance.GetMiss();
        }
    }

    void OnTriggerStay2D(Collider2D other)  // 판정 체크용
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note.transform.GetSiblingIndex() >= 1) return;
        //콜라이더에 노트가 2개이상 있을 경우, 제1 우선순위 이외의 노트는 처리안함

        if (!note.isStateChecked) // 아직 노트가 판정되지 않았다면
        {
            switch (note.noteType)
            {
                case Note.NoteType.NORMAL:
                    if (isMouseClicked) //마우스를 누른 최초시점에서만 실행되도록
                    {
                        note.isStateChecked = true;
                        isMouseClicked = false;
                        JudgeScore(note);
                        Judge(note.trackNum);
                    }
                    break;
                case Note.NoteType.LONG:
                    if (isMouseHold)
                    {
                        note.isStateChecked = true;
                        if (!isStateCheckedLong) // 롱노트의 판정이 결정나지 않은 경우
                        {
                            JudgeScore(note); //판정값을 가져옴
                            LongScoreType = note.scoreType; // 쭉 퍼펙트? 쭉 굿?
                            isStateCheckedLong = true;
                        }
                        else note.scoreType = LongScoreType;

                        if (note.transform.position.y > -4.0f) //판정선 중앙까지 오게 한다음 판정
                            note.isStateChecked = false;
                        else Judge(note.trackNum);
                    }
                    break;
                case Note.NoteType.FLICK_DOWN:
                    note.isStateChecked = true;
                    JudgeScore(note);
                    Judge(note.trackNum);
                    break;
                case Note.NoteType.FLICK_UP:
                    note.isStateChecked = true;
                    break;
            }
        }
    }

    void JudgeScore(Note note) // 판정
    {
        var noteTiming = note.willPlayTiming;
        now = HighSpeed.Instance.CurrentTime; // 클릭한 시간

        if (noteTiming - 0.08 <= now && now <= noteTiming + 0.08) // perfect
        {
            note.scoreType = Note.ScoreType.PERFECT;
        }
        else if (noteTiming - 0.18 <= now && now <= noteTiming + 0.18) // good
        {
            note.scoreType = Note.ScoreType.GOOD;
        }
        else if (noteTiming - 0.26 <= now && now <= noteTiming + 0.26)// bad
        {
            note.scoreType = Note.ScoreType.BAD;
        }
    }

    void Judge(int trackIndex)
    {
        if (spawnPoints[trackIndex].transform.childCount < 1) return;
        var note = spawnPoints[trackIndex].transform.GetChild(0).GetComponent<Note>();
        //가장 아래의 노트부터 판정

        if (note == null)
        {
            Debug.LogError("Note is null");
        }

        ScoreProcess(note);
        noteDecisionEffectGenerator.NoteDecisionEvent(note);

        note.Eliminate();
    }

    public void ToggleClick()
    {
        StopCoroutine(ToggleClickCT());
        StartCoroutine(ToggleClickCT());
    }

    IEnumerator ToggleClickCT() //단타 클릭시 0.1초를 인정
    {
        isMouseClicked = true;
        yield return new WaitForSeconds(0.1f);
        isMouseClicked = false;
    }
}