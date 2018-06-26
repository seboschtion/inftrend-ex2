using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {

    private Player player;

    public Text goodbye;

	// Use this for initialization
	void Start () {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        goodbye.text = "You scored " + playerObj.name + " points\nSee you soon...";
	}

}
