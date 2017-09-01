using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {
	



	int currentRedScore = 0;

	int currentBlueScore = 0;

	public Text RedGoalText;

	public Text BlueGoalText;

	public Rigidbody Ball;
	public GameObject ballposition;

	public GameScoreManager gsm;

	void Start()
	{
		
	}

	void OnTriggerEnter(Collider col) 
	{
		if (col.gameObject.tag == "RedGoalTag")
		{
			Debug.Log("Redgoal");
			//Ball.position = ballposition.gameObject.transform.position;
			//currentRedScore = currentRedScore + 1;
			//RedGoalText.text = "" + currentRedScore;
			gsm.RpcupdateRedScore();
		


		}

		if (col.gameObject.tag == "BlueGoalTag")
		{

			Debug.Log("Bluegoal");

			//currentBlueScore = currentBlueScore + 1;
		//	BlueGoalText.text = "" + currentBlueScore;
		//	gsm.BlueGoalText.text = "" + currentBlueScore;
			gsm.RpcupdateBlueScore();


		}
		Ball.position = ballposition.gameObject.transform.position;
	}




}
