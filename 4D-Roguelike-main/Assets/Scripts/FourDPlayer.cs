using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Drawing;
using NUnit.Framework;
using System.Linq;

[System.Serializable]
public class KeyAxis{public KeyCode n, p;}

//public class KeyAxis2{public KeyCode nn, np, pn, pp;}
//public class UniMoveWay {public KeyCode nnnn, nnnp, nnpn, nnpp, npnn, npnp, nppn, nppp, pnnn, pnnp, pnpn, pnpp, ppnn, ppnp, pppn, pppp;}

/*[System.Serializable]
public class TiltedAxis { public KeyCode n, p; }*/

[System.Serializable]
public class FourDkeys
{   public KeyAxis X, Y, Z, W; //straight 4D moving axis
    /*public KeyAxis2 XY, XZ, XW, YZ, YW, ZW; //tilted 4D moving axis
    public KeyAxis3 XYZ, XYW, XZW, YZW; //tilted 4D moving axis
    public KeyAxis4 XYZW; //tilted 4D moving axis √4 = 2 */
}

public class FourDPlayer : MonoBehaviour
{
    public static FourDPlayer instance;
    public Combat combat;
    public CameraFollow follow;

    [Header("Movement")]
    public Vector4 position, lookfarPosition;
    public FourDkeys moveKeys, moveKeysPlus;//additional moving keys
    bool bPlus, bEQCZ, bQZEC, bIJKL;//check what type of move
    public FourDkeys EQCZ_moveSet;//EQCZ/Left Hunch moving keys
    public FourDkeys QZEC_moveSet;//QZEC/Left Rhaskia moving keys
    public FourDkeys IJKL_moveSet;//IJKL/Igor Galochkin moving keys
    public Tilemap tilemap;
    public Tile wallTile;

    public GameObject OpenChat, chineseThings, chineseThings2, englishThings, englishThings2;
    public GameObject[] colorSquareXY_ZW;

    public int playColorSquare;
    
    public Vector4 x1, y1, z1, w1; //orthodoxy 4D moving 1 square

    MapDrawer gridSize;

    [Header("Sprites")]
    public SpriteRenderer sprite;
    public Sprite deathSprite;
    bool dead;
    public GameObject deadRed;

    [Header("Data")]
    public int killCount;
    public int stepCount;
    public int healCount;
    public int levelCount;
    public int cheatCount;
    public int deadCount;

    [Header("Player Data")]
    //public int HP, maxHP, power, maxPower, defense, healing, firmness, stability, perception, temperature;
    public int[] cheatCountSet, hpLogSaid, temperatureLogSaid, stepCountSaid;
    public string colorHP = "FFFFFF", colorTemperature = "FFFFFF";

    void Start()
    {   gridSize = FindObjectOfType<MapDrawer>();
        moveKeys = EQCZ_moveSet; bPlus = bEQCZ = true; bQZEC = bIJKL = false;
        Audio.instance.playBGM(Random.Range(1, Audio.instance.BGM.Length+1));

        combat.HP = AllData.plr_HP;
        combat.maxHP = AllData.plr_maxHP;
        combat.power = AllData.plr_power;
        combat.maxPower = AllData.plr_maxPower;
        combat.defense = AllData.plr_defense;
        combat.healing = AllData.plr_healing;
        combat.firmness = AllData.plr_firmness;
        combat.stability = AllData.plr_stability;
        combat.perception = AllData.plr_perception;
        combat.temperature = AllData.plr_temperature;

        killCount = AllData.plr_killCount;
        stepCount = AllData.plr_stepCount;
        healCount = AllData.plr_healCount; // unused
        cheatCount = AllData.plr_cheatCount;
        levelCount = AllData.plr_levelCount;

        //foreach (var i in cheatCountSet) { cheatCountSet[i] = AllData.plr_cheatCountSet[i]; }

        deadCount = AllData.plr_deadCount;

        hpLogSaid = AllData.hpLogSaid;
        temperatureLogSaid = AllData.temperatureLogSaid;
        stepCountSaid = AllData.stepCountSaid;



        if (!AllData.chineseThings) { chineseThings.SetActive(false); chineseThings2.SetActive(false); } else { chineseThings.SetActive(true); chineseThings2.SetActive(true); } 
        if (!AllData.englishThings) { englishThings.SetActive(false); englishThings2.SetActive(false); } else { englishThings.SetActive(true); englishThings2.SetActive(true); }


    }


