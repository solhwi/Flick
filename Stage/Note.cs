using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Sprite[] sprites;
    public float SpeedInSecond;      //판정선까지 걸리는 시간 (초)
    public int trackNum;            //몇 번 트랙에 놓여있는지
    public bool isStateChecked;     //판정 받았는지

    public double spawnedTiming;    //스폰된 타이밍
    public double willPlayTiming;   //연주될 타이밍

    float needArriveTime;           //라인까지 걸리는 시간

    public enum NoteType
    {
        NORMAL,
        LONG,
        FLICK_UP,
        FLICK_DOWN
    }

    public enum ScoreType
    {
        PERFECT,
        GOOD,
        BAD,
        MISS
    }

    public NoteType noteType;
    public ScoreType scoreType;

    void Start()
    {
        InitSprite();
        //InvokeRepeating("Timer", 0, 0.03f);
        InitMovement();
    }
    void InitMovement()
    {
        var DecisionLinePos = new Vector3(transform.position.x, -4.0f, transform.position.z);
        //판정선의 y위치 : 4.0f

        SpeedInSecond = HighSpeed.Instance.MoveSpeed;
        StartCoroutine(MoveToPosition(transform, DecisionLinePos, SpeedInSecond));
        //speed(초) 에 걸쳐서 이동함
        willPlayTiming = spawnedTiming + SpeedInSecond;
    }

    void InitSprite()
    {
        if (noteType == NoteType.NORMAL)
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        else if (noteType == NoteType.LONG)
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        else if (noteType == NoteType.FLICK_UP)
            GetComponent<SpriteRenderer>().sprite = sprites[2];
        else if (noteType == NoteType.FLICK_DOWN)
            GetComponent<SpriteRenderer>().sprite = sprites[3];
    }

    void Timer() //생성부터 라인까지 걸리는 시간, 디버깅용
    {
        needArriveTime += 0.03f;
        if (gameObject.transform.position.y < -4.0f) //4.0f는 Line 오브젝트가 있는 y좌표
        {
            Debug.Log("생성부터 소멸까지 " + needArriveTime + "초 걸림");
            CancelInvoke("Timer");
            //Eliminate();
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        //여기까지가 판정선 정중앙까지 가는 코드
        //Debug.Log(spawnedTiming + " / " + HighSpeed.Instance.CurrentTime + " / " + willPlayTiming);

        var distance = currentPos.y - position.y;
        var movedSpeed = distance / timeToMove;
        //첫 위치부터 판정선 중앙까지 오기까지의 속도

        while (true)
        {
            transform.Translate(Vector3.down * Time.deltaTime * movedSpeed);
            yield return null;
        } //판정선 지나서 삭제될때까지 아래로 더 가는 코드
    }

    public void Eliminate()
    {
        //ObjectPool.ReturnObject(this);
        SFXMgr.Instance.SetSFXbyIndex(7);
        SFXMgr.Instance.PlaySFX();
        Destroy(gameObject);
    }

}