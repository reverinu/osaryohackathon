using UnityEngine;
using System.Collections;

public class DestroyFromOther : MonoBehaviour {

	void Start()
	{
		Debug.Log("Destroy : " + gameObject.name);
		if(!GetComponent<PhotonView>().isMine)
		{
			Destroy(gameObject);
		}
	}
}
