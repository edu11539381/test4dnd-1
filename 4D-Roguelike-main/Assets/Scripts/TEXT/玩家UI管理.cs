using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

public class 玩家UI管理 : MonoBehaviour {
    public static 玩家UI管理 Instance;
    public FourDPlayer plr;
    public TextMeshProUGUI text;
    string 頭狀態, 頸狀態, 脊狀態, 腰狀態, 左臂狀態, 右臂狀態, 左腿狀態, 右腿狀態; // need del

    void Update() {
        /*if (plr.combat.head == true) { 頭狀態 = "還算靈活"; } else { 頭狀態 = "瞳孔散大"; }
        if (plr.combat.neck == true) { 頸狀態 = "沒斷"; } else { 頸狀態 = "死了"; }
        if (plr.combat.spine == true) { 脊狀態 = "伶俐矯捷"; } else { 脊狀態 = "斷了"; }
        if (plr.combat.waist == true) { 腰狀態 = "能轉"; } else { 腰狀態 = "軟了"; }
        if (plr.combat.left_arm == true) { 左臂狀態 = "還能用"; } else { 左臂狀態 = "撂了"; }
        if (plr.combat.right_arm == true) { 右臂狀態 = "還能打"; } else { 右臂狀態 = "撂了"; }
        if (plr.combat.left_leg == true) { 左腿狀態 = "還能走"; } else { 左腿狀態 = "丟了"; }
        if (plr.combat.right_leg == true) { 右腿狀態 = "還能游"; } else { 右腿狀態 = "丟了"; }*/

        if (plr.combat.head == true) { 頭狀態 = "還算靈活"; } else { 頭狀態 = "瞳孔散大"; }
        if (plr.combat.head == true) { 頸狀態 = "沒斷"; } else { 頸狀態 = "死了"; }
        if (plr.combat.head == true) { 脊狀態 = "伶俐矯捷"; } else { 脊狀態 = "斷了"; }
        if (plr.combat.head == true) { 腰狀態 = "能轉"; } else { 腰狀態 = "軟了"; }
        if (plr.combat.head == true) { 左臂狀態 = "還能用"; } else { 左臂狀態 = "撂了"; }
        if (plr.combat.head == true) { 右臂狀態 = "還能打"; } else { 右臂狀態 = "撂了"; }
        if (plr.combat.head == true) { 左腿狀態 = "還能走"; } else { 左腿狀態 = "丟了"; }
        if (plr.combat.head == true) { 右腿狀態 = "還能游"; } else { 右腿狀態 = "丟了"; }

        text.text = "<color=#" + plr.colorHP + ">" + "命: " + plr.combat.HP + " /" + plr.combat.maxHP +
        "\n防: " + plr.combat.defense + "+0+0+0" +
        "\n力: " + plr.combat.power + " (" + plr.combat.maxPower + ")" +
        "\n癒: " + plr.combat.healing +
        "\n毅: " + plr.combat.firmness +
        "\n心: " + plr.combat.stability +
        "\n察: " + plr.combat.perception +
        /*"<color=#" + plr.colorTemperature + ">" + */"\n熱: " + plr.combat.temperature + "度" + /*"</color>" +*/

        "\n<color=#666666>層: " + plr.levelCount + "</color>" +
        "\n殺: " + plr.killCount + "  履: " + plr.stepCount +
        "\n<color=#666666>弊: " + plr.cheatCount + "  死: " + plr.deadCount + "</color>" +

        "\n頭:" + 頭狀態 + " 頸:" + 頸狀態 + 
        "\n脊:" + 脊狀態 + " 腰:" + 腰狀態 +
        "\n臂:" + "左" + 左臂狀態 + " 右" + 右臂狀態 +
        "\n腿:" + "左" + 左腿狀態 + " 右" + 右腿狀態 + "</color>";
    }


}

