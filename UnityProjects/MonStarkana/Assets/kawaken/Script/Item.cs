using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
    public GameManager gamemanager;
    public PlayAudio playaudio;
    public int light_check = 0;

	public void spotlight() 
    {
        playaudio.GetComponent<PlayAudio>().GetItem();
        gamemanager.GetComponent<GameManager>().ev2 = 0;
        gamemanager.GetComponent<GameManager>().evt1.isTrigger = true;
        gamemanager.GetComponent<GameManager>().evt2.isTrigger = true;
        gamemanager.GetComponent<GameManager>().evt3.isTrigger = true;
        gamemanager.GetComponent<GameManager>().evtutorial = 0;
        gamemanager.GetComponent<GameManager>().spotlight.intensity = 4.7f;
        Destroy(gamemanager.GetComponent<GameManager>().deslight);
        light_check = 1;
        gamemanager.GetComponent<GameManager>().selif.StartCoroutine("Selif2");
    }

    public void bullskull()
    {
        gamemanager.GetComponent<GameManager>().selif.StartCoroutine("SelifBull");
    }

    public void meats()
    {
        gamemanager.GetComponent<GameManager>().selif.StartCoroutine("SelifMeat");
    }

    public void gomibako()
    {
        gamemanager.GetComponent<GameManager>().selif.StartCoroutine("SelifGomibako");
    }
}
