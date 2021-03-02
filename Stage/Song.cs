using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public string MusicName;
    public string CSVName;
    public string composer;
    public AudioClip clip;
    public int bpm;                           //BPM
    public double beat;                       //1박

    public int Level;                         //레벨 난이도

    [Range(3, 10)]
    public float musicStartDelay;             //음악 시작까지 딜레이 (속도 조절을 고려해 최소 5초 필요)
                                              //가급적 시작시간만 결정하는 용도로 쓰고, 싱크는 noteSrartDelay로 조절을 권장

    [Range(0.0f, 5.0f)]
    public float noteStartDelay;              //'음악이 시작되고 첫 노트가 나올때까지'의 딜레이. 
                                              // 노트와 음악의 싱크를 맞추기 위해 미세하게 조정해야함

    public bool isRandomNote;                 //노트를 무작위 트랙에 생성할 것인가
    public Sprite JacketImage;                //앨범 커버 사진
}
