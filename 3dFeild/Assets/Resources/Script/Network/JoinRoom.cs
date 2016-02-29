using UnityEngine;
using System.Collections;

public class JoinRoom : MonoBehaviour {

	public void OnJoinedRoom()
	{
		GetComponent<SpawnProcess>().SpawnCharacter();
	}

	void Update()
	{
		Debug.Log("ExistMasterClient:" +PhotonNetwork.isNonMasterClientInRoom);
		Debug.Log("AreYouMasterClient:"+PhotonNetwork.isMasterClient);
	}
}
