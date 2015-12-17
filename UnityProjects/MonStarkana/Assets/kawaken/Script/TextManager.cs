using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    //点数を格納する変数
    public int stamina = 0;
    public string selif = "";
    public int life = 100;
    public string system = "";
    public Text staminaText;
    public Text lifeText;
    public Text selifText;
    public Text systemText;
    public Item item;


    // Update is called once per frame
    void Update () {
        if (item.GetComponent<Item>().light_check == 1)
        {
            staminaText.GetComponent<Text>().text = "Stamina " + stamina;
            lifeText.GetComponent<Text>().text = "LIFE " + life;
        }
        selifText.GetComponent<Text>().text = selif;
        systemText.GetComponent<Text>().text = system;
    }
}
