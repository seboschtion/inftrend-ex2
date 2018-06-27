using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player player;

    public int score = 0;

    void Awake()
    {
        if (player != null)
        {
            GameObject.Destroy(player);
        }
        else
        {
            player = this;
            player.name = "";
        }

        DontDestroyOnLoad(this);
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
    }
}
