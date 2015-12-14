using UnityEngine;
using System.Collections;

public class GetObject : MonoBehaviour {

    public float distance = 100f;
    public string objectName;
    public Item item;

    // Update is called once per frame
    void Update () {
        // 左クリックを取得
        if (Input.GetMouseButtonDown(2))
        {
            // クリックしたスクリーン座標をrayに変換
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Rayの当たったオブジェクトの情報を格納する
            RaycastHit hit = new RaycastHit();
            // オブジェクトにrayが当たった時
            if (Physics.Raycast(ray, out hit, distance))
            {
                // rayが当たったオブジェクトの名前を取得
                objectName = hit.collider.gameObject.name;
                if (objectName == "Raito" || objectName == "Hikari")
                {
                    item.GetComponent<Item>().spotlight();
                }

                if (objectName == "BullSkull" || objectName == "BullJaw")
                {
                    item.GetComponent<Item>().bullskull();
                }

                if (objectName == "meat_01" || objectName == "meat_02" || objectName == "meat_03" || objectName == "meat_04")
                {
                    item.GetComponent<Item>().meats();
                }

                if (objectName == "gomibako")
                {
                    item.GetComponent<Item>().gomibako();
                }

                Debug.Log(objectName);
            }
        }
    }
}
