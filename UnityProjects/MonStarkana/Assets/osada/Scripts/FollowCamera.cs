using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	Vector3 diff;
	PlayerController playerController;
	GameManager2 gameManager;

	public GameObject target;
	public float followSpeedX;
	public float followSpeedY;
	public float WideRange = 3.0f;
	public float HeightRange = 1.5f;
	private Vector2 leave;		//カメラとターゲットの差
	private Vector3 Position;

	void Awake(){
		transform.position = new Vector3 (target.transform.position.x + 12.0f, target.transform.position.y + 1.5f,transform.position.z);
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager2> ();
		playerController = target.GetComponent<PlayerController> ();
		diff = target.transform.position - transform.position;
		diff = new Vector3 (Mathf.Abs (diff.x), Mathf.Abs (diff.y));
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isOver) {
			leave = transform.position - (target.transform.position + diff);
			Position = transform.position;
			if (playerController.directionX == 0 && leave.x < 0) {
				Position.x = Mathf.Lerp (
					transform.position.x,
					target.transform.position.x + diff.x,
					Time.deltaTime * followSpeedX
				);
				transform.position = Position;
			} else if (leave.x < -WideRange) {
				Position.x = target.transform.position.x + diff.x - WideRange;
				transform.position = Position;
			} else {
				//なにもしない
			}
			
			if (playerController.grounded) {
				Position.y = Mathf.Lerp (
					transform.position.y,
					target.transform.position.y + diff.y,
					Time.deltaTime * followSpeedY
				);
				transform.position = Position;
			} else if (leave.y > HeightRange) {
				Position.y = target.transform.position.y + diff.y + HeightRange;
				transform.position = Position;
			} else if (leave.y < -HeightRange) {
				Position.y = target.transform.position.y + diff.y - HeightRange;
				transform.position = Position;
			} else {
				//なにもしない
			}
		}
	}
}
