using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour
{
    public AudioClip ashi01;
    public AudioClip ashi02;
    public AudioClip getitem;
    public AudioClip bulldown;
    private AudioSource[] audio;
    public int walking_check = 0;
    public int walking_check2 = 0;



    void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    public void Walking()
    {
        if (walking_check2 == 1 && walking_check > 0)
        {
            walking_check = 1;
        } 
        else if (walking_check2 == 2 && walking_check > 0) 
        {
            walking_check = 2;
        }


        if (walking_check == 1)
        {
            StartCoroutine("Walking_");
        }
        else if (walking_check == 2)
        {
            StartCoroutine("SlowWalking_");
        }
    }

    public void BullDown()
    {
        audio[1].PlayOneShot(bulldown);
    }

    public void GetItem()
    {
        audio[1].PlayOneShot(getitem, 0.5f);
    }

    IEnumerator Walking_()
    {
        walking_check = 0;
        yield return new WaitForSeconds(0);
        audio[0].PlayOneShot(ashi01, 0.5f);
        yield return new WaitForSeconds(0.2f);
        audio[0].PlayOneShot(ashi02, 0.5f);
        yield return new WaitForSeconds(0.4f);
        walking_check = 1;
    }

    IEnumerator SlowWalking_()
    {
        walking_check = 0;
        yield return new WaitForSeconds(0);
        audio[0].PlayOneShot(ashi01, 0.5f);
        yield return new WaitForSeconds(0.6f);
        audio[0].PlayOneShot(ashi02, 0.5f);
        yield return new WaitForSeconds(0.8f);
        walking_check = 2;
    }

}