using UnityEngine;
using System.Collections;

public class SpawnProcess : MonoBehaviour {
	[SerializeField]
	Camera FieldCamera;

	public GameObject spawnObjects;
	public Transform[] spawnPosition;
	private PlayerInformation playerInformation;
	private int alphaCount = 0;
	private int betaCount = 0;
	private int memberCount = 0;
	private int count;

	public void SpawnCharacter()
	{
		FieldCamera.enabled = false;
		int spawnIndex;

		Vector3 spawnPos = Vector3.up;
		if (this.spawnPosition != null)
		{
			spawnIndex = Random.Range(0,spawnPosition.Length-1);
			spawnPos = this.spawnPosition[spawnIndex].position;
			Debug.Log(spawnPos);
		}
		PhotonNetwork.Instantiate(spawnObjects.name,Vector3.zero,Quaternion.identity,0);
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
			playerInformation =  player.GetComponent<PlayerInformation>();
			if(player.GetComponent<PhotonView>().isMine)
			{
				
				CountProcess();
				if(TeamInfo.alphaMemberCount < TeamInfo.betaMemberCount){
					playerInformation.isAlpha = true;
					playerInformation.meshrenderer.material.color = Color.red;

				}else{
					playerInformation.isAlpha = false;
					playerInformation.meshrenderer.material.color = Color.blue;
				}
				playerInformation.GetComponent<PhotonView>().RPC("SyncTeamInfo",PhotonTargets.All,playerInformation.isAlpha);
				CountProcess();
				player.transform.position = spawnPos;
				break;
			}
		}
	}

	public void RespawnCharancter(GameObject character)
	{
		FieldCamera.enabled = false;
		int spawnIndex;

		Vector3 spawnPos = Vector3.up;
		if (this.spawnPosition != null)
		{
			spawnIndex = Random.Range(0,spawnPosition.Length-1);
			spawnPos = this.spawnPosition[spawnIndex].position;
			Debug.Log(spawnPos);
		}
		character.transform.position = spawnPos;
	}

	void Update()
	{
		if(memberCount != GameObject.FindGameObjectsWithTag("Player").Length)
		{
			CountProcess();
		}
	}
		
	void CountProcess()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		alphaCount = 0;
		betaCount = 0;
		memberCount = players.Length;
		TeamInfo.MemberCount = memberCount;
		for(int i= 0;i< players.Length;i++)
		{
			if(players[i].GetComponent<PlayerInformation>().isAlpha)
			{
				alphaCount++;
			}else{
				betaCount++;
			}
		}
		TeamInfo.alphaRestCount = alphaCount;
		TeamInfo.betaRestCount = betaCount;
		GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonView>().RPC("SyncValue",PhotonTargets.Others,
			TeamInfo.alphaRestCount,TeamInfo.alphaRestCount,TeamInfo.betaRestCount,TeamInfo.betaMemberCount);
	}
}
