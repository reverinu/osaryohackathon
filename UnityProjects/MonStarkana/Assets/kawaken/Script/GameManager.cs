using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    
    public Light spotlight;
    public CameraFilter camerafilter;
    public PlayAudio playaudio;
    public Enemy enemy;
    public Enemy enemy2;
    public Enemy enemy3;
    public Collider enemy2c;
    public Collider enemy3c;
    public GameObject enemyobj;
    public GameObject enemyobj2;
    public GameObject enemyobj3;
    public UIManager uimanager;
    public Selif selif;
    public Playercontroller playercontroller;
    public Item item;
    public Collider ushi;
    public Collider evt1, evt2, evt3;
    public GameObject deslight;
    
    string gameover = "GameOver";
    string gameclear = "GameClear";
    string nothing = "";

    public int evn1 = 1, ev2 = 1, evF = 1, evO = 1, evS = 1, evS2 = 1, evS3 = 1;
    public int evn2 = 1, evn3 = 1, evn4 = 1, evtutorial = 1;



    void Start()
    {
        uimanager.GetComponent<UIManager>().system = nothing;
        uimanager.GetComponent<UIManager>().selif = nothing;
        selif.StartCoroutine("Selif1");
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (uimanager.GetComponent<UIManager>().stamina > 0)
            {
                    if (evtutorial == 0) 
                {
                    playaudio.GetComponent<PlayAudio>().walking_check2 = 1;
                }
                uimanager.GetComponent<UIManager>().stamina--;
            }
            else
            {
                playaudio.GetComponent<PlayAudio>().walking_check2 = 2;
                selif.StartCoroutine("Selifst0");
            }

            playaudio.GetComponent<PlayAudio>().Walking();
        }
        else
        {
            if (uimanager.GetComponent<UIManager>().stamina < 1500)
            {
                uimanager.GetComponent<UIManager>().stamina += 3;
            }
            else if (uimanager.GetComponent<UIManager>().stamina >= 1501)
            {
                uimanager.GetComponent<UIManager>().stamina = 1500;
            }
        }

        if (enemy.GetComponent<Enemy>().distance < enemy.GetComponent<Enemy>().limitDistance && evS == 2 && evn1 == 0 && evn3 == 0)
        {
            evS = 0;
            selif.StartCoroutine("SelifS2");
        }
        else if (enemy.GetComponent<Enemy>().distance >= enemy.GetComponent<Enemy>().limitDistance && evS == 0 && evn1 == 0 && evn3 == 0)
        {
            evS = 2;
            selif.StartCoroutine("SelifS3");
        }

        if (enemy2.GetComponent<Enemy>().distance < enemy2.GetComponent<Enemy>().limitDistance && evS2 == 2 && evn1 == 0)
        {
            evS2 = 0;
            selif.StartCoroutine("SelifS2");
        }
        else if (enemy2.GetComponent<Enemy>().distance >= enemy2.GetComponent<Enemy>().limitDistance && evS2 == 0 && evn1 == 0)
        {
            evS2 = 2;
            enemyobj2.SetActive(false);
            selif.StartCoroutine("SelifS3");
        }

        if (enemy3.GetComponent<Enemy>().distance < enemy3.GetComponent<Enemy>().limitDistance && evS3 == 2 && evn4 == 0)
        {
            evS3 = 0;
            selif.StartCoroutine("SelifS2");
        }
        else if (enemy3.GetComponent<Enemy>().distance >= enemy3.GetComponent<Enemy>().limitDistance && evS3 == 0 && evn4 == 0)
        {
            evS3 = 2;
            selif.StartCoroutine("SelifS3");
        }


        if (uimanager.GetComponent<UIManager>().life <= 30)
        {
            camerafilter.GetComponent<CameraFilter>().life_check = 1;
        }


    }

    
    

    //衝突している間呼ばれる関数（完全に静止したオブジェクトでは呼ばれない） 
    void OnCollisionStay(Collision col)
    {
        //SendMessage(“関数名”) で対象が持っているスクリプトが持っている関数を呼び出すことができる  
        if (col.gameObject.tag == "Enemy")
        {
            
            if (uimanager.GetComponent<UIManager>().life > 0)
            {
                uimanager.GetComponent<UIManager>().life--;
            }
            else if (uimanager.GetComponent<UIManager>().life == 0 && evO == 1)
            {
                evO = 0;
                selif.StartCoroutine("SelifO");
                GameOver();
            }
        }

        if (col.gameObject.tag == "Medicine" && evF == 1)
        {
            evF = 0;
            selif.StartCoroutine("SelifF");
            GameClear();
            
        }

        if (col.gameObject.tag == "EvTutorial" && evtutorial == 1)
        {

            selif.StartCoroutine("Seliftutorial");
            
        }

    }

    //注意：こちらは静止していても中にいる間は呼ばれ続ける 
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ev1" && evn1 == 1)
        {
            evn1 = 0;
            selif.StartCoroutine("Selif3");
            ushi.attachedRigidbody.useGravity = true;
        }

        if (col.gameObject.tag == "Ev2" && evn2 == 1)
        {
            evn2 = 0;
            evS = 0;
            evS2 = 0;
            enemyobj2.SetActive(true);
            enemy2c.attachedRigidbody.useGravity = true;
            enemy2.GetComponent<Enemy>().move_check = 0;
            selif.StartCoroutine("SelifS");
        }

        if (col.gameObject.tag == "Ev3" && evn3 == 1)
        {
            evn3 = 0;
            evS = 0;
            evS2 = 0;
            enemyobj.SetActive(true);
            enemy.GetComponent<Enemy>().move_check = 0;
            selif.StartCoroutine("SelifS2");
        }

        if (col.gameObject.tag == "Ev4" && evn4 == 1)
        {
            evn4 = 0;
            evS = 0;
            evS2 = 0;
            evS3 = 0;
            enemyobj3.SetActive(true);
            enemy3c.attachedRigidbody.useGravity = true;
            enemy3.GetComponent<Enemy>().move_check = 0;
            selif.StartCoroutine("SelifS2");
        }
    }


    void GameOver()
    {
        playercontroller.GetComponent<Playercontroller>().move_check = 0;
        uimanager.GetComponent<UIManager>().system = gameover;

    }

    void GameClear()
    {
        playercontroller.GetComponent<Playercontroller>().move_check = 0;
        camerafilter.GetComponent<CameraFilter>().life_check = 0;
        enemy.GetComponent<Enemy>().move_check = 1;
        enemy2.GetComponent<Enemy>().move_check = 1;
        enemy3.GetComponent<Enemy>().move_check = 1;
        uimanager.GetComponent<UIManager>().system = gameclear;
    }
    /*
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
        uimanager.GetComponent<UIManager>().gLifegauge.SetActive(true);
        uimanager.GetComponent<UIManager>().gStaminagauge.SetActive(true);
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
        uimanager.GetComponent<UIManager>().gLifegauge.SetActive(false);
        uimanager.GetComponent<UIManager>().gStaminagauge.SetActive(false);
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
        uimanager.GetComponent<UIManager>().gLifegauge.SetActive(false);
        uimanager.GetComponent<UIManager>().gStaminagauge.SetActive(false);
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
     * */



}

