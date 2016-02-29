using UnityEngine;
using System.Collections;

public class DestroyOnHit : MonoBehaviour {

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
			if(col.gameObject.tag == "Player")
			{
				col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				col.gameObject.GetComponent<PlayerInformation>().DamageProcess(this.gameObject);
				PhotonNetwork.Instantiate(hitManager.PlayerHit.name,transform.position,Quaternion.Euler(Vector3.zero),0);
			}else{
				PhotonNetwork.Instantiate(hitManager.FeildHit.name,transform.position,Quaternion.Euler(Vector3.zero),0);
			}
			Debug.Log("BulletHit:" + col.gameObject.name);
			DeleteProcess();

		}
	}

	void DeleteProcess()
	{
		PhotonNetwork.Destroy(gameObject);
	}
		

}
