using UnityEngine;
using System.Collections;



public class VariableManager : MonoBehaviour {

	/// <summary>
	/// 武器管理番号
	/// </summary>
	public enum Weapons
	{
		AK47,
		M4A1,
		SKORPION,
		UMP45,
		SMOKEGRENADE,
		WRONG
	};

	/// <summary>
	/// ゲーム情報
	/// </summary>
	public enum GameState
	{
		STANDBY,
		PLAY,
		RESULT,
		WRONG
	};



	/// <summary> 武器タイプ </summary>
	public static int WeaponType = (int)Weapons.AK47;


	public static bool isDefaultMap = true;		//初期ミニマップ

	public static string ValueToWeaponName(int weaponNumber)
	{
		switch(weaponNumber)
		{
		case (int)Weapons.AK47:
			return Weapons.AK47.ToString();
		case (int)Weapons.M4A1:
			return Weapons.M4A1.ToString();
		case (int)Weapons.SKORPION:
			return Weapons.SKORPION.ToString();
		case (int)Weapons.UMP45:
			return Weapons.UMP45.ToString();
		case (int)Weapons.SMOKEGRENADE:
			return Weapons.SMOKEGRENADE.ToString();
			default:
			return Weapons.WRONG.ToString();
		}
	}

	public static int WeaponNameToValue(string weaponName)
	{
		if(weaponName.CompareTo(Weapons.AK47.ToString())==0){
			return (int)Weapons.AK47;
		}else if(weaponName.CompareTo(Weapons.M4A1.ToString())==0){
			return (int)Weapons.M4A1;
		}else if(weaponName.CompareTo(Weapons.SKORPION.ToString())==0){
			return (int)Weapons.SKORPION;
		}else if(weaponName.CompareTo(Weapons.UMP45.ToString())==0){
			return (int)Weapons.UMP45;
		}else if(weaponName.CompareTo(Weapons.SMOKEGRENADE.ToString())==0){
			return (int)Weapons.SMOKEGRENADE;
		}else{
			Debug.Log("未実装");
			return -1;
		}
	}
}
