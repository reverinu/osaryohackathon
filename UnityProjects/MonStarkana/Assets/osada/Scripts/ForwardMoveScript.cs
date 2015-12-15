using UnityEngine;
using System.Collections;

public class ForwardMoveScript : MonoBehaviour {

	public bool isFall = true;		//崖で落下するかどうか
	public bool isLeft = true;		//最初どちらに進むか
	public LayerMask groundlayer;
	public bool grounded;
	public float moveSpeed = 1.5f;
	// Use this for initialization
	void Start () {
		if (!isLeft) {
			transform.eulerAngles = new Vector3 (0.0f, 180.0f, 0.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position,
			transform.position - (transform.up * 0.5f), groundlayer);

		if (isLeft) {
			transform.position -= new Vector3(moveSpeed*Time.deltaTime,0.0f,0.0f);
		} else {
			transform.position += new Vector3 (moveSpeed * Time.deltaTime, 0.0f, 0.0f);
		}
	}

	void OnCollisionEnter2D(Collision2D co2d)
	{
		if (co2d.transform.tag == "stage" || co2d.transform.tag =="enemy") {
			for (int Index = 0; Index < co2d.contacts.Length; Index++) {
				if (co2d.contacts [Index].point.y > transform.position.y) {
					transform.eulerAngles = new Vector3 (0.0f, 180.0f, 0.0f);
					isLeft = !isLeft;
					break;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D co2d)
	{
		if (co2d.transform.tag == "stage") {
			if (!isFall && grounded) {
				transform.eulerAngles = new Vector3 (0.0f, 180.0f, 0.0f);
				isLeft = !isLeft;		
			}
		}
	}
}
