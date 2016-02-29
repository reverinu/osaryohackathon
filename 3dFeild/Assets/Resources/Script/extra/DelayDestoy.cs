using UnityEngine;
using System.Collections;

public class DelayDestoy : MonoBehaviour {

	[Range(0,20)]
	public float destroyTime;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,destroyTime);
	}
}
