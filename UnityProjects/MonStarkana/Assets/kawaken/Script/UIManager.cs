using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //点数を格納する変数
    public int stamina = 0;
    public string selif = "";
    public int life = 100;
    public string system = "";
    public float lifesize = 1f;
    public int heartbeatcheck = 1;
    public Text selifText;
    public Text systemText;
    public Item item;
    public Image StaminaCircleGauge;
    public Image LIFEImage;
    public RectTransform rLIFEImage;
    public GameObject gStaminaCircleGauge;
    public GameObject gLIFEImage;
    public GameObject gManual;

    // Update is called once per frame
    void Update () {
        if (item.GetComponent<Item>().light_check == 1)
        {
            StaminaCircleGauge.fillAmount = stamina / 1500f;
            LIFEImage.GetComponent<Image>().color = new Color((float)life / 100f,1,1,1);
            rLIFEImage.GetComponent<RectTransform>().localScale = new Vector3(lifesize,lifesize,1f);
            if (heartbeatcheck == 1)
            {
                heartbeatcheck = 0;
                StartCoroutine("HeartBeat");
                
            }
        }
        selifText.GetComponent<Text>().text = selif;
        systemText.GetComponent<Text>().text = system;
    }


    IEnumerator HeartBeat()
    {
        yield return new WaitForSeconds(0);
        lifesize = 0.875f;
        yield return new WaitForSeconds(0.2f);
        lifesize = 1f;
        yield return new WaitForSeconds(0.5f);
        heartbeatcheck = 1;
    }
}
