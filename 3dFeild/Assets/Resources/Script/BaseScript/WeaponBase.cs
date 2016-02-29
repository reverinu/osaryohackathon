using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour {
	[Header("所有プレイヤー")]
	public GameObject Player;
	[Header("武器情報")]
	public string WeaponName;		//武器名
	public float FireRate;			//連射速度
	public float Speed;				//射出スピード
	public int Damage;				//被弾ダメージ
	public float ReloadTime;		//リロード時間
	[Header("弾数情報")]
	public int MaxAmmo;				//最大弾数
	public int CurrentAmmo;			//現在の弾数
	public int MaxMagazineAmmo;		//マガジンの最大装填数
	public int CurrentMagazineAmmo;	//現在のマガジンの装填数
	[Header("射出オブジェクト")]
	public GameObject Bullet;		//弾丸オブジェクト
	public GameObject Muzzule;		//発射口
	[Header("エフェクト")]
	public GameObject MuzzuleFlush;	//発射エフェクト
	[Header("サウンド")]
	public AudioClip FireSound;		//発射音
	public AudioClip ReloadSound;	//リロード音

	/// <summary>
	/// 武器の使用状態
	/// </summary>
	public enum State
	{
		DEFAULT,
		EMPTY,
		RELOAD,
	};
	[System.NonSerialized]
	public int currentState = (int)State.DEFAULT;

	public WeaponBase(GameObject player,string weaponName,float fireRate,float speed,int damage,float reloadTime,int maxAmmo,int currentAmmo,
		int maxMagazineAmmo,int currentMagazineAmmo,GameObject bullet,GameObject muzzule,GameObject muzzuleFlush,AudioClip fireSound,AudioClip reloadSound)
	{
		Player = player;
		WeaponName = weaponName;
		FireRate = fireRate;
		Speed = speed;
		Damage = damage;
		ReloadTime = reloadTime;
		MaxAmmo = maxAmmo;
		CurrentAmmo = currentAmmo;
		MaxMagazineAmmo = maxMagazineAmmo;
		CurrentMagazineAmmo = currentAmmo;
		Bullet = bullet;
		Muzzule = muzzule;
		MuzzuleFlush = muzzuleFlush;
		FireSound = fireSound;
		ReloadSound = reloadSound;
	}
}
