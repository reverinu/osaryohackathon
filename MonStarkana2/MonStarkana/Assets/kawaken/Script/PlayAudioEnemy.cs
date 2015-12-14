using UnityEngine;
using System.Collections;

public class PlayAudioEnemy : MonoBehaviour
{
    public AudioClip enemyashi;
    private AudioSource[] audio;
    public int enemy_check = 1;
    public int enemy_check2 = 0;



    void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    public void Enemyashi()
    {
        if (enemy_check2 == 1 && enemy_check > 0)
        {
            enemy_check = 1;
        }

        if(enemy_check == 1)
        {
            StartCoroutine("Enemyashi_");
        }
    }

    IEnumerator Enemyashi_()
    {
        enemy_check = 0;
        yield return new WaitForSeconds(0);
        audio[0].PlayOneShot(enemyashi);
        yield return new WaitForSeconds(0.5f);
        audio[0].PlayOneShot(enemyashi);
        yield return new WaitForSeconds(0.5f);
        enemy_check = 1;
    }


}