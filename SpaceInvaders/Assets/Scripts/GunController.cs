using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public Camera GameCamera;
	public GameObject GunRotatorX;
	public GameObject GunRotatorY;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 direction = GameCamera.transform.eulerAngles;
		TargetDirection (direction);
		// Ray ray = GameCamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		// RaycastHit hit;
		// if (Physics.Raycast (ray, out hit)) { } else {
		// 	print ("I'm looking at nothing!");
		// }

	}

	void TargetDirection (Vector3 direction) {
		GunRotatorY.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -1 * direction.y));
		GunRotatorX.transform.localRotation = Quaternion.Euler (new Vector3 (0, -90, -1 * direction.x));
	}
}