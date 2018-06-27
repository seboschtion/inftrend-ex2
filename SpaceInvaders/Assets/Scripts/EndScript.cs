using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public Text goodbye;
    public Text score;

    private Player player;

    void Start()
    {
        GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        score.text = string.Format("You scored {0} point{1}!", player.score, player.score > 1 ? "s" : "");
        goodbye.text = string.Format("See you soon, {0}...", player.name);;
        StartCoroutine(LastScene());
    }

    IEnumerator LastScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("LastScene");
    }
}