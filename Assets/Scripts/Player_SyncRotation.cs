using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncRotation : NetworkBehaviour {

private Transform myTransform;

// Object rotation vars
[SyncVar] private Quaternion syncObjectRotation;
public float rotationLerpRate = 15;

private Quaternion lastRot;
private float threshold = 5f;


private void Start() {
    myTransform = GetComponent<Transform> ();
}

private void FixedUpdate () {
    LerpRotation ();
    TransmitRotation (); 
}

private void LerpRotation() {

    if (!isLocalPlayer) {
        myTransform.rotation = Quaternion.Lerp (myTransform.rotation, syncObjectRotation, Time.deltaTime * rotationLerpRate);
    }

}

[Command]
private void Cmd_ProvideRotationToServer (Quaternion objectRotation) {
    syncObjectRotation = objectRotation;
}

[ClientCallback]
private void TransmitRotation() { // Send rotation to server
    if (isLocalPlayer && Quaternion.Angle(myTransform.rotation, lastRot) > threshold) {
        Cmd_ProvideRotationToServer (myTransform.rotation);
        lastRot = myTransform.rotation;
    }
}

}