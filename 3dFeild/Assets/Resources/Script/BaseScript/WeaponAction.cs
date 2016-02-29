using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponAction : MonoBehaviour {
	public AudioClip emptySound;
	public GameObject bulletIcon;
	public GameObject bulletIconFolder;
	public GameObject precol;
	public GameObject weaponPos;

	private AudioSource myAudioSource;
	private int currentWeaponNumber;
	private float interval = 0;
	private float reload = 0;
	private PhotonView myPV;
	WeaponBase shootWeapon;
	WeaponUI weaponUI;

	//BulletIcon管理
	List<GameObject> bulletIcons = new List<GameObject>();

	public void RespawnProcess()
	{
		UpdateWeapon();
		SetCurrentBulletIcon();
	}

	void Start()
	{
		myPV = GetComponent<PhotonView>();
		if(myPV.isMine){
			weaponUI = GetComponent<WeaponUI>();
			UpdateWeapon();
			SetCurrentBulletIcon();
		}
	}
		
	void UpdateWeapon()
	{
		currentWeaponNumber = GetComponent<WeaponChanger>().WeaponType;
		shootWeapon = GetComponent<WeaponList>().weapons[currentWeaponNumber];

	}

	// Update is called once per frame
	void Update () {
		if(myPV.isMine){

			if(currentWeaponNumber != GetComponent<WeaponChanger>().WeaponType)
			{
				UpdateWeapon();
				shootWeapon.currentState = (int)WeaponBase.State.DEFAULT;
				reload = 0;

				SetCurrentBulletIcon();
			}

			if(shootWeapon.CurrentAmmo <= 0 && shootWeapon.CurrentMagazineAmmo <= 0)
			{
				shootWeapon.currentState = (int)WeaponBase.State.EMPTY;
				if(Input.GetButtonDown("Fire1"))
				{
					myPV.RPC("EmptySound",PhotonTargets.All,currentWeaponNumber);
				}
			}

			if(shootWeapon.currentState == (int)WeaponBase.State.RELOAD)
			{
				reloadProcess();
			}else{
				if(myAudioSource != null){
					myPV.RPC("ResetPitch",PhotonTargets.All,currentWeaponNumber);
				}
			}

			interval += Time.deltaTime;
			if(Input.GetButton("Fire1")) {
				if(shootWeapon.CurrentMagazineAmmo <=0 )
				{
					if(shootWeapon.CurrentAmmo >0){
						if(shootWeapon.currentState != (int)WeaponBase.State.RELOAD){
							shootWeapon.currentState = (int)WeaponBase.State.RELOAD;
							myPV.RPC("ReloadSound",PhotonTargets.All,currentWeaponNumber);
							reload = 0;
						}
					}
				}else{
					shootWeapon.currentState = (int)WeaponBase.State.DEFAULT;
					if(interval >= shootWeapon.FireRate){
						interval = 0;
						shootWeapon.CurrentMagazineAmmo --;
						weaponUI.setTexts();
						Shoot();
						DecCurrentBulletIcon();
					}
				}
			}else if(Input.GetKeyDown(KeyCode.Z)){

				if(shootWeapon.CurrentMagazineAmmo >= 0 && shootWeapon.CurrentMagazineAmmo < shootWeapon.MaxMagazineAmmo)
				{
					if(shootWeapon.currentState != (int)WeaponBase.State.RELOAD){
						shootWeapon.currentState = (int)WeaponBase.State.RELOAD;
						myPV.RPC("ReloadSound",PhotonTargets.All,currentWeaponNumber);
						reload = 0;
					}
				}
			}else{
				;//なにもない
			}
		}
	}

	/// <summary>
	/// リロード中
	/// </summary>
	void reloadProcess()
	{
		reload += Time.deltaTime;
		if(reload >= shootWeapon.ReloadTime)
		{
			shootWeapon.currentState = (int)WeaponBase.State.DEFAULT;
			reload = 0;
			shootWeapon.CurrentAmmo += shootWeapon.CurrentMagazineAmmo;
			shootWeapon.CurrentMagazineAmmo = Mathf.Min(shootWeapon.MaxMagazineAmmo,shootWeapon.CurrentAmmo);
			shootWeapon.CurrentAmmo -= Mathf.Min(shootWeapon.MaxMagazineAmmo,shootWeapon.CurrentAmmo);
			weaponUI.setTexts();
			SetCurrentBulletIcon();
		}
	}

	[PunRPC]
	void ResetPitch(int recieveNumber)
	{
		shootWeapon = GetComponent<WeaponList>().weapons[recieveNumber];
		myAudioSource =  shootWeapon.Player.GetComponent<AudioSource>();
		myAudioSource.pitch = 1;
	}

	[PunRPC]
	void ReloadSound(int recieveNumber)
	{
		shootWeapon = GetComponent<WeaponList>().weapons[recieveNumber];
		myAudioSource.clip = shootWeapon.ReloadSound;
		myAudioSource.pitch = shootWeapon.ReloadSound.length/shootWeapon.ReloadTime;
		myAudioSource.PlayOneShot(myAudioSource.clip);
	}

	/// <summary>
	/// 弾の発射
	/// </summary>
	void Shoot()
	{
		shootWeapon = GetComponent<WeaponList>().weapons[currentWeaponNumber];
		GameObject obj = PhotonNetwork.Instantiate(shootWeapon.Bullet.name,shootWeapon.Muzzule.transform.position,Quaternion.identity,0) as GameObject;
		obj.transform.parent = shootWeapon.Muzzule.transform;
		obj.transform.localEulerAngles = shootWeapon.Bullet.transform.localEulerAngles;
		if(obj.GetComponent<DestroyOnHit>()){
			obj.GetComponent<DestroyOnHit>().BulletDamage = shootWeapon.Damage;
		}else{
			obj.GetComponent<DestroyAndSmoke>().BulletDamage = shootWeapon.Damage;
		}
		weaponPos.transform.localEulerAngles = Vector3.zero;
		Vector2 precolSide = new Vector2(precol.transform.position.x,precol.transform.position.y);
		Vector2 muzzleSide = new Vector2(shootWeapon.Muzzule.transform.position.x,shootWeapon.Muzzule.transform.position.y);
		Vector2 precolUp = new Vector2(precol.transform.position.y,precol.transform.position.z);
		Vector2 muzzleUp = new Vector2(shootWeapon.Muzzule.transform.position.y,shootWeapon.Muzzule.transform.position.z);
		float sideDistance = (precolSide - muzzleSide).magnitude;
		float upDistance = (precolUp - muzzleUp).magnitude;
		float towardDistance = precol.GetComponent<FocusTarget>().GetDistanceValue();
		float angleY = sideDistance / Mathf.Sqrt(sideDistance*sideDistance + towardDistance * towardDistance);
		float angleX = upDistance / Mathf.Sqrt(upDistance*upDistance + towardDistance * towardDistance);
		weaponPos.transform.localEulerAngles = new Vector3(-Mathf.Asin(angleX),-Mathf.Acos(angleY),0);
		Vector3 force;
		force = shootWeapon.Muzzule.transform.forward *
			shootWeapon.Speed;
		
		obj.GetComponent<Rigidbody>().AddForce(force);
		myPV.RPC("ShotSound",PhotonTargets.All,currentWeaponNumber);
		myPV.RPC("ShotEffect",PhotonTargets.All,currentWeaponNumber);

	}
		
	[PunRPC]
	void ShotSound(int recieveNumber)
	{
		shootWeapon = GetComponent<WeaponList>().weapons[recieveNumber];
		myAudioSource =  shootWeapon.Player.GetComponent<AudioSource>();
		myAudioSource.clip = shootWeapon.FireSound;
		if(myAudioSource.clip != null){
			myAudioSource.PlayOneShot(myAudioSource.clip);
		}else{
			Debug.Log("audioSouce is empty : " + gameObject.name);
		}
	}

	[PunRPC]
	void ShotEffect(int recieveNumber)
	{
		shootWeapon = GetComponent<WeaponList>().weapons[recieveNumber];
		if(shootWeapon.MuzzuleFlush != null){
			GameObject obj = Instantiate(shootWeapon.MuzzuleFlush,shootWeapon.Muzzule.transform.position,Quaternion.identity) as GameObject;
			obj.transform.parent = shootWeapon.Muzzule.transform;
			obj.transform.localEulerAngles = shootWeapon.MuzzuleFlush.transform.localEulerAngles;
			obj.transform.Rotate(0,180,0);
		}
	}

	[PunRPC]
	void EmptySound(int recieveNumber)
	{
		shootWeapon = GetComponent<WeaponList>().weapons[recieveNumber];
		myAudioSource = shootWeapon.Player.GetComponent<AudioSource>();
		myAudioSource.clip = emptySound;
		if(myAudioSource.clip != null)
		{
			myAudioSource.PlayOneShot(myAudioSource.clip);
		}else{
			Debug.Log("audioSouce is empty : " + gameObject.name);
		}
	}

	void DecCurrentBulletIcon()
	{
		Destroy(bulletIcons[bulletIcons.Count-1]);
		bulletIcons.RemoveAt(bulletIcons.Count-1);
	}

	void SetCurrentBulletIcon()
	{
		for(int i= 0;i<bulletIcons.Count;i++)
		{
			Destroy(bulletIcons[i]);
		}
		bulletIcons.Clear();

		for(int i= 0;i<shootWeapon.CurrentMagazineAmmo;i++)
		{
			bulletIcons.Add(bulletIcon);
			GameObject obj = Instantiate(bulletIcons[i]) as GameObject;
			obj.transform.SetParent(bulletIconFolder.transform,false);
			bulletIcons[i] = obj;
		}

	}
}
