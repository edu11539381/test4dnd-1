using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector4 position;
    public Combat combat;
    
    MapDrawer gridSize;
    FourDPlayer plr;

    public int moveChanceMax, moveChanceNow, moveCrossDirections; // moveCrossDirections (1-4)


    public SpriteRenderer sprite;

    void Start()
    {
        gridSize = FindObjectOfType<MapDrawer>();
        plr = FindObjectOfType<FourDPlayer>();

        moveChanceNow = UnityEngine.Random.Range(0, moveChanceMax); // startup shuffle (Move Chance)
    }

    void Update()
    {
        transform.position = new Vector3(position.x / gridSize.gridSize + position.w, position.y / gridSize.gridSize + position.z, 0);
        sprite.color = gridSize.Within5(position, plr.position) ? Color.white : new Color(255, 255, 255, 0); // outside the scence is not showen

        if (combat.HP < 0) { print("died"); plr.killCount++;
            
            EnemySpawner.Instance.enemies.Remove(this); Destroy(gameObject);
            MapDrawer.Instance.Spawn2thEnemies();
        }
    }

    public void Move()
    {
        if (!MapDrawer.Instance.Within5(plr.position, position)) return; // out of the scence = no // future maybe update to a better code
        
        if (moveChanceNow<moveChanceMax) { moveChanceNow++; return; } // move chance timer
        else { moveChanceNow = 0; // reset for next movement

            Vector4 dxToPlr = plr.position - position; // direction To Player
            float dxToPlrMag = dxToPlr.magnitude; // old // float distance = Vector4.Distance(plr.position, position);

            if (dxToPlrMag == 0) return; // avoid division 0

            Vector4 b4DxToPlr = new Vector4(Mathf.RoundToInt(dxToPlr.x / dxToPlrMag), Mathf.RoundToInt(dxToPlr.y / dxToPlrMag), Mathf.RoundToInt(dxToPlr.z / dxToPlrMag), Mathf.RoundToInt(dxToPlr.w / dxToPlrMag)); // dxToPlr / distance; //
            //print(b4DxToPlr); // (b,b,b,b) 

            // old // only 1 direction to the player
            // Vector4 b1DxToPlr = GetDirection(new Vector4(Mathf.RoundToInt(dx.x / distance), Mathf.RoundToInt(dx.y / distance), Mathf.RoundToInt(dx.z / distance), Mathf.RoundToInt(dx.w / distance)));
            //print(b1DxToPlr); // (b,0,0,0)

            // new // different enemy can have different directions to the player
            Vector4 nb4DxToPlr = GetDirection(b4DxToPlr, moveCrossDirections);
            //print(nb4DxToPlr); // (x,x,x,x)

            print("mob pos:" + position + " & b4:" + b4DxToPlr + " & n:" + nb4DxToPlr);


            if (position + nb4DxToPlr == plr.position) { combat.Attack(plr.combat); } // if the way it walk is the player, it attacks
            else if (gridSize.tilemap.GetTile(FourToTwo(position + nb4DxToPlr)) == null) { // if out of the map


                // !! // try another way to get closer to player // shit code start
                Vector4 b4DxToPlrHold = b4DxToPlr;

                /*
                for (int i = 0; i < 4; i++) {
                    b4DxToPlr[i] = 0;
                    if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[i] = b4DxToPlrHold[i]; } // if it still out of the map, try other
                    else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                }*//*  try
                    0111
                    1011
                    1101
                    1110 *//*

                b4DxToPlr[0] = 0;
                for (int j = 1; j < 4; j++) {
                    b4DxToPlr[j] = 0;
                    if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[j] = b4DxToPlrHold[j]; } // if it still out of the map, try other
                    else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                }*//*  try
                    0011
                    0101
                    0110 *//*

                b4DxToPlr[0] = b4DxToPlrHold[0];
                b4DxToPlr[1] = 0; b4DxToPlr[2] = 0;
                if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[2] = b4DxToPlrHold[2]; } // if it still out of the map, try other
                else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                b4DxToPlr[3] = 0;
                if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[1] = b4DxToPlrHold[1]; } // if it still out of the map, try other
                else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                b4DxToPlr[2] = 0;
                if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[1] = 0; } // if it still out of the map, try other
                else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                *//*  try
                    1001
                    1010
                    1100 *//*

                for (int i = 1; i < 4; i++) {
                    if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[i-1] = 0; } // if it still out of the map, try other
                    else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                    b4DxToPlr[i] = b4DxToPlrHold[i];
                }*//*  try
                    1000
                    0100
                    0010
                    0001*/


                // don't fuck wall
                b4DxToPlr[1] = b4DxToPlr[2] = b4DxToPlr[3] = 0; // 1000
                for (int i = 1; i < 4; i++) {
                    if (gridSize.tilemap.GetTile(FourToTwo(position + b4DxToPlr)) == null) { b4DxToPlr[i-1] = 0; } // if it still out of the map, try other
                    else if (!EnemySpawner.Instance.EnemyAtPoint(position + b4DxToPlr)) { position += b4DxToPlr; gridSize.UpdateScreen(); return; } // inside the map, just go
                    b4DxToPlr[i] = b4DxToPlrHold[i];
                }/*  try
                    1000
                    0100
                    0010
                    0001*/

            } else if (!EnemySpawner.Instance.EnemyAtPoint(position + nb4DxToPlr)) {position += nb4DxToPlr; gridSize.UpdateScreen();}
            
            //enemy move

        }
    }

    Vector4 GetDirection(Vector4 d) { // old // only 1 direction to the player 
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == 1) return d;
        d.x = 0; // 0111 // also 0xxx = 1
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == 1) return d;
        d.y = 0; // 0011 // also 00xx = 1
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == 1) return d;
        d.z = 0; // 0001 // also 000x = 1
        return d;
    }

    Vector4 GetDirection(Vector4 d, int moveCrossDirections) {

        if (moveCrossDirections<1) { return GetDirection(d); } // 1000~0001

        Vector4 dH = d; // b4DxToPlrHold // holding the date

        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == moveCrossDirections) return d;

        for (int i = 0; i < 4; i++) {
            d[i] = 0; // 0111 ~ 1110 //
            if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == moveCrossDirections) return d;
            else d[i] = dH[i];
        }

        d[0] = 0;
        for (int j = 1; j < 4; j++) {
            d[j] = 0; // 0011 0101 0110//
            if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == moveCrossDirections) return d;
            else d[j] = dH[j];
        }

        d[0] = dH[0];
        d[1] = 0; d[2] = 0; // 1001
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == moveCrossDirections) return d;
        else d[2] = dH[2]; d[3] = 0; // 1010
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == moveCrossDirections) return d;
        else d[1] = dH[1]; d[2] = 0; // 1100
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == moveCrossDirections) return d;
        
        return GetDirection(d); // 1000~0001

    }

    public Vector3Int FourToTwo(Vector4 v4) { //4D projecting to 2D
        return new Vector3Int((int)v4.x + (int)v4.w * gridSize.gridSize, (int)v4.y + (int)v4.z * gridSize.gridSize, 0);
    }
}
