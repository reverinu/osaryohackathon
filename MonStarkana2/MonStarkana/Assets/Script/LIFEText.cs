using UnityEngine;
using System.Collections;
using UnityEngine.UI;  ////ここを追加////

public class LIFEText : MonoBehaviour
{

    //点数を格納する変数
    public int life = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "LIFE " + life;
    }
}