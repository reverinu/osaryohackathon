using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

	public Text[] WeaponText ;

	public Text[] AmmoText;

	public Text NoticeText;

	public float switchTime = 0.3f;	//テキストの切り替え時間
	private float interval = 0;

	private WeaponList weaponList;
	private PhotonView myPV;

	// Use this for initialization
	void Start () {
		myPV = GetComponent<PhotonView>();
		if(myPV.isMine){
			NoticeText.text = "";
			setTexts();
		}
	}

	/// <summary>
	/// ウェポンテキストの書き換え
	/// </summary>
	public void setTexts()
	{
		int Number = GetComponent<WeaponChanger>().WeaponType;
		weaponList = 
			GetComponent<WeaponList>();

		for(int i= 0;i<3;i++)
		{
			if(Number >= (int)VariableManager.Weapons.WRONG)
				{
					Number = 0;
				}
			WeaponText[i].text =
				weaponList.weapons[Number].WeaponName;
			AmmoText[i].text =
				weaponList.weapons[Number].CurrentMagazineAmmo + "/" +weaponList.weapons[Number].CurrentAmmo;
		
			Number++;
		}
	}

	void Update()
	{
		if(myPV.isMine){
		
			if(weaponList.weapons[GetComponent<WeaponChanger>().WeaponType].currentState == (int)WeaponBase.State.RELOAD)
			{
				interval += Time.deltaTime;
				if(interval >= switchTime)
					{
					interval = 0;
					if(NoticeText.gameObject.activeInHierarchy)
					{
						NoticeText.gameObject.SetActive(false);
						NoticeText.text = "";
					}else{
						NoticeText.text = "Reload...";
						NoticeText.color = Color.yellow;
						NoticeText.gameObject.SetActive(true);
					}
				}
			}else if(weaponList.weapons[GetComponent<WeaponChanger>().WeaponType].currentState == (int)WeaponBase.State.EMPTY){
				NoticeText.text = "EMPTY!!";
				NoticeText.color = Color.red;
				NoticeText.gameObject.SetActive(true);
				interval = 0;
			}else{
				NoticeText.gameObject.SetActive(false);
				interval = 0;
			}
		}
	}
}
