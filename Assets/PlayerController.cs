using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform playerCam;

	CharacterController cc;

	float playerSpeed = 10f;
	float playerBackwardSpeed = 5f;

	float camDistance = 15.0f;
	float camXSpeed = 20.0f;
	float camYSpeed = 200.0f;

	float yMinLimit = -20f;
	float yMaxLimit = 80f;

	float camDistanceMin = .5f;
	float camDistanceMax = 25f;

	float camX = 0.0f;
	float camY = 0.0f;

	void Start () 
	{
		cc = GetComponent<CharacterController> ();
		updateCam (); //initializes it
	}

	void Update () 
	{
		if (Input.GetMouseButton (0) && Input.GetMouseButton (1)) {
			
			Quaternion rotation = Quaternion.Euler (0, camX, 0);
			transform.rotation = rotation;

			Vector3 movementDir = transform.forward;
			bool canMove = true;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.S)) {
				movementDir = Quaternion.Euler (0, -90, 0) * movementDir;
			}else if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.S)) {
				movementDir = Quaternion.Euler (0, 90, 0) * movementDir;
			}else if (Input.GetKey (KeyCode.A)) {
				movementDir = Quaternion.Euler (0, -45, 0) * movementDir;
			} else if (Input.GetKey (KeyCode.D)) {
				movementDir = Quaternion.Euler (0, 45, 0) * movementDir;
			}else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
				movementDir = Quaternion.Euler (0, -90, 0) * movementDir;
			} else if (Input.GetKey (KeyCode.S)) {
				canMove = false;
			} 

			if(canMove)
				cc.SimpleMove (movementDir * playerSpeed);

			updateCam ();

		} else if (Input.GetMouseButton (0)) {
			
			if (playerCam.transform.parent != null) {
				Vector3 globalCamRot = transform.InverseTransformDirection (playerCam.transform.rotation.eulerAngles);
				playerCam.transform.parent = null;
				camX = globalCamRot.y;
			} 

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			Vector3 movementDir = transform.forward;

			if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.W)) {
				Vector3 rot = transform.rotation.eulerAngles;
				rot.y = Mathf.Lerp(rot.y, rot.y - 10f, Time.deltaTime * 20f);
				transform.rotation = Quaternion.Euler(rot);
				movementDir = Quaternion.Euler (0, 0, 0) * movementDir;
				cc.SimpleMove (movementDir * playerSpeed);
			}else if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.W)) {
				Vector3 rot = transform.rotation.eulerAngles;
				rot.y = Mathf.Lerp(rot.y, rot.y + 10f, Time.deltaTime * 20f);
				transform.rotation = Quaternion.Euler(rot);
				movementDir = Quaternion.Euler (0, 0, 0) * movementDir;
				cc.SimpleMove (movementDir * playerSpeed);
			}else if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.S)) {
				Vector3 rot = transform.rotation.eulerAngles;
				rot.y = Mathf.Lerp(rot.y, rot.y + 10f, Time.deltaTime * 20f);
				transform.rotation = Quaternion.Euler(rot);
				movementDir = Quaternion.Euler (0, 180, 0) * movementDir;
				cc.SimpleMove (movementDir * playerBackwardSpeed);
			}else if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.S)) {
				Vector3 rot = transform.rotation.eulerAngles;
				rot.y = Mathf.Lerp(rot.y, rot.y - 10f, Time.deltaTime * 20f);
				transform.rotation = Quaternion.Euler(rot);
				movementDir = Quaternion.Euler (0, 180, 0) * movementDir;
				cc.SimpleMove (movementDir * playerBackwardSpeed);
			}else if (Input.GetKey (KeyCode.A)) {
				Vector3 rot = transform.rotation.eulerAngles;
				rot.y = Mathf.Lerp(rot.y, rot.y - 10f, Time.deltaTime * 20f);
				transform.rotation = Quaternion.Euler(rot);
			} else if (Input.GetKey (KeyCode.D)) {
				Vector3 rot = transform.rotation.eulerAngles;
				rot.y = Mathf.Lerp(rot.y, rot.y + 10f, Time.deltaTime * 20f);
				transform.rotation = Quaternion.Euler(rot);
			} else if (Input.GetKey (KeyCode.W)) {
				cc.SimpleMove (movementDir * playerSpeed);
			} else if (Input.GetKey (KeyCode.S)) {
				movementDir = Quaternion.Euler (0, -180, 0) * movementDir;
				cc.SimpleMove (movementDir * playerBackwardSpeed);
			}

			if (playerCam.transform.parent == null) {
				updateCam ();
			}

		}else if (Input.GetMouseButton (1)) {
			
			if (playerCam.transform.parent != null) {
				Vector3 globalCamRot = transform.InverseTransformDirection (playerCam.transform.rotation.eulerAngles);
				playerCam.transform.parent = null;
				camX = globalCamRot.y;
			}

			//make player face same direction as cam
			Quaternion rotation = Quaternion.Euler (0, camX, 0);
			transform.rotation = rotation;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			Vector3 movementDir = transform.forward;

			if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.W)) {
				movementDir = Quaternion.Euler (0, -45, 0) * movementDir;
				cc.SimpleMove (movementDir * playerSpeed);
			}else if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.W)) {
				movementDir = Quaternion.Euler (0, 45, 0) * movementDir;
				cc.SimpleMove (movementDir * playerSpeed);
			}else if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.S)) {
				movementDir = Quaternion.Euler (0, 135, 0) * movementDir;
				cc.SimpleMove (movementDir * playerBackwardSpeed);
			}else if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.S)) {
				movementDir = Quaternion.Euler (0, -135, 0) * movementDir;
				cc.SimpleMove (movementDir * playerBackwardSpeed);
			}else if (Input.GetKey (KeyCode.A)) {
				movementDir = Quaternion.Euler (0, -90, 0) * movementDir;
				cc.SimpleMove (movementDir * playerSpeed);
			} else if (Input.GetKey (KeyCode.D)) {
				movementDir = Quaternion.Euler (0, 90, 0) * movementDir;
				cc.SimpleMove (movementDir * playerSpeed);
			} else if (Input.GetKey (KeyCode.W)) {
				cc.SimpleMove (movementDir * playerSpeed);
			} else if (Input.GetKey (KeyCode.S)) {
				movementDir = Quaternion.Euler (0, -180, 0) * movementDir;
				cc.SimpleMove (movementDir * playerBackwardSpeed);
			}

			if (playerCam.transform.parent == null) {
				updateCam ();
			}

		}else if (Input.GetKey (KeyCode.A)) {
			playerCam.transform.parent = this.transform;
			Vector3 rot = transform.rotation.eulerAngles;
			rot.y = Mathf.Lerp(rot.y, rot.y - 10f, Time.deltaTime * 20f);
			transform.rotation = Quaternion.Euler(rot);
		}else if (Input.GetKey (KeyCode.D)) {
			playerCam.transform.parent = this.transform;
			Vector3 rot = transform.rotation.eulerAngles;
			rot.y = Mathf.Lerp(rot.y, rot.y + 10f, Time.deltaTime * 20f);
			transform.rotation = Quaternion.Euler(rot);
		}else if (Input.GetKey (KeyCode.S)) {
			playerCam.transform.parent = this.transform;
			Vector3 movementDir = transform.forward;
			movementDir = Quaternion.Euler (0, 180, 0) * movementDir;
			cc.SimpleMove (movementDir * playerBackwardSpeed);
		} else if (Input.GetKey (KeyCode.W)) {
			playerCam.transform.parent = this.transform;
			cc.SimpleMove (transform.forward * playerSpeed);
		} else {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			cc.SimpleMove (Vector3.zero);
		}
	}

	void updateCam(){
		camX += Input.GetAxis ("Mouse X") * camXSpeed * camDistance * 0.02f;
		camY -= Input.GetAxis ("Mouse Y") * camYSpeed * 0.02f;

		camY = ClampAngle (camY, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler (camY, camX, 0);

		camDistance = Mathf.Clamp (camDistance - Input.GetAxis ("Mouse ScrollWheel") * 5, camDistanceMin, camDistanceMax);

		RaycastHit hit;
		if (Physics.Linecast (transform.position, transform.position, out hit)) {
			camDistance -= hit.distance;
		}
		Vector3 negDistance = new Vector3 (0.0f, 0.0f, -camDistance);
		Vector3 position = rotation * negDistance + transform.position;

		playerCam.rotation = rotation;
		playerCam.position = position;
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