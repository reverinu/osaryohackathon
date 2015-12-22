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
    public TextManager textmanager;
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
        textmanager.GetComponent<TextManager>().system = nothing;
        textmanager.GetComponent<TextManager>().selif = nothing;
        StartCoroutine("Selif1");
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (textmanager.GetComponent<TextManager>().stamina > 0)
            {
                    if (evtutorial == 0) 
                {
                    playaudio.GetComponent<PlayAudio>().walking_check2 = 1;
                }
                textmanager.GetComponent<TextManager>().stamina--;
            }
            else
            {
                playaudio.GetComponent<PlayAudio>().walking_check2 = 2;
                StartCoroutine("Selifst0");
            }

            playaudio.GetComponent<PlayAudio>().Walking();
        }
        else
        {
            if (textmanager.GetComponent<TextManager>().stamina < 1500)
            {
                textmanager.GetComponent<TextManager>().stamina += 3;
            }
            else if (textmanager.GetComponent<TextManager>().stamina >= 1501)
            {
                textmanager.GetComponent<TextManager>().stamina = 1500;
            }
        }

        if (enemy.GetComponent<Enemy>().distance < enemy.GetComponent<Enemy>().limitDistance && evS == 2 && evn1 == 0 && evn3 == 0)
        {
            evS = 0;
            StartCoroutine("SelifS2");
        }
        else if (enemy.GetComponent<Enemy>().distance >= enemy.GetComponent<Enemy>().limitDistance && evS == 0 && evn1 == 0 && evn3 == 0)
        {
            evS = 2;
            StartCoroutine("SelifS3");
        }

        if (enemy2.GetComponent<Enemy>().distance < enemy2.GetComponent<Enemy>().limitDistance && evS2 == 2 && evn1 == 0)
        {
            evS2 = 0;
            StartCoroutine("SelifS2");
        }
        else if (enemy2.GetComponent<Enemy>().distance >= enemy2.GetComponent<Enemy>().limitDistance && evS2 == 0 && evn1 == 0)
        {
            evS2 = 2;
            enemyobj2.SetActive(false);
            StartCoroutine("SelifS3");
        }

        if (enemy3.GetComponent<Enemy>().distance < enemy3.GetComponent<Enemy>().limitDistance && evS3 == 2 && evn4 == 0)
        {
            evS3 = 0;
            StartCoroutine("SelifS2");
        }
        else if (enemy3.GetComponent<Enemy>().distance >= enemy3.GetComponent<Enemy>().limitDistance && evS3 == 0 && evn4 == 0)
        {
            evS3 = 2;
            StartCoroutine("SelifS3");
        }


        if (textmanager.GetComponent<TextManager>().life <= 30)
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
            
            if (textmanager.GetComponent<TextManager>().life > 0)
            {
                textmanager.GetComponent<TextManager>().life--;
            }
            else if (textmanager.GetComponent<TextManager>().life == 0 && evO == 1)
            {
                evO = 0;
                StartCoroutine("SelifO");
                GameOver();
            }
        }

        if (col.gameObject.tag == "Medicine" && evF == 1)
        {
            evF = 0;
            StartCoroutine("SelifF");
            GameClear();
            
        }

        if (col.gameObject.tag == "EvTutorial" && evtutorial == 1)
        {
            
            StartCoroutine("Seliftutorial");
            
        }

    }

    //注意：こちらは静止していても中にいる間は呼ばれ続ける 
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ev1" && evn1 == 1)
        {
            evn1 = 0;
            StartCoroutine("Selif3");
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
            StartCoroutine("SelifS");
        }

        if (col.gameObject.tag == "Ev3" && evn3 == 1)
        {
            evn3 = 0;
            evS = 0;
            evS2 = 0;
            enemyobj.SetActive(true);
            enemy.GetComponent<Enemy>().move_check = 0;
            StartCoroutine("SelifS2");
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
            StartCoroutine("SelifS2");
        }
    }


    void GameOver()
    {
        playercontroller.GetComponent<Playercontroller>().move_check = 0;
        textmanager.GetComponent<TextManager>().system = gameover;

    }

    void GameClear()
    {
        playercontroller.GetComponent<Playercontroller>().move_check = 0;
        camerafilter.GetComponent<CameraFilter>().life_check = 0;
        enemy.GetComponent<Enemy>().move_check = 1;
        enemy2.GetComponent<Enemy>().move_check = 1;
        enemy3.GetComponent<Enemy>().move_check = 1;
        textmanager.GetComponent<TextManager>().system = gameclear;
    }

    IEnumerator Selif1()
    {
        textmanager.GetComponent<TextManager>().system = "reverProject";
        yield return new WaitForSeconds(1);
        textmanager.GetComponent<TextManager>().selif = "「ん？」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().system = "@reverinu";
        textmanager.GetComponent<TextManager>().selif = "「なんだこれは」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "「身体が小さくなってるぞ！？」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().system = "since 2015";
        textmanager.GetComponent<TextManager>().selif = "「夢でも見てるのか……」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "「とりあえず、暗すぎて何も見えん。そこのライトを手に取るか」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().system = "Start";
        textmanager.GetComponent<TextManager>().selif = "";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().system = "";
        playercontroller.GetComponent<Playercontroller>().move_check = 1;
        playaudio.GetComponent<PlayAudio>().walking_check = 1;
        yield return new WaitForSeconds(0);
    }

    IEnumerator Selif2()
    {
        textmanager.GetComponent<TextManager>().stamina = 1000;
        yield return new WaitForSeconds(1);
        textmanager.GetComponent<TextManager>().selif = "「よし、これで明るくなったな……」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "「それにしても小さな体にはライトの重さは堪える……」";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator Selif3()
    {
        yield return new WaitForSeconds(1.9f);
        playaudio.GetComponent<PlayAudio>().BullDown();
        textmanager.GetComponent<TextManager>().selif = "「うわっ！」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "上から落ちてきたようだ";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifS()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "左から不気味な音が聞こえてくる";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifS2()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "あの音が聞こえてくる……";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifS3()
    {
        yield return new WaitForSeconds(1);
        textmanager.GetComponent<TextManager>().selif = "「……？」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "音が消えた？";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "追ってきてないようだ";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator Selifst0()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "これ以上走ることができないようだ";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifO()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "「ここ……までか……」";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator Seliftutorial()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "「俺はライトを手に取るんじゃなかったのか……？」";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifF()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "「ここまで来たが、何がどうなってるのかいまだにわからない」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "「化け物に遭遇して、何度も追いかけられるし、もうコリゴリだ」";
        yield return new WaitForSeconds(2);
        textmanager.GetComponent<TextManager>().selif = "「誰か助けてくれ……」";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifBull()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "今まで落ちてきたことがなかったのに……";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifMeat()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "肉からは腐乱臭がする……";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }

    IEnumerator SelifGomibako()
    {
        yield return new WaitForSeconds(0);
        textmanager.GetComponent<TextManager>().selif = "ごみ箱がある";
        yield return new WaitForSeconds(3);
        textmanager.GetComponent<TextManager>().selif = "";
    }



}

