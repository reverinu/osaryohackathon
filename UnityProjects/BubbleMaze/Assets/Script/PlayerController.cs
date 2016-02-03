using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public float MoveSpeed = 10f;
    public int move_check = 0;

    private float down_x;
    private float drag_x;
    private float check_x;



    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //左押し下げで前進
                
                player.transform.position += player.transform.forward * Time.deltaTime * MoveSpeed;
                
        }
        

        if (Input.GetMouseButtonDown(1))
        {
            down_x = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(1))
        {
            //ドラッグ時のマウス位置を変数に格納
            drag_x = Input.mousePosition.x;
            check_x = drag_x - down_x;
            //右押し下げで回転
            if (check_x > 0 && check_x <= 20)
            {
                // 時計回り
                player.transform.localEulerAngles += new Vector3(0, 0, 0);
            }
            else if (check_x > 20 && check_x <= 200)
            {
                // 時計回り
                player.transform.localEulerAngles += new Vector3(0, 0.5f, 0);
            }
            else if (check_x > 200)
            {
                // 時計回り
                player.transform.localEulerAngles += new Vector3(0, 3f, 0);
            }
            else if (check_x < 0 && check_x >= -20)
            {
                // 反時計回り
                player.transform.localEulerAngles += new Vector3(0, 0, 0);
            }
            else if (check_x < -20 && check_x >= -200)
            {
                // 反時計回り
                player.transform.localEulerAngles += new Vector3(0, -0.5f, 0);
            }
            else if (check_x < -200)
            {
                // 反時計回り
                player.transform.localEulerAngles += new Vector3(0, -3f, 0);
            }

            if (Camera.main.transform.localEulerAngles.x >= 358)
            {
                Camera.main.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            

            if (player.transform.localEulerAngles.z > 10f && player.transform.localEulerAngles.z < 350f)
            {
                player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y, 0);
            }

            if (player.transform.position.y >= 10f && player.transform.position.y <= -10f)
            {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }

        }
    }
}
