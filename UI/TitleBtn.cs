using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBtn : MonoBehaviour
{
    // 음악 선택창에서 메인 씬으로 넘어가는 버튼
    // On Destory Load 때문에, 게임오버 후 다시 돌아오면 버튼 함수가 Missing되는 문제가 있어서 이렇게 작성
    void Start()
    {
        var component = GameObject.Find("SelectMusicScene").GetComponent<SelectMusicScene>();
        GetComponent<Button>().onClick.AddListener(component.BackToTitle);
    }
}
