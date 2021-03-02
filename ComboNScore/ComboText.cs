// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ComboText : MonoBehaviour
// {
//     public Sprite[] Numbers;
//     public Image[] Combo;
//     private int nowCombo = 0;
//     private Text comBoText;

//     public static ComboText Instance;

//     // Start is called before the first frame update
//     void Start()
//     {
//         comBoText = GetComponent<Text>();
//         Instance = this;
//     }

//     // 노트를 정상적으로 받을 때마다 ComboText.Instance.GetNoteExactly(); 를 써주시면 자동으로 콤보가 올라갈겁니다.
//     public void GetNoteExactly()
//     {
//         comBoText.text = $"{++nowCombo}\nCombo";
//         //Debug.Log($"nowCombo = {nowCombo}");
//     }

//     // Miss 판정이 났을 때, ComboText.Instance.GetMiss(); 를 써주시면 콤보 UI에서 자동으로 0으로 바뀔겁니다.
//     public void GetMiss()
//     {
//         comBoText.text = "0\nCombo";
//         nowCombo = 0;

//     }
// }
