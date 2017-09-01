using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour 
{
	static public Transform target;
	public float distance = 4.0f;
	public float height = 1.0f;
	public float smoothLag = 0.2f;
	public float maxSpeed = 10.0f;
	public float snapLag = 0.3f;
	public float clampHeadPositionScreenSpace = 0.75f;
	LayerMask lineOfSightMask = 0;
	Vector3 headOffset = Vector3.zero;
	Vector3 centerOffset = Vector3.zero;

	bool isSnapping = false;
	Vector3 velocity = Vector3.zero;
	float targetHeight = 100000.0f;

	void Apply (Transform dummyTarget, Vector3 dummyCenter)
	{	
		Vector3 targetCenter = target.position + centerOffset;
		Vector3 targetHead = target.position + headOffset;

		targetHeight = targetCenter.y + height;

		if (Input.GetButton("Fire2") && !isSnapping)
		{
			velocity = Vector3.zero;
			isSnapping = true;
		}

		if (isSnapping)
		{
			ApplySnapping (targetCenter);
		}
		else
		{
			ApplyPositionDamping (new Vector3(targetCenter.x, targetHeight, targetCenter.z));
		}
		
		SetUpRotation(targetCenter, targetHead);
	}

	void LateUpdate ()
	{
		if (target)
			Apply (null, Vector3.zero);	
	}

	void ApplySnapping (Vector3 targetCenter)
	{
		Vector3 position = transform.position;
		Vector3 offset = position - targetCenter;
		offset.y = 0;
		float currentDistance = offset.magnitude;

		float targetAngle = target.eulerAngles.y;
		float currentAngle = transform.eulerAngles.y;

		currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref velocity.x, snapLag);
		currentDistance = Mathf.SmoothDamp(currentDistance, distance, ref velocity.z, snapLag);

		Vector3 newPosition = targetCenter;
		newPosition += Quaternion.Euler(0, currentAngle, 0) * Vector3.back * currentDistance;

		newPosition.y = Mathf.SmoothDamp (position.y, targetCenter.y + height, ref velocity.y, smoothLag, maxSpeed);

		newPosition = AdjustLineOfSight(newPosition, targetCenter);
		
		transform.position = newPosition;
		
		if (AngleDistance (currentAngle, targetAngle) < 3.0)
		{
			isSnapping = false;
			velocity = Vector3.zero;
		}
	}

	Vector3 AdjustLineOfSight (Vector3 newPosition, Vector3 target)
	{
		RaycastHit hit;
		if (Physics.Linecast (target, newPosition, out hit, lineOfSightMask.value))
		{
			velocity = Vector3.zero;
			return hit.point;
		}
		return newPosition;
	}

	void ApplyPositionDamping (Vector3 targetCenter)
	{
		Vector3 position = transform.position;
		Vector3 offset = position - targetCenter;
		offset.y = 0;
		Vector3 newTargetPos = offset.normalized * distance + targetCenter;
		
		Vector3 newPosition;
		newPosition.x = Mathf.SmoothDamp (position.x, newTargetPos.x, ref velocity.x, smoothLag, maxSpeed);
		newPosition.z = Mathf.SmoothDamp (position.z, newTargetPos.z, ref velocity.z, smoothLag, maxSpeed);
		newPosition.y = Mathf.SmoothDamp (position.y, targetCenter.y, ref velocity.y, smoothLag, maxSpeed);
		
		newPosition = AdjustLineOfSight(newPosition, targetCenter);
		
		transform.position = newPosition;
	}

	void SetUpRotation (Vector3 centerPos, Vector3 headPos)
	{
		Vector3 cameraPos = transform.position;
		Vector3 offsetToCenter = centerPos - cameraPos;
		
		// Generate base rotation only around y-axis
		Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, 0.0f, offsetToCenter.z));

		Vector3 relativeOffset = Vector3.forward * distance + Vector3.down * height;
		transform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);

		// Calculate the projected center position and top position in world space
		Ray centerRay = this.GetComponent<Camera>().ViewportPointToRay(new Vector3(.5f, 0.5f, 1f));
		Ray topRay = this.GetComponent<Camera>().ViewportPointToRay(new Vector3(.5f, clampHeadPositionScreenSpace, 1.0f));

		Vector3 centerRayPos = centerRay.GetPoint(distance);
		Vector3 topRayPos = topRay.GetPoint(distance);
		
		float centerToTopAngle = Vector3.Angle(centerRay.direction, topRay.direction);
		
		float heightToAngle = centerToTopAngle / (centerRayPos.y - topRayPos.y);

		float extraLookAngle = heightToAngle * (centerRayPos.y - centerPos.y);
		if (extraLookAngle < centerToTopAngle)
		{
			extraLookAngle = 0;
		}
		else
		{
			extraLookAngle = extraLookAngle - centerToTopAngle;
			transform.rotation *= Quaternion.Euler(-extraLookAngle, 0, 0);
		}
	}

	float AngleDistance (float a, float b)
	{
		a = Mathf.Repeat(a, 360);
		b = Mathf.Repeat(b, 360);
		
		return Mathf.Abs(b - a);
	}

	Vector3 GetCenterOffset ()
	{
		return centerOffset;
	}

	void SetTarget (Transform t)
	{
		target = t;
	}
}
