using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITeamManager : MonoBehaviour {

	[Header("チームα")]
	[SerializeField]
	Image teamAlphaImage;
	[SerializeField]
	Text  teamAlphaText;

	[Header("チームβ")]
	[SerializeField]
	Image teamBetaImage;
	[SerializeField]
	Text teamBetaText;

	private PhotonView myPV;
	private TeamPlayManager teamPlayerManager;
	private int myTeamAlphaRestCount;
	private int myTeamBetaRestCount;

	// Use this for initialization
	void Start () {
		myPV = GetComponent<PhotonView>();
		if(myPV.isMine)
		{
			teamPlayerManager = GetComponent<TeamPlayManager>();
			UpdatePlayerRestCount();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(myPV.isMine)
		{
			if(myTeamAlphaRestCount != teamPlayerManager.ShowRestPlayer(true) || myTeamBetaRestCount != teamPlayerManager.ShowRestPlayer(false))
			{
				UpdatePlayerRestCount();
			}
		}
	}

	void UpdatePlayerRestCount()
	{
		myTeamAlphaRestCount = teamPlayerManager.ShowRestPlayer(true);
		myTeamBetaRestCount = teamPlayerManager.ShowRestPlayer(false);
		teamAlphaText.text = myTeamAlphaRestCount.ToString();
		teamBetaText.text = myTeamBetaRestCount.ToString();
		teamAlphaImage.fillAmount = teamPlayerManager.PercentageOfRestPlayer(true);
		teamBetaImage.fillAmount = teamPlayerManager.PercentageOfRestPlayer(false);
	}
}
