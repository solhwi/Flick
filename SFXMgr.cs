using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMgr : MonoBehaviour
{

    private static SFXMgr instance;
    [SerializeField] AudioClip[] Sound;
    AudioSource sound;

    public static SFXMgr Instance
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
        sound = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float v)
    {
        sound.volume = v * 0.8f;
    }

    public void PlaySFX()
    {
        sound.Play();
    }

    public void StopSFX()
    {
        sound.Stop();
    }
    public void SetSFXbyIndex(int idx)
    {
        switch (idx)
        {
            case 0:
                sound.clip = Sound[0];
                break;
            case 1:
                sound.clip = Sound[1];
                break;
            case 2:
                sound.clip = Sound[2];
                break;
            case 3:
                sound.clip = Sound[3];
                break;
            case 4:
                sound.clip = Sound[4];
                break;
            case 5:
                sound.clip = Sound[5];
                break;
            case 6:
                sound.clip = Sound[6];
                break;
            case 7:
                sound.clip = Sound[7];
                SetVolume(0.2f);
                break;
            default:
                break;
        }
    }
}
