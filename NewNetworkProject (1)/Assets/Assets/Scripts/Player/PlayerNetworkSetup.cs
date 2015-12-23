using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {
	
	public override void OnStartLocalPlayer ()
	{
		//LocalPlayerのAnimatorパラメータを自動的に送る
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}

	public override void PreStartClient ()
	{
		//ClientのAnimatorパラメータを自動的に送る
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}
}
