using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteGenerator : MonoBehaviour
{
    public ReadCSV csv;
    public static NoteGenerator Instance;
    public ResultSlave moveEnding;
    public GameObject notePrefab;
    public GameObject spawnedPrefab;
    public GameObject[] spawnPoint;
    public Queue<GameObject> NoteQueue = new Queue<GameObject>();
    public bool isEnded;

    float tick = 0.03f;
    double count;                             //디버깅 편의상, 나중엔 private로 바꾸기

    void Awake()
    {
        Instance = this;
        Init();
    }

    void Init()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        isEnded = false;
        moveEnding = FindObjectOfType<ResultSlave>();
        for (int i = 0; i < 6; i++)
            spawnPoint[i] = GameObject.Find("SP" + i);
        //for (int i = 0; i < noteList.Count / 10; i++) NoteQueue.Enqueue(CreateNote());
        //주의 : 인스펙터에서 "SP+(숫자)" 이름을 바꾸지 마시오
    }

    void Start()
    {
        Debug.Log("음악 시작 딜레이 : " + csv.musicStartDelay);
        Debug.Log("노트 시작 딜레이 : " + csv.noteStartDelay);
        Invoke("PlayMusic", csv.musicStartDelay);
        var delay = csv.musicStartDelay - HighSpeed.Instance.MoveSpeed + csv.noteStartDelay;
        InvokeRepeating("Timer", delay, tick);

        //0.03초 간격으로 timer 메서드 호출
        //노래 시작 타이밍을 조절해서 싱크 맞출 수 있음
    }
    //Invoke를 써야 정확하다!

    void PlayMusic()
    {
        GetComponent<AudioSource>().clip = csv.currentSong.clip;
        GetComponent<AudioSource>().Play();
    }

    void Timer()
    {
        count += csv.CountperSec * tick;

        if (csv.note.Count < 1) //남은 큐가 없으면
        {
            if (!isEnded) Invoke("gotoNextScene", 7.0f); //7초뒤에 엔딩으로 이동
            isEnded = true;
            return;
        }

        if (count > csv.note.Peek().startTime)
        {
            int previousStartTime;
            do
            {
                previousStartTime = csv.note.Peek().startTime;
                GenerateNote();
                if (csv.note.Count < 1) break;
            } while (previousStartTime == csv.note.Peek().startTime);
            //같은 타이밍에 시작하는 노트면 동시에 생성
        }
    }

    void gotoNextScene()
    {
        moveEnding.TouchTitle();
    }

    void GenerateNote()
    {
        //spawnedPrefab = ObjectPool.GetObject(); // 수정
        //spawnedPrefab.transform.SetParent(spawnPoint[csv.note.Peek().track].transform);
        var NoteObject = ObjectPool.GetObject();
        NoteObject.transform.SetParent(spawnPoint[csv.note.Peek().track].transform);
        NoteObject.transform.position = spawnPoint[csv.note.Peek().track].transform.position;
        NoteObject.spawnedTiming = HighSpeed.Instance.CurrentTime;
        NoteObject.noteType = csv.note.Peek().noteType;
        NoteObject.trackNum = csv.note.Peek().track;

        //spawnedPrefab = Instantiate(notePrefab, spawnPoint[csv.note.Peek().track].transform);
        //spawnedPrefab.GetComponent<Note>().spawnedTiming = HighSpeed.Instance.CurrentTime;
        //spawnedPrefab.GetComponent<Note>().noteType = csv.note.Peek().noteType;
        //spawnedPrefab.GetComponent<Note>().trackNum = csv.note.Peek().track;
        csv.note.Dequeue();
    }
}
