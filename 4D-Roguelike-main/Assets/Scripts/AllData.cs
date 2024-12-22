using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllData : MonoBehaviour
{
    public static AllData Instance;
    public static bool chineseThings, englishThings;
    public static string playerGender; //start mode
    public static int plr_HP, plr_maxHP,
        plr_power, plr_maxPower,
        plr_defense,
        plr_healing,
        plr_firmness,
        plr_stability,
        plr_perception,
        plr_temperature,
        plr_moveChance,

        plr_killCount,
        plr_stepCount,
        plr_healCount, // unused
        plr_levelCount,
        plr_cheatCount,

        plr_deadCount = 0;

    public static int[] playerDataSet, plr_cheatCountSet, 
        cheatCountSet, hpLogSaid, temperatureLogSaid, stepCountSaid;


    void Awake() {
        if (SceneManager.GetActiveScene().buildIndex==0) {
            plr_maxHP = /*#if (playerGender=="female")*/ Random.Range(45, 64);
            plr_HP = plr_maxHP-Random.Range(0, 5);
            plr_power = /*#if (playerGender=="female")*/ Random.Range(4, 9);
            plr_maxPower = (int)1.5*plr_power;
            plr_defense = Random.Range(0, 2);
            plr_healing = Random.Range(1, 3);
            plr_firmness = Random.Range(40, 101);
            plr_stability = Random.Range(40, 101);
            plr_perception = Random.Range(90, 101);
            plr_temperature = Random.Range(18, 30);

            plr_moveChance = 1;

            plr_killCount = 0; // 1? :)))
            plr_stepCount = 0;
            plr_healCount = 0; // unused
            plr_levelCount = 0;
            plr_cheatCount = 0;

            chineseThings = englishThings = true;

            hpLogSaid = temperatureLogSaid = stepCountSaid = new int[6];
            

        }
    }
}
