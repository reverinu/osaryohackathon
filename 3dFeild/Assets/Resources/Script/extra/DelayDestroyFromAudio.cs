using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]


public class DelayDestroyFromAudio : MonoBehaviour {
	
	private AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource>();
		if(myAudioSource != null)
		{
			Destroy(gameObject,myAudioSource.clip.length);
		}else{
			Destroy(gameObject,1);
			Debug.Log("Missing AudioClip!! : " + gameObject.name);
		}
	}
}
