using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public bool UpMove = false;
	public bool DownMove = false;
	public bool LeftMove = false;
	public bool RightMove = false;
	public float MoveSpeed = 2.0f;
	public float MoveDistance = 1.0f;
	public float waitTime = 0.5f;
	private Vector3 StartPosition;
	public bool isUp = true;
	public bool isRight = true;
	public bool isCenter = true;
	private float maxUp,maxDown,maxRight,maxLeft;
	private bool isRunning_Height = false;
	private bool isRunning_Width = false;
	// Use this for initialization
	void Start () {
		StartPosition = transform.position;
		maxUp = StartPosition.y;
		if (UpMove) {
			maxUp += MoveDistance;
		} 
		maxDown = StartPosition.y;
		if (DownMove) {
			maxDown -= MoveDistance;
		}
		maxRight = StartPosition.x;
		if (RightMove) {
			maxRight += MoveDistance;
		}
		maxLeft = StartPosition.x;
		if (LeftMove) {
			maxLeft -= MoveDistance;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= StartPosition.y && UpMove) {
			isCenter = true;
		} else if(transform.position.y >= StartPosition.y && DownMove){
			isCenter = true;
		}else {
			isCenter = false;
		}
		if (UpMove || DownMove) {
			if (isUp) {
				if (maxUp <= transform.position.y) {
					if (!isRunning_Height) {
						isRunning_Height = true;
						StartCoroutine (HeightWaitAt (waitTime));
					}
				} else {
					transform.position += new Vector3 (0.0f, MoveSpeed * Time.deltaTime,0.0f);
				}
			} else {
				if (maxDown >= transform.position.y) {
					if (!isRunning_Height) {
						isRunning_Height = true;
						StartCoroutine (HeightWaitAt (waitTime));
					}
				} else {
					transform.position -= new Vector3 (0.0f, MoveSpeed * Time.deltaTime,0.0f);
				}
			}
		}
		if (RightMove || LeftMove) {
			if (isRight) {
				if (maxRight <= transform.position.x) {
					if (!isRunning_Width) {
						isRunning_Width = true;
						StartCoroutine (WidthWaitAt (waitTime));
					}
				} else {
					transform.position += new Vector3 ( MoveSpeed * Time.deltaTime,0.0f,0.0f);
				}
			} else {
				if (maxLeft >= transform.position.x) {
					if (!isRunning_Width) {
						isRunning_Width = true;
						StartCoroutine (WidthWaitAt (waitTime));
					}
				} else {
					transform.position -= new Vector3 ( MoveSpeed * Time.deltaTime,0.0f,0.0f);
				}
			}
		}
	}
		

	IEnumerator HeightWaitAt(float waitTime){
		yield return new WaitForSeconds (waitTime);
		isUp = !isUp;
		isRunning_Height = false;
	}
	IEnumerator WidthWaitAt(float waitTime){
		yield return new WaitForSeconds (waitTime);
		isRight = !isRight;
		isRunning_Width = false;
	}
}
