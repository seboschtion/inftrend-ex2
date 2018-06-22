using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public Camera GameCamera;
	public GameObject GunRotatorX;
	public GameObject GunRotatorY;
	public ParticleSystem LaserChargeBeam1;
	public ParticleSystem LaserChargeBeam2;

	void Start () {
		StartLaser (LaserChargeBeam1);
		StartLaser (LaserChargeBeam2);
	}

	void StartLaser (ParticleSystem laser) {
		var main = laser.main;
		main.simulationSpeed = 10;
		laser.Stop ();
	}

	// Update is called once per frame
	void Update () {
		Ray ray = GameCamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			TargetGameObject (hit.transform.gameObject);
		} else {
			TargetDirection (ray.direction);
		}

		if (Input.GetButton ("Fire1")) {
			Fire ();
		}

	}

	void TargetDirection (Quaternion direction) {
		GunRotatorY.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -1 * direction.eulerAngles.y));
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

	void Fire () {
		if (!LaserChargeBeam1.isPlaying && !LaserChargeBeam2.isPlaying) {
			LaserChargeBeam1.Play ();
			LaserChargeBeam2.Play ();
		}

	}
}