using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject menu;
    public void OnClickPause()
    {
        Time.timeScale = 0f;
        NoteGenerator.Instance.GetComponent<AudioSource>().Pause();
        menu.SetActive(true);
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
    }

    public void OnClickContinue()
    {
        Time.timeScale = 1f;
        NoteGenerator.Instance.GetComponent<AudioSource>().UnPause();
        menu.SetActive(false);
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
    }

    public void OnClickRetry()
    {
        if (ReadCSV.Instance.note.Count < 1) return;
        Time.timeScale = 1f;
        menu.SetActive(false);
        SFXMgr.Instance.SetSFXbyIndex(4);
        SFXMgr.Instance.PlaySFX();
        SceneLoader.Instance.LoadScene("SelectMusicScene");
    }
}
