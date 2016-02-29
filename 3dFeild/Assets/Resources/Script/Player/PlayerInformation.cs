using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour {
	public MeshRenderer meshrenderer;
	public GameObject weaponManager;
	public int maxPlayerHP = 100;
	[Header("変更不可")]
	public int currentPlayerHP;

	private int tmpPlayerHP;
	public Image image_playerHP;

	private float alpha_PlayerHP;
	public bool isAlpha = true;
	private PhotonView myPV;
	//１フレームごとの回復値
	private int recoveryPointPerMiliSecond = 1;
	private float recoveryTimer =0;

	void Start()
	{
		myPV = GetComponent<PhotonView>();
		currentPlayerHP = maxPlayerHP;
		tmpPlayerHP = currentPlayerHP;
		image_playerHP.color = new Color(image_playerHP.color.r,image_playerHP.color.g,image_playerHP.color.b,0);
		if(!myPV.isMine)
		{
			foreach(Camera camera in GetComponentsInChildren<Camera>()){
				if(camera.gameObject.name == "MainCamera")
				{
					camera.GetComponent<UnityStandardAssets.Characters.FirstPerson.HeadBob>().enabled = false;
					camera.GetComponent<AudioListener>().enabled = false;
					camera.enabled = false;
				}else{
					camera.gameObject.SetActive(false);
				}
			}
			GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
			gameObject.transform.FindChild("playerIcon").gameObject.SetActive(false);
			GetComponent<Rigidbody>().useGravity = false;
			//GetComponent<PlayerInformation>().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if(myPV.isMine){
			if(currentPlayerHP == 0 ){
				DeadProcess();
			}
			if(currentPlayerHP != 0 && currentPlayerHP < maxPlayerHP){
				recoveryTimer += Time.deltaTime;
				if(recoveryTimer > 0.1f){
					currentPlayerHP = Mathf.Min(maxPlayerHP, currentPlayerHP + recoveryPointPerMiliSecond);
					recoveryTimer = 0;
				}
			}

			if(currentPlayerHP != tmpPlayerHP)
			{
				alpha_PlayerHP = 0.5f*(1-(float)currentPlayerHP/maxPlayerHP);
					image_playerHP.color = new Color(image_playerHP.color.r,image_playerHP.color.g,image_playerHP.color.b,alpha_PlayerHP);
				tmpPlayerHP = currentPlayerHP;
			}
		}
	}

	void DeadProcess()
	{
		if(myPV.isMine){
			GameObject.FindGameObjectWithTag("UITeamManager").GetComponent<TeamPlayManager>().DecreaseRestPlayer(isAlpha);
			GameObject.FindGameObjectWithTag("SpawnManage").GetComponent<SpawnProcess>().RespawnCharancter(this.gameObject);
			myPV.RPC("RecoveryProcess",PhotonTargets.All);
		}
	}

	[PunRPC]
	void RecoveryProcess()
	{
		currentPlayerHP = maxPlayerHP;
		weaponManager.GetComponent<WeaponList>().RespawnProcess();
		weaponManager.GetComponent<WeaponChanger>().RespawnProcess();
		weaponManager.GetComponent<WeaponAction>().RespawnProcess();
	}


		

	void OnCollisionEnter(Collision col)
	{
		if(myPV.isMine){
			if(col.gameObject.tag == "Bullet")
			{
				GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}


	void OnCollisionExit(Collision col)
	{
		if(myPV.isMine){
			if(col.gameObject.tag == "Bullet")
			{
				GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}
		
	public void DamageProcess(GameObject hitBullet)
	{
		currentPlayerHP = 
			Mathf.Max(currentPlayerHP-hitBullet.GetComponent<DestroyOnHit>().BulletDamage,0);
		myPV.RPC("SyncCurrentPlayerHP",PhotonTargets.All,currentPlayerHP);
	}

	public void SyncTeam()
	{
		if(PhotonNetwork.isMasterClient){
			GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonView>().RPC("SyncValue",PhotonTargets.Others,
				TeamInfo.alphaRestCount,TeamInfo.alphaRestCount,TeamInfo.betaRestCount,TeamInfo.betaMemberCount);
		}
	}
		

	[PunRPC]
	void SyncCurrentPlayerHP(int HP)
	{
		currentPlayerHP = HP;
	}

	[PunRPC]
	void SyncTeamInfo(bool alphaCheck)
	{
		isAlpha = alphaCheck;
		if(isAlpha)
		{
			meshrenderer.material.color = Color.red;
		}else{
			meshrenderer.material.color = Color.blue;
		}
	}
}
