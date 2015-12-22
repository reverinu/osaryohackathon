using UnityEngine;
using System.Collections;

public class PlayAudioEnemy : MonoBehaviour
{
    public AudioClip enemyashi;
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


}