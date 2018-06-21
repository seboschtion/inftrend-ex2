using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	private float speed = 5.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		var y = Input.GetAxis ("Vertical") * Time.deltaTime * speed;

		transform.Translate (x, 0, 0);
		transform.Translate (0, y, 0);
		
		if (Input.GetKey(KeyCode.E))
      		transform.Rotate(Vector3.up * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.Q))
      		transform.Rotate(Vector3.down * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.R))
      		transform.Rotate(Vector3.left * speed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.F))
      		transform.Rotate(Vector3.right * speed * Time.deltaTime);
	}
}