using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    //点数を格納する変数
    public int stamina = 0;
    public string selif = "";
    public int life = 100;
    public string system = "";
    public Text selifText;
    public Text systemText;
    public Item item;
    public RectTransform LIFEGauge;
    public RectTransform StaminaGauge;
    public GameObject gLifegauge;
    public GameObject gStaminagauge;
    public GameObject gManual;

    // Update is called once per frame
    void Update () {
        if (item.GetComponent<Item>().light_check == 1)
        {
            StaminaGauge.GetComponent<RectTransform>().sizeDelta = new Vector2(stamina / 5, 20);
            LIFEGauge.GetComponent<RectTransform>().sizeDelta = new Vector2(life*3, 20);
        }
        selifText.GetComponent<Text>().text = selif;
        systemText.GetComponent<Text>().text = system;
    }
}
