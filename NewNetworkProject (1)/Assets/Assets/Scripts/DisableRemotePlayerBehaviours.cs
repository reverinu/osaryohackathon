using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DisableRemotePlayerBehaviours : NetworkBehaviour {

	public Behaviour[] behaviours;

	// Use this for initialization
	void Start () {
		//登録されたコンポーネントをリモート側で disableにする
		if (!isLocalPlayer) {
			foreach (var behaviour in behaviours) {
				behaviour.enabled = false;
			}
		}
	}
}