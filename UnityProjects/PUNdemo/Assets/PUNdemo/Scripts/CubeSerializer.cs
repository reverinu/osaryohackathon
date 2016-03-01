using UnityEngine;
using System.Collections;

public class CubeSerializer : Photon.MonoBehaviour
{
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            //stream.SendNext(transform.position);
            //stream.SendNext(transform.rotation);
            stream.SendNext(GetComponent<Renderer>().material.color.r);
            stream.SendNext(GetComponent<Renderer>().material.color.g);
            stream.SendNext(GetComponent<Renderer>().material.color.b);
            stream.SendNext(GetComponent<Renderer>().material.color.a);
        }
        else
        {
            //データの受信
            //transform.position = (Vector3)stream.ReceiveNext();
            //transform.rotation = (Quaternion)stream.ReceiveNext();
            float r = (float)stream.ReceiveNext();
            float g = (float)stream.ReceiveNext();
            float b = (float)stream.ReceiveNext();
            float a = (float)stream.ReceiveNext();
            GetComponent<Renderer>().material.color = new Vector4(r, g, b, a);
        }
    }
}