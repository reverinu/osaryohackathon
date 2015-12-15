using UnityEngine;
using System.Collections;
using UnityEngine.UI;  ////ここを追加////

public class SelifText : MonoBehaviour
{
    public string text = "";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = text;
    }
}