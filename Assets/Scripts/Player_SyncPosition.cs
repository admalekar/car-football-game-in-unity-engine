using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncPosition : NetworkBehaviour {

private Transform myTransform;

	[SerializeField]
	float lerpRate = 15;




	[SyncVar] 
	private Vector3 syncPos;

private Vector3 lastPos;
private float threshold = 0.5f;

void Start () {
    myTransform = GetComponent<Transform> ();
}


void FixedUpdate () {
    TransmitPosition ();
    LerpPosition ();
}

void LerpPosition () {
    if (!isLocalPlayer) {
        myTransform.position = Vector3.Lerp (myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }
}

[Command]
void Cmd_ProvidePositionToServer (Vector3 pos) {
    syncPos = pos;
}

[ClientCallback]
void TransmitPosition () {
    if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold) {
        Cmd_ProvidePositionToServer (myTransform.position);
        lastPos = myTransform.position;
    }
}
}