﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    // Use this for initialization
    public Text field;
    public Button button;
	void Start () {
        //Set the text and images according to who won the match
        if (PlayerPrefs.GetString("Loser") == "Player 1")
        {
            field.text = "Player Two Wins!";
            GameObject.Find("P2 Sad").SetActive(false);
            GameObject.Find("P1 Happy").SetActive(false);
        } 
        else 
        {
            field.text = "Player One Wins!";
            GameObject.Find("P1 Sad").SetActive(false);
            GameObject.Find("P2 Happy").SetActive(false);
        }
        button.onClick.AddListener(LoadLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //Go back to the game
    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