    void Update()
    {
        ChatManager();
        if (OpenChat.active) return;
        GobleInputManager();
        if (deadRed.active) return;
        if (dead) { deadRed.SetActive(true); sprite.sprite = deathSprite; }
        InputManager();
        transform.position = new Vector3(position.x / gridSize.gridSize + position.w, position.y / gridSize.gridSize + position.z, 0);

        if (stepCount==100 && stepCountSaid[0]==0) { stepCountSaid[0]++; Log.AddLine("<color=#CCAA34>a hundred steps.</color>"); 木.AddLine("<color=#CCAA34>百步探索。</color>"); }
        else if (stepCount==1000 && stepCountSaid[1]==0) { stepCountSaid[1]++; Log.AddLine("<color=#CCAA34>walk mile.</color>"); 木.AddLine("<color=#CCAA34>千步者。</color>");}
        else if (stepCount==2000 && stepCountSaid[2]==0) { stepCountSaid[2]++; Log.AddLine("<color=#CCAA34>walk miles.</color>"); 木.AddLine("<color=#CCAA34>二千步王。</color>"); }
        else if (stepCount==10000 && stepCountSaid[3]==0) { stepCountSaid[3]++; Log.AddLine("<color=#CCAA34>myriad steps.</color>"); 木.AddLine("<color=#CCAA34>萬步大帝。</color>"); }
    
        if (combat.maxHP-combat.HP<combat.HP) { colorHP = "FCFCFC"; } // normal
        else if (combat.maxHP-combat.HP>combat.HP&&combat.HP>=10) { colorHP = "FCB2B2"; } // >=10 and <half
        //else if (combat.HP<10&&combat.HP>=2) { colorHP = "FC2B2B"; } // HP <10
        else if (combat.HP<=10) { colorHP = "FC0101"; if (hpLogSaid[0]==0) { hpLogSaid[0]++;  Log.AddLine("<color=#FC0101>escape the death!</color>"); 木.AddLine("<color=#FC0101>絶路逢生！</color>"); } } // HP <=1

        if (combat.temperature<=35&&combat.temperature>=5) { colorTemperature = "FCFCFC"; } // normal
        else if (combat.temperature<5&&combat.temperature>=0) { colorTemperature = "EEEEFF"; }
        else if (combat.temperature<0&&combat.temperature>=-10) { colorTemperature = "CCCCFF"; if (temperatureLogSaid[0]==0) { temperatureLogSaid[0]++; Log.AddLine("<color=#CCCCFF>a bit cold.</color>"); 木.AddLine("<color=#CCCCFF>有點冷。</color>");  }}
        else if (combat.temperature<-10&&combat.temperature>=-20) { colorTemperature = "AAAAFF"; if (temperatureLogSaid[1]==0) { temperatureLogSaid[1]++; Log.AddLine("<color=#AAAAFF>frigid and piercing.</color>"); 木.AddLine("<color=#CCCCFF>冷颼颼的，切骨之寒。</color>"); }}
        else if (combat.temperature<-20&&combat.temperature>=-30) { colorTemperature = "6666FF"; if (temperatureLogSaid[2]==0) { temperatureLogSaid[2]++; Log.AddLine("<color=#6666FF>icy hand.</color>"); 木.AddLine("<color=#6666FF>手像冰一樣。</color>"); }}
        else if (combat.temperature<-30) { colorTemperature = "2222FF"; } // too cold
        else if (combat.temperature>35&&combat.temperature<=45) { colorTemperature = "FFDDDD"; }
        else if (combat.temperature>45&&combat.temperature<=55) { colorTemperature = "FF9999"; }
        else if (combat.temperature>55) { colorTemperature = "FF4444"; } // too hot
    }

