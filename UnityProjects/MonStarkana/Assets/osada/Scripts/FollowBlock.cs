using UnityEngine;
using System.Collections;

public class FollowBlock : MonoBehaviour {
	GameObject player;
	//Camera _camera;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		//_camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		transform.position = new Vector2 (Camera.main.ScreenToWorldPoint(Vector3.zero).x, player.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < Camera.main.ScreenToWorldPoint (Vector2.zero).x) {
			transform.position = new Vector2 (Camera.main.ScreenToWorldPoint (Vector3.zero).x, player.transform.position.y);
		}
	}
}
