using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {

    private GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
    private Player player;

    public Text goodbye;

	// Use this for initialization
	void Start () {
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        goodbye.text = "You scored " + player.score + " points\nSee you soon :-)";
	}

}
