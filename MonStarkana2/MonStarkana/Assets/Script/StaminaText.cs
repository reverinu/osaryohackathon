using UnityEngine;
using System.Collections;
using UnityEngine.UI;  ////ここを追加////

public class StaminaText : MonoBehaviour
{

    //点数を格納する変数
    public int st = 0;
    public int light_check = 0;

    // Update is called once per frame
    void Update()
    {
        if (light_check == 1)
        {
            this.GetComponent<Text>().text = "Stamina " + st;
        }
    }
}