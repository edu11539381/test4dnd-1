using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


// use only at log
public class Log : MonoBehaviour
{
    public static Log Instance;
    public string text;
    public TextMeshProUGUI front;

    void Start()
    {
        Instance = this;
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            text = "ground floor \n\nusing <color=#22FF22>wasd+eqcz</color> to move in 4d dungeon!\n\nadditionally using <color=#22FF22>arrow keys+ijkl</color> to move is also ok! \n\npress <color=#22FF22>x</color> to rest \npress <color=#22FF22>g</color> new game \npress <color=#22FF22>m</color> more mobs \npress <color=#22FF22>v</color> more guides \n\nmore in itch.io description";
        } else { text = "you came to b" + SceneManager.GetActiveScene().buildIndex+ "floor \n\npress <color=#22FF22>x</color> to rest \npress <color=#22FF22>g</color> new game \npress <color=#22FF22>m</color> more mobs \n\nmore in itch.io"; }
    }
    void Update(){front.text = text;}
    public static void AddLine(string line){Instance.text = line.ToLower() + "\n\n" + Instance.text;}
}
