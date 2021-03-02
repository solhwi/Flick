using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class NoteData
{
    public int startTime;
    public int endTime;
    public int track;
    public Note.NoteType noteType;

    //시작시간, 끝시간, 트랙번호, 노트유형
    public NoteData(int _startTime, int _endTime, int _track, Note.NoteType _noteType)
    {

        startTime = _startTime;
        endTime = _endTime;
        track = _track;
        noteType = _noteType;
    }
}

public class ReadCSV : MonoBehaviour
{
    public static ReadCSV Instance;
    public Song currentSong;
    public int bpm;                           //BPM
    public double beat;                       //1박
    public double CountperSec;                //1분 BPM을 채우기 위해 1초에 카운트
    public float musicStartDelay;             //첫 음악 시작까지 딜레이
    public float noteStartDelay;              //첫 노트 생성까지 딜레이

    public List<NoteData> noteList = new List<NoteData>();
    public Queue<NoteData> note = new Queue<NoteData>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ReadSelectedMusic();
        Init();
        ReadCSVFile();
    }

    void ReadSelectedMusic()
    {
        var obj = GameObject.Find("SelectMusicScene");
        if (obj != null)
        {
            currentSong = obj.GetComponent<SelectMusicScene>().GetSelectedSong(); //곡 지정
            var speed = obj.GetComponent<SelectMusicScene>().musicSpeed; //속도 지정
            HighSpeed.Instance.MoveSpeed = 1 / speed + 0.5f;
        }
    }

    void Init()
    {
        bpm = currentSong.bpm;
        beat = currentSong.beat;
        musicStartDelay = currentSong.musicStartDelay;
        noteStartDelay = currentSong.noteStartDelay;
    }

    void ReadCSVFile()
    {
        //var CSVdir = (Application.dataPath + "/Resources/" + currentSong.CSVName + ".csv");
        //PC버전, 상대경로 지정
        FileReadWrite file = new FileReadWrite();
        TextAsset CSVdata = Resources.Load<TextAsset>(currentSong.CSVName);

        StringReader strReader = new StringReader(CSVdata.text);
        string currentTitle = "";
        bool endOfFile = false;
        bool endOfTrack = false;

        List<string> StreamList = new List<string>();

        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            StreamList.Add(data_String);
        }

        for (int i = 0; i < StreamList.Count; i++)
        {
            string[] csvText = StreamList[i].Split(',');
            string[] csvNextLineText = csvText;
            if (i + 2 < StreamList.Count)
                csvNextLineText = StreamList[i + 2].Split(',');

            if (csvText[2].ToString() == " Title_t")
            {
                currentTitle = csvText[3].ToString();
                Debug.Log(currentTitle);
            }

            if (csvText[2] == " Start_track") endOfTrack = false;
            if (csvText[2] == " End_track") endOfTrack = true;

            if (endOfTrack == false)
            {
                RandomGenerateMode(currentTitle, csvText, csvNextLineText);
                BasicGenerateMode(currentTitle, csvText, csvNextLineText);
            }
        }
        CountperSec = bpm * beat / 60;

        Debug.Log("노트 개수 : " + noteList.Count);

        //리스트 정렬하기
        noteList.Sort(delegate (NoteData A, NoteData B)
        {
            if (A.startTime > B.startTime) { return 1; }
            else if (A.startTime < B.startTime) { return -1; }
            return 0;
        });
        //리스트 정렬 후 큐에 담기
        for (int i = 0; i < noteList.Count; i++)
        {
            note.Enqueue(noteList[i]);
        }
        ObjectPool.Instance.Initialize(noteList.Count);
    }

    public int GetNoteCount() //노트 개수 반환
    {
        return noteList.Count;
    }

    public int RandomGenerateMode(string title, string[] csvText, string[] csvNextLineText) //메인멜로디를 트랙마다 랜덤하게 배치
    {
        if (currentSong.isRandomNote == true)
        {
            if (title == " \"Main Melody\"")
            {
                if (csvText[2] == " Note_on_c" && csvText[5] != " 0")
                {
                    var startTimeInt = Convert.ToInt32(csvText[1]);
                    var endTimeInt = Convert.ToInt32(csvNextLineText[1]);
                    int rand = UnityEngine.Random.Range(0, 6);
                    if (csvText[4] == " 71")
                        noteList.Add(new NoteData(startTimeInt, endTimeInt, rand, Note.NoteType.NORMAL));
                    if (csvText[4] == " 72")
                        noteList.Add(new NoteData(startTimeInt, endTimeInt, rand, Note.NoteType.LONG));
                    if (csvText[4] == " 73")
                        noteList.Add(new NoteData(startTimeInt, endTimeInt, rand, Note.NoteType.FLICK_DOWN));
                }
            }
        }
        return 1;
    }

    public int BasicGenerateMode(string title, string[] csvText, string[] csvNextLineText) //정해진 트랙별로 노트를 배치
    {
        if (currentSong.isRandomNote == false)
        {
            for (int index = 0; index < 6; index++)
            {
                if (title == " \"track_" + index + "\"")
                {
                    if (csvText[2] == " Note_on_c" && csvText[5] != " 0")
                    {
                        var startTimeInt = Convert.ToInt32(csvText[1]);
                        var endTimeInt = Convert.ToInt32(csvNextLineText[1]);
                        //Debug.Log("시작: " + startTimeInt + "끝 : " + endTimeInt);
                        if (csvText[4] == " 71")
                            noteList.Add(new NoteData(startTimeInt, endTimeInt, index, Note.NoteType.NORMAL));
                        if (csvText[4] == " 72")
                            noteList.Add(new NoteData(startTimeInt, endTimeInt, index, Note.NoteType.LONG));
                        if (csvText[4] == " 73")
                            noteList.Add(new NoteData(startTimeInt, endTimeInt, index, Note.NoteType.FLICK_DOWN));
                    }

                }
            }
        }
        return 1;
    }

}
