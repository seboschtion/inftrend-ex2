using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInside : MonoBehaviour {

	public Camera PlayerCamera;
	public Camera WindowCamera;

	private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = WindowCamera.transform.position;
	}

	// Update is called once per frame
	void Update () {
		Ray ray = PlayerCamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (Physics.Raycast (ray, out hit)) {
				Vector3 delta = hit.point - transform.position;
				WindowCamera.transform.position = originalPosition + delta;
				WindowCamera.transform.rotation = PlayerCamera.transform.rotation;
			} else {
				print ("I'm looking at nothing!");
			}
		}
	}
}