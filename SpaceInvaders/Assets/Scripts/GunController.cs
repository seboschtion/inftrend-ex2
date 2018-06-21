using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public Camera GameCamera;
	public GameObject GunRotator;
	public GameObject HitBox;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Ray ray = GameCamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
			print ("I'm looking at " + hit.transform.name);
		else
			print ("I'm looking at nothing!");
	}
}