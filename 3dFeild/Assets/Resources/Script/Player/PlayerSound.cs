using UnityEngine;
using System.Collections;


public class PlayerSound : MonoBehaviour {

	public AudioClip[] footSounds;
	public AudioClip jumpSound;
	private AudioSource myAudiosource;
	private int FootSoundNumber =0;
	private float updateRate =0.3f; 
	private float updateTimer = 0f;
	private bool isRunning;
	private bool isJumping;
	private bool isLanding;	//着地
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rigidbodyFirstPersonController;

	void Start(){
		myAudiosource = GetComponent<AudioSource>();
		rigidbodyFirstPersonController = gameObject.transform.parent.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
	}

	void Update()
	{
		isRunning = rigidbodyFirstPersonController.Running;
		isJumping = rigidbodyFirstPersonController.Jumping;
		isLanding = rigidbodyFirstPersonController.Grounded;
		if(isRunning)
		{
			updateTimer += Time.deltaTime;
			if(updateTimer > updateRate)
			{
				FootSound();
				updateTimer = 0;
			}
		}else if(isJumping && isLanding)
		{
			JumpSound();
		}else{
			;//nothing
		}
	}

	void JumpSound()
	{
		myAudiosource.clip = jumpSound;
		myAudiosource.PlayOneShot(myAudiosource.clip);
	}

	void FootSound()
	{
		myAudiosource.clip = footSounds[FootSoundNumber];
		myAudiosource.PlayOneShot(myAudiosource.clip);
		FootSoundNumber++;
		if(FootSoundNumber>footSounds.Length-1)
		{
			FootSoundNumber = 0;
		}
	}
}
