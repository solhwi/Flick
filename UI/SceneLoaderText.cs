using UnityEngine;
using UnityEngine.UI;

public class SceneLoaderText : MonoBehaviour
{
    Text text;
    string[] strings;
    void Awake()
    {
        text = GetComponent<Text>();
        strings = new string[4];
        strings[0] = "Tip.  '롱 노트' (Yellow Note) 는 꾹 누르고 있어야 합니다.";
        strings[1] = "Tip.  다음 곡을 플레이하기 위해선 A 랭크 이상을 받아야 합니다.";
        strings[2] = "장기간의 게임은 오히려 코로나 대비에 좋습니다.";
        strings[3] = "이 게임의 이름은 'Flick'입니다. ('Fuck' 아님)";
    }
    void OnEnable()
    {
        text.text = strings[new System.Random().Next(0, 4)];
    }
}
