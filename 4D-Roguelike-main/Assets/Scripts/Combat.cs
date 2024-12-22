using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class Combat : MonoBehaviour
{
    public static Combat Instance;
    public string color, creatureName, creature名;

    public int HP, maxHP, power, maxPower, defense, healing, firmness = 100, stability = 100, perception, temperature;
    public double worstHit = 0.5, criticalHit = 1.5;
    public string[][] bodyState; public int bodyDamageNum = 0;
    public bool head, neck, waist, spine, left_arm, right_arm, left_leg, right_leg, addFightdetail, add詳鬥;

    public UnityEvent death = new UnityEvent(); bool died = false;

    void Start() {

        string 名 = "A";
        switch(gameObject.name) {
            case "You": 名 = "你"; break; // version a00
            case "Troll": 名 = "大怪"; break;
            case "Goblin": 名 = "小怪"; break;

            case "Blue Troll": 名 = "水藍大怪"; break; // version a06
            case "Blue Goblin": 名 = "水藍小怪"; break;
            case "Muddy Troll": 名 = "泥濘大怪"; break;
            case "Muddy Goblin": 名 = "泥濘小怪"; break;
            case "Dark Red Troll": 名 = "深紅大怪"; break;
            case "Dark Red Goblin": 名 = "深紅小怪"; break;
            case "Dark Blue Troll": 名 = "深藍大怪"; break;
            case "Dark Blue Goblin": 名 = "深藍小怪"; break;

            case "Red-eyed Troll": 名 = "紅目大怪"; break; // version <a05
            case "Red-eyed Goblin": 名 = "紅目小怪"; break;
            case "Purple-eyed Troll": 名 = "紫目大怪"; break;
            case "Purple-eyed Goblin": 名 = "紫目小怪"; break;

            case "Your": 名 = "你"; break; // version a06.0.23
            case "you ": 名 = "你"; break;


        } creature名 = "<color=#" + color + ">" + 名 + "</color>";
        creatureName = "<color=#" + color + ">" + gameObject.name + "</color>";
        AllBodyGood();
    }

    void Update()
    {
        if (HP < 0 && !died) { death.Invoke(); died = true;
            Log.AddLine("you died\n\npress 'g' to restart a new game"); 木.AddLine("你死死了。按G新遊戲。");
        }

    }
    public void Attack(Combat other)
    {   int damage = (int)( power*Random.Range((float)worstHit*stability/100, (float)criticalHit))-other.defense;
        if (damage > 0)
        {   Log.AddLine(creatureName + " attacks " + other.creatureName +/* "'s " + BodyPart() +*/ " and does " + damage.ToString() + " damage!");
            木.AddLine(creature名 + "攻擊" + other.creature名 +/* "'s " + BodyPart() +*/ "造成" + damage.ToString() + "傷害！");
            other.TakeDamage(damage);
            /*if (Random.Range(0, 1) == 0)*/ { Audio.instance.Play("punchWood"); }
            {
                /*bodyState[bodyDamageNum][0] = BodyPart();
                bodyState[bodyDamageNum][1] = BodyPartDamage();
                Log.AddLine(other.creatureName + " " + bodyState[bodyDamageNum][0] + " are " + bodyState[bodyDamageNum][1]+ " by "+ creatureName);
                bodyDamageNum++;*/
                if (addFightdetail) { Log.AddLine(other.creatureName + " " + BodyPart()[0] + " are " + BodyPartDamage() + " by " + creatureName + "!"); }
                if (add詳鬥) { 木.AddLine(creature名+BodyPartDamage()[1]+other.creature名 + BodyPart()[1]+"。"); } //BodyPartDamage()[1] = char
            }
        }
        else {   /*if (Random.Range(0, 1) == 0) */{ Audio.instance.Play("punchAir"); }
            Log.AddLine(creatureName + " attacks " + other.creatureName +/* "'s " + BodyPart() +*/ " but it has no effect!");
            木.AddLine(creature名 + "攻擊" + other.creature名 +/* "'s " + BodyPart() +*/ "但沒用！");}

        if (Random.Range(0, 42)==0) { maxPower++; } // more fight = better fight //
        if (Random.Range(0, 150)==0) { power++; } // more fight = better fight //

        if (temperature<=18) { temperature+=Random.Range(0, 4); } // 0,3
        else { temperature+=Random.Range(-1, 3); } // -1,2
    }

    public void TakeDamage(int v){ HP -= v; /*if (Random.Range(0, 1) == 0)*/ { Audio.instance.Play("playerHit"); }
        if (maxHP-HP>HP) { stability -= (v * ((100 - firmness)/100)); }
        if (stability == 50) { Log.AddLine(creatureName + " is giddy now."); 木.AddLine(creature名 + "頭暈。"); }
        if (stability == 30) { Log.AddLine(creatureName + " is dizzy now."); 木.AddLine(creature名 + "頭昏。"); }
        if (stability == 10) { Log.AddLine(creatureName + " is faint now."); 木.AddLine(creature名 + "暈眩。"); }
        if (stability <= 0) { Log.AddLine(creatureName + " is not able to control their mind fully."); 木.AddLine(creature名 + "昏厥。");
        //if (power <= 0 && power > defense) { power -= 1; } else { defense -= 1; }
        }

        if (temperature>18) { temperature+=Random.Range(-2, 2); } // -2,1
        else { temperature+=Random.Range(-1, 2); } // -1,1
    }
    public int restCount = 0;
    //void TakeDamage(int v) { HP -= v; }
    public void Rest(){ restCount++;
        if (restCount > 5) { Log.AddLine(creatureName + "had enough rest."); 木.AddLine(creature名 + "休息夠了。"); } 
        else { 
            if (maxHP > HP) { HP += Random.Range(healing/2, healing+6);
                if (Random.Range(0, 5)==0) { healing++; }
                Log.AddLine(creatureName + " are healing."); 木.AddLine(creature名 + "在恢復。"); }
            if (stability<100) { stability += 10;  Log.AddLine(creatureName + " look around."); 木.AddLine(creature名 + "提心吊膽。") ; } 
                else { Log.AddLine(creatureName + " feel good."); 木.AddLine(creature名 + "龍精虎猛。"); }
            if (maxPower > power) { power++; Log.AddLine(creatureName + " feel power."); 木.AddLine(creature名 + "孔武有力。"); }
            if (firmness<100 && Random.Range(0,3)==0) { firmness++; } // 1/3 can firmness++
        }
    }

    public void WalkHealing() { int walkHeal;
        if (maxHP > HP/* && temperature<80*/) { walkHeal = Random.Range(~healing*2, healing);
            if (walkHeal > 1) { HP += walkHeal; /*maxHP+=Random.Range(0, 1);*/ 
                if (Random.Range(0, 40)==0) { healing++; } // upgrade healing
                Log.AddLine(creatureName+" healed "+walkHeal+"."); 木.AddLine(creature名+"恢復"+walkHeal+"點。"); ; }
        }

        restCount-=Random.Range(0, 2); // 0,1
        if (temperature>18) { temperature+=Random.Range(-2, 2); } // -2,1
        else { temperature+=Random.Range(-1, 2); } // -1,1
    }

    public string[] BodyPart()
    {   
        string[] uniBodyPart = new string[]{"abdominal", "artery", "belly", "breast", "chest", "forehead", "jaw", "loin", "lung",
            "neck", "occiput", "ribcage", "shin", "shoulder", "spleen", "stomach", "thorax", "throat", "torso", "waist", };
        string[] uni體 = new string[] { "腹", "動脈", "肚", "腔", "脯", "額", "顎", "腰", "肺",
            "頸", "枕骨", "肋", "脛", "肩", "脾臟", "胃", "膛", "喉", "軀幹", "蜂身"};

        string[] duoBodyPart = new string[] { "ankle", "arm", "armpit", "calf", "elbow", "foot", "hip", "leg", "thigh", "wrist" };
        string[] duo體 = new string[] { "踝", "臂", "腋", "腿", "肘", "腳", "臀", "脛", "髀", "腕" };

        string[] duo = { "right", "右" }, thePart = { "a", "啊" };
        int randomUniBody = Random.Range(0, uniBodyPart.Length), randomDuoBody = Random.Range(0, duoBodyPart.Length);//20+(10*2)


        if (0 > Random.Range(-1, 1)){ thePart[0] = uniBodyPart[randomUniBody]; thePart[1] = uni體[randomUniBody];
        } else { if (0 > Random.Range(-1, 1)) { duo[0] = "left"; duo[1] = "左"; }; }
        thePart[0] = duo[0] + " " + duoBodyPart[randomDuoBody]; thePart[1] = duo[1]+duo體[randomDuoBody];

        return thePart;
    }

    public string BodyPartDamage()
    {   string damageLv = "ravaged";
        switch (Random.Range(0, 9)) {
            case 0: damageLv = "debilitated"; break;
            case 1: damageLv = "injured"; break;
            case 2: damageLv = "mangled"; break;
            case 3: damageLv = "bruised"; break;
            case 4: damageLv = "cracked"; break;
            case 5: damageLv = "damaged"; break;
            case 6: damageLv = "wounded"; break;
            case 7: damageLv = "lacerated"; break;
            case 8: damageLv = "shattered"; break;
            case 9: damageLv = "mutilated"; break;
        } return damageLv;
    }
    public void AddFightdetail() { if (addFightdetail) { addFightdetail = false; Log.AddLine("turned off fighting detail. "); } else { addFightdetail = true; Log.AddLine("turned on fighting detail. "); }}
    public void Add詳鬥() {if (add詳鬥) { add詳鬥 = false; 木.AddLine("屏蔽戰鬥細節。"); } else { add詳鬥 = true; 木.AddLine("開啟戰鬥細節。"); }}
    
    public void AllGood() { SetHP(maxHP); AllBodyGood(); Log.AddLine(creatureName + " all good."); 木.AddLine(creature名 + "原地滿血。");}
    public void AllBodyGood() { head = neck = waist = spine = left_arm = right_arm = left_leg = right_leg = true;}
    public void SetHP(int newHP) { HP = newHP; }

}
