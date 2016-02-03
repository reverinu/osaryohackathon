using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selif : MonoBehaviour {

    public UIManager uimanager;
    public Playercontroller playercontroller;
    public PlayAudio playaudio;



    IEnumerator Selif1()
    {
        uimanager.GetComponent<UIManager>().system = "reverProject";
        yield return new WaitForSeconds(1);
        uimanager.GetComponent<UIManager>().selif = "「ん？」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().system = "@reverinu";
        uimanager.GetComponent<UIManager>().selif = "「なんだこれは」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "「身体が小さくなってるぞ！？」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().system = "since 2015";
        uimanager.GetComponent<UIManager>().selif = "「夢でも見てるのか……」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "「とりあえず、暗すぎて何も見えん。そこのライトを手に取るか」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().system = "Start";
        uimanager.GetComponent<UIManager>().selif = "";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().system = "";
        uimanager.GetComponent<UIManager>().gManual.SetActive(true);
        playercontroller.GetComponent<Playercontroller>().move_check = 1;
        playaudio.GetComponent<PlayAudio>().walking_check = 1;
        yield return new WaitForSeconds(0);
    }

    IEnumerator Selif2()
    {
        uimanager.GetComponent<UIManager>().stamina = 1000;
        uimanager.GetComponent<UIManager>().gLIFEImage.SetActive(true);
        uimanager.GetComponent<UIManager>().gStaminaCircleGauge.SetActive(true);
        yield return new WaitForSeconds(1);
        uimanager.GetComponent<UIManager>().selif = "「よし、これで明るくなったな……」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "「それにしても小さな体にはライトの重さは堪える……」";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator Selif3()
    {
        yield return new WaitForSeconds(1.9f);
        playaudio.GetComponent<PlayAudio>().BullDown();
        uimanager.GetComponent<UIManager>().selif = "「うわっ！」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "上から落ちてきたようだ";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifS()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "左から不気味な音が聞こえてくる";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifS2()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "あの音が聞こえてくる……";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifS3()
    {
        yield return new WaitForSeconds(1);
        uimanager.GetComponent<UIManager>().selif = "「……？」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "音が消えた？";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "追ってきてないようだ";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator Selifst0()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "これ以上走ることができないようだ";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifO()
    {
        uimanager.GetComponent<UIManager>().gLIFEImage.SetActive(false);
        uimanager.GetComponent<UIManager>().gStaminaCircleGauge.SetActive(false);
        uimanager.GetComponent<UIManager>().gManual.SetActive(false);
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "「ここ……までか……」";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator Seliftutorial()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "「俺はライトを手に取るんじゃなかったのか……？」";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifF()
    {
        uimanager.GetComponent<UIManager>().gLIFEImage.SetActive(false);
        uimanager.GetComponent<UIManager>().gStaminaCircleGauge.SetActive(false);
        uimanager.GetComponent<UIManager>().gManual.SetActive(false);
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "「ここまで来たが、何がどうなってるのかいまだにわからない」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "「化け物に遭遇して、何度も追いかけられるし、もうコリゴリだ」";
        yield return new WaitForSeconds(2);
        uimanager.GetComponent<UIManager>().selif = "「誰か助けてくれ……」";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifBull()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "今まで落ちてきたことがなかったのに……";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifMeat()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "肉からは腐乱臭がする……";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }

    IEnumerator SelifGomibako()
    {
        yield return new WaitForSeconds(0);
        uimanager.GetComponent<UIManager>().selif = "ごみ箱がある";
        yield return new WaitForSeconds(3);
        uimanager.GetComponent<UIManager>().selif = "";
    }
}
