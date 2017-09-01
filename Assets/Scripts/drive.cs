using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class drive : MonoBehaviour {

	float speed= 10.0f;
	float rotationSpeed = 100.0f;
	// Update is called once per frame
	void Update () {
		
		float translation = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
		float rotation = CrossPlatformInputManager.GetAxis ("Horizontal") * rotationSpeed;

		translation *= Time.deltaTime;
		rotation*= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate(0,rotation, 0);



	}
}
