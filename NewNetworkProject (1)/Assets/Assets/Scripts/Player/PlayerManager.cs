using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour {

	public  static int MenberNumber = 0;
	private string[] name = new string[4];

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (MenberNumber);
	}

	//プレイヤー数の登録
	public void PlayerCount()
	{
		MenberNumber++;
	}

	//鬼の割当(人数が増えるたびに行われる)
	void AssignOgre()
	{

	}
}
