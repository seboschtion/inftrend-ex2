
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {

    private Player player;

    public Text goodbye;
    public Text score;

	// Use this for initialization
	void Start () {
        GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObj != null)
        {
          player = playerGameObj.GetComponent<Player>();
        }
        score.text = "You scored " + player.score + " points";
        goodbye.text = "See you soon, " + player.name + " ...";
        StartCoroutine("LastScene");
    }

    IEnumerator LastScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("LastScene");
    }
}
