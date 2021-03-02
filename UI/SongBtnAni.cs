using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SongBtnAni : MonoBehaviour
{
    public RectTransform SelectBar; //노란 화살표
    public CanvasGroup[] SongBtns;
    void Awake()
    {
        for (int i = 0; i < PlayerDataMgr.playerData_SO.SongProgress; i++)
        {
            SongBtns[i].gameObject.SetActive(true);
            SongBtns[i].alpha = 1f;
        }
        if (PlayerDataMgr.playerData_SO.SongProgress >= 4) return;
        StartCoroutine(FadeSelectMusicButton(SongBtns[PlayerDataMgr.playerData_SO.SongProgress]));

    }

    IEnumerator FadeSelectMusicButton(CanvasGroup fadeIn)
    {
        fadeIn.gameObject.SetActive(true);
        SFXMgr.Instance.SetSFXbyIndex(6);
        SFXMgr.Instance.PlaySFX();
        float timeElapsed = 0f;
        float fadeTime = 0.8f;
        while (timeElapsed < fadeTime)
        {
            fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeTime);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 1f;
    }

    public void MoveBtnToLeft(GameObject obj) //버튼 좌측으로 나오는 애니메이션
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        DisableAllBtn(obj.transform);

        if (obj.GetComponent<SongSelector>().hasBtnMoved == false)
        {
            var desiredPosition = new Vector2(rt.anchoredPosition.x - 40.0f, rt.anchoredPosition.y);
            rt.DOAnchorPos(desiredPosition, 0.5f).SetEase(Ease.OutCubic);
            obj.GetComponent<SongSelector>().hasBtnMoved = true;
        }
    }

    public void SelectBarAnimation(GameObject obj) //노란 화살표 애니메이션
    {
        SelectBar.transform.gameObject.SetActive(true);
        SelectBar.DOKill();
        var desiredXPos = obj.GetComponent<RectTransform>().position.x - 710f;
        var desiredYPos = obj.GetComponent<RectTransform>().position.y + 10f;
        SelectBar.GetComponent<RectTransform>().DOMove(new Vector2(desiredXPos, desiredYPos), 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        SelectBar.GetComponent<RectTransform>().DOAnchorPosX(3, 50, 0.5f).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo)); ;
    }

    public void DisableAllBtn(Transform obj) //모든 버튼 위치 초기화
    {
        Transform[] child = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            child[i] = transform.GetChild(i);
            if (child[i] == obj) continue;
            if (child[i].GetComponent<SongSelector>().hasBtnMoved == true)
            {
                var rt = child[i].GetComponent<RectTransform>();
                var desiredXPosition = new Vector2(rt.anchoredPosition.x + 40.0f, rt.anchoredPosition.y);
                rt.DOAnchorPos(desiredXPosition, 0.5f).SetEase(Ease.OutCubic);
                child[i].GetComponent<SongSelector>().hasBtnMoved = false;
            }
        }
    }
}
