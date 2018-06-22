using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public Camera GameCamera;
	public GameObject GunRotatorX;
	public GameObject GunRotatorY;
	public GameObject TestTarget;

	// Update is called once per frame
	void Update () {
		Ray ray = GameCamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			TargetGameObject (hit.transform.gameObject);
		} else {
			TargetDirection (ray.direction);
		}

	}

	void TargetDirection (Quaternion direction) {
		GunRotatorY.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, direction.eulerAngles.y));
		GunRotatorX.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90, direction.eulerAngles.x));
	}

	void TargetDirection (Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation (direction);
		TargetDirection (rotation);
	}

	void TargetGameObject (GameObject target) {
		Vector3 direction = target.transform.position - GunRotatorX.transform.position;
		TargetDirection (direction);
	}
}