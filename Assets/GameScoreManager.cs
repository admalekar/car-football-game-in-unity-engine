using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameScoreManager : NetworkBehaviour {
	
	public Text RedGoalText;
	public Text BlueGoalText;
	public Text RedWinsText;
	public Text BlueWinsText;

	int currentRedScore = 0;

	int currentBlueScore = 0;
	// Use this for initialization
	void Start () {
	
	}

	[ClientRpc]
	public void RpcupdateRedScore()
	{
		currentRedScore = currentRedScore + 1;
		RedGoalText.text = "" + currentRedScore;
	}

	[ClientRpc]
	public void RpcupdateBlueScore()
	{
		currentBlueScore = currentBlueScore + 1;
		BlueGoalText.text = "" + currentBlueScore;
	}


	void Update () {
		if (RedGoalText.text == "2") 
		{
			RedWinsText.gameObject.SetActive (true);

		}
		if (BlueGoalText.text == "2") 
		{
			
			BlueWinsText.gameObject.SetActive (true);

		}

	}
}
