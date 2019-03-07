using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    // Use this for initialization
    public Text field;
    public Button button;
	void Start () {
        //Set the text according to who won the match
        if (PlayerPrefs.GetString("Loser") == "Player 1") field.text = "Player 2 Wins!";
        else field.text = "Player 1 Wins!";
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
