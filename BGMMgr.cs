using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMMgr : MonoBehaviour
{
    private static BGMMgr instance;
    [SerializeField] AudioClip BGM_main;
    [SerializeField] AudioClip BGM_select_ending_Story;
    //[SerializeField] AudioClip BGM_ending;
    //[SerializeField] AudioClip BGM_story;
    AudioSource BGM;
    Coroutine fadein;
    Coroutine fadeout;

    public static BGMMgr Instance
    {
        get
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        BGM = GetComponent<AudioSource>();
        BGMMgr.Instance.BGM.Play();


        DontDestroyOnLoad(gameObject);

        // 씬이 바뀔 때 호출되는 함수를 정합니다.
        SceneManager.activeSceneChanged += OnChangedActiveScene;
    }

    public void OnChangedActiveScene(Scene current, Scene next)
    {
        int idx;

        switch (next.name)
        {
            case "MainTitle":
                idx = 0;
                break;

            case "SelectMusicScene":
                idx = 1;
                break;

            case "DesignTest":
                idx = 2;
                break;

            case "EndingTest":
                idx = 1;
                break;

            case "Story":
                idx = 1;
                break;

            default: // 첫번째 게임시작시 next == ""
                idx = 0;
                break;
        }


        SetBGMbyIndex(idx);


        if (fadeout != null)
            StopCoroutine(fadeout);

        fadein = StartCoroutine(FadeIn());
    }



    public void SetVolume(float v)
    {
        BGM.volume = v;
    }

    public void FadeOutBGM()
    {
        Debug.Log("BGM Fade out");
        StartCoroutine(FadeOut());
    }

    public void StopBGMImmediate() //음악 즉시 끄기
    {
        BGM.Stop();
    }

    IEnumerator FadeOut()
    {
        float f_time = 0f;
        float currVolume = BGM.volume;
        while (BGM.volume > 0)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(currVolume, 0, f_time);
            yield return null;
        }
        BGM.Stop();
        BGM.volume = currVolume;
    }

    public void FadeInBGM(int idx)
    {
        Debug.Log("BGM Fade in");
        SetBGMbyIndex(idx);
        StartCoroutine(FadeIn());
    }

    public void FadeInBGMbyClip(AudioClip clip)
    {
        if (clip == null) return;
        BGM.clip = clip;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float f_time = 0f;
        float currVolume = BGM.volume;
        BGM.volume = 0f;
        BGM.Play();
        while (BGM.volume < 1)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(0, currVolume, f_time);
            yield return null;
        }
    }

    public void FadeBGMbyIndex(int idx)
    {
        Debug.Log("BGM change");
        StartCoroutine(FadeVolume(idx));
    }

    IEnumerator FadeVolume(int idx)
    {
        float f_time = 0f;
        float currVolume = BGM.volume;
        while (BGM.volume > 0)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(currVolume, 0, f_time);
            yield return null;
        }
        BGM.Stop();
        SetBGMbyIndex(idx);
        f_time = 0f;
        BGM.Play();
        while (BGM.volume < 1)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(0, currVolume, f_time);
            yield return null;
        }
    }

    public void SetBGMbyIndex(int idx)
    {
        Debug.Log("BGM idx : " + idx);
        switch (idx)
        {
            case 0:
                BGM.clip = BGM_main;
                break;
            case 1:
                BGM.clip = BGM_select_ending_Story;
                break;

            case 2:
                BGM.clip = null;
                break;
            // case 2:
            //     BGM.clip = BGM_ending;
            //     break;
            // case 3:
            //     BGM.clip = BGM_story;
            //     break;
            default:
                break;
        }
    }

}
