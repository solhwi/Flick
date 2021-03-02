using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboEffect : MonoBehaviour
{
    public static ComboEffect Instance;
    RectTransform rt;

    public Sprite[] sprites = new Sprite[4];
    Image fxImage;
    private string[] judgement = new[] { "Perfect", "Good", "Bad", "Miss" };

    // Start is called before the first frame update
    void Start()
    {
        fxImage = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        Instance = this;
    }

    public void GetJudgement(int judge)
    {
        TextEffect();
        fxImage.sprite = sprites[judge];
    }

    public void TextEffect()
    {
        fxImage.DOKill();
        rt.DOKill();

        //fxText[judge].color = new Color(fxText[judge].color.r, fxText[judge].color.g, fxText[judge].color.b, 1.0f);
        fxImage.color = new Color(fxImage.color.r, fxImage.color.g, fxImage.color.b, 1.0f);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -30.0f);

        rt.DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.OutElastic).OnComplete(() => fxImage.DOFade(0, 0.5f));
    }
}