    public void InputManager()
    {   if (!Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.LeftControl))
        {   if (Input.GetKeyDown(moveKeys.X.p)) { Move(x1); }
            if (Input.GetKeyDown(moveKeys.X.n)) { Move(-x1); }

            if (Input.GetKeyDown(moveKeys.Y.p)) { Move(y1); }
            if (Input.GetKeyDown(moveKeys.Y.n)) { Move(-y1); }

            if (Input.GetKeyDown(moveKeys.Z.p)) { Move(z1); }
            if (Input.GetKeyDown(moveKeys.Z.n)) { Move(-z1); }

            if (Input.GetKeyDown(moveKeys.W.p)) { Move(w1); }
            if (Input.GetKeyDown(moveKeys.W.n)) { Move(-w1); }

            if (Input.GetKeyDown(moveKeysPlus.X.p)) { Move(x1); }
            if (Input.GetKeyDown(moveKeysPlus.X.n)) { Move(-x1); }

            if (Input.GetKeyDown(moveKeysPlus.Y.p)) { Move(y1); }
            if (Input.GetKeyDown(moveKeysPlus.Y.n)) { Move(-y1); }

            if (Input.GetKeyDown(moveKeysPlus.Z.p)) { Move(z1); }
            if (Input.GetKeyDown(moveKeysPlus.Z.n)) { Move(-z1); }

            if (Input.GetKeyDown(moveKeysPlus.W.p)) { Move(w1); }
            if (Input.GetKeyDown(moveKeysPlus.W.n)) { Move(-w1); }
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetKeyDown(KeyCode.C)) { if (chineseThings.active) { chineseThings.SetActive(false); chineseThings2.SetActive(false); } else { chineseThings.SetActive(true); chineseThings2.SetActive(true); } }
            if (Input.GetKeyDown(KeyCode.E)) { if (englishThings.active) { englishThings.SetActive(false); englishThings2.SetActive(false); } else { englishThings.SetActive(true); englishThings2.SetActive(true); } }
            if (Input.GetKeyDown(KeyCode.L)) { levelCount++; SaveAllData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); Audio.instance.Play("stair"); print("down level");
            Log.AddLine("you need key to go down"); 木.AddLine("你要用鑰匙才能下去。");}
            if (Input.GetKeyDown(KeyCode.U)) { levelCount--; SaveAllData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1); Audio.instance.Play("stair"); print("up level");}

        } else {
            lookfarPosition = position;
            /*if (Input.GetKeyDown(moveKeys.X.p)) { follow.LookFarXr(); }
            if (Input.GetKeyDown(moveKeys.X.n)) { follow.LookFarXl(); }

            if (Input.GetKeyDown(moveKeys.Y.p)) { follow.LookFarYr(); }
            if (Input.GetKeyDown(moveKeys.Y.n)) { follow.LookFarYl(); }

            if (Input.GetKeyDown(moveKeys.Z.p)) { follow.LookFarZr(); }
            if (Input.GetKeyDown(moveKeys.Z.n)) { follow.LookFarZl(); }

            if (Input.GetKeyDown(moveKeys.W.p)) { follow.LookFarWr(); }
            if (Input.GetKeyDown(moveKeys.W.n)) { follow.LookFarWl(); }*/



            /*if (Input.GetKeyDown(moveKeys.X.p)) { LookFar(x1); }
            if (Input.GetKeyDown(moveKeys.X.n)) { LookFar(-x1); }

            if (Input.GetKeyDown(moveKeys.Y.p)) { LookFar(y1); }
            if (Input.GetKeyDown(moveKeys.Y.n)) { LookFar(-y1); }

            if (Input.GetKeyDown(moveKeys.Z.p)) { LookFar(z1); }
            if (Input.GetKeyDown(moveKeys.Z.n)) { LookFar(-z1); }

            if (Input.GetKeyDown(moveKeys.W.p)) { LookFar(w1); }
            if (Input.GetKeyDown(moveKeys.W.n)) { LookFar(-w1); }

            if (Input.GetKeyDown(moveKeysPlus.X.p)) { LookFar(x1); }
            if (Input.GetKeyDown(moveKeysPlus.X.n)) { LookFar(-x1); }

            if (Input.GetKeyDown(moveKeysPlus.Y.p)) { LookFar(y1); }
            if (Input.GetKeyDown(moveKeysPlus.Y.n)) { LookFar(-y1); }

            if (Input.GetKeyDown(moveKeysPlus.Z.p)) { LookFar(z1); }
            if (Input.GetKeyDown(moveKeysPlus.Z.n)) { LookFar(-z1); }

            if (Input.GetKeyDown(moveKeysPlus.W.p)) { LookFar(w1); }
            if (Input.GetKeyDown(moveKeysPlus.W.n)) { LookFar(-w1); }

*/

        }
        //if (Input.GetKeyUp(KeyCode.LeftShift)) { lookfarPosition = position; LookBack(); }




        /*if (Input.GetKeyDown(KeyCode.Alpha6)) { CameraFollow.Instance.LookFarXr(); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { follow.LookFarXl(); }*/


        
        if (Input.GetKeyDown(KeyCode.X)) { EnemySpawner.Instance.MoveEnemies(); Rest1(); }
        if (Input.GetKeyDown(KeyCode.M)) { MapDrawer.Instance.Spawn2thEnemies(); }

        if (Input.GetKeyDown(KeyCode.Y)) { combat.AllGood(); cheatCount++; }

        
    }

    public void GobleInputManager()
    {   if (Input.GetKeyDown(KeyCode.G)) { RestartGame0(); AllData.plr_deadCount = deadCount; } //save deadCount
        
        if (Input.GetKeyDown(KeyCode.Minus)) { Audio.instance.playBGM(-1); }
        if (Input.GetKeyDown(KeyCode.Equals)) { Audio.instance.playBGM(1); }
        if (Input.GetKeyDown(KeyCode.Backspace)) { Audio.instance.StopBGM(); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { moveKeys = EQCZ_moveSet;
            if (bIJKL) { moveKeysPlus = new FourDkeys(); }
            bEQCZ = true; bPlus = bQZEC = bIJKL = false;
            木.AddLine("以單手<color=#22FF22>WASD+EQCZ</color>移動！");
            Log.AddLine("Left Hunch moving keys: <color=#22FF22>WASD+EQCZ</color>");
        } if (Input.GetKeyDown(KeyCode.Alpha2)) { moveKeys = QZEC_moveSet;
            if (bIJKL) { moveKeysPlus = new FourDkeys(); }
            bQZEC = true; bPlus = bEQCZ = bIJKL = false;
            木.AddLine("以單手<color=#22FF22>WASD+QZEC</color>移動！");
            Log.AddLine("Left Hunch moving keys: <color=#22FF22>WASD+QZEC</color>");
        } if (Input.GetKeyDown(KeyCode.Alpha3)) { moveKeys = IJKL_moveSet;
            if (bIJKL) { moveKeysPlus = new FourDkeys(); }
            bIJKL = true; bPlus = bEQCZ = bQZEC = false;
            木.AddLine("以<color=#22FF22>WASD+IJKL</color>移動！");
            Log.AddLine("Igor Galochkin moving keys: <color=#22FF22>WASD+IJKL</color>");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) { if (combat.healing>=4) { combat.healing++; } else { combat.healing = 4; } cheatCount++; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { combat.HP+=5; combat.maxHP+=5; cheatCount++; }

        if (Input.GetKeyDown(KeyCode.Alpha8)) { EnemySpawner.Instance.KillAllEnemies(); cheatCount++; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { combat.AddFightdetail(); }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { combat.Add詳鬥(); }

        if (Input.GetKeyDown(KeyCode.LeftBracket)) { if (colorSquareXY_ZW[0].active) { colorSquareXY_ZW[0].SetActive(false); } else { colorSquareXY_ZW[0].SetActive(true); } playColorSquare = 0; }
        if (Input.GetKeyDown(KeyCode.RightBracket)) { if (colorSquareXY_ZW[1].active) { colorSquareXY_ZW[1].SetActive(false); } else { colorSquareXY_ZW[1].SetActive(true); } playColorSquare = 1; }
        if (Input.GetKeyDown(KeyCode.Semicolon)) { if (colorSquareXY_ZW[2].active) { colorSquareXY_ZW[2].SetActive(false); } else { colorSquareXY_ZW[2].SetActive(true); } playColorSquare = 2; }
        if (Input.GetKeyDown(KeyCode.Backslash)) { if (colorSquareXY_ZW[3].active) { colorSquareXY_ZW[3].SetActive(false); } else { colorSquareXY_ZW[3].SetActive(true); } playColorSquare = 3; }
        if (Input.GetKeyDown(KeyCode.Comma)) { if (colorSquareXY_ZW[playColorSquare * 2 + 4].active) { colorSquareXY_ZW[playColorSquare * 2 + 4].SetActive(false); } else { colorSquareXY_ZW[playColorSquare * 2 + 4].SetActive(true); } }
        if (Input.GetKeyDown(KeyCode.Period)) { if (colorSquareXY_ZW[playColorSquare * 2 + 5].active) { colorSquareXY_ZW[playColorSquare * 2 + 5].SetActive(false); } else { colorSquareXY_ZW[playColorSquare * 2 + 5].SetActive(true); } }
        if (Input.GetKeyDown(KeyCode.Slash)) { if (colorSquareXY_ZW[playColorSquare].active) { colorSquareXY_ZW[playColorSquare].SetActive(false); } else { colorSquareXY_ZW[playColorSquare].SetActive(true); } }}
    public void ChatManager(){if (Input.GetKeyDown(KeyCode.V)) { if (OpenChat.active) { OpenChat.SetActive(false); } else { OpenChat.SetActive(true); } }}
    void Move(Vector4 newPosition) {
/*        if (EnemySpawner.Instance.EnemyAtClost(position + newPosition)) {
            combat.Attack(EnemySpawner.Instance.GetClostEnemy(position + newPosition).combat); }*/
        if (EnemySpawner.Instance.EnemyAtPoint(position + newPosition)) {
            combat.Attack(EnemySpawner.Instance.GetEnemy(position + newPosition).combat);
        } else if (StairBuild.Instance.TouchDownStair(position + newPosition)) { // go Down Stair
            levelCount++; SaveAllData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); Audio.instance.Play("stair"); print("down level");
            Log.AddLine("you need key to go down"); 木.AddLine("你要用鑰匙才能下去。");}
        else if (StairBuild.Instance.TouchUpStair(position + newPosition) && SceneManager.GetActiveScene().buildIndex != 0) { // go Up Stair
            levelCount--; SaveAllData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1); Audio.instance.Play("stair"); print("up level");}
        else if (tilemap.GetTile(FourToTwo(position + newPosition)) != null) {
            position += newPosition; gridSize.UpdateScreen(); stepCount++; combat.WalkHealing(); }
        EnemySpawner.Instance.MoveEnemies();}

    /*void LookFar(Vector4 newPosition) {
        if (tilemap.GetTile(FourToTwo(position + newPosition)) != null) {
            lookfarPosition += newPosition; gridSize.UpdateScreen();
        }
    }
    void LookBack() {
        lookfarPosition = position; gridSize.UpdateScreen();
    }*/

    public void RestartGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); print("RestartGame"); }
    public void RestartGame0() { SceneManager.LoadScene(0); print("RestartGame0"); }
    public Vector3Int FourToTwo(Vector4 v4){ return new Vector3Int((int)v4.x + (int)v4.w * gridSize.gridSize, (int)v4.y + (int)v4.z * gridSize.gridSize, 0);}
    void Rest1() { combat.Rest(); }
    public void Death()
    {   dead = true;
        combat.head = combat.neck = combat.waist = combat.spine = combat.left_arm = combat.right_arm = combat.left_leg = combat.right_leg = false;

        sprite.sprite = deathSprite;

        Log.AddLine("You Died...");
        deadCount++;
    }//unused

    public void SaveAllData() {
        AllData.plr_HP = combat.HP;
        AllData.plr_maxHP = combat.maxHP;
        AllData.plr_power = combat.power;
        AllData.plr_maxPower = combat.maxPower;
        AllData.plr_defense = combat.defense;
        AllData.plr_healing = combat.healing;
        AllData.plr_firmness = combat.firmness;
        AllData.plr_stability = combat.stability;
        AllData.plr_perception = combat.perception;
        AllData.plr_temperature = combat.temperature;

        AllData.plr_killCount = killCount; 
        AllData.plr_stepCount = stepCount;
        AllData.plr_healCount = healCount; // unused
        AllData.plr_levelCount = levelCount;
        AllData.plr_cheatCount = cheatCount;

        AllData.plr_deadCount = deadCount;

        AllData.chineseThings = chineseThings.active;
        AllData.englishThings = englishThings.active;
        
    }


}
