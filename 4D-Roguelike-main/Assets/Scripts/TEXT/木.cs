using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class 木 : MonoBehaviour
{
    public static 木 Instance;
    public string text, 加油, a,b,c,d;
    public TextMeshProUGUI front;

    void Start()
    {
        Instance = this;
        if (SceneManager.GetActiveScene().buildIndex == 0) { // del
            text = "你如今在第" + SceneManager.GetActiveScene().buildIndex+"層。\n\n四維八向，\n以<color=#22FF22>WASD+EQCZ</color>走吧！\n也能用<color=#22FF22>方向鍵+IJKL</color>動喔！\n\n按<color=#22FF22>X</color>休息。\n按<color=#22FF22>G</color>新遊戲。\n按<color=#22FF22>M</color>加怪。\n按<color=#22FF22>V</color>更多描述。\n\n更多廢話看itch.io吧。";
        } else { text = "你到第" + SceneManager.GetActiveScene().buildIndex+"層了，"+冒險加油()+"！\n\n按<color=#22FF22>X</color>休息。\n按<color=#22FF22>G</color>新遊戲。\n按<color=#22FF22>M</color>加怪。\n\n更多無用按鍵請詳閱itch.io描述。"; }
    }

    public string 冒險加油()
    {
        switch (Random.Range(0, 4)){
            case 0: a = "祝"; break;
            case 1: a = "願"; break;
            case 2: a = "望"; break;
            case 3: d = "加油"; return d;}
        switch (Random.Range(0, 11)){
            case 0: b = "你"; break;
            case 1: b = "君"; break;
            case 2: b = "伊"; break;
            case 3: b = "尔"; break;
            case 4: b = "您"; break;
            case 5: b = "爾"; break;
            case 6: b = "汝"; break;
            case 7: b = "一切"; break;
            case 8: b = "萬事"; break;
            case 9: b = "冒險"; break;
            case 10: d = "如拾地芥"; return a + d;}
        switch (Random.Range(0, 13)){
            case 0: c = "成功"; break;
            case 1: c = "順利"; break;
            case 2: c = "好運"; break;
            case 3: c = "順遂"; break;
            case 4: c = "成事"; break;
            case 5: c = "垂成"; break;
            case 6: c = "得志"; break;
            case 7: c = "報捷"; break;
            case 8: c = "勝利"; break;
            case 9: c = "大勝"; break;
            case 10: c = "奏捷"; break;
            case 11: d = "我保佑你"; return d;
            case 12: d = "耕耘必有收穫"; return a + d;}
        return a + b + c;
    }

    void Update() { front.text = text; }
    public static void AddLine(string line) { Instance.text = line.ToLower() + "\n\n" + Instance.text; }
}
