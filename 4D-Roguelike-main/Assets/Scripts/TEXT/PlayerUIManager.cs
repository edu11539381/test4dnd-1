using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using System.Drawing;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public FourDPlayer plr;
    public TextMeshProUGUI text;
    string headState, neckState, spineState, waistState, leftArmState, rightArmState, leftLegState, rightLegState; // need del


    void Update()
    {
        /*if (plr.combat.head == true) { headState = "fine"; } else { headState = "mydriasis"; }
        if (plr.combat.neck == true) { neckState = "good"; } else { neckState = "demise"; }
        if (plr.combat.spine == true) { spineState = "poor"; } else { spineState = "perish"; }
        if (plr.combat.waist == true) { waistState = "thin"; } else { waistState = "droped"; }
        if (plr.combat.left_arm == true) { leftArmState = "usable"; } else { leftArmState = "lost"; }
        if (plr.combat.right_arm == true) { rightArmState = "usable"; } else { leftArmState = "lost"; }
        if (plr.combat.left_leg == true) { leftLegState = "unsteady"; } else { leftArmState = "lost"; }
        if (plr.combat.right_leg == true) { rightLegState = "movable"; } else { leftArmState = "lost"; }*/

        if (plr.combat.head == true) { headState = "fine"; } else { headState = "mydriasis"; }
        if (plr.combat.head == true) { neckState = "good"; } else { neckState = "demise"; }
        if (plr.combat.head == true) { spineState = "poor"; } else { spineState = "perish"; }
        if (plr.combat.head == true) { waistState = "thin"; } else { waistState = "droped"; }
        if (plr.combat.head == true) { leftArmState = "usable"; } else { leftArmState = "lost"; }
        if (plr.combat.head == true) { rightArmState = "usable"; } else { leftArmState = "lost"; }
        if (plr.combat.head == true) { leftLegState = "unsteady"; } else { leftArmState = "lost"; }
        if (plr.combat.head == true) { rightLegState = "movable"; } else { leftArmState = "lost"; }


        text.text = "<color=#"+ plr.colorHP + ">" + "hp: " + plr.combat.HP + " /" + plr.combat.maxHP +
        "\ndefense: " + plr.combat.defense + " + 0 + 0 + 0" +
        "\nstrength: " + plr.combat.power + " (" + plr.combat.maxPower + ")" +
        "\nhealing: " + plr.combat.healing +
        "\nfirmness: " + plr.combat.firmness +
        "\nstability: " + plr.combat.stability +
        "\nperception: " + plr.combat.perception +
        /*"<color=#"+ plr.colorTemperature + ">" + */"\ntemperature: " + plr.combat.temperature + "°e" + /*"</color>" +*/

        "\n<color=#444444>levels: " + plr.levelCount + "</color>" +

        "\nkills: " + plr.killCount +
        "\nsteps: " + plr.stepCount +
        "\ncheats: " + plr.cheatCount +

        "\n<color=#444444>deads: " + plr.deadCount + "</color>" +

        "\nhead: " + headState + //" neck: " + neckState +
        "\nspine: " + spineState + "\nwaist: " + waistState +
        "\nleft arm: " + leftArmState +
        "\nright arm: " + rightArmState +
        "\nleft leg: " + leftLegState +
        "\nright leg: " + rightLegState + "</color>";
    }


}
