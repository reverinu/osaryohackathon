using UnityEngine;
using System.Collections;

public class PlayAudioEnemy : MonoBehaviour
{
    public AudioClip enemyashi;
    public AudioClip enemyata;
    private AudioSource[] audioenemy;
    public int enemy_check = 1;
    public int enemy_check2 = 0;



    void Start()
    {
        audioenemy = GetComponents<AudioSource>();
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

    public void Enemyata()
    {

        if (enemy_check2 == 2 && enemy_check > 0)
        {
            enemy_check = 2;
        }

        if(enemy_check == 2)
        {
            StartCoroutine("Enemyata_");
        }
    }

    IEnumerator Enemyashi_()
    {
        enemy_check = 0;
        yield return new WaitForSeconds(0);
        audioenemy[0].PlayOneShot(enemyashi);
        yield return new WaitForSeconds(0.5f);
        audioenemy[0].PlayOneShot(enemyashi);
        yield return new WaitForSeconds(0.5f);
        enemy_check = 1;
    }

    IEnumerator Enemyata_()
    {
        enemy_check = 0;
        yield return new WaitForSeconds(0);
        audioenemy[0].PlayOneShot(enemyata);
        yield return new WaitForSeconds(2f);
        enemy_check = 2;
    }


}