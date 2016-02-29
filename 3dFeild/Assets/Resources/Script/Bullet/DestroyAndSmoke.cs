using UnityEngine;
using System.Collections;

public class DestroyAndSmoke : MonoBehaviour {
	public int BulletDamage;	//ダメージ量
	HitManager hitManager;

	private PhotonView myPV;

	void Start()
	{
		myPV = GetComponent<PhotonView>();
		hitManager = GameObject.FindGameObjectWithTag("HitManager").GetComponent<HitManager>();
		if(myPV.isMine)
		{
			Invoke("DeleteProcess",3);
		}

	}


	void OnCollisionEnter(Collision col)
	{
		if(myPV.isMine){
			PhotonNetwork.Instantiate(hitManager.Smoke.name,transform.position,Quaternion.Euler(Vector3.zero),0);
			Debug.Log("BulletHit:" + col.gameObject.name);
			DeleteProcess();

		}
	}

	void DeleteProcess()
	{
		PhotonNetwork.Destroy(gameObject);
	}
		
}
