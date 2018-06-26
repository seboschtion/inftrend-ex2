using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GettingNameScript : MonoBehaviour {

    public Player player;
    public Text welcome;
    public InputField inputName;

	// Use this for initialization
	void Start () {
        inputName.ActivateInputField();
        inputName.onEndEdit.AddListener(delegate { SubmitName(inputName.text);});
	}

    private void SubmitName(string playerName) {
        player.name = playerName;
        welcome.text = "Welcome " + playerName + "!";
        gameObject.GetComponent<Animator>().Play("StartAnimation");
        StartMainScene();
    }

    public void StartMainScene() {
        StartCoroutine(WaitForFiveSeconds());
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator WaitForFiveSeconds()
    {
        yield return new WaitForSeconds(5);
    }
}
