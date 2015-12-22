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
	private const float MaxJumpPower = 400.0f;
	private const float MinJumpPower = 400.0f;
	private float jumpPower = MinJumpPower;
	private const float JumpChargePoint = 2800.0f;
	private const float movePower = 3.0f;
	private  AudioSource audioSource;
	private float jumpPointY;

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
				if (directionX != 0) {
					anim.SetBool ("isMove", true);
					//移動スピードを求め代入
					if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
						anim.speed = 3.0f;
						rb2d.velocity = new Vector2 (directionX * speed * 1.8f, rb2d.velocity.y);
					} else {
						anim.speed = 1.0f;
						rb2d.velocity = new Vector2 (directionX * speed, rb2d.velocity.y);
					}
				} else {
					anim.SetBool ("isMove", false);
				}
				if (Input.GetKeyDown ("space")) {
					rb2d.AddForce (Vector2.up * jumpPower);
					audioSource.PlayOneShot (audioSource.clip);
				}
			} else {
				if (rb2d.velocity.y > 0 && Input.GetKeyUp("space")) {
					rb2d.velocity = new Vector2(rb2d.velocity.x,0);
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
