using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

public class setupLoaclPlayer : NetworkBehaviour {



	[SyncVar]
	public string pname= "player";






	[SyncVar]
	public Color playerColor = Color.white;

	Transform trans; // Used to cache the transform
	Rigidbody body; // Used to cache the RigidBody
	//float hInput;
	//float vInput;
	//float rotationSpeed = 90f;
	//float moveSpeed = 15f;

	/*

	void Awake()
	{
		trans = transform;
		body = GetComponent<Rigidbody>();
	}
	*/
	Camera playerCam;
	void Awake()
	{
		playerCam = GetComponentInChildren<Camera>();
		playerCam.gameObject.SetActive(false);
	}
	/*
	void OnGUI()
	{
		if (isLocalPlayer) {
		
			score = GUI.TextField (new Rect (25, Screen.height - 40, 100, 30), pname);

		}
	}

	[Command]
	public void CmdChangeName(string newName)
	{
		pname = newName;

	}
*/


	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<CarUserControl> ().enabled = true;
			playerCam.gameObject.SetActive(true);
			//}

		}

		Renderer[] rends = GetComponentsInChildren<Renderer> ();
		//foreach (Renderer r in rends) {
		rends[0].materials[2].color = playerColor;
		rends[1].material.color = playerColor;
	}

	void Update()
	{
		
		this.GetComponentInChildren<TextMesh> ().text = pname;


		/*
		if (isLocalPlayer) {// Don't check input if this isn't
			// running on the local player object
			hInput = Input.GetAxis ("Horizontal");
			vInput = Input.GetAxis ("Vertical");
		}

*/
	}
	// Update is called once per frame


}

