using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSpeed : MonoBehaviour
{
    public static HighSpeed Instance;

    [Range(0.5f, 3)]
    public float MoveSpeed;

    public float CurrentTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        CurrentTime = 0;
        StartMusicCount();
    }

    public void StartMusicCount()
    {
        StartCoroutine(CountMusicPlayedTime());
    }

    public void StopMusicCount()
    {
        StopCoroutine(CountMusicPlayedTime());
        CurrentTime = 0;
    }

    public IEnumerator CountMusicPlayedTime()
    {
        CurrentTime = 0;
        while (true)
        {
            CurrentTime += Time.deltaTime;
            yield return null;
        }
    }

}
