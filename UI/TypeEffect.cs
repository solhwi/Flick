using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public bool isAnim; //애니메이션 진행 중인지 알 수 있는 bool값
    int index;
    int ListIndex;
    string targetMsg;
    float interval; //재귀함수에 들어갈 공백시간 값
    AudioSource audioSource; //Sound
    private TMP_Text msgText;
    private List<string>[] list;
    public TypeEffect typeEffect;
    public CanvasGroup[] backgrounds;
    int progress;
    bool endingLock = false;
    void Awake()
    {
        progress = PlayerDataMgr.playerData_SO.SongProgress;
        int MusicIdx = PlayerDataMgr.playerData_SO.GetIdxByName(SelectMusicScene.Instance.selectedSong.MusicName);
        if (MusicIdx == PlayerDataMgr.playerData_SO.SongProgress) PlayerDataMgr.playerData_SO.SongProgress++;
        PlayerDataMgr.Sync_Cache_To_Persis();
        ListIndex = 0;
        msgText = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        list = new List<string>[4];
        for (int i = 0; i < 4; i++) list[i] = new List<string>();
        list[0].Add("최첨단의 에너지 거점 도시, 푸라.");
        list[0].Add("어느 날 학자들은 전기를 관리할 중앙 통제 AI '플레미아'를 개발하는데 성공한다.");
        list[0].Add("이 '플레미아'의 강력한 통제에 '푸라'는 상상도 못할 발전을 이루게 되지만,");
        list[0].Add("얼마 못 가 인간들은 상상도 못한 문제에 직면하게 된다.");
        list[0].Add("'플레미아가 강력히 주도하는 AI 로봇 군단의 침략'");
        list[0].Add("인간들은 이를 저지할 방법이 없었다.");
        list[0].Add("침략 이후 인간들은 '플레미아'에 의해 서로 만날 수도, 얘기할 수도 없게 되었다.");
        list[0].Add("인간들은 무기력하게 반란의 뿌리가 뽑힌 듯 했다.");
        list[0].Add(".........");
        list[0].Add("하지만 어느 날 들려오는 조그마한 선율과 소문...");
        list[0].Add("'...AI 로봇은 아름다운 노래를 들으면 이상해진대.'");
        list[0].Add("'끼긱..끽..'");
        list[0].Add("'끼기긱...'");
        list[0].Add("'......쿵'");
        list[0].Add("'정말인가?'");
        list[0].Add("무기력하게 앉아 있던 인간들은 저마다 아름다운 선율을 찾기 시작했다.");
        list[0].Add("......");

        list[1].Add("사람들이 모인다.");
        list[1].Add("자신들의 약점을 인지하지 못한 AI 사이를 비집고,");
        list[1].Add("아름다운 선율을 무기로 사람들이 모인다.");
        list[1].Add("이름하여 '레지스탕스'");
        list[1].Add("......");
        list[1].Add("'로봇 따위한테 지배받는 삶보다야 훨씬 낫지!'");
        list[1].Add("......");
        list[1].Add("'AI들이 어디에 있든 들을 수 있도록 중앙 전력에서 거대한 합창을 하는 거야!'");
        list[1].Add("'와아-!'");
        list[1].Add("'AI는 학습을 합니다. 기회는 한 번입니다. 제발 신중히... 신중히 부탁드립니다 여러분.'");
        list[1].Add("한 늙은 학자가 말했다.");
        list[1].Add("......");
        list[1].Add("...?");
        list[1].Add("침묵 끝에 양복의 청년이 입을 떼었다.");
        list[1].Add("'그래요. 아무튼 겨우 조그만 약점 하나를 찾았다고 해서, 너무 들뜨지 맙시다.'");
        list[1].Add("'내일까지 철저히 준비해서 이곳으로 모입시다.'");
        list[1].Add("'중앙 전력에 도착할 때까지 최대한 그들이 본인들의 약점을 알게 해선 안됩니다.'");
        list[1].Add("......");

        list[2].Add("......");
        list[2].Add("여기가 '중앙 전력'인가?");
        list[2].Add("...끼기긱");
        list[2].Add("'자, 여러분 준비합시다!'");
        list[2].Add("......");
        list[2].Add("'...여러분?'");
        list[2].Add("'으으악-! '플레미아'들이다!!!'");
        list[2].Add("'도망쳐!!'");
        list[2].Add("......");
        list[2].Add("...");
        list[2].Add("'여러분! 당황하지 마세요! 노래를 불러야 합니다..!'");
        list[2].Add("'아름다운 선율이 필요합니다!!'");
        list[2].Add("......");
        list[2].Add("...");
        list[2].Add("현장은 사람들의 비명소리로 가득해졌고,");
        list[2].Add("'플레미아'들은 이 상황을 예측이라도 한 듯,");
        list[2].Add("아무 동요없이 사람들을 없애 나갔다.");
        list[2].Add("......");

        list[3].Add("......");
        list[3].Add("'플레미아' 이야기 마지막");
        list[3].Add("'우우우~'");
        list[3].Add("'아아아~'");
        list[3].Add("'~~~'");
        list[3].Add("벽에서 느껴지는 조그마한 진동.");
        list[3].Add("'우우우~'");
        list[3].Add("'음악 소린가...?'");
        list[1].Add("...");
        list[3].Add("'아, 그 늙은 학자는 레지스탕스를 별로 믿지 않았나 보다.'");
        list[3].Add("'...기분은 나쁘지만 정말 다행인걸.'");
        list[3].Add("'여러분! 지금입니다! AI들이 잠시 멈췄어요!'");
        list[3].Add("노래를 부르지 않으면 모두 죽어요!");
        list[3].Add("'우우우~~'");
        list[3].Add("'아아아~'");
        list[3].Add("'아아~'");
        list[3].Add("......");
        list[3].Add("...");
        list[3].Add("레지스탕스의 한 용감한 경찰관은 모두를 대신하여 동작이 멈춘 플레미아에게 다가갔다.");
        list[3].Add("......");
        list[3].Add("...");
        list[3].Add("기계들은 원래 아무 존재도 아니었던 듯,");
        list[3].Add("철그럭거리며 바닥에 널부러졌다.");
        list[3].Add("사람들 또한 안도의 한숨과 함께 쓰러졌다.");
        list[3].Add("......");
        list[3].Add("...");
        list[3].Add("'플레미아' 이야기 끝");
    }
    public void SetText()
    {
        if (endingLock) return;
        if (isAnim)
        {
            typeEffect.SetMsg("");
            return;
        }
        if (ListIndex == list[progress].Count)
        {
            endingLock = true;
            StartCoroutine(FadeBackground(backgrounds[progress % 3]));
            return;
        }

        typeEffect.SetMsg(list[progress][ListIndex]);
        ListIndex++;
    }

    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke(); //재귀함수 정지
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;

        interval = 1.0f / CharPerSeconds;
        //audioSource.Play();
        isAnim = true;
        Invoke("Effecting", interval);//interval만큼 있다가 실행
    }
    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];

        //띄어쓰기와 .이 아닌 char에만 사운드 출력
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {
            //Sound
            audioSource.Play();
        }

        index++;

        Invoke("Effecting", interval);//interval만큼 있다가 실행
    }
    void EffectEnd()
    {
        isAnim = false;
    }

    IEnumerator FadeBackground(CanvasGroup fadeIn)
    {
        fadeIn.gameObject.SetActive(true);
        float timeElapsed = 0f;
        float fadeTime = 4.0f;
        while (timeElapsed < fadeTime)
        {
            fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeTime);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 1f;
        SceneLoader.Instance.LoadScene("SelectMusicScene");
        //SelectMusicScene.Instance.BackToSelectScene();
    }
}
