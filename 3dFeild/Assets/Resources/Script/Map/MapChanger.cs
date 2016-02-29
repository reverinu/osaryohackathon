using UnityEngine;
using System.Collections;

public class MapChanger : MonoBehaviour {

	public Camera stageMapCamera;
	public Camera zoomMapCamera;

	// Use this for initialization
	void Start () {
		if(VariableManager.isDefaultMap)
		{
			activeCameraNumber(0);
		}else{
			activeCameraNumber(1);
		}
	}

	/// <summary>
	/// ミニマップで有効にするカメラ
	/// </summary>
	/// <param name="number">0...stageMapCamera [ON] / 1...zoomMapCamera [ON]</param>
	private void activeCameraNumber(int number)
	{
		switch(number)
		{
		case 0:
			stageMapCamera.gameObject.SetActive(true);
			zoomMapCamera.gameObject.SetActive(false);
			break;
		case 1:
			zoomMapCamera.gameObject.SetActive(true);
			stageMapCamera.gameObject.SetActive(false);
			break;
		}
	}

	private void changeMapCamera()
	{
		if(VariableManager.isDefaultMap)
		{
			VariableManager.isDefaultMap = false;
			activeCameraNumber(1);
		}else{
			VariableManager.isDefaultMap = true;
			activeCameraNumber(0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M))
		{
			changeMapCamera();
		}
	}
}
