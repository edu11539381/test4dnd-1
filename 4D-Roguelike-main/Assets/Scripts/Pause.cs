using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour {
    public void Load(int index) { SceneManager.LoadScene(index); } //menu button use it

    public void Quit() { Application.Quit(); } //menu button use it

    void Update() {
        /*if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Alpha0)) { Load(0); }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { Load(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { Load(2); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { Load(3); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { Load(4); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { Load(5); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { Load(6); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { Load(7); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { Load(8); }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { Load(9); }
        if (Input.GetKeyDown(KeyCode.Minus)) { Load(10); }
        if (Input.GetKeyDown(KeyCode.Equals)) { Load(11); }*/
    }
}
