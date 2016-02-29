using UnityEngine;
using System.Collections;

public class FocusTarget : MonoBehaviour {
	[SerializeField]
	GameObject preShotPrefab;
	private float distance =500f;
	private float Speed = 10000;
	private float Rate = 0.3f;
	private float timer = 0;



	public void DistanceValue(Vector3 pos)
	{
		distance = (pos - gameObject.transform.position).magnitude -4;
	}

	public float  GetDistanceValue()
	{
		return distance;
	}

	void Update()
	{
		timer += Time.deltaTime;
		if(timer > Rate){
			GameObject obj = Instantiate(preShotPrefab,gameObject.transform.position,Quaternion.identity) as GameObject;
			obj.transform.parent = gameObject.transform;
			obj.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward*Speed);
			Debug.Log("distnce = " + distance);
			timer = 0;
		}
	}
}
