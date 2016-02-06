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
    public Events events;
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
        events.StartCoroutine("Selif1");
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
                events.StartCoroutine("Selifst0");
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
            events.StartCoroutine("SelifS2");
        }
        else if (enemy.GetComponent<Enemy>().distance >= enemy.GetComponent<Enemy>().limitDistance && evS == 0 && evn1 == 0 && evn3 == 0)
        {
            evS = 2;
            events.StartCoroutine("SelifS3");
        }

        if (enemy2.GetComponent<Enemy>().distance < enemy2.GetComponent<Enemy>().limitDistance && evS2 == 2 && evn1 == 0)
        {
            evS2 = 0;
            events.StartCoroutine("SelifS2");
        }
        else if (enemy2.GetComponent<Enemy>().distance >= enemy2.GetComponent<Enemy>().limitDistance && evS2 == 0 && evn1 == 0)
        {
            evS2 = 2;
            enemyobj2.SetActive(false);
            events.StartCoroutine("SelifS3");
        }

        if (enemy3.GetComponent<Enemy>().distance < enemy3.GetComponent<Enemy>().limitDistance && evS3 == 2 && evn4 == 0)
        {
            evS3 = 0;
            events.StartCoroutine("SelifS2");
        }
        else if (enemy3.GetComponent<Enemy>().distance >= enemy3.GetComponent<Enemy>().limitDistance && evS3 == 0 && evn4 == 0)
        {
            evS3 = 2;
            events.StartCoroutine("SelifS3");
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
                events.StartCoroutine("SelifO");
                GameOver();
            }
        }

        if (col.gameObject.tag == "Medicine" && evF == 1)
        {
            evF = 0;
            events.StartCoroutine("SelifF");
            GameClear();
            
        }

        if (col.gameObject.tag == "EvTutorial" && evtutorial == 1)
        {

            events.StartCoroutine("Seliftutorial");
            
        }

    }

    //注意：こちらは静止していても中にいる間は呼ばれ続ける 
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ev1" && evn1 == 1)
        {
            evn1 = 0;
            events.StartCoroutine("Selif3");
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
            events.StartCoroutine("SelifS");
        }

        if (col.gameObject.tag == "Ev3" && evn3 == 1)
        {
            evn3 = 0;
            evS = 0;
            evS2 = 0;
            enemyobj.SetActive(true);
            enemy.GetComponent<Enemy>().move_check = 0;
            events.StartCoroutine("SelifS2");
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
            events.StartCoroutine("SelifS2");
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


}

