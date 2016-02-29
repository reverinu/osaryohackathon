using UnityEngine;
using System.Collections;

public class TeamPlayManager : MonoBehaviour {

	private int maxRestPlayer ;	//お互いの残機
	private int teamAlphaRestPlayer ;
	private int teamBetaRestPlayer;

	private PhotonView myPV;


	// Use this for initialization
	void Start () {
		myPV = GetComponent<PhotonView>();
		GameObject[] members = GameObject.FindGameObjectsWithTag("Player");
		if(PhotonNetwork.isMasterClient){
			
				maxRestPlayer = 30;
				teamAlphaRestPlayer = maxRestPlayer;
				teamBetaRestPlayer = maxRestPlayer;
			TeamInfo.alphaRestCount = teamAlphaRestPlayer;
			TeamInfo.betaRestCount = teamBetaRestPlayer;
		}else{
			for(int i= 0;i<members.Length;i++)
			{
				members[i].GetComponent<PlayerInformation>().SyncTeam();
			}
			maxRestPlayer = 30;
			teamAlphaRestPlayer = TeamInfo.alphaRestCount;
			teamBetaRestPlayer = TeamInfo.betaRestCount;
		}
	}

	/// <summary>
	/// プレイヤー残機を割合で表示
	/// </summary>
	/// <returns>The of rest player.</returns>
	/// <param name="isAlpha">If set to <c>true</c> is alpha.</param>
	public float PercentageOfRestPlayer(bool isAlpha)
	{
		if(isAlpha){
			return (float)teamAlphaRestPlayer/maxRestPlayer;
		}else{
			return (float)teamBetaRestPlayer/maxRestPlayer;
		}

	}

	public void DecreaseRestPlayer(bool isAlpha)
	{
		if(isAlpha)
		{
			teamAlphaRestPlayer = Mathf.Max(teamAlphaRestPlayer-1,0);
			TeamInfo.alphaRestCount = teamAlphaRestPlayer;

		}else{
			teamBetaRestPlayer = Mathf.Max(teamBetaRestPlayer-1,0);
			TeamInfo.betaRestCount = teamBetaRestPlayer;
		}
		myPV.RPC("UpdateAll",PhotonTargets.Others,teamAlphaRestPlayer,teamBetaRestPlayer);
		GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonView>().RPC("SyncValue",PhotonTargets.Others,
			TeamInfo.alphaRestCount,TeamInfo.alphaRestCount,TeamInfo.betaRestCount,TeamInfo.betaMemberCount);
	}

	[PunRPC]
	void UpdateAll(int alpha,int beta)
	{
		teamAlphaRestPlayer = alpha;
		teamBetaRestPlayer = beta;
	}

	public int ShowRestPlayer(bool isAlpha)
	{
		if(isAlpha)
		{
			return teamAlphaRestPlayer;
		}else{
			return teamBetaRestPlayer;
		}
	}
}
