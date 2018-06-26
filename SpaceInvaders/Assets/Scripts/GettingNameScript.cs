using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GettingNameScript : MonoBehaviour {

    public Player player;
    public Text welcome;

	// Use this for initialization
	void Start () {
	}

    void OnGUI()
    {
        player.name = GUI.TextField(new Rect(270, 160, 130, 20), player.name, 25);
        if (GUI.Button(new Rect(400, 160, 40, 20), "Save"))
        {
            SubmitName();
        }
    }

    private void SubmitName() {
        welcome.text = "Welcome " + player.name + "!";
        StartCoroutine(LoadMain());
    }


    IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("EndOfGame");
    }
}
