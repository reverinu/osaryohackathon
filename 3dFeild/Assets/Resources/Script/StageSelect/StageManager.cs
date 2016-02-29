using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
	[SerializeField]
	Camera UIcamera;
	[SerializeField]
	GameObject selectImage;
	[SerializeField]
	Image GardImage;

	void Start()
	{
		selectImage.SetActive(false);
		GardImage.enabled = false;
	}

	void startSound()
	{
		GetComponent<AudioSource>().Play();
	}

	public void Click_Stage1()
	{
		StartCoroutine("LoadScene","Main");
	}

	public void CLick_Stage2()
	{
		StartCoroutine("LoadScene","Main2");
	}

	private IEnumerator LoadScene(string name)
	{
		startSound();
		selectImage.SetActive(true);
		GardImage.enabled = true;
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(name,LoadSceneMode.Single);
	}
}
