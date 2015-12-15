using UnityEngine;
using System.Collections;

public class FreezeChildren : MonoBehaviour {

	private GameObject _child;
	MoveScript moveScript;
	private bool defaultUp;

	// Use this for initialization
	void Start () {
		_child = transform.FindChild ("UpDownEnemy").gameObject;
		moveScript = _child.GetComponent<MoveScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D co2d)
	{
		if (moveScript.UpMove) {
			defaultUp = true;
		} else {
			defaultUp = false;
		}
	}

	void OnTriggerStay2D(Collider2D co2d)
	{
		if (moveScript.isCenter) {
			moveScript.isUp = !defaultUp;
		}
	}
}
