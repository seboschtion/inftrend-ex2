using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnButtonClickAction : MonoBehaviour {

	public GameObject startAgain;
	public GameObject quit;
	private Player player;

	private void Start()
	{
		GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
		if (playerGameObj != null)
		{
			player = playerGameObj.GetComponent<Player>();
		}
		player.score = 0;
		startAgain.GetComponent<Button>().onClick.AddListener(LoadMain);
		quit.GetComponent<Button>().onClick.AddListener(QuitGame);
	}

	void LoadMain()
	{
			SceneManager.LoadScene("MainScene");
	}

	void QuitGame()
	{
			Application.Quit();
	}
}
