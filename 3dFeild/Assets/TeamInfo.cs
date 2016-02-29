using UnityEngine;
using System.Collections;

public class TeamInfo : MonoBehaviour {
	public static int alphaRestCount;
	public static int betaRestCount;
	public static int alphaMemberCount;
	public static int betaMemberCount;
	public static int MemberCount;


	[PunRPC]
	void SyncValue(int alphaRestCnt,int alphaMemberCnt,int betaRestCnt,int betaMemberCnt)
	{
		alphaRestCount = alphaRestCnt;
		alphaMemberCount = alphaRestCnt;
		betaRestCount = betaRestCnt;
		betaMemberCount = betaMemberCnt;
	}
}
