using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBest : MonoBehaviour
{
    public Image Panel;

    void Awake()
    {
        Panel.fillAmount = 0;
    }
    void OnEnable()
    {
        StartCoroutine(FadePanel());
    }
    IEnumerator FadePanel()
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.8f)
        {
            Panel.fillAmount = Mathf.Lerp(0f, 1f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
    }

}
