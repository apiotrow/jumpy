using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public Transform target;
	float distance = 15.0f;
	float xSpeed = 20.0f;
	float ySpeed = 160.0f;

	float yMinLimit = -20f;
	float yMaxLimit = 80f;

	float distanceMin = .5f;
	float distanceMax = 25f;

	float x = 0.0f;
	float y = 0.0f;

	void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		y = ClampAngle (y, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler (y, x, 0);

		distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

		RaycastHit hit;
		if (Physics.Linecast (target.position, transform.position, out hit)) {
			distance -= hit.distance;
		}
		Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;
	}

	void LateUpdate () 
	{
		if (Input.GetMouseButton (0)) {
			x += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle (y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler (y, x, 0);

			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) {
				distance -= hit.distance;
			}
			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}