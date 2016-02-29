using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponList : MonoBehaviour {

	public List<WeaponBase> weapons = new List<WeaponBase>();

	public void RespawnProcess()
	{
		for(int i= 0;i<weapons.Count;i++)
		{
			weapons[i].CurrentAmmo = weapons[i].MaxAmmo;
			weapons[i].CurrentMagazineAmmo = weapons[i].MaxMagazineAmmo;
		}
	}
}
