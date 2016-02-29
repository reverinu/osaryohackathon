using UnityEngine;
using System.Collections;

public class DestroyAndSendPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject,1);
	}
	
	void OnTriggerEnter(Collider col)
	{
		gameObject.transform.parent.gameObject.GetComponent<FocusTarget>().DistanceValue(gameObject.transform.position);
		Destroy(gameObject);
	}
}
