using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	GameManager2 gameManager;
	Animator anim;
	private float speed = 3.5f;
	private Rigidbody2D rb2d;
	public LayerMask groundlayer;
	public bool grounded = false;
	public float directionX;
	private const float MaxJumpPower = 650.0f;
	private const float MinJumpPower = 300.0f;
	private float jumpPower = MinJumpPower;
	private const float JumpChargePoint = 2800.0f;
	private const float movePower = 3.0f;
	private bool jumpSpaceClicked = false;
	private  AudioSource audioSource;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager2> ();
		audioSource = gameObject.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isOver) {
			directionX = Input.GetAxisRaw ("Horizontal");
			//接地しているかどうか調べる
			grounded = Physics2D.Linecast (transform.position+transform.up,
				transform.position - (transform.up * 0.05f), groundlayer);
			
			if (grounded) {
				anim.SetBool ("isJump", false);
				if (directionX != 0) {
					anim.SetBool ("isMove", true);
					//移動スピードを求め代入
					if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
						anim.SetFloat ("speed", 3.0f);
						rb2d.velocity = new Vector2 (directionX * speed*1.8f, rb2d.velocity.y);
					}else{
						anim.SetFloat ("speed", 1.0f);
						rb2d.velocity = new Vector2 (directionX * speed, rb2d.velocity.y);
					}
				}
				if (Input.GetKeyDown ("space")) {
					jumpSpaceClicked = false;
					jumpPower = MinJumpPower;
				}
				if (Input.GetKey ("space") && !jumpSpaceClicked) {
					jumpPower = Mathf.Clamp (jumpPower + Time.deltaTime * JumpChargePoint, MinJumpPower, MaxJumpPower);
				}
				//　ジャンプさせる
				if ((Input.GetKey ("space") && jumpPower == MaxJumpPower) || Input.GetKeyUp ("space")) {
					if (!Input.GetKeyUp ("space")) {
						jumpSpaceClicked = true;
					}
					if (jumpPower > 0) {
						anim.SetBool ("isJump", true);
						anim.SetBool ("isMove", false);
						audioSource.PlayOneShot (audioSource.clip);
					}
					rb2d.AddForce (Vector2.up * jumpPower);
					jumpPower = 0.0f;
				}
			} else {
				anim.SetBool ("isJump", true);
				if (Input.GetKeyDown ("space")) {
					jumpSpaceClicked = true;
				}
				if (directionX != 0) {
					if (directionX == 1 && rb2d.velocity.x < directionX*speed) {
						//移動スピードを求め代入
						rb2d.AddForce (new Vector2 (directionX * movePower, 0.0f));
					} else if (directionX == -1 && rb2d.velocity.x > directionX*speed) {
						rb2d.AddForce(new Vector2(directionX * movePower ,0.0f));
					}
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col2d)
	{
		if (col2d.gameObject.tag == "enemy") {
			if (col2d.transform.localScale.y + col2d.transform.position.y -0.3f > transform.position.y  ) {
				DeadProcess ();
			} else {
				rb2d.velocity = (Vector2.up * 0);
				rb2d.AddForce (Vector2.up * 300);
				Destroy (col2d.gameObject);
			}
		}
		if (col2d.gameObject.tag == "strongEnemy") {
			DeadProcess ();
		}
	}

	void DeadProcess()
	{
		rb2d.velocity = (Vector2.up * 0);
		rb2d.AddForce (Vector2.up * 500);
		transform.eulerAngles = new Vector3 (0,0,180.0f);
		gameManager.SendMessage ("Dead");
	}
}
