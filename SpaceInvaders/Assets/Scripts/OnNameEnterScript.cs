﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnNameEnterScript : MonoBehaviour
{
    public InputField nameInput;
    public Player player;
    public GameObject button;

    private void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(LoadMain);
    }

    void Update()
    {
        if (nameInput.isFocused && !string.IsNullOrEmpty(nameInput.text))
        {
            player.name = nameInput.text;
            button.SetActive(true);
            nameInput.placeholder.GetComponent<Text>().text = string.Empty;
        }
    }

    void LoadMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
