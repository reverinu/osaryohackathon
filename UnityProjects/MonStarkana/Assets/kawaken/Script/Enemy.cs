using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//敵の動き制御。距離を考慮するタイプ
public class Enemy : MonoBehaviour
{
    public PlayAudioEnemy playaudioenemy;
    public Transform player;    //プレイヤーを代入
    public float speed = 3; //移動速度
    public float secondspeed = 2; //移動速度
    public float limitDistance = 1000f; //敵キャラクターがどの程度近づいてくるか設定(この値以下には近づかない）
    public float distance;
    public int move_check = 1;


    //ゲーム開始時に一度
    void Start()
    {
        //Playerオブジェクトを検索し、参照を代入
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //毎フレームに一度
    void Update()
    {
        Vector3 playerPos = player.position;                 //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向と距離を求める。
        float distance2 = direction.sqrMagnitude;            //directionから距離要素だけを取り出す。
        distance = distance2;
        direction = direction.normalized;                   //単位化（距離要素を取り除く）
        direction.y = 0f;                                   //後に敵の回転制御に使うためY軸情報を消去。これにより敵上下を向かなくなる。


        if (move_check == 0)
        {
            //プレイヤーの距離が一定以下でなければ、敵キャラクターはプレイヤーへ近寄ろうとしない
            if (distance >= limitDistance)
            {

                playaudioenemy.GetComponent<PlayAudioEnemy>().enemy_check2 = 0;
                this.GetComponent<Animation>().Play("Idle");

            }
            else if (distance < limitDistance && distance > 15)
            {

                //プレイヤーとの距離が制限値未満なので、前進する。
                playaudioenemy.GetComponent<PlayAudioEnemy>().enemy_check2 = 1;
                playaudioenemy.GetComponent<PlayAudioEnemy>().Enemyashi();
                this.GetComponent<Animation>().Play("Walk");
                transform.position = transform.position + (direction * secondspeed * Time.deltaTime);
            }
            else if (distance <= 15)
            {
                playaudioenemy.GetComponent<PlayAudioEnemy>().enemy_check2 = 2;
                playaudioenemy.GetComponent<PlayAudioEnemy>().Enemyata();
                this.GetComponent<Animation>().Play("Attack");
                transform.position = transform.position + (direction * secondspeed * Time.deltaTime);
            }
        }

        //プレイヤーの方を向く
        transform.rotation = Quaternion.LookRotation(direction);
        
    }

}