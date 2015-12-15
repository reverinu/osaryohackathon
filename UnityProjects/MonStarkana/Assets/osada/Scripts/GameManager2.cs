using UnityEngine;
using System.Collections;

public class GameManager2 : MonoBehaviour {
	public bool isOver = false;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//sendされてくる
	void Dead()
	{
		isOver = true;
		audioSource.time = 0.5f;
		audioSource.Play ();
		BoxCollider2D bc2d = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
		CircleCollider2D cc2d = GameObject.Find ("Player").GetComponent<CircleCollider2D> ();
		bc2d.enabled = false;
		cc2d.enabled = false;
	}
}
