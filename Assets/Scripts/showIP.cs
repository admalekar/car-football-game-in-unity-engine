using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class showIP : NetworkBehaviour {

	// Use this for initialization
	public Text ip;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ip.GetComponent<Text> ().text = Network.player.ipAddress;
	}
}
