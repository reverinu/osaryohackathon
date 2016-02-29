using UnityEngine;
using System.Collections;
using System;

public class WeaponChanger : MonoBehaviour {

	public GameObject[] Weapons;
	public AudioClip changeSE;
	[System.NonSerialized]
	public int WeaponType;
	private AudioSource myAudioSource;
	private PhotonView myPV;

	public void RespawnProcess()
	{
		WeaponType = (int)VariableManager.Weapons.AK47;
		if(myPV.isMine)
		{
			myPV.RPC("allDiactiveWeapon",PhotonTargets.All);
			myPV.RPC("activeWeapon",PhotonTargets.All,WeaponType);
		}
	}

	// Use this for initialization
	void Start () {
		myPV = GetComponent<PhotonView>();
		if(myPV.isMine){
			WeaponType = (int)VariableManager.Weapons.AK47;
			myPV.RPC("allDiactiveWeapon",PhotonTargets.All);
			myPV.RPC("activeWeapon",PhotonTargets.All,WeaponType);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(myPV.isMine){
			if(Input.GetButtonDown("Fire2")){
				WeaponType ++;
					
				if(WeaponType >= (int)VariableManager.Weapons.WRONG)
					WeaponType = 0;
				GetComponent<WeaponUI>().setTexts();
				myPV.RPC("activeWeapon",PhotonTargets.All,WeaponType);
				ChangeSound();
			}
		}
	}

	void ChangeSound()
	{
		myAudioSource =  GetComponent<WeaponList>().weapons[WeaponType].Player.GetComponent<AudioSource>();
		myAudioSource.clip = changeSE;
		if(myAudioSource.clip != null){
			myAudioSource.PlayOneShot(myAudioSource.clip);
		}else{
			Debug.Log("audioSouce is empty : " + gameObject.name);
		}
	}

		

	/// <summary>
	/// 全ての登録ウェポンを非表示にする
	/// </summary>
	[PunRPC]
	void allDiactiveWeapon()
	{
		for(int i= 0;i<(int)VariableManager.Weapons.WRONG;i++)
		{
			Weapons[i].SetActive(false);
		}
	}

	/// <summary>
	/// 指定番号のウェポンをアクティブにする
	/// </summary>
	/// <param name="weaponType">VariableManagerの武器番号</param>
	[PunRPC]
	void activeWeapon(int weaponType)
	{
		if(weaponType < 0 || weaponType >= (int)VariableManager.Weapons.WRONG)
		{
			Debug.Log("WrongNumber:" + this.gameObject.name);
			return;
		}

		allDiactiveWeapon();
		//指定Weaponをアクティブにする
		for(int i=0;i<(int)VariableManager.Weapons.WRONG;i++)
		{
			if(Weapons[weaponType].name.ToString().CompareTo(VariableManager.ValueToWeaponName(weaponType))==0)
			{
				Weapons[weaponType].SetActive(true);
			}
		}
	}
}
